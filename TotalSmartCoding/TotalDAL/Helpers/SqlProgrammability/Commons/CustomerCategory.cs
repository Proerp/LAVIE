using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Commons
{
    public class CustomerCategory
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public CustomerCategory(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetCustomerCategoryIndexes();

            //this.CustomerCategoryEditable(); 
            //this.CustomerCategorySaveRelative();

            this.GetCustomerCategoryBases();
        }


        private void GetCustomerCategoryIndexes()
        {
            string queryString;

            queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      CustomerCategories.CustomerCategoryID, CustomerCategories.Name " + "\r\n";
            queryString = queryString + "       FROM        CustomerCategories " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetCustomerCategoryIndexes", queryString);
        }


        private void CustomerCategorySaveRelative()
        {
            string queryString = " @EntityID int, @SaveRelativeOption int " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       IF (@SaveRelativeOption = 1) " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";

            queryString = queryString + "               INSERT INTO CustomerCategoryCustomerCategories (CustomerCategoryID, CustomerCategoryID, CustomerCategoryTaskID, EntryDate, Remarks, InActive) " + "\r\n";
            queryString = queryString + "               SELECT      CustomerCategoryID, 46 AS CustomerCategoryID, " + (int)GlobalEnums.NmvnTaskID.SalesOrder + " AS CustomerCategoryTaskID, GETDATE(), '', 0 FROM CustomerCategories WHERE CustomerCategoryID = @EntityID " + "\r\n";

            queryString = queryString + "               INSERT INTO CustomerCategoryCustomerCategories (CustomerCategoryID, CustomerCategoryID, CustomerCategoryTaskID, EntryDate, Remarks, InActive) " + "\r\n";
            queryString = queryString + "               SELECT      CustomerCategories.CustomerCategoryID, CustomerCategories.CustomerCategoryID, " + (int)GlobalEnums.NmvnTaskID.DeliveryAdvice + " AS CustomerCategoryTaskID, GETDATE(), '', 0 FROM CustomerCategories INNER JOIN CustomerCategories ON CustomerCategories.CustomerCategoryID = @EntityID AND CustomerCategories.CustomerCategoryCategoryID NOT IN (4, 5, 7, 9, 10, 11, 12) AND CustomerCategories.CustomerCategoryCategoryID = CustomerCategories.CustomerCategoryCategoryID " + "\r\n";

            queryString = queryString + "               INSERT INTO CustomerCategoryCustomerCategories (CustomerCategoryID, CustomerCategoryID, CustomerCategoryTaskID, EntryDate, Remarks, InActive) " + "\r\n";
            queryString = queryString + "               SELECT      CustomerCategoryID, 82 AS CustomerCategoryID, " + (int)GlobalEnums.NmvnTaskID.DeliveryAdvice + " AS CustomerCategoryTaskID, GETDATE(), '', 0 FROM CustomerCategories WHERE CustomerCategoryID = @EntityID AND CustomerCategoryCategoryID IN (4, 5, 7, 9, 10, 11, 12) " + "\r\n";

            queryString = queryString + "           END " + "\r\n";

            queryString = queryString + "       ELSE " + "\r\n"; //(@SaveRelativeOption = -1) 
            queryString = queryString + "           DELETE      CustomerCategoryCustomerCategories WHERE CustomerCategoryID = @EntityID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("CustomerCategorySaveRelative", queryString);
        }


        private void CustomerCategoryEditable()
        {
            string[] queryArray = new string[0];

            //queryArray[0] = " SELECT TOP 1 @FoundEntity = CustomerCategoryID FROM CustomerCategories WHERE CustomerCategoryID = @EntityID AND (InActive = 1 OR InActivePartial = 1)"; //Don't allow approve after void
            //queryArray[1] = " SELECT TOP 1 @FoundEntity = CustomerCategoryID FROM GoodsIssueDetails WHERE CustomerCategoryID = @EntityID ";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("CustomerCategoryEditable", queryArray);
        }


        private void GetCustomerCategoryBases()
        {
            string queryString;

            queryString = " " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      CustomerCategoryID, Name " + "\r\n";
            queryString = queryString + "       FROM        CustomerCategories " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetCustomerCategoryBases", queryString);
        }

    }
}
