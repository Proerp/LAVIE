using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Commons
{
    public class BatchStatus
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public BatchStatus(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetBatchStatusIndexes();

            //this.BatchStatusEditable(); 
            //this.BatchStatusSaveRelative();

            this.GetBatchStatusBases();
        }


        private void GetBatchStatusIndexes()
        {
            string queryString;

            queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      BatchStatuses.BatchStatusID, BatchStatuses.Code, BatchStatuses.Name " + "\r\n";
            queryString = queryString + "       FROM        BatchStatuses " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetBatchStatusIndexes", queryString);
        }


        private void BatchStatusSaveRelative()
        {
            string queryString = " @EntityID int, @SaveRelativeOption int " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       IF (@SaveRelativeOption = 1) " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";

            queryString = queryString + "               INSERT INTO BatchStatusBatchStatuses (BatchStatusID, BatchStatusID, BatchStatusTaskID, EntryDate, Remarks, InActive) " + "\r\n";
            queryString = queryString + "               SELECT      BatchStatusID, 46 AS BatchStatusID, " + (int)GlobalEnums.NmvnTaskID.SalesOrder + " AS BatchStatusTaskID, GETDATE(), '', 0 FROM BatchStatuses WHERE BatchStatusID = @EntityID " + "\r\n";

            queryString = queryString + "               INSERT INTO BatchStatusBatchStatuses (BatchStatusID, BatchStatusID, BatchStatusTaskID, EntryDate, Remarks, InActive) " + "\r\n";
            queryString = queryString + "               SELECT      BatchStatuses.BatchStatusID, BatchStatuses.BatchStatusID, " + (int)GlobalEnums.NmvnTaskID.DeliveryAdvice + " AS BatchStatusTaskID, GETDATE(), '', 0 FROM BatchStatuses INNER JOIN BatchStatuses ON BatchStatuses.BatchStatusID = @EntityID AND BatchStatuses.BatchStatusBatchStatusID NOT IN (4, 5, 7, 9, 10, 11, 12) AND BatchStatuses.BatchStatusBatchStatusID = BatchStatuses.BatchStatusBatchStatusID " + "\r\n";

            queryString = queryString + "               INSERT INTO BatchStatusBatchStatuses (BatchStatusID, BatchStatusID, BatchStatusTaskID, EntryDate, Remarks, InActive) " + "\r\n";
            queryString = queryString + "               SELECT      BatchStatusID, 82 AS BatchStatusID, " + (int)GlobalEnums.NmvnTaskID.DeliveryAdvice + " AS BatchStatusTaskID, GETDATE(), '', 0 FROM BatchStatuses WHERE BatchStatusID = @EntityID AND BatchStatusBatchStatusID IN (4, 5, 7, 9, 10, 11, 12) " + "\r\n";

            queryString = queryString + "           END " + "\r\n";

            queryString = queryString + "       ELSE " + "\r\n"; //(@SaveRelativeOption = -1) 
            queryString = queryString + "           DELETE      BatchStatusBatchStatuses WHERE BatchStatusID = @EntityID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("BatchStatusSaveRelative", queryString);
        }


        private void BatchStatusEditable()
        {
            string[] queryArray = new string[0];

            //queryArray[0] = " SELECT TOP 1 @FoundEntity = BatchStatusID FROM BatchStatuses WHERE BatchStatusID = @EntityID AND (InActive = 1 OR InActivePartial = 1)"; //Don't allow approve after void
            //queryArray[1] = " SELECT TOP 1 @FoundEntity = BatchStatusID FROM GoodsIssueDetails WHERE BatchStatusID = @EntityID ";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("BatchStatusEditable", queryArray);
        }


        private void GetBatchStatusBases()
        {
            this.totalSmartCodingEntities.CreateStoredProcedure("GetBatchStatusBase", this.GetBatchStatusBUILD(1));
            this.totalSmartCodingEntities.CreateStoredProcedure("GetBatchStatusBases", this.GetBatchStatusBUILD(0));
            this.totalSmartCodingEntities.CreateStoredProcedure("GetBatchStatusBaseByCode", this.GetBatchStatusBUILD(2));
        }

        private string GetBatchStatusBUILD(int switchID)
        {
            string queryString;

            queryString = (switchID == 0 ? "" : (switchID == 1 ? "@BatchStatusID int" : "@Code nvarchar(50)")) + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      BatchStatusID, Code, Name " + "\r\n";
            queryString = queryString + "       FROM        BatchStatuses " + "\r\n";
            queryString = queryString + (switchID == 0 ? "" : "WHERE " + (switchID == 1 ? "   BatchStatusID = @BatchStatusID " : "Code = @Code")) + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            return queryString;
        }

    }
}
