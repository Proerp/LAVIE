using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Sales
{
    public class TransferOrder
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public TransferOrder(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetTransferOrderIndexes();

            this.GetTransferOrderViewDetails();


            this.TransferOrderSaveRelative();
            this.TransferOrderPostSaveValidate();

            this.TransferOrderApproved();
            this.TransferOrderEditable();
            this.TransferOrderVoidable();

            this.TransferOrderToggleApproved();
            this.TransferOrderToggleVoid();

            this.TransferOrderInitReference();
        }


        private void GetTransferOrderIndexes()
        {
            string queryString;
            
            queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      TransferOrders.TransferOrderID, CAST(TransferOrders.EntryDate AS DATE) AS EntryDate, TransferOrders.Reference, TransferOrders.VoucherCode, TransferOrders.DeliveryDate, Locations.Code AS LocationCode, Warehouses.Code AS WarehouseCode, Warehouses.Name AS WarehouseName, WarehouseReceipts.Code AS WarehouseReceiptCode, WarehouseReceipts.Name AS WarehouseReceiptName, TransferOrders.Description, TransferOrders.TotalQuantity, TransferOrders.TotalLineVolume, TransferOrders.Approved, TransferOrders.InActive " + "\r\n";
            queryString = queryString + "       FROM        TransferOrders " + "\r\n";
            queryString = queryString + "                   INNER JOIN Locations ON TransferOrders.EntryDate >= @FromDate AND TransferOrders.EntryDate <= @ToDate AND TransferOrders.OrganizationalUnitID IN (SELECT OrganizationalUnitID FROM AccessControls WHERE UserID = @UserID AND NMVNTaskID = " + (int)TotalBase.Enums.GlobalEnums.NmvnTaskID.TransferOrder + " AND AccessControls.AccessLevel > 0) AND Locations.LocationID = TransferOrders.LocationID " + "\r\n";
            queryString = queryString + "                   INNER JOIN Warehouses ON TransferOrders.WarehouseID = Warehouses.WarehouseID " + "\r\n";
            queryString = queryString + "                   INNER JOIN Warehouses WarehouseReceipts ON TransferOrders.WarehouseReceiptID = WarehouseReceipts.WarehouseID " + "\r\n";
            queryString = queryString + "       " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetTransferOrderIndexes", queryString);
        }


        #region X


        private void GetTransferOrderViewDetails()
        {
            string queryString;

            queryString = " @TransferOrderID Int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       DECLARE     @TransferOrderDetails TABLE (TransferOrderID int NOT NULL, TransferOrderDetailID int NOT NULL, EntryDate datetime NOT NULL, LocationID int NOT NULL, WarehouseID int NOT NULL, CommodityID int NOT NULL, BatchID int NULL, Quantity decimal(18, 2) NOT NULL, LineVolume decimal(18, 2) NOT NULL, QuantityIssue decimal(18, 2) NOT NULL, LineVolumeIssue decimal(18, 2) NOT NULL, Remarks nvarchar(100) NULL) " + "\r\n";
            queryString = queryString + "       INSERT INTO @TransferOrderDetails (TransferOrderID, TransferOrderDetailID, EntryDate, LocationID, WarehouseID, CommodityID, BatchID, Quantity, LineVolume, QuantityIssue, LineVolumeIssue, Remarks) SELECT TransferOrderID, TransferOrderDetailID, EntryDate, LocationID, WarehouseID, CommodityID, BatchID, Quantity, LineVolume, QuantityIssue, LineVolumeIssue, Remarks FROM TransferOrderDetails WHERE TransferOrderID = @TransferOrderID " + "\r\n";

            queryString = queryString + "                   " + GenerateSQLCommoditiesAvailable.BuildSQL("@TransferOrderDetails", false, true, true, true, true, true, false) + "\r\n";

            queryString = queryString + "       SELECT      TransferOrderDetails.TransferOrderDetailID, TransferOrderDetails.TransferOrderID, Commodities.CommodityID, Commodities.Code AS CommodityCode, Commodities.Name AS CommodityName, Commodities.Unit, Commodities.PackageSize, Commodities.Volume, Commodities.PackageVolume, TransferOrderDetails.BatchID, Batches.Code AS BatchCode, " + "\r\n";
            queryString = queryString + "                   ISNULL(CommoditiesAvailable.QuantityAvailable, 0) AS QuantityAvailable, ISNULL(CommoditiesAvailable.LineVolumeAvailable, 0) AS LineVolumeAvailable, ISNULL(CommoditiesAvailableByBatches.QuantityAvailable, 0) AS QuantityBatchAvailable, ISNULL(CommoditiesAvailableByBatches.LineVolumeAvailable, 0) AS LineVolumeBatchAvailable, TransferOrderDetails.Quantity, TransferOrderDetails.LineVolume, TransferOrderDetails.QuantityIssue, TransferOrderDetails.LineVolumeIssue, TransferOrderDetails.Remarks " + "\r\n";
            queryString = queryString + "       FROM        @TransferOrderDetails TransferOrderDetails " + "\r\n";
            queryString = queryString + "                   INNER JOIN Commodities ON TransferOrderDetails.CommodityID = Commodities.CommodityID" + "\r\n";
            queryString = queryString + "                   LEFT JOIN Batches ON TransferOrderDetails.BatchID = Batches.BatchID " + "\r\n";

            queryString = queryString + "                   LEFT JOIN (SELECT CommodityID, SUM(QuantityAvailable) AS QuantityAvailable, SUM(LineVolumeAvailable) AS LineVolumeAvailable FROM @CommoditiesAvailable GROUP BY CommodityID) CommoditiesAvailable ON TransferOrderDetails.CommodityID = CommoditiesAvailable.CommodityID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN (SELECT CommodityID, BatchID, SUM(QuantityAvailable) AS QuantityAvailable, SUM(LineVolumeAvailable) AS LineVolumeAvailable FROM @CommoditiesAvailableByBatches GROUP BY CommodityID, BatchID) CommoditiesAvailableByBatches ON TransferOrderDetails.BatchID = CommoditiesAvailableByBatches.BatchID AND TransferOrderDetails.CommodityID = CommoditiesAvailableByBatches.CommodityID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetTransferOrderViewDetails", queryString);
        }





        private void TransferOrderSaveRelative()
        {
            string queryString = " @EntityID int, @SaveRelativeOption int " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "   IF (@SaveRelativeOption = 1) ";
            queryString = queryString + "       BEGIN ";
            queryString = queryString + "           UPDATE          TransferOrderDetails " + "\r\n";
            queryString = queryString + "           SET             TransferOrderDetails.Reference = TransferOrders.Reference " + "\r\n";
            queryString = queryString + "           FROM            TransferOrders INNER JOIN TransferOrderDetails ON TransferOrders.TransferOrderID = @EntityID AND TransferOrders.TransferOrderID = TransferOrderDetails.TransferOrderID " + "\r\n";
            queryString = queryString + "       END ";

            this.totalSmartCodingEntities.CreateStoredProcedure("TransferOrderSaveRelative", queryString);
        }

        private void TransferOrderPostSaveValidate()
        {
            string[] queryArray = new string[0];

            //queryArray[0] = " SELECT TOP 1 @FoundEntity = N'Ngày giao hàng: ' + CAST(Pallets.EntryDate AS nvarchar) FROM TransferOrderDetails INNER JOIN Pallets ON TransferOrderDetails.TransferOrderID = @EntityID AND TransferOrderDetails.PalletID = Pallets.PalletID AND TransferOrderDetails.EntryDate < Pallets.EntryDate ";
            //queryArray[1] = " SELECT TOP 1 @FoundEntity = N'Số lượng nhập kho vượt quá số lượng giao: ' + CAST(ROUND(Quantity - QuantityTransferOrder, " + (int)GlobalEnums.rndQuantity + ") AS nvarchar) FROM Pallets WHERE (ROUND(Quantity - QuantityTransferOrder, " + (int)GlobalEnums.rndQuantity + ") < 0) ";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("TransferOrderPostSaveValidate", queryArray);
        }




        private void TransferOrderApproved()
        {
            string[] queryArray = new string[1];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = TransferOrderID FROM TransferOrders WHERE TransferOrderID = @EntityID AND Approved = 1";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("TransferOrderApproved", queryArray);
        }


        private void TransferOrderEditable()
        {
            string[] queryArray = new string[3];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = TransferOrderID FROM TransferOrders WHERE TransferOrderID = @EntityID AND (InActive = 1 OR InActivePartial = 1)"; //Don't allow approve after void

            queryArray[1] = " SELECT TOP 1 @FoundEntity = TransferOrderID FROM GoodsIssues WHERE TransferOrderID = @EntityID ";
            queryArray[2] = " SELECT TOP 1 @FoundEntity = TransferOrderID FROM GoodsIssueDetails WHERE TransferOrderID = @EntityID ";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("TransferOrderEditable", queryArray);
        }

        private void TransferOrderVoidable()
        {
            string[] queryArray = new string[1];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = TransferOrderID FROM TransferOrders WHERE TransferOrderID = @EntityID AND Approved = 0"; //Must approve in order to allow void

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("TransferOrderVoidable", queryArray);
        }


        private void TransferOrderToggleApproved()
        {
            string queryString = " @EntityID int, @Approved bit " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       UPDATE      TransferOrders  SET Approved = @Approved, ApprovedDate = GetDate() WHERE TransferOrderID = @EntityID AND Approved = ~@Approved" + "\r\n";

            queryString = queryString + "       IF @@ROWCOUNT = 1 " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";
            queryString = queryString + "               UPDATE          TransferOrderDetails  SET Approved = @Approved WHERE TransferOrderID = @EntityID ; " + "\r\n";
            queryString = queryString + "           END " + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";
            queryString = queryString + "               DECLARE     @msg NVARCHAR(300) = N'Dữ liệu không tồn tại hoặc đã ' + iif(@Approved = 0, 'hủy', '')  + ' duyệt' ; " + "\r\n";
            queryString = queryString + "               THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "           END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("TransferOrderToggleApproved", queryString);
        }


        private void TransferOrderToggleVoid()
        {
            string queryString = " @EntityID int, @InActive bit, @VoidTypeID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       UPDATE      TransferOrders  SET InActive = @InActive, InActiveDate = GetDate(), VoidTypeID = IIF(@InActive = 1, @VoidTypeID, NULL) WHERE TransferOrderID = @EntityID AND InActive = ~@InActive" + "\r\n";

            queryString = queryString + "       IF @@ROWCOUNT = 1 " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";
            queryString = queryString + "               UPDATE          TransferOrderDetails  SET InActive = @InActive WHERE TransferOrderID = @EntityID ; " + "\r\n";
            queryString = queryString + "           END " + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";
            queryString = queryString + "               DECLARE     @msg NVARCHAR(300) = N'Dữ liệu không tồn tại hoặc đã ' + iif(@InActive = 0, 'phục hồi lệnh', '')  + ' hủy' ; " + "\r\n";
            queryString = queryString + "               THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "           END " + "\r\n";


            this.totalSmartCodingEntities.CreateStoredProcedure("TransferOrderToggleVoid", queryString);
        }


        private void TransferOrderInitReference()
        {
            SimpleInitReference simpleInitReference = new SimpleInitReference("TransferOrders", "TransferOrderID", "Reference", ModelSettingManager.ReferenceLength, ModelSettingManager.ReferencePrefix(GlobalEnums.NmvnTaskID.TransferOrder));
            this.totalSmartCodingEntities.CreateTrigger("TransferOrderInitReference", simpleInitReference.CreateQuery());
        }


        #endregion
    }
}
