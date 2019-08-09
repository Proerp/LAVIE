using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Commons
{
    public class CommodityType
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public CommodityType(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetCommodityTypeIndexes();

            //this.CommodityTypeEditable(); 
            //this.CommodityTypeSaveRelative();

            this.GetCommodityTypeBases();
            this.GetCommodityTypeTrees();
        }


        private void GetCommodityTypeIndexes()
        {
            string queryString;

            queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      CommodityTypes.CommodityTypeID, CommodityTypes.Name " + "\r\n";
            queryString = queryString + "       FROM        CommodityTypes " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetCommodityTypeIndexes", queryString);
        }


        private void CommodityTypeSaveRelative()
        {
            string queryString = " @EntityID int, @SaveRelativeOption int " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       IF (@SaveRelativeOption = 1) " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";

            queryString = queryString + "               INSERT INTO CommodityTypeCommodityTypes (CommodityTypeID, CommodityTypeID, CommodityTypeTaskID, EntryDate, Remarks, InActive) " + "\r\n";
            queryString = queryString + "               SELECT      CommodityTypeID, 46 AS CommodityTypeID, " + (int)GlobalEnums.NmvnTaskID.SalesOrder + " AS CommodityTypeTaskID, GETDATE(), '', 0 FROM CommodityTypes WHERE CommodityTypeID = @EntityID " + "\r\n";

            queryString = queryString + "               INSERT INTO CommodityTypeCommodityTypes (CommodityTypeID, CommodityTypeID, CommodityTypeTaskID, EntryDate, Remarks, InActive) " + "\r\n";
            queryString = queryString + "               SELECT      CommodityTypes.CommodityTypeID, CommodityTypes.CommodityTypeID, " + (int)GlobalEnums.NmvnTaskID.DeliveryAdvice + " AS CommodityTypeTaskID, GETDATE(), '', 0 FROM CommodityTypes INNER JOIN CommodityTypes ON CommodityTypes.CommodityTypeID = @EntityID AND CommodityTypes.CommodityTypeTypeID NOT IN (4, 5, 7, 9, 10, 11, 12) AND CommodityTypes.CommodityTypeTypeID = CommodityTypes.CommodityTypeTypeID " + "\r\n";

            queryString = queryString + "               INSERT INTO CommodityTypeCommodityTypes (CommodityTypeID, CommodityTypeID, CommodityTypeTaskID, EntryDate, Remarks, InActive) " + "\r\n";
            queryString = queryString + "               SELECT      CommodityTypeID, 82 AS CommodityTypeID, " + (int)GlobalEnums.NmvnTaskID.DeliveryAdvice + " AS CommodityTypeTaskID, GETDATE(), '', 0 FROM CommodityTypes WHERE CommodityTypeID = @EntityID AND CommodityTypeTypeID IN (4, 5, 7, 9, 10, 11, 12) " + "\r\n";

            queryString = queryString + "           END " + "\r\n";

            queryString = queryString + "       ELSE " + "\r\n"; //(@SaveRelativeOption = -1) 
            queryString = queryString + "           DELETE      CommodityTypeCommodityTypes WHERE CommodityTypeID = @EntityID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("CommodityTypeSaveRelative", queryString);
        }


        private void CommodityTypeEditable()
        {
            string[] queryArray = new string[0];

            //queryArray[0] = " SELECT TOP 1 @FoundEntity = CommodityTypeID FROM CommodityTypes WHERE CommodityTypeID = @EntityID AND (InActive = 1 OR InActivePartial = 1)"; //Don't allow approve after void
            //queryArray[1] = " SELECT TOP 1 @FoundEntity = CommodityTypeID FROM GoodsIssueDetails WHERE CommodityTypeID = @EntityID ";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("CommodityTypeEditable", queryArray);
        }


        private void GetCommodityTypeBases()
        {
            string queryString;

            queryString = " " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      CommodityTypeID, Name " + "\r\n";
            queryString = queryString + "       FROM        CommodityTypes " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetCommodityTypeBases", queryString);
        }

        private void GetCommodityTypeTrees()
        {
            string queryString;

            queryString = " " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      " + GlobalEnums.RootNode + " AS NodeID, 0 AS ParentNodeID, NULL AS PrimaryID, NULL AS AncestorID, '[All]' AS Code, NULL AS Name, NULL AS ParameterName, CAST(1 AS bit) AS Selected " + "\r\n";
            queryString = queryString + "       UNION ALL " + "\r\n";
            queryString = queryString + "       SELECT      " + GlobalEnums.AncestorNode + " + CommodityTypeID AS NodeID, " + GlobalEnums.RootNode + " + 0 AS ParentNodeID, CommodityTypeID AS PrimaryID, NULL AS AncestorID, Name AS Code, N'' AS Name, 'CommodityTypeID' AS ParameterName, CAST(0 AS bit) AS Selected " + "\r\n";
            queryString = queryString + "       FROM        CommodityTypes " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetCommodityTypeTrees", queryString);
        }
    }
}
