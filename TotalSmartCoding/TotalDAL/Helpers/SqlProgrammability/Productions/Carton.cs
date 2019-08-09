using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Productions
{
    public class Carton
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public Carton(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.CartonSaveRelative();
            this.CartonPostSaveValidate();

            this.CartonEditable();

            this.GetCartons();
            this.CartonUpdateEntryStatus();

            this.SearchCartons();
        }

        private void CartonSaveRelative()
        {
            //BE CAREFULL WHEN SAVE: NEED TO SET @PackIDs (FOR BOTH WHEN SAVE - Update AND DELETE - Undo
            string queryString = " @EntityID int, @SaveRelativeOption int, @PackIDs varchar(3999), @DeletePack bit, @Remarks nvarchar(800) " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "   IF (@PackIDs <> '') " + "\r\n";
            queryString = queryString + "       BEGIN " + "\r\n";

            queryString = queryString + "           IF (@SaveRelativeOption = 1) " + "\r\n";

            queryString = queryString + "               UPDATE      Packs" + "\r\n";
            queryString = queryString + "               SET         CartonID = @EntityID, EntryStatusID = " + (int)GlobalVariables.BarcodeStatus.Wrapped + "\r\n"; //WHERE: NOT BELONG TO ANY CARTON, AND NUMBER OF PACK EFFECTED: IS THE SAME PackID PASS BY VARIBLE: PackIDs
            queryString = queryString + "               WHERE       CartonID IS NULL AND EntryStatusID = " + (int)GlobalVariables.BarcodeStatus.Readytoset + " AND PackID IN (SELECT Id FROM dbo.SplitToIntList (@PackIDs)) " + "\r\n";

            queryString = queryString + "           ELSE " + "\r\n"; //(@SaveRelativeOption = -1) 

            queryString = queryString + "               BEGIN " + "\r\n";

            queryString = queryString + "                   INSERT INTO     DeletedCartons (CartonID, EntryDate, FillingLineID, BatchID, LocationID, CommodityID, PalletID, Code, Quantity, LineVolume, PackCounts, EntryStatusID, DeletedDate, Remarks) " + "\r\n";
            queryString = queryString + "                   SELECT          CartonID, EntryDate, FillingLineID, BatchID, LocationID, CommodityID, 0 AS PalletID, Code, Quantity, LineVolume, PackCounts, EntryStatusID, GETDATE() AS DeletedDate, @Remarks AS Remarks FROM Cartons WHERE CartonID = @EntityID " + "\r\n";

            queryString = queryString + "                   IF (@DeletePack = 1) " + "\r\n";
            queryString = queryString + "                       BEGIN " + "\r\n";
            queryString = queryString + "                           INSERT INTO DeletedPacks (PackID, EntryDate, FillingLineID, BatchID, LocationID, QueueID, CommodityID, RelatedPackID, CartonID, Code, LineVolume, EntryStatusID, DeletedDate, Remarks) " + "\r\n";
            queryString = queryString + "                           SELECT      PackID, EntryDate, FillingLineID, BatchID, LocationID, QueueID, CommodityID, 0 AS RelatedPackID, 0 AS CartonID, Code, LineVolume, EntryStatusID, GETDATE() AS DeletedDate, @Remarks AS Remarks FROM Packs WHERE CartonID = @EntityID " + "\r\n";

            queryString = queryString + "                           DELETE      " + "\r\n";
            queryString = queryString + "                           FROM        Packs " + "\r\n";
            queryString = queryString + "                           WHERE       CartonID = @EntityID AND EntryStatusID = " + (int)GlobalVariables.BarcodeStatus.Wrapped + "\r\n";
            queryString = queryString + "                       END " + "\r\n";
            queryString = queryString + "                   ELSE " + "\r\n";
            queryString = queryString + "                       UPDATE      Packs" + "\r\n";
            queryString = queryString + "                       SET         CartonID = NULL, EntryStatusID = " + (int)GlobalVariables.BarcodeStatus.Readytoset + "\r\n"; //WHERE: NOT BELONG TO ANY CARTON, AND NUMBER OF PACK EFFECTED: IS THE SAME PackID PASS BY VARIBLE: PackIDs
            queryString = queryString + "                       WHERE       CartonID = @EntityID AND EntryStatusID = " + (int)GlobalVariables.BarcodeStatus.Wrapped + "\r\n";
            
            queryString = queryString + "               END " + "\r\n";


            queryString = queryString + "           IF @@ROWCOUNT <> (SELECT PackCounts FROM Cartons WHERE CartonID = @EntityID)  OR  @@ROWCOUNT <> ((SELECT (LEN(@PackIDs) - LEN(REPLACE(@PackIDs, ',', '')))) + 1) " + "\r\n"; //CHECK BOTH CONDITION FOR SURE. BUT: WE CAN OMIT THE SECOND CONDITION
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   DECLARE     @msg NVARCHAR(300) = N'System Error: Some pack does not exist!' + cast(@@ROWCOUNT as nvarchar) ; " + "\r\n";
            queryString = queryString + "                   THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "               END " + "\r\n";
            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("CartonSaveRelative", queryString);
        }

        private void CartonPostSaveValidate()
        {
            //string[] queryArray = new string[1];

            //string queryString = "              DECLARE @Code varchar(50) " + "\r\n";
            //queryString = queryString + "       SELECT TOP 1 @Code = Code FROM Cartons WHERE CartonID = @EntityID " + "\r\n";

            //queryArray[0] = " SELECT TOP 1 @FoundEntity = N'Trùng carton: ' + @Code + ', Ngày: ' + CONVERT(varchar(215), EntryDate, 113) FROM Cartons WHERE CartonID <> @EntityID AND Code = @Code ";

            //this.totalSmartCodingEntities.CreateProcedureToCheckExisting("CartonPostSaveValidate", queryArray, queryString);


            string[] queryArray = new string[0];

            string queryString = "  INSERT INTO UniqueCartons (Code, EntryDate) SELECT Code, EntryDate FROM Cartons WHERE CartonID = @EntityID " + "\r\n";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("CartonPostSaveValidate", queryArray, queryString);
        }

        private void CartonEditable()
        {
            string[] queryArray = new string[1];
            
            queryArray[0] = " SELECT TOP 1 @FoundEntity = CartonID FROM Cartons WHERE CartonID = @EntityID AND NOT PalletID IS NULL";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("CartonEditable", queryArray);
        }





        private void GetCartons()
        {
            string sqlSelect = "       SELECT * FROM Cartons WHERE FillingLineID = @FillingLineID AND EntryStatusID IN (SELECT Id FROM dbo.SplitToIntList (@EntryStatusIDs)) " + "\r\n";

            string queryString = " @FillingLineID int, @EntryStatusIDs varchar(3999), @PalletID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "   IF ((@FillingLineID IS NULL OR @FillingLineID = 0) AND @EntryStatusIDs IS NULL AND @PalletID IS NOT NULL) " + "\r\n";
            queryString = queryString + "       BEGIN " + "\r\n";
            queryString = queryString + "           SELECT * FROM Cartons WHERE PalletID = @PalletID " + "\r\n";
            queryString = queryString + "       END " + "\r\n";
            queryString = queryString + "   ELSE " + "\r\n";
            queryString = queryString + "       BEGIN " + "\r\n";
            queryString = queryString + "           IF (@PalletID IS NULL) " + "\r\n";
            queryString = queryString + "               " + sqlSelect + "\r\n";
            queryString = queryString + "           ELSE " + "\r\n";
            queryString = queryString + "               " + sqlSelect + " AND PalletID = @PalletID " + "\r\n";
            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetCartons", queryString);
        }

        private void CartonUpdateEntryStatus()
        {
            //BE CAREFULL WHEN SAVE: NEED TO SET @CartonIDs (FOR BOTH WHEN SAVE - Update AND DELETE - Undo
            string queryString = " @CartonIDs varchar(3999), @EntryStatusID int " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       UPDATE      Cartons" + "\r\n";
            queryString = queryString + "       SET         EntryStatusID = @EntryStatusID " + "\r\n";
            queryString = queryString + "       WHERE       CartonID IN (SELECT Id FROM dbo.SplitToIntList (@CartonIDs)) " + "\r\n";

            queryString = queryString + "       IF @@ROWCOUNT <> ((SELECT (LEN(@CartonIDs) - LEN(REPLACE(@CartonIDs, ',', '')))) + 1) " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";
            queryString = queryString + "               DECLARE     @msg NVARCHAR(300) = N'System Error: Some carton does not exist!' ; " + "\r\n";
            queryString = queryString + "               THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "           END " + "\r\n";


            this.totalSmartCodingEntities.CreateStoredProcedure("CartonUpdateEntryStatus", queryString);
        }



        private void SearchCartons()
        {
            string queryString;

            queryString = " @Barcode varchar(50) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";
            queryString = queryString + "       SELECT TOP (200) * FROM Cartons WHERE Code LIKE '%' + @Barcode+ '%' ORDER BY EntryDate DESC " + "\r\n";
            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("SearchCartons", queryString);
        }

    }
}
