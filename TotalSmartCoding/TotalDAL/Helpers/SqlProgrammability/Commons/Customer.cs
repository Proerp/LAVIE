using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Commons
{
    public class Customer
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public Customer(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetCustomerIndexes();

            this.CustomerEditable();
            this.CustomerSaveRelative();

            this.GetCustomerBases();
            this.GetCustomerTrees();
        }


        private void GetCustomerIndexes()
        {
            string queryString;

            queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      Customers.CustomerID, Customers.Code AS CustomerCode, Customers.Name AS CustomerName, Customers.OfficialName AS CustomerOfficialName, Customers.ContactInfo, Customers.BillingAddress, EntireTerritories.TerritoryID, EntireTerritories.EntireName AS EntireTerritoryEntireName, Employees.EmployeeID, Employees.Name AS SalespersonName, Customers.InActive " + "\r\n";
            queryString = queryString + "       FROM        Customers " + "\r\n";
            queryString = queryString + "                   INNER JOIN EntireTerritories ON Customers.TerritoryID = EntireTerritories.TerritoryID " + "\r\n";
            queryString = queryString + "                   INNER JOIN Employees ON Customers.SalespersonID = Employees.EmployeeID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetCustomerIndexes", queryString);
        }


        private void CustomerSaveRelative()
        {
            string queryString = " @EntityID int, @SaveRelativeOption int " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("CustomerSaveRelative", queryString);
        }


        private void CustomerEditable()
        {
            string[] queryArray = new string[2];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = CustomerID FROM SalesOrders WHERE CustomerID = @EntityID ";
            queryArray[1] = " SELECT TOP 1 @FoundEntity = CustomerID FROM DeliveryAdvices WHERE CustomerID = @EntityID ";


            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("CustomerEditable", queryArray);
        }


        private void GetCustomerBases()
        {
            string queryString;

            queryString = " " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      CustomerID, Code, Name, ContactInfo, SalespersonID, CASE WHEN ShippingAddress <> '' THEN ShippingAddress ELSE BillingAddress END AS ShippingAddress " + "\r\n";
            queryString = queryString + "       FROM        Customers " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetCustomerBases", queryString);
        }

        private void GetCustomerTrees()
        {
            string queryString;

            queryString = " " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      " + GlobalEnums.RootNode + " AS NodeID, 0 AS ParentNodeID, NULL AS PrimaryID, NULL AS AncestorID, '[All]' AS Code, NULL AS Name, NULL AS ParameterName, CAST(1 AS bit) AS Selected " + "\r\n";
            queryString = queryString + "       UNION ALL " + "\r\n";
            queryString = queryString + "       SELECT      " + GlobalEnums.AncestorNode + " + CustomerCategoryID AS NodeID, " + GlobalEnums.RootNode + " AS ParentNodeID, CustomerCategoryID AS PrimaryID, NULL AS AncestorID, Name AS Code, NULL AS Name, 'CustomerCategoryID' AS ParameterName, CAST(0 AS bit) AS Selected " + "\r\n";
            queryString = queryString + "       FROM        CustomerCategories " + "\r\n";
            queryString = queryString + "       UNION ALL " + "\r\n";
            queryString = queryString + "       SELECT      CustomerID AS NodeID, " + GlobalEnums.AncestorNode + " + CustomerCategoryID AS ParentNodeID, CustomerID AS PrimaryID, CustomerCategoryID AS AncestorID, Code, Name, 'CustomerID' AS ParameterName, CAST(0 AS bit) AS Selected " + "\r\n";
            queryString = queryString + "       FROM        Customers " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetCustomerTrees", queryString);
        }
    }
}
