using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Commons
{
    public class CustomerType
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public CustomerType(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetCustomerTypeIndexes();

            //this.CustomerTypeEditable(); 
            //this.CustomerTypeSaveRelative();

            this.GetCustomerTypeBases();
        }


        private void GetCustomerTypeIndexes()
        {
            string queryString;

            queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      CustomerTypes.CustomerTypeID, CustomerTypes.Name " + "\r\n";
            queryString = queryString + "       FROM        CustomerTypes " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetCustomerTypeIndexes", queryString);
        }


        private void CustomerTypeSaveRelative()
        {
            string queryString = " @EntityID int, @SaveRelativeOption int " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       IF (@SaveRelativeOption = 1) " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";

            queryString = queryString + "               INSERT INTO CustomerTypeCustomerTypes (CustomerTypeID, CustomerTypeID, CustomerTypeTaskID, EntryDate, Remarks, InActive) " + "\r\n";
            queryString = queryString + "               SELECT      CustomerTypeID, 46 AS CustomerTypeID, " + (int)GlobalEnums.NmvnTaskID.SalesOrder + " AS CustomerTypeTaskID, GETDATE(), '', 0 FROM CustomerTypes WHERE CustomerTypeID = @EntityID " + "\r\n";

            queryString = queryString + "               INSERT INTO CustomerTypeCustomerTypes (CustomerTypeID, CustomerTypeID, CustomerTypeTaskID, EntryDate, Remarks, InActive) " + "\r\n";
            queryString = queryString + "               SELECT      CustomerTypes.CustomerTypeID, CustomerTypes.CustomerTypeID, " + (int)GlobalEnums.NmvnTaskID.DeliveryAdvice + " AS CustomerTypeTaskID, GETDATE(), '', 0 FROM CustomerTypes INNER JOIN CustomerTypes ON CustomerTypes.CustomerTypeID = @EntityID AND CustomerTypes.CustomerTypeCategoryID NOT IN (4, 5, 7, 9, 10, 11, 12) AND CustomerTypes.CustomerTypeCategoryID = CustomerTypes.CustomerTypeCategoryID " + "\r\n";

            queryString = queryString + "               INSERT INTO CustomerTypeCustomerTypes (CustomerTypeID, CustomerTypeID, CustomerTypeTaskID, EntryDate, Remarks, InActive) " + "\r\n";
            queryString = queryString + "               SELECT      CustomerTypeID, 82 AS CustomerTypeID, " + (int)GlobalEnums.NmvnTaskID.DeliveryAdvice + " AS CustomerTypeTaskID, GETDATE(), '', 0 FROM CustomerTypes WHERE CustomerTypeID = @EntityID AND CustomerTypeCategoryID IN (4, 5, 7, 9, 10, 11, 12) " + "\r\n";

            queryString = queryString + "           END " + "\r\n";

            queryString = queryString + "       ELSE " + "\r\n"; //(@SaveRelativeOption = -1) 
            queryString = queryString + "           DELETE      CustomerTypeCustomerTypes WHERE CustomerTypeID = @EntityID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("CustomerTypeSaveRelative", queryString);
        }


        private void CustomerTypeEditable()
        {
            string[] queryArray = new string[0];

            //queryArray[0] = " SELECT TOP 1 @FoundEntity = CustomerTypeID FROM CustomerTypes WHERE CustomerTypeID = @EntityID AND (InActive = 1 OR InActivePartial = 1)"; //Don't allow approve after void
            //queryArray[1] = " SELECT TOP 1 @FoundEntity = CustomerTypeID FROM GoodsIssueDetails WHERE CustomerTypeID = @EntityID ";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("CustomerTypeEditable", queryArray);
        }


        private void GetCustomerTypeBases()
        {
            string queryString;

            queryString = " " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      CustomerTypeID, Name " + "\r\n";
            queryString = queryString + "       FROM        CustomerTypes " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetCustomerTypeBases", queryString);
        }

    }
}
