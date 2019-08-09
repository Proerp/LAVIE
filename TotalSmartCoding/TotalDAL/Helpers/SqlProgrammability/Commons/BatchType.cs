using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Commons
{
    public class BatchType
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public BatchType(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetBatchTypeIndexes();

            //this.BatchTypeEditable(); 
            //this.BatchTypeSaveRelative();

            this.GetBatchTypeBases();
            this.GetBatchTypeTrees();
        }


        private void GetBatchTypeIndexes()
        {
            string queryString;

            queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      BatchTypes.BatchTypeID, BatchTypes.Code, BatchTypes.Name " + "\r\n";
            queryString = queryString + "       FROM        BatchTypes " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetBatchTypeIndexes", queryString);
        }


        private void BatchTypeSaveRelative()
        {
            string queryString = " @EntityID int, @SaveRelativeOption int " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       IF (@SaveRelativeOption = 1) " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";

            queryString = queryString + "               INSERT INTO BatchTypeBatchTypes (BatchTypeID, BatchTypeID, BatchTypeTaskID, EntryDate, Remarks, InActive) " + "\r\n";
            queryString = queryString + "               SELECT      BatchTypeID, 46 AS BatchTypeID, " + (int)GlobalEnums.NmvnTaskID.SalesOrder + " AS BatchTypeTaskID, GETDATE(), '', 0 FROM BatchTypes WHERE BatchTypeID = @EntityID " + "\r\n";

            queryString = queryString + "               INSERT INTO BatchTypeBatchTypes (BatchTypeID, BatchTypeID, BatchTypeTaskID, EntryDate, Remarks, InActive) " + "\r\n";
            queryString = queryString + "               SELECT      BatchTypes.BatchTypeID, BatchTypes.BatchTypeID, " + (int)GlobalEnums.NmvnTaskID.DeliveryAdvice + " AS BatchTypeTaskID, GETDATE(), '', 0 FROM BatchTypes INNER JOIN BatchTypes ON BatchTypes.BatchTypeID = @EntityID AND BatchTypes.BatchTypeCategoryID NOT IN (4, 5, 7, 9, 10, 11, 12) AND BatchTypes.BatchTypeCategoryID = BatchTypes.BatchTypeCategoryID " + "\r\n";

            queryString = queryString + "               INSERT INTO BatchTypeBatchTypes (BatchTypeID, BatchTypeID, BatchTypeTaskID, EntryDate, Remarks, InActive) " + "\r\n";
            queryString = queryString + "               SELECT      BatchTypeID, 82 AS BatchTypeID, " + (int)GlobalEnums.NmvnTaskID.DeliveryAdvice + " AS BatchTypeTaskID, GETDATE(), '', 0 FROM BatchTypes WHERE BatchTypeID = @EntityID AND BatchTypeCategoryID IN (4, 5, 7, 9, 10, 11, 12) " + "\r\n";

            queryString = queryString + "           END " + "\r\n";

            queryString = queryString + "       ELSE " + "\r\n"; //(@SaveRelativeOption = -1) 
            queryString = queryString + "           DELETE      BatchTypeBatchTypes WHERE BatchTypeID = @EntityID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("BatchTypeSaveRelative", queryString);
        }


        private void BatchTypeEditable()
        {
            string[] queryArray = new string[0];

            //queryArray[0] = " SELECT TOP 1 @FoundEntity = BatchTypeID FROM BatchTypes WHERE BatchTypeID = @EntityID AND (InActive = 1 OR InActivePartial = 1)"; //Don't allow approve after void
            //queryArray[1] = " SELECT TOP 1 @FoundEntity = BatchTypeID FROM GoodsIssueDetails WHERE BatchTypeID = @EntityID ";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("BatchTypeEditable", queryArray);
        }


        private void GetBatchTypeBases()
        {
            string queryString;

            queryString = " " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      BatchTypeID, Code, Name, Code + '-' + Name AS CodeName " + "\r\n";
            queryString = queryString + "       FROM        BatchTypes " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetBatchTypeBases", queryString);
        }

        private void GetBatchTypeTrees()
        {
            string queryString;

            queryString = " " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      " + GlobalEnums.RootNode + " AS NodeID, 0 AS ParentNodeID, NULL AS PrimaryID, NULL AS AncestorID, '[All]' AS Code, NULL AS Name, NULL AS ParameterName, CAST(1 AS bit) AS Selected " + "\r\n";
            queryString = queryString + "       UNION ALL " + "\r\n";
            queryString = queryString + "       SELECT      " + GlobalEnums.AncestorNode + " + BatchTypeID AS NodeID, " + GlobalEnums.RootNode + " + 0 AS ParentNodeID, BatchTypeID AS PrimaryID, NULL AS AncestorID, Name AS Code, N'' AS Name, 'BatchTypeID' AS ParameterName, CAST(0 AS bit) AS Selected " + "\r\n";
            queryString = queryString + "       FROM        BatchTypes " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetBatchTypeTrees", queryString);

        }

    }
}
