using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Productions
{
    public class Pallet
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public Pallet(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.PalletSaveRelative();
            this.PalletPostSaveValidate();

            this.PalletEditable();
            this.PalletLocked();

            this.PalletToggleLocked();

            this.GetPallets();
            this.GetPalletChanged();

            this.PalletUpdateEntryStatus();

            this.SearchPallets();
        }

        private void PalletSaveRelative()
        {
            //BE CAREFULL WHEN SAVE: NEED TO SET @CartonIDs (FOR BOTH WHEN SAVE - Update AND DELETE - Undo
            string queryString = " @EntityID int, @SaveRelativeOption int, @CartonIDs varchar(3999), @Remarks nvarchar(800) " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "   IF (@CartonIDs <> '') " + "\r\n";
            queryString = queryString + "       BEGIN " + "\r\n";

            queryString = queryString + "           IF (@SaveRelativeOption = 1) " + "\r\n";

            queryString = queryString + "               UPDATE      Cartons" + "\r\n";
            queryString = queryString + "               SET         PalletID = @EntityID, EntryStatusID = " + (int)GlobalVariables.BarcodeStatus.Wrapped + "\r\n"; //WHERE: NOT BELONG TO ANY CARTON, AND NUMBER OF PACK EFFECTED: IS THE SAME CartonID PASS BY VARIBLE: CartonIDs
            queryString = queryString + "               WHERE       PalletID IS NULL AND EntryStatusID = " + (int)GlobalVariables.BarcodeStatus.Readytoset + " AND CartonID IN (SELECT Id FROM dbo.SplitToIntList (@CartonIDs)) " + "\r\n";

            queryString = queryString + "           ELSE " + "\r\n"; //(@SaveRelativeOption = -1) 

            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   INSERT INTO DeletedPallets (PalletID, EntryDate, MinPackDate, MaxPackDate, FillingLineID, BatchID, LocationID, CommodityID, Code, PackCounts, CartonCounts, Quantity, QuantityPickup, LineVolume, LineVolumePickup, EntryStatusID, Locked, DeletedDate, Remarks) " + "\r\n";
            queryString = queryString + "                   SELECT      PalletID, EntryDate, MinPackDate, MaxPackDate, FillingLineID, BatchID, LocationID, CommodityID, Code, PackCounts, CartonCounts, Quantity, QuantityPickup, LineVolume, LineVolumePickup, EntryStatusID, Locked, GETDATE() AS DeletedDate, @Remarks AS Remarks FROM Pallets WHERE PalletID = @EntityID " + "\r\n";

            queryString = queryString + "                   UPDATE      Cartons" + "\r\n";
            queryString = queryString + "                   SET         PalletID = NULL, EntryStatusID = " + (int)GlobalVariables.BarcodeStatus.Readytoset + "\r\n"; //WHERE: NOT BELONG TO ANY CARTON, AND NUMBER OF PACK EFFECTED: IS THE SAME CartonID PASS BY VARIBLE: CartonIDs
            queryString = queryString + "                   WHERE       PalletID = @EntityID AND EntryStatusID = " + (int)GlobalVariables.BarcodeStatus.Wrapped + " AND CartonID IN (SELECT Id FROM dbo.SplitToIntList (@CartonIDs)) " + "\r\n";
            queryString = queryString + "               END " + "\r\n";

            queryString = queryString + "           IF @@ROWCOUNT <> (SELECT CartonCounts FROM Pallets WHERE PalletID = @EntityID)  OR  @@ROWCOUNT <> ((SELECT (LEN(@CartonIDs) - LEN(REPLACE(@CartonIDs, ',', '')))) + 1) " + "\r\n"; //CHECK BOTH CONDITION FOR SURE. BUT: WE CAN OMIT THE SECOND CONDITION 
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   DECLARE     @msg NVARCHAR(300) = N'System Error: Some carton does not exist!' ; " + "\r\n";
            queryString = queryString + "                   THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "               END " + "\r\n";
            queryString = queryString + "           ELSE " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   IF (@SaveRelativeOption = 1) " + "\r\n";
            queryString = queryString + "                       BEGIN " + "\r\n";
            queryString = queryString + "                           UPDATE      Pallets     SET     MinPackDate = (SELECT MIN(Packs.EntryDate) FROM Packs INNER JOIN Cartons ON Packs.CartonID = Cartons.CartonID WHERE Cartons.PalletID = @EntityID) WHERE PalletID = @EntityID " + "\r\n";
            queryString = queryString + "                           UPDATE      Pallets     SET     MaxPackDate = (SELECT MAX(Packs.EntryDate) FROM Packs INNER JOIN Cartons ON Packs.CartonID = Cartons.CartonID WHERE Cartons.PalletID = @EntityID) WHERE PalletID = @EntityID " + "\r\n";
            queryString = queryString + "                       END " + "\r\n";
            queryString = queryString + "                   ELSE " + "\r\n"; //(@SaveRelativeOption = -1) 
            queryString = queryString + "                       BEGIN " + "\r\n";
            queryString = queryString + "                           UPDATE      Pallets     SET     MinPackDate = EntryDate, MaxPackDate = EntryDate WHERE PalletID = @EntityID " + "\r\n";
            queryString = queryString + "                       END " + "\r\n";
            queryString = queryString + "               END " + "\r\n";

            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("PalletSaveRelative", queryString);
        }

        private void PalletPostSaveValidate()
        {
            //string[] queryArray = new string[1];

            //string queryString = "              DECLARE @Code varchar(50) " + "\r\n";
            //queryString = queryString + "       SELECT TOP 1 @Code = Code FROM Pallets WHERE PalletID = @EntityID " + "\r\n";

            //queryArray[0] = " SELECT TOP 1 @FoundEntity = N'Trùng pallet: ' + @Code + ', Ngày: ' + CONVERT(varchar(215), EntryDate, 113) FROM Pallets WHERE PalletID <> @EntityID AND Code = @Code ";

            //this.totalSmartCodingEntities.CreateProcedureToCheckExisting("PalletPostSaveValidate", queryArray, queryString);

            string[] queryArray = new string[0];

            string queryString = "  INSERT INTO UniquePallets (Code, EntryDate) SELECT Code, EntryDate FROM Pallets WHERE PalletID = @EntityID " + "\r\n";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("PalletPostSaveValidate", queryArray, queryString);
        }

        private void PalletEditable()
        {
            string[] queryArray = new string[0];

            //queryArray[0] = " SELECT TOP 1 @FoundEntity = PalletID FROM Pallets WHERE PalletID = @EntityID AND NOT  some ID: pickup already IS NULL"; SHOULD CHECK AGAIN

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("PalletEditable", queryArray);
        }

        private void PalletLocked()
        {
            string[] queryArray = new string[1];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = PalletID FROM Pallets WHERE PalletID = @EntityID AND Locked = 1";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("PalletLocked", queryArray);
        }


        private void PalletToggleLocked()
        {
            string queryString = " @EntityID int, @Locked bit " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       UPDATE      Pallets  SET Locked = @Locked WHERE PalletID = @EntityID AND Locked = ~@Locked " + "\r\n";

            queryString = queryString + "       IF @@ROWCOUNT <> 1 " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";
            queryString = queryString + "               DECLARE     @msg NVARCHAR(300) = N'Không thể khóa hay mở khóa pallet này' ; " + "\r\n";
            queryString = queryString + "               THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "           END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("PalletToggleLocked", queryString);
        }


        private void GetPalletChanged()
        {
            string queryString = " @FillingLineID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       SELECT PalletChanged FROM FillingLines WHERE FillingLineID = @FillingLineID " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetPalletChanged", queryString);
        }

        private void GetPallets()
        {
            string queryString = " @FillingLineID int, @BatchID int, @EntryStatusIDs varchar(3999) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       UPDATE FillingLines SET PalletChanged = 0 WHERE FillingLineID = @FillingLineID " + "\r\n";
            queryString = queryString + "       SELECT * FROM Pallets WHERE FillingLineID = @FillingLineID AND (BatchID = @BatchID) AND EntryStatusID IN (SELECT Id FROM dbo.SplitToIntList (@EntryStatusIDs))  " + "\r\n"; //AT NUTIFOOD: REPLACE (QuantityPickup = 0 OR BatchID = @BatchID) BY (BatchID = @BatchID)

            this.totalSmartCodingEntities.CreateStoredProcedure("GetPallets", queryString);
        }

        private void PalletUpdateEntryStatus()
        {
            //BE CAREFULL WHEN SAVE: NEED TO SET @PalletIDs (FOR BOTH WHEN SAVE - Update AND DELETE - Undo
            string queryString = " @PalletIDs varchar(3999), @EntryStatusID int " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       UPDATE      Pallets" + "\r\n";
            queryString = queryString + "       SET         EntryStatusID = @EntryStatusID " + "\r\n";
            queryString = queryString + "       WHERE       PalletID IN (SELECT Id FROM dbo.SplitToIntList (@PalletIDs)) " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("PalletUpdateEntryStatus", queryString);
        }



        private void SearchPallets()
        {
            string queryString;

            queryString = " @Barcode varchar(50) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";
            queryString = queryString + "       SELECT TOP (200) * FROM Pallets WHERE Code LIKE '%' + @Barcode+ '%' ORDER BY EntryDate DESC " + "\r\n";
            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("SearchPallets", queryString);
        }

    }
}
