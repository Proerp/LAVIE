using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Commons
{
    public class CommodityCategory
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public CommodityCategory(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetCommodityCategoryIndexes();

            //this.CommodityCategoryEditable(); 
            //this.CommodityCategorySaveRelative();

            this.GetCommodityCategoryBases();
        }


        private void GetCommodityCategoryIndexes()
        {
            string queryString;

            queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      CommodityCategories.CommodityCategoryID, CommodityCategories.Name " + "\r\n";
            queryString = queryString + "       FROM        CommodityCategories " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetCommodityCategoryIndexes", queryString);
        }


        private void CommodityCategorySaveRelative()
        {
            string queryString = " @EntityID int, @SaveRelativeOption int " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       IF (@SaveRelativeOption = 1) " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";

            queryString = queryString + "               INSERT INTO CommodityCategoryCommodityCategories (CommodityCategoryID, CommodityCategoryID, CommodityCategoryTaskID, EntryDate, Remarks, InActive) " + "\r\n";
            queryString = queryString + "               SELECT      CommodityCategoryID, 46 AS CommodityCategoryID, " + (int)GlobalEnums.NmvnTaskID.SalesOrder + " AS CommodityCategoryTaskID, GETDATE(), '', 0 FROM CommodityCategories WHERE CommodityCategoryID = @EntityID " + "\r\n";

            queryString = queryString + "               INSERT INTO CommodityCategoryCommodityCategories (CommodityCategoryID, CommodityCategoryID, CommodityCategoryTaskID, EntryDate, Remarks, InActive) " + "\r\n";
            queryString = queryString + "               SELECT      CommodityCategories.CommodityCategoryID, CommodityCategories.CommodityCategoryID, " + (int)GlobalEnums.NmvnTaskID.DeliveryAdvice + " AS CommodityCategoryTaskID, GETDATE(), '', 0 FROM CommodityCategories INNER JOIN CommodityCategories ON CommodityCategories.CommodityCategoryID = @EntityID AND CommodityCategories.CommodityCategoryCategoryID NOT IN (4, 5, 7, 9, 10, 11, 12) AND CommodityCategories.CommodityCategoryCategoryID = CommodityCategories.CommodityCategoryCategoryID " + "\r\n";

            queryString = queryString + "               INSERT INTO CommodityCategoryCommodityCategories (CommodityCategoryID, CommodityCategoryID, CommodityCategoryTaskID, EntryDate, Remarks, InActive) " + "\r\n";
            queryString = queryString + "               SELECT      CommodityCategoryID, 82 AS CommodityCategoryID, " + (int)GlobalEnums.NmvnTaskID.DeliveryAdvice + " AS CommodityCategoryTaskID, GETDATE(), '', 0 FROM CommodityCategories WHERE CommodityCategoryID = @EntityID AND CommodityCategoryCategoryID IN (4, 5, 7, 9, 10, 11, 12) " + "\r\n";

            queryString = queryString + "           END " + "\r\n";

            queryString = queryString + "       ELSE " + "\r\n"; //(@SaveRelativeOption = -1) 
            queryString = queryString + "           DELETE      CommodityCategoryCommodityCategories WHERE CommodityCategoryID = @EntityID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("CommodityCategorySaveRelative", queryString);
        }


        private void CommodityCategoryEditable()
        {
            string[] queryArray = new string[0];

            //queryArray[0] = " SELECT TOP 1 @FoundEntity = CommodityCategoryID FROM CommodityCategories WHERE CommodityCategoryID = @EntityID AND (InActive = 1 OR InActivePartial = 1)"; //Don't allow approve after void
            //queryArray[1] = " SELECT TOP 1 @FoundEntity = CommodityCategoryID FROM GoodsIssueDetails WHERE CommodityCategoryID = @EntityID ";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("CommodityCategoryEditable", queryArray);
        }


        private void GetCommodityCategoryBases()
        {
            string queryString;

            queryString = " " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      CommodityCategoryID, Name " + "\r\n";
            queryString = queryString + "       FROM        CommodityCategories " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetCommodityCategoryBases", queryString);
        }

    }
}
