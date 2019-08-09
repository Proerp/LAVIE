using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Sales
{
    public class SalesOrder
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public SalesOrder(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetSalesOrderIndexes();

            this.GetSalesOrderViewDetails();

            this.SalesOrderSaveRelative();
            this.SalesOrderPostSaveValidate();

            this.SalesOrderApproved();
            this.SalesOrderEditable();
            this.SalesOrderVoidable();

            this.SalesOrderToggleApproved();
            this.SalesOrderToggleVoid();

            this.SalesOrderInitReference();
        }


        private void GetSalesOrderIndexes()
        {
            string queryString;

            queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      SalesOrders.SalesOrderID, CAST(SalesOrders.EntryDate AS DATE) AS EntryDate, SalesOrders.Reference, SalesOrders.VoucherCode, SalesOrders.DeliveryDate, Locations.Code AS LocationCode, Customers.Code AS CustomerCode, Customers.Name AS CustomerName, SalesOrders.Description, SalesOrders.TotalQuantity, SalesOrders.TotalLineVolume, SalesOrders.Approved, SalesOrders.InActive " + "\r\n";
            queryString = queryString + "       FROM        SalesOrders " + "\r\n";
            queryString = queryString + "                   INNER JOIN Locations ON SalesOrders.EntryDate >= @FromDate AND SalesOrders.EntryDate <= @ToDate AND SalesOrders.OrganizationalUnitID IN (SELECT OrganizationalUnitID FROM AccessControls WHERE UserID = @UserID AND NMVNTaskID = " + (int)TotalBase.Enums.GlobalEnums.NmvnTaskID.SalesOrder + " AND AccessControls.AccessLevel > 0) AND Locations.LocationID = SalesOrders.LocationID " + "\r\n";
            queryString = queryString + "                   INNER JOIN Customers ON SalesOrders.CustomerID = Customers.CustomerID " + "\r\n";
            queryString = queryString + "       " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetSalesOrderIndexes", queryString);
        }


        #region X


        private void GetSalesOrderViewDetails()
        {
            string queryString;

            queryString = " @SalesOrderID Int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       DECLARE     @SalesOrderDetails TABLE (SalesOrderID int NOT NULL, SalesOrderDetailID int NOT NULL, EntryDate datetime NOT NULL, LocationID int NOT NULL, CommodityID int NOT NULL, Quantity decimal(18, 2) NOT NULL, LineVolume decimal(18, 2) NOT NULL, Remarks nvarchar(100) NULL) " + "\r\n";
            queryString = queryString + "       INSERT INTO @SalesOrderDetails (SalesOrderID, SalesOrderDetailID, EntryDate, LocationID, CommodityID, Quantity, LineVolume, Remarks) SELECT SalesOrderID, SalesOrderDetailID, EntryDate, LocationID, CommodityID, Quantity, LineVolume, Remarks FROM SalesOrderDetails WHERE SalesOrderID = @SalesOrderID " + "\r\n";

            queryString = queryString + "                   " + GenerateSQLCommoditiesAvailable.BuildSQL("@SalesOrderDetails", false, false, true, true, true, false, false) + "\r\n";

            queryString = queryString + "       SELECT      SalesOrderDetails.SalesOrderDetailID, SalesOrderDetails.SalesOrderID, Commodities.CommodityID, Commodities.Code AS CommodityCode, Commodities.Name AS CommodityName, Commodities.Unit, Commodities.PackageSize, Commodities.Volume, Commodities.PackageVolume, " + "\r\n";
            queryString = queryString + "                   ISNULL(CommoditiesAvailable.QuantityAvailable, 0) AS QuantityAvailable, ISNULL(CommoditiesAvailable.LineVolumeAvailable, 0) AS LineVolumeAvailable, SalesOrderDetails.Quantity, SalesOrderDetails.LineVolume, SalesOrderDetails.Remarks " + "\r\n";
            queryString = queryString + "       FROM        @SalesOrderDetails SalesOrderDetails " + "\r\n";
            queryString = queryString + "                   INNER JOIN Commodities ON SalesOrderDetails.CommodityID = Commodities.CommodityID" + "\r\n";

            queryString = queryString + "                   LEFT JOIN (SELECT CommodityID, SUM(QuantityAvailable) AS QuantityAvailable, SUM(LineVolumeAvailable) AS LineVolumeAvailable FROM @CommoditiesAvailable GROUP BY CommodityID) CommoditiesAvailable ON SalesOrderDetails.CommodityID = CommoditiesAvailable.CommodityID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetSalesOrderViewDetails", queryString);
        }





        private void SalesOrderSaveRelative()
        {
            string queryString = " @EntityID int, @SaveRelativeOption int " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "   IF (@SaveRelativeOption = 1) ";
            queryString = queryString + "       BEGIN ";
            queryString = queryString + "           UPDATE          SalesOrderDetails " + "\r\n";
            queryString = queryString + "           SET             SalesOrderDetails.Reference = SalesOrders.Reference " + "\r\n";
            queryString = queryString + "           FROM            SalesOrders INNER JOIN SalesOrderDetails ON SalesOrders.SalesOrderID = @EntityID AND SalesOrders.SalesOrderID = SalesOrderDetails.SalesOrderID " + "\r\n";
            queryString = queryString + "       END ";

            this.totalSmartCodingEntities.CreateStoredProcedure("SalesOrderSaveRelative", queryString);
        }

        private void SalesOrderPostSaveValidate()
        {
            string[] queryArray = new string[0];

            //queryArray[0] = " SELECT TOP 1 @FoundEntity = N'Ngày giao hàng: ' + CAST(Pallets.EntryDate AS nvarchar) FROM SalesOrderDetails INNER JOIN Pallets ON SalesOrderDetails.SalesOrderID = @EntityID AND SalesOrderDetails.PalletID = Pallets.PalletID AND SalesOrderDetails.EntryDate < Pallets.EntryDate ";
            //queryArray[1] = " SELECT TOP 1 @FoundEntity = N'Số lượng nhập kho vượt quá số lượng giao: ' + CAST(ROUND(Quantity - QuantitySalesOrder, " + (int)GlobalEnums.rndQuantity + ") AS nvarchar) FROM Pallets WHERE (ROUND(Quantity - QuantitySalesOrder, " + (int)GlobalEnums.rndQuantity + ") < 0) ";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("SalesOrderPostSaveValidate", queryArray);
        }




        private void SalesOrderApproved()
        {
            string[] queryArray = new string[1];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = SalesOrderID FROM SalesOrders WHERE SalesOrderID = @EntityID AND Approved = 1";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("SalesOrderApproved", queryArray);
        }


        private void SalesOrderEditable()
        {
            string[] queryArray = new string[3];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = SalesOrderID FROM SalesOrders WHERE SalesOrderID = @EntityID AND (InActive = 1 OR InActivePartial = 1)"; //Don't allow approve after void

            queryArray[1] = " SELECT TOP 1 @FoundEntity = SalesOrderID FROM DeliveryAdvices WHERE SalesOrderID = @EntityID ";
            queryArray[2] = " SELECT TOP 1 @FoundEntity = SalesOrderID FROM DeliveryAdviceDetails WHERE SalesOrderID = @EntityID ";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("SalesOrderEditable", queryArray);
        }


        private void SalesOrderVoidable()
        {
            string[] queryArray = new string[1];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = SalesOrderID FROM SalesOrders WHERE SalesOrderID = @EntityID AND Approved = 0"; //Must approve in order to allow void

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("SalesOrderVoidable", queryArray);
        }


        private void SalesOrderToggleApproved()
        {
            string queryString = " @EntityID int, @Approved bit " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       UPDATE      SalesOrders  SET Approved = @Approved, ApprovedDate = GetDate() WHERE SalesOrderID = @EntityID AND Approved = ~@Approved" + "\r\n";

            queryString = queryString + "       IF @@ROWCOUNT = 1 " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";
            queryString = queryString + "               UPDATE          SalesOrderDetails  SET Approved = @Approved WHERE SalesOrderID = @EntityID ; " + "\r\n";
            queryString = queryString + "           END " + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";
            queryString = queryString + "               DECLARE     @msg NVARCHAR(300) = N'Dữ liệu không tồn tại hoặc đã ' + iif(@Approved = 0, 'hủy', '')  + ' duyệt' ; " + "\r\n";
            queryString = queryString + "               THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "           END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("SalesOrderToggleApproved", queryString);
        }


        private void SalesOrderToggleVoid()
        {
            string queryString = " @EntityID int, @InActive bit, @VoidTypeID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       UPDATE      SalesOrders  SET InActive = @InActive, InActiveDate = GetDate(), VoidTypeID = IIF(@InActive = 1, @VoidTypeID, NULL) WHERE SalesOrderID = @EntityID AND InActive = ~@InActive" + "\r\n";

            queryString = queryString + "       IF @@ROWCOUNT = 1 " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";
            queryString = queryString + "               UPDATE          SalesOrderDetails  SET InActive = @InActive WHERE SalesOrderID = @EntityID ; " + "\r\n";
            queryString = queryString + "           END " + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";
            queryString = queryString + "               DECLARE     @msg NVARCHAR(300) = N'Dữ liệu không tồn tại hoặc đã ' + iif(@InActive = 0, 'phục hồi lệnh', '')  + ' hủy' ; " + "\r\n";
            queryString = queryString + "               THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "           END " + "\r\n";


            this.totalSmartCodingEntities.CreateStoredProcedure("SalesOrderToggleVoid", queryString);
        }


        private void SalesOrderInitReference()
        {
            SimpleInitReference simpleInitReference = new SimpleInitReference("SalesOrders", "SalesOrderID", "Reference", ModelSettingManager.ReferenceLength, ModelSettingManager.ReferencePrefix(GlobalEnums.NmvnTaskID.SalesOrder));
            this.totalSmartCodingEntities.CreateTrigger("SalesOrderInitReference", simpleInitReference.CreateQuery());
        }


        #endregion
    }
}
