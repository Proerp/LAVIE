using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Productions
{
    public class Pack
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public Pack(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.PackSaveRelative();
            this.PackPostSaveValidate();

            this.PackEditable();


            this.GetPacks();

            this.PackUpdateQueueID();
            this.PackUpdateEntryStatus();

            this.SearchPacks();
            this.GetRelatedPackID();
        }

        private void PackSaveRelative()
        {
            string queryString = " @EntityID int, @SaveRelativeOption int, @Remarks nvarchar(800) " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       BEGIN " + "\r\n";

            queryString = queryString + "           IF (@SaveRelativeOption = -1) ";
            queryString = queryString + "               BEGIN ";
            queryString = queryString + "                   INSERT INTO     DeletedPacks (PackID, EntryDate, FillingLineID, BatchID, LocationID, QueueID, CommodityID, RelatedPackID, CartonID, Code, LineVolume, EntryStatusID, DeletedDate, Remarks) " + "\r\n";
            queryString = queryString + "                   SELECT          PackID, EntryDate, FillingLineID, BatchID, LocationID, QueueID, CommodityID, 0 AS RelatedPackID, 0 AS CartonID, Code, LineVolume, EntryStatusID, GETDATE() AS DeletedDate, @Remarks AS Remarks FROM Packs WHERE PackID = @EntityID " + "\r\n";

            queryString = queryString + "                   IF @@ROWCOUNT <> 1 " + "\r\n";
            queryString = queryString + "                       BEGIN " + "\r\n";
            queryString = queryString + "                           DECLARE     @msg NVARCHAR(300) = N'Lỗi hủy pack' ; " + "\r\n";
            queryString = queryString + "                           THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "                       END " + "\r\n";

            queryString = queryString + "               END ";
            
            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("PackSaveRelative", queryString);
        }

        private void PackPostSaveValidate()
        {
            //string[] queryArray = new string[1];

            //string queryString = "              DECLARE @Code varchar(50) " + "\r\n";
            //queryString = queryString + "       SELECT TOP 1 @Code = Code FROM Packs WHERE PackID = @EntityID " + "\r\n";

            //queryArray[0] = " SELECT TOP 1 @FoundEntity = N'Trùng lon: ' + @Code + ', Ngày: ' + CONVERT(varchar(215), EntryDate, 113) FROM Packs WHERE PackID <> @EntityID AND Code = @Code ";

            //this.totalSmartCodingEntities.CreateProcedureToCheckExisting("PackPostSaveValidate", queryArray, queryString);


            //string[] queryArray = new string[0];

            //this.totalSmartCodingEntities.CreateProcedureToCheckExisting("PackPostSaveValidate", queryArray);


            string[] queryArray = new string[0];

            string queryString = "  INSERT INTO UniquePacks (Code, EntryDate) SELECT Code, EntryDate FROM Packs WHERE PackID = @EntityID " + "\r\n";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("PackPostSaveValidate", queryArray, queryString);
        }

        private void PackEditable()
        {
            string[] queryArray = new string[1];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = PackID FROM Packs WHERE PackID = @EntityID AND NOT CartonID IS NULL";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("PackEditable", queryArray);
        }





        private void GetPacks()
        {
            string sqlSelect = "       SELECT * FROM Packs WHERE FillingLineID = @FillingLineID AND EntryStatusID IN (SELECT Id FROM dbo.SplitToIntList (@EntryStatusIDs)) " + "\r\n";

            string queryString = " @FillingLineID int, @EntryStatusIDs varchar(3999), @CartonID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "   IF (@CartonID IS NULL) " + "\r\n";
            queryString = queryString + "       " + sqlSelect + "\r\n";
            queryString = queryString + "   ELSE " + "\r\n";
            queryString = queryString + "       " + sqlSelect + " AND CartonID = @CartonID " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetPacks", queryString);
        }

        private void PackUpdateQueueID()
        {
            string queryString = " @PackIDs varchar(3999), @QueueID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       UPDATE      Packs" + "\r\n";
            queryString = queryString + "       SET         QueueID = @QueueID " + "\r\n";
            queryString = queryString + "       WHERE       PackID IN (SELECT Id FROM dbo.SplitToIntList (@PackIDs)) " + "\r\n";

            queryString = queryString + "       IF @@ROWCOUNT <> ((SELECT (LEN(@PackIDs) - LEN(REPLACE(@PackIDs, ',', '')))) + 1) " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";
            queryString = queryString + "               DECLARE     @msg NVARCHAR(300) = N'System Error: Some pack does not exist!' ; " + "\r\n";
            queryString = queryString + "               THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "           END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("PackUpdateQueueID", queryString);
        }

        private void PackUpdateEntryStatus()
        {
            string queryString = " @PackIDs varchar(3999), @EntryStatusID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       UPDATE      Packs" + "\r\n";
            queryString = queryString + "       SET         EntryStatusID = @EntryStatusID " + "\r\n";
            queryString = queryString + "       WHERE       PackID IN (SELECT Id FROM dbo.SplitToIntList (@PackIDs)) " + "\r\n";

            queryString = queryString + "       IF @@ROWCOUNT <> ((SELECT (LEN(@PackIDs) - LEN(REPLACE(@PackIDs, ',', '')))) + 1) " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";
            queryString = queryString + "               DECLARE     @msg NVARCHAR(300) = N'System Error: Some pack does not exist!' ; " + "\r\n";
            queryString = queryString + "               THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "           END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("PackUpdateEntryStatus", queryString);
        }


        private void SearchPacks()
        {
            string queryString;

            queryString = " @Barcode varchar(50) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";
            queryString = queryString + "       SELECT TOP (200) * FROM Packs WHERE Code LIKE '%' + @Barcode+ '%' ORDER BY EntryDate DESC " + "\r\n";
            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("SearchPacks", queryString);
        }

        private void GetRelatedPackID()
        {
            string queryString;

            queryString = " @BatchID int, @Barcode varchar(50) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";
            queryString = queryString + "       DECLARE     @RelatedPackID int ";
            queryString = queryString + "       DECLARE     @Barcode1 varchar(50) = LEFT(@Barcode, 4) + 'N' + RIGHT(@Barcode, 26), @Barcode2 varchar(50) = LEFT(@Barcode, 4) + 'R' + RIGHT(@Barcode, 26), @Barcode3 varchar(50)  = LEFT(@Barcode, 4) + 'T' + RIGHT(@Barcode, 26)";
            queryString = queryString + "       SELECT      TOP 1 @RelatedPackID = PackID FROM Repacks WHERE BatchID = @BatchID AND (Code = @Barcode1 OR Code = @Barcode2 OR Code = @Barcode3) ";
            queryString = queryString + "       SELECT      @RelatedPackID " + "\r\n";
            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetRelatedPackID", queryString);
        }

    }
}
