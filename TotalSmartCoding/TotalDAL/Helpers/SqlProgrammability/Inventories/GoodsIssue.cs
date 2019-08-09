using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Inventories
{
    public class GoodsIssue
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public GoodsIssue(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetGoodsIssueIndexes();


            this.GetGoodsIssueViewDetails();

            this.GetPendingDeliveryAdvices();
            this.GetPendingDeliveryAdviceCustomers();

            this.GetPendingTransferOrders();
            this.GetPendingTransferOrderWarehouses();


            GenerateSQLPendingDetails generatePendingDeliveryAdviceDetails = new GenerateSQLPendingDetails(totalSmartCodingEntities, "GetPendingDeliveryAdviceDetails", "DeliveryAdviceDetails", "DeliveryAdviceID", "@DeliveryAdviceID", "DeliveryAdviceDetailID", "@DeliveryAdviceDetailIDs", "CustomerID", "@CustomerID", true, false, "PrimaryReference", "PrimaryEntryDate");
            generatePendingDeliveryAdviceDetails.GetPendingDeliveryAdviceDetails();
            GenerateSQLPendingDetails generatePendingTransferOrderDetails = new GenerateSQLPendingDetails(totalSmartCodingEntities, "GetPendingTransferOrderDetails", "TransferOrderDetails", "TransferOrderID", "@TransferOrderID", "TransferOrderDetailID", "@TransferOrderDetailIDs", "WarehouseReceiptID", "@WarehouseReceiptID", false, true, "PrimaryReference", "PrimaryEntryDate");
            generatePendingTransferOrderDetails.GetPendingDeliveryAdviceDetails();


            this.GoodsIssueSaveRelative();
            this.GoodsIssuePostSaveValidate();

            this.GoodsIssueApproved();
            this.GoodsIssueEditable();

            this.GoodsIssueToggleApproved();

            this.GoodsIssueInitReference();

            this.GetGoodsIssueSheet();
        }


        private void GetGoodsIssueIndexes()
        {
            string queryString;

            queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      GoodsIssues.GoodsIssueID, CAST(GoodsIssues.EntryDate AS DATE) AS EntryDate, GoodsIssues.Reference, GoodsIssues.PrimaryReferences, GoodsIssues.VoucherCodes, Locations.Code AS LocationCode, Customers.Code AS CustomerCode, Customers.Name AS CustomerName, WarehouseReceipts.Name AS WarehouseReceiptName, CASE WHEN Customers.Name IS NULL THEN WarehouseReceipts.Name ELSE Customers.Name END AS GoodsIssueName, GoodsIssues.TotalQuantity, GoodsIssues.TotalLineVolume, GoodsIssues.Approved " + "\r\n";
            queryString = queryString + "       FROM        GoodsIssues " + "\r\n";
            queryString = queryString + "                   INNER JOIN Locations ON GoodsIssues.EntryDate >= @FromDate AND GoodsIssues.EntryDate <= @ToDate AND GoodsIssues.OrganizationalUnitID IN (SELECT OrganizationalUnitID FROM AccessControls WHERE UserID = @UserID AND NMVNTaskID = " + (int)TotalBase.Enums.GlobalEnums.NmvnTaskID.GoodsIssue + " AND AccessControls.AccessLevel > 0) AND Locations.LocationID = GoodsIssues.LocationID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Customers ON GoodsIssues.CustomerID = Customers.CustomerID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Warehouses WarehouseReceipts ON GoodsIssues.WarehouseReceiptID = WarehouseReceipts.WarehouseID " + "\r\n";
            queryString = queryString + "       " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetGoodsIssueIndexes", queryString);
        }


        #region X


        private void GetGoodsIssueViewDetails()
        {
            string queryString;

            queryString = " @GoodsIssueID Int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      GoodsIssueDetails.GoodsIssueDetailID, GoodsIssueDetails.GoodsIssueID, GoodsIssueDetails.DeliveryAdviceID, GoodsIssueDetails.DeliveryAdviceDetailID, DeliveryAdviceDetails.Reference AS DeliveryAdviceReference, DeliveryAdviceDetails.EntryDate AS DeliveryAdviceEntryDate, GoodsIssueDetails.TransferOrderID, GoodsIssueDetails.TransferOrderDetailID, TransferOrderDetails.Reference AS TransferOrderReference, TransferOrderDetails.EntryDate AS TransferOrderEntryDate, " + "\r\n";
            queryString = queryString + "                   GoodsIssueDetails.GoodsReceiptID, GoodsIssueDetails.GoodsReceiptDetailID, GoodsReceiptDetails.BatchID, GoodsReceiptDetails.BatchEntryDate, GoodsReceiptDetails.Reference AS GoodsReceiptReference, GoodsReceiptDetails.EntryDate AS GoodsReceiptEntryDate," + "\r\n";
            queryString = queryString + "                   GoodsReceiptDetails.WarehouseID, Commodities.CommodityID, Commodities.Code AS CommodityCode, Commodities.Name AS CommodityName, BinLocations.BinLocationID, BinLocations.Code AS BinLocationCode, " + "\r\n";
            queryString = queryString + "                   GoodsReceiptDetails.PackID, Packs.Code AS PackCode, GoodsReceiptDetails.CartonID, Cartons.Code AS CartonCode, GoodsReceiptDetails.PalletID, Pallets.Code AS PalletCode, GoodsReceiptDetails.PackCounts, GoodsReceiptDetails.CartonCounts, GoodsReceiptDetails.PalletCounts, " + "\r\n";
            queryString = queryString + "                   ROUND(GoodsReceiptDetails.Quantity - GoodsReceiptDetails.QuantityIssue + GoodsIssueDetails.Quantity, " + (int)GlobalEnums.rndQuantity + ") AS QuantityAvailable, ROUND(GoodsReceiptDetails.LineVolume - GoodsReceiptDetails.LineVolumeIssue + GoodsIssueDetails.LineVolume, " + (int)GlobalEnums.rndVolume + ") AS LineVolumeAvailable, ROUND(ISNULL(DeliveryAdviceDetails.Quantity, 0) - ISNULL(DeliveryAdviceDetails.QuantityIssue, 0) + ISNULL(TransferOrderDetails.Quantity, 0) - ISNULL(TransferOrderDetails.QuantityIssue, 0) + GoodsIssueDetails.Quantity, " + (int)GlobalEnums.rndQuantity + ") AS QuantityRemains, ROUND(ISNULL(DeliveryAdviceDetails.LineVolume, 0) - ISNULL(DeliveryAdviceDetails.LineVolumeIssue, 0) + ISNULL(TransferOrderDetails.LineVolume, 0) - ISNULL(TransferOrderDetails.LineVolumeIssue, 0) + GoodsIssueDetails.LineVolume, " + (int)GlobalEnums.rndVolume + ") AS LineVolumeRemains, GoodsIssueDetails.Quantity, GoodsIssueDetails.LineVolume, GoodsIssueDetails.Remarks " + "\r\n";

            queryString = queryString + "       FROM        GoodsIssueDetails " + "\r\n";
            queryString = queryString + "                   INNER JOIN Commodities ON GoodsIssueDetails.GoodsIssueID = @GoodsIssueID AND GoodsIssueDetails.CommodityID = Commodities.CommodityID " + "\r\n";
            queryString = queryString + "                   INNER JOIN GoodsReceiptDetails ON GoodsIssueDetails.GoodsReceiptDetailID = GoodsReceiptDetails.GoodsReceiptDetailID " + "\r\n";
            queryString = queryString + "                   INNER JOIN BinLocations ON GoodsReceiptDetails.BinLocationID = BinLocations.BinLocationID " + "\r\n";

            queryString = queryString + "                   LEFT JOIN DeliveryAdviceDetails ON GoodsIssueDetails.DeliveryAdviceDetailID = DeliveryAdviceDetails.DeliveryAdviceDetailID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN TransferOrderDetails ON GoodsIssueDetails.TransferOrderDetailID = TransferOrderDetails.TransferOrderDetailID " + "\r\n";

            queryString = queryString + "                   LEFT JOIN Pallets ON GoodsReceiptDetails.PalletID = Pallets.PalletID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Packs ON GoodsReceiptDetails.PackID = Packs.PackID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Cartons ON GoodsReceiptDetails.CartonID = Cartons.CartonID " + "\r\n";

            queryString = queryString + "       ORDER BY    GoodsIssueDetails.GoodsIssueDetailID DESC " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetGoodsIssueViewDetails", queryString);
        }





        #region Y

        #region DeliveryAdvice
        private void GetPendingDeliveryAdvices()
        {
            string queryString = " @LocationID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       SELECT          DeliveryAdvices.DeliveryAdviceID, DeliveryAdvices.Reference AS DeliveryAdviceReference, DeliveryAdvices.EntryDate AS DeliveryAdviceEntryDate, DeliveryAdvices.SalesOrderReferences, DeliveryAdvices.VoucherCode, DeliveryAdvices.CustomerID, Customers.Code AS CustomerCode, Customers.Name AS CustomerName, DeliveryAdvices.ReceiverID, Receivers.Code AS ReceiverCode, Receivers.Name AS ReceiverName, DeliveryAdvices.ForkliftDriverID, DeliveryAdvices.StorekeeperID, DeliveryAdvices.Vehicle, DeliveryAdvices.VehicleDriver, DeliveryAdvices.Description, DeliveryAdvices.Remarks " + "\r\n";
            queryString = queryString + "       FROM            DeliveryAdvices " + "\r\n";
            queryString = queryString + "                       INNER JOIN Customers ON DeliveryAdvices.DeliveryAdviceID IN (SELECT DeliveryAdviceID FROM DeliveryAdviceDetails WHERE LocationID = @LocationID AND Approved = 1 AND InActive = 0 AND ROUND(Quantity - QuantityIssue, " + (int)GlobalEnums.rndQuantity + ") > 0) AND DeliveryAdvices.CustomerID = Customers.CustomerID " + "\r\n";
            queryString = queryString + "                       INNER JOIN Customers Receivers ON DeliveryAdvices.ReceiverID = Receivers.CustomerID " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetPendingDeliveryAdvices", queryString);
        }

        private void GetPendingDeliveryAdviceCustomers()
        {
            string queryString = " @LocationID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       SELECT          Customers.CustomerID, Customers.Code AS CustomerCode, Customers.Name AS CustomerName, CustomerPENDING.ReceiverID, Receivers.Code AS ReceiverCode, Receivers.Name AS ReceiverName " + "\r\n";
            queryString = queryString + "       FROM           (SELECT CustomerID, ReceiverID FROM DeliveryAdviceDetails WHERE LocationID = @LocationID AND Approved = 1 AND InActive = 0 AND ROUND(Quantity - QuantityIssue, " + (int)GlobalEnums.rndQuantity + ") > 0 GROUP BY CustomerID, ReceiverID) CustomerPENDING " + "\r\n";
            queryString = queryString + "                       INNER JOIN Customers ON CustomerPENDING.CustomerID = Customers.CustomerID " + "\r\n";
            queryString = queryString + "                       INNER JOIN Customers Receivers ON CustomerPENDING.ReceiverID = Receivers.CustomerID " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetPendingDeliveryAdviceCustomers", queryString);
        }
        #endregion DeliveryAdvice



        #region TransferOrder
        private void GetPendingTransferOrders()
        {
            string queryString = " @LocationID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       SELECT          TransferOrders.TransferOrderID, TransferOrders.Reference AS TransferOrderReference, TransferOrders.EntryDate AS TransferOrderEntryDate, TransferOrders.VoucherCode, TransferOrders.TransferJobs, TransferOrders.WarehouseID, Warehouses.Code AS WarehouseCode, Warehouses.Name AS WarehouseName, TransferOrders.WarehouseReceiptID, WarehouseReceipts.Code AS WarehouseReceiptCode, WarehouseReceipts.Name AS WarehouseReceiptName, TransferOrders.ForkliftDriverID, TransferOrders.StorekeeperID, TransferOrders.Vehicle, TransferOrders.VehicleDriver, TransferOrders.Description, TransferOrders.Remarks " + "\r\n";
            queryString = queryString + "       FROM            TransferOrders " + "\r\n";
            queryString = queryString + "                       INNER JOIN Warehouses ON TransferOrders.TransferOrderID IN (SELECT TransferOrderID FROM TransferOrderDetails WHERE LocationID = @LocationID AND Approved = 1 AND InActive = 0 AND ROUND(Quantity - QuantityIssue, " + (int)GlobalEnums.rndQuantity + ") > 0) AND TransferOrders.WarehouseID = Warehouses.WarehouseID " + "\r\n";
            queryString = queryString + "                       INNER JOIN Warehouses WarehouseReceipts ON TransferOrders.WarehouseReceiptID = WarehouseReceipts.WarehouseID " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetPendingTransferOrders", queryString);
        }

        private void GetPendingTransferOrderWarehouses()
        {
            string queryString = " @LocationID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       SELECT          Warehouses.WarehouseID, Warehouses.Code AS WarehouseCode, Warehouses.Name AS WarehouseName, WarehouseReceipts.WarehouseID AS WarehouseReceiptID, WarehouseReceipts.Code AS WarehouseReceiptCode, WarehouseReceipts.Name AS WarehouseReceiptName " + "\r\n";
            queryString = queryString + "       FROM           (SELECT WarehouseID, WarehouseReceiptID FROM TransferOrderDetails WHERE LocationID = @LocationID AND Approved = 1 AND InActive = 0 AND ROUND(Quantity - QuantityIssue, " + (int)GlobalEnums.rndQuantity + ") > 0 GROUP BY WarehouseID, WarehouseReceiptID) WarehousePENDING " + "\r\n";
            queryString = queryString + "                       INNER JOIN Warehouses ON WarehousePENDING.WarehouseID = Warehouses.WarehouseID " + "\r\n";
            queryString = queryString + "                       INNER JOIN Warehouses WarehouseReceipts ON WarehousePENDING.WarehouseReceiptID = WarehouseReceipts.WarehouseID " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetPendingTransferOrderWarehouses", queryString);
        }
        #endregion TransferOrder

        #endregion Y




        private void GoodsIssueSaveRelative()
        {
            string queryString = " @EntityID int, @SaveRelativeOption int " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       BEGIN " + "\r\n";

            queryString = queryString + "           IF (@SaveRelativeOption = 1) ";
            queryString = queryString + "               BEGIN ";
            queryString = queryString + "                   UPDATE          GoodsIssueDetails " + "\r\n";
            queryString = queryString + "                   SET             GoodsIssueDetails.Reference = GoodsIssues.Reference " + "\r\n";
            queryString = queryString + "                   FROM            GoodsIssues INNER JOIN GoodsIssueDetails ON GoodsIssues.GoodsIssueID = @EntityID AND GoodsIssues.GoodsIssueID = GoodsIssueDetails.GoodsIssueID " + "\r\n";
            queryString = queryString + "               END ";

            queryString = queryString + "           DECLARE @GoodsIssueTypeID int ";
            queryString = queryString + "           SELECT @GoodsIssueTypeID = GoodsIssueTypeID FROM GoodsIssues WHERE GoodsIssueID = @EntityID ";
            queryString = queryString + "           IF (@GoodsIssueTypeID = " + (int)GlobalEnums.GoodsIssueTypeID.DeliveryAdvice + ") " + "\r\n";
            queryString = queryString + "               BEGIN  " + "\r\n";
            queryString = queryString + "                   UPDATE          DeliveryAdviceDetails" + "\r\n";
            queryString = queryString + "                   SET             DeliveryAdviceDetails.QuantityIssue = ROUND(DeliveryAdviceDetails.QuantityIssue + GoodsIssueDetails.Quantity * @SaveRelativeOption, " + (int)GlobalEnums.rndQuantity + "), DeliveryAdviceDetails.LineVolumeIssue = ROUND(DeliveryAdviceDetails.LineVolumeIssue + GoodsIssueDetails.LineVolume * @SaveRelativeOption, " + (int)GlobalEnums.rndVolume + ") " + "\r\n";
            queryString = queryString + "                   FROM            (SELECT DeliveryAdviceDetailID, SUM(Quantity) AS Quantity, SUM(LineVolume) AS LineVolume FROM GoodsIssueDetails WHERE GoodsIssueID = @EntityID AND DeliveryAdviceDetailID IS NOT NULL GROUP BY DeliveryAdviceDetailID) GoodsIssueDetails " + "\r\n";
            queryString = queryString + "                                   INNER JOIN DeliveryAdviceDetails ON GoodsIssueDetails.DeliveryAdviceDetailID = DeliveryAdviceDetails.DeliveryAdviceDetailID" + "\r\n";
            queryString = queryString + "               END  " + "\r\n";

            queryString = queryString + "           IF (@GoodsIssueTypeID = " + (int)GlobalEnums.GoodsIssueTypeID.TransferOrder + ") " + "\r\n";
            queryString = queryString + "               BEGIN  " + "\r\n";
            queryString = queryString + "                   UPDATE          TransferOrderDetails" + "\r\n";
            queryString = queryString + "                   SET             TransferOrderDetails.QuantityIssue = ROUND(TransferOrderDetails.QuantityIssue + GoodsIssueDetails.Quantity * @SaveRelativeOption, " + (int)GlobalEnums.rndQuantity + "), TransferOrderDetails.LineVolumeIssue = ROUND(TransferOrderDetails.LineVolumeIssue + GoodsIssueDetails.LineVolume * @SaveRelativeOption, " + (int)GlobalEnums.rndVolume + ") " + "\r\n";
            queryString = queryString + "                   FROM            (SELECT TransferOrderDetailID, SUM(Quantity) AS Quantity, SUM(LineVolume) AS LineVolume FROM GoodsIssueDetails WHERE GoodsIssueID = @EntityID AND TransferOrderDetailID IS NOT NULL GROUP BY TransferOrderDetailID) GoodsIssueDetails " + "\r\n";
            queryString = queryString + "                                   INNER JOIN TransferOrderDetails ON GoodsIssueDetails.TransferOrderDetailID = TransferOrderDetails.TransferOrderDetailID" + "\r\n";
            queryString = queryString + "               END  " + "\r\n";


            queryString = queryString + "           UPDATE          GoodsReceiptDetails" + "\r\n";
            queryString = queryString + "           SET             GoodsReceiptDetails.QuantityIssue = ROUND(GoodsReceiptDetails.QuantityIssue + GoodsIssueDetails.Quantity * @SaveRelativeOption, " + (int)GlobalEnums.rndQuantity + "), GoodsReceiptDetails.LineVolumeIssue = ROUND(GoodsReceiptDetails.LineVolumeIssue + GoodsIssueDetails.LineVolume * @SaveRelativeOption, " + (int)GlobalEnums.rndVolume + ") " + "\r\n";
            queryString = queryString + "           FROM            (SELECT GoodsReceiptDetailID, SUM(Quantity) AS Quantity, SUM(LineVolume) AS LineVolume FROM GoodsIssueDetails WHERE GoodsIssueID = @EntityID GROUP BY GoodsReceiptDetailID) GoodsIssueDetails " + "\r\n";
            queryString = queryString + "                           INNER JOIN GoodsReceiptDetails ON GoodsIssueDetails.GoodsReceiptDetailID = GoodsReceiptDetails.GoodsReceiptDetailID" + "\r\n";

            queryString = queryString + "           IF @@ROWCOUNT > (SELECT COUNT(*) FROM GoodsIssueDetails WHERE GoodsIssueID = @EntityID) " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   DECLARE     @msg NVARCHAR(300) = N'Phiếu giao hàng đã hủy, hoặc chưa duyệt, hoặc 1 line xuất kho 2 lần' ; " + "\r\n";
            queryString = queryString + "                   THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "               END " + "\r\n";

            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GoodsIssueSaveRelative", queryString);
        }

        private void GoodsIssuePostSaveValidate()
        {
            string[] queryArray = new string[6];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = N'Ngày đề nghị giao hàng: ' + CAST(DeliveryAdvices.EntryDate AS nvarchar) FROM GoodsIssueDetails INNER JOIN DeliveryAdvices ON GoodsIssueDetails.GoodsIssueID = @EntityID AND GoodsIssueDetails.DeliveryAdviceID = DeliveryAdvices.DeliveryAdviceID AND GoodsIssueDetails.EntryDate < DeliveryAdvices.EntryDate ";
            queryArray[1] = " SELECT TOP 1 @FoundEntity = N'Ngày đề nghị chuyển kho: ' + CAST(TransferOrders.EntryDate AS nvarchar) FROM GoodsIssueDetails INNER JOIN TransferOrders ON GoodsIssueDetails.GoodsIssueID = @EntityID AND GoodsIssueDetails.TransferOrderID = TransferOrders.TransferOrderID AND GoodsIssueDetails.EntryDate < TransferOrders.EntryDate ";
            queryArray[2] = " SELECT TOP 1 @FoundEntity = N'Ngày nhập kho: ' + CAST(GoodsReceipts.EntryDate AS nvarchar) FROM GoodsIssueDetails INNER JOIN GoodsReceipts ON GoodsIssueDetails.GoodsIssueID = @EntityID AND GoodsIssueDetails.GoodsReceiptID = GoodsReceipts.GoodsReceiptID AND GoodsIssueDetails.EntryDate < GoodsReceipts.EntryDate ";
            queryArray[3] = " SELECT TOP 1 @FoundEntity = N'Đề nghị giao hàng chưa duyệt hoặc số lượng xuất kho vượt quá số lượng đề nghị: ' + CAST(ROUND(Quantity - QuantityIssue, " + (int)GlobalEnums.rndQuantity + ") AS nvarchar) + N' Hoặc khối lượng: ' + CAST(ROUND(LineVolume - LineVolumeIssue, " + (int)GlobalEnums.rndVolume + ") AS nvarchar) FROM DeliveryAdviceDetails WHERE (Approved = 0 AND (QuantityIssue > 0 OR LineVolumeIssue > 0)) OR (ROUND(Quantity - QuantityIssue, " + (int)GlobalEnums.rndQuantity + ") < 0) OR (ROUND(LineVolume - LineVolumeIssue, " + (int)GlobalEnums.rndVolume + ") < 0) ";
            queryArray[4] = " SELECT TOP 1 @FoundEntity = N'Đề nghị chuyển kho chưa duyệt hoặc số lượng xuất kho vượt quá số lượng đề nghị: ' + CAST(ROUND(Quantity - QuantityIssue, " + (int)GlobalEnums.rndQuantity + ") AS nvarchar) + N' Hoặc khối lượng: ' + CAST(ROUND(LineVolume - LineVolumeIssue, " + (int)GlobalEnums.rndVolume + ") AS nvarchar) FROM TransferOrderDetails WHERE (Approved = 0 AND (QuantityIssue > 0 OR LineVolumeIssue > 0)) OR (ROUND(Quantity - QuantityIssue, " + (int)GlobalEnums.rndQuantity + ") < 0) OR (ROUND(LineVolume - LineVolumeIssue, " + (int)GlobalEnums.rndVolume + ") < 0) ";            
            queryArray[5] = " SELECT TOP 1 @FoundEntity = N'Phiếu nhập kho chưa duyệt hoặc số lượng xuất kho vượt quá số lượng tồn kho: ' + CAST(ROUND(Quantity - QuantityIssue, " + (int)GlobalEnums.rndQuantity + ") AS nvarchar) + N' Hoặc khối lượng: ' + CAST(ROUND(LineVolume - LineVolumeIssue, " + (int)GlobalEnums.rndVolume + ") AS nvarchar) FROM GoodsReceiptDetails WHERE (Approved = 0 AND (QuantityIssue > 0 OR LineVolumeIssue > 0)) OR (ROUND(Quantity - QuantityIssue, " + (int)GlobalEnums.rndQuantity + ") < 0) OR (ROUND(LineVolume - LineVolumeIssue, " + (int)GlobalEnums.rndVolume + ") < 0) ";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("GoodsIssuePostSaveValidate", queryArray);
        }




        private void GoodsIssueApproved()
        {
            string[] queryArray = new string[1];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = GoodsIssueID FROM GoodsIssues WHERE GoodsIssueID = @EntityID AND Approved = 1";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("GoodsIssueApproved", queryArray);
        }


        private void GoodsIssueEditable()
        {
            string[] queryArray = new string[1];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = GoodsIssueID FROM GoodsReceiptDetails WHERE GoodsIssueID = @EntityID ";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("GoodsIssueEditable", queryArray);
        }

        private void GoodsIssueToggleApproved()
        {
            string queryString = " @EntityID int, @Approved bit " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       UPDATE      GoodsIssues  SET Approved = @Approved, ApprovedDate = GetDate() WHERE GoodsIssueID = @EntityID AND Approved = ~@Approved" + "\r\n";

            queryString = queryString + "       IF @@ROWCOUNT = 1 " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";
            queryString = queryString + "               UPDATE          GoodsIssueDetails  SET Approved = @Approved WHERE GoodsIssueID = @EntityID ; " + "\r\n";

            queryString = queryString + "               IF (@Approved = 1) " + "\r\n";
            queryString = queryString + "                   BEGIN " + "\r\n";

            queryString = queryString + "                       DECLARE @TransferOrderID Int, @TransferPackageTypeID Int, @LocationID Int, @WarehouseReceiptID Int " + "\r\n";
            queryString = queryString + "                       SELECT  TOP 1 @TransferOrderID = TransferOrderID, @WarehouseReceiptID = WarehouseReceiptID FROM GoodsIssueDetails WHERE GoodsIssueID = @EntityID " + "\r\n";
            queryString = queryString + "                       IF (NOT @TransferOrderID IS NULL) " + "\r\n";
            queryString = queryString + "                           BEGIN " + "\r\n";

            queryString = queryString + "                               SELECT  TOP 1 @LocationID = LocationID FROM Warehouses WHERE WarehouseID = @WarehouseReceiptID " + "\r\n";
            queryString = queryString + "                               SELECT  TOP 1 @TransferPackageTypeID = TransferPackageTypeID FROM TransferOrders WHERE TransferOrderID = @TransferOrderID " + "\r\n";            
            
            queryString = queryString + "                               INSERT INTO     GoodsIssueTransferDetails (GoodsIssueDetailID, GoodsIssueID, TransferOrderDetailID, TransferOrderID, EntryDate, Reference, LocationID, WarehouseID, WarehouseReceiptID, CommodityID, BinLocationID, BatchID, BatchEntryDate, PackID, CartonID, PalletID, PackCounts, CartonCounts, PalletCounts, Quantity, QuantityReceipt, LineVolume, LineVolumeReceipt, Remarks, Approved) " + "\r\n";
            queryString = queryString + "                               SELECT          GoodsIssueDetailID, GoodsIssueID, TransferOrderDetailID, TransferOrderID, EntryDate, Reference, @LocationID, WarehouseID, WarehouseReceiptID, CommodityID, BinLocationID, BatchID, BatchEntryDate, PackID, CartonID, PalletID, PackCounts, CartonCounts, PalletCounts, Quantity, 0 AS QuantityReceipt, LineVolume, 0 AS LineVolumeReceipt, Remarks, Approved FROM GoodsIssueDetails WHERE GoodsIssueID = @EntityID AND (@TransferPackageTypeID = " + (int)GlobalEnums.TransferPackageTypeID.Pallets + " OR PalletID IS NULL OR (NOT PalletID IS NULL AND CartonCounts = 0)) " + "\r\n";

            queryString = queryString + "                               IF (@TransferPackageTypeID = " + (int)GlobalEnums.TransferPackageTypeID.Cartons + ") " + "\r\n";
            queryString = queryString + "                                   INSERT INTO     GoodsIssueTransferDetails (GoodsIssueDetailID, GoodsIssueID, TransferOrderDetailID, TransferOrderID, EntryDate, Reference, LocationID, WarehouseID, WarehouseReceiptID, CommodityID, BinLocationID, BatchID, BatchEntryDate, PackID, CartonID, PalletID, PackCounts, CartonCounts, PalletCounts, Quantity, QuantityReceipt, LineVolume, LineVolumeReceipt, Remarks, Approved) " + "\r\n";
            queryString = queryString + "                                   SELECT          GoodsIssueDetails.GoodsIssueDetailID, GoodsIssueDetails.GoodsIssueID, GoodsIssueDetails.TransferOrderDetailID, GoodsIssueDetails.TransferOrderID, GoodsIssueDetails.EntryDate, GoodsIssueDetails.Reference, @LocationID, GoodsIssueDetails.WarehouseID, GoodsIssueDetails.WarehouseReceiptID, GoodsIssueDetails.CommodityID, GoodsIssueDetails.BinLocationID, GoodsIssueDetails.BatchID, GoodsIssueDetails.BatchEntryDate, GoodsIssueDetails.PackID, Cartons.CartonID, NULL AS PalletID, Cartons.PackCounts, 1 AS CartonCounts, 0 AS PalletCounts, 1 AS Quantity, 0 AS QuantityReceipt, Cartons.LineVolume, 0 AS LineVolumeReceipt, GoodsIssueDetails.Remarks, GoodsIssueDetails.Approved FROM GoodsIssueDetails INNER JOIN Cartons ON GoodsIssueDetails.PalletID = Cartons.PalletID WHERE GoodsIssueDetails.GoodsIssueID = @EntityID AND NOT GoodsIssueDetails.PalletID IS NULL " + "\r\n";

            queryString = queryString + "                           END " + "\r\n";

            queryString = queryString + "                   END " + "\r\n";

            queryString = queryString + "               ELSE " + "\r\n";
            queryString = queryString + "                   BEGIN " + "\r\n";
            queryString = queryString + "                       DELETE FROM GoodsIssueTransferDetails WHERE GoodsIssueID = @EntityID ; " + "\r\n";
            queryString = queryString + "                   END " + "\r\n";

            queryString = queryString + "           END " + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";
            queryString = queryString + "               DECLARE     @msg NVARCHAR(300) = N'Dữ liệu không tồn tại hoặc đã ' + iif(@Approved = 0, 'hủy', '')  + ' duyệt' ; " + "\r\n";
            queryString = queryString + "               THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "           END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GoodsIssueToggleApproved", queryString);
        }

        private void GoodsIssueInitReference()
        {
            SimpleInitReference simpleInitReference = new SimpleInitReference("GoodsIssues", "GoodsIssueID", "Reference", ModelSettingManager.ReferenceLength, ModelSettingManager.ReferencePrefix(GlobalEnums.NmvnTaskID.GoodsIssue));
            this.totalSmartCodingEntities.CreateTrigger("GoodsIssueInitReference", simpleInitReference.CreateQuery());
        }

        private void GetGoodsIssueSheet()
        {
            string queryString;

            queryString = " @GoodsIssueID Int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      GoodsIssues.GoodsIssueID, GoodsIssues.EntryDate, GoodsIssues.Reference, GoodsIssues.GoodsIssueTypeID, GoodsIssueTypes.Name AS GoodsIssueTypeName, GoodsIssues.PrimaryReferences, GoodsIssues.VoucherCodes, GoodsIssues.LocationID, Locations.OfficialName AS LocationName, GoodsIssues.ForkliftDriverID, Storekeepers.Name AS StorekeeperName, GoodsIssues.StorekeeperID, ForkliftDrivers.Name AS ForkliftDriverName, " + "\r\n";
            queryString = queryString + "                   GoodsIssues.CustomerID, Customers.Code AS CustomerCode, Customers.OfficialName AS CustomerOfficialName, GoodsIssues.ReceiverID, Receivers.Code AS ReceiverCode, GoodsIssues.WarehouseReceiptID, WarehouseReceipts.Name AS WarehouseReceiptName, GoodsIssues.Vehicle, GoodsIssues.VehicleDriver, GoodsIssues.CreatedDate AS LoadingStart, GoodsIssues.ApprovedDate AS LoadingCompletion, GoodsIssues.Description, GoodsIssues.Remarks, " + "\r\n";
            queryString = queryString + "                   GoodsIssueDetails.WarehouseID, GoodsIssueDetails.CommodityID, Commodities.Code AS CommodityCode, Commodities.Name AS CommodityName, Commodities.OfficialName AS CommodityOfficialName, Commodities.PackageSize, 1 AS PackageQuantity, Commodities.PackageVolume, GoodsIssueDetails.BatchID, Batches.Code AS BatchCode, Batches.EntryDate AS BatchEntryDate, GoodsIssueDetails.BinLocationID, BinLocations.Code AS BinLocationCode, GoodsIssueDetails.PalletID, Pallets.Code AS PalletCode, GoodsIssueDetails.CartonID, Cartons.Code AS CartonCode, PalletCartons.Code AS PalletCartonCode, " + "\r\n";
            queryString = queryString + "                   CASE WHEN NOT GoodsIssueDetails.CartonID IS NULL THEN Cartons.Code ELSE Pallets.Code END AS LineBarcode, CASE WHEN NOT GoodsIssueDetails.CartonID IS NULL THEN Cartons.Code ELSE PalletCartons.Code END AS AllCartonCode, GoodsIssueDetails.Quantity, GoodsIssueDetails.LineVolume, GoodsIssues.TotalQuantity, GoodsIssues.TotalLineVolume " + "\r\n";
            queryString = queryString + "       FROM        GoodsIssues " + "\r\n";
            queryString = queryString + "                   INNER JOIN GoodsIssueTypes ON GoodsIssues.GoodsIssueID = @GoodsIssueID AND GoodsIssues.GoodsIssueTypeID = GoodsIssueTypes.GoodsIssueTypeID " + "\r\n";
            queryString = queryString + "                   INNER JOIN Locations ON GoodsIssues.LocationID = Locations.LocationID " + "\r\n";
            queryString = queryString + "                   INNER JOIN Employees AS ForkliftDrivers ON ForkliftDrivers.EmployeeID = GoodsIssues.ForkliftDriverID " + "\r\n";
            queryString = queryString + "                   INNER JOIN Employees AS Storekeepers ON GoodsIssues.StorekeeperID = Storekeepers.EmployeeID " + "\r\n";
            queryString = queryString + "                   INNER JOIN GoodsIssueDetails ON GoodsIssues.GoodsIssueID = GoodsIssueDetails.GoodsIssueID " + "\r\n";
            queryString = queryString + "                   INNER JOIN Batches ON GoodsIssueDetails.BatchID = Batches.BatchID " + "\r\n";
            queryString = queryString + "                   INNER JOIN Commodities ON GoodsIssueDetails.CommodityID = Commodities.CommodityID " + "\r\n";
            queryString = queryString + "                   INNER JOIN BinLocations ON GoodsIssueDetails.BinLocationID = BinLocations.BinLocationID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Pallets ON GoodsIssueDetails.PalletID = Pallets.PalletID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Cartons PalletCartons ON Pallets.PalletID = PalletCartons.PalletID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Cartons ON GoodsIssueDetails.CartonID = Cartons.CartonID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Customers ON GoodsIssues.CustomerID = Customers.CustomerID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Customers AS Receivers ON GoodsIssues.ReceiverID = Receivers.CustomerID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Warehouses WarehouseReceipts ON GoodsIssues.WarehouseReceiptID = WarehouseReceipts.WarehouseID " + "\r\n";
            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetGoodsIssueSheet", queryString);
        }


        #endregion





        #region xyz


        private class GenerateSQLPendingDetails
        {
            private readonly string functionName;

            private readonly string primaryTableDetails;

            private readonly string primaryKey;
            private readonly string paraPrimaryKey;

            private readonly string primaryKeyDetail;
            private readonly string paraPrimaryKeyDetails;

            private readonly string filterKey;
            private readonly string paraFilterKey;

            private readonly bool isReceiverID;
            private readonly bool isWarehouse;

            private readonly string primaryReference;
            private readonly string primaryEntryDate;

            private readonly TotalSmartCodingEntities totalSmartCodingEntities;

            public GenerateSQLPendingDetails(TotalSmartCodingEntities totalSmartCodingEntities, string functionName, string primaryTableDetails, string primaryKey, string paraPrimaryKey, string primaryKeyDetails, string paraPrimaryKeyDetails, string filterKey, string paraFilterKey, bool isReceiverID, bool isWarehouse, string primaryReference, string primaryEntryDate)
            {
                this.totalSmartCodingEntities = totalSmartCodingEntities;

                this.functionName = functionName;
                this.primaryTableDetails = primaryTableDetails;

                this.primaryKey = primaryKey;
                this.paraPrimaryKey = paraPrimaryKey;

                this.filterKey = filterKey;
                this.paraFilterKey = paraFilterKey;

                this.primaryKeyDetail = primaryKeyDetails;
                this.paraPrimaryKeyDetails = paraPrimaryKeyDetails;

                this.isReceiverID = isReceiverID;
                this.isWarehouse = isWarehouse;

                this.primaryReference = primaryReference;
                this.primaryEntryDate = primaryEntryDate;
            }


            private bool alwaysTrue = true;
            //HERE: WE ALWAYS AND ONLY CALL GetPendingDeliveryAdviceDetails AFTER SAVE SUCCESSFUL
            //AT GoodsIssues VIEWS: WE DON'T ALLOW TO USE CURRENT RESULT FROM GetPendingDeliveryAdviceDetails IF THE LAST SAVE IS NOT SUCCESSFULLY. WHEN SAVE SUCCESSFUL, THE GetPendingDeliveryAdviceDetails IS CALL IMMEDIATELY
            //SO => HERE: WE DON'T CARE BOTH: @DeliveryAdviceDetailIDs AND BuildSQLEdit. THIS IS THE REASON WHY WE USE: alwaysTrue = true WHEN BULD SQL QUERY
            public void GetPendingDeliveryAdviceDetails()
            {
                string queryString;

                queryString = " @LocationID Int, @GoodsIssueID Int, " + (this.isWarehouse ? "@WarehouseID Int, " : "") + this.paraPrimaryKey + " Int, " + this.paraFilterKey + " Int, " + (this.isReceiverID ? "@ReceiverID Int, " : "") + this.paraPrimaryKeyDetails + " varchar(3999), @IsReadonly bit " + "\r\n";
                queryString = queryString + " WITH ENCRYPTION " + "\r\n";
                queryString = queryString + " AS " + "\r\n";

                queryString = queryString + "   BEGIN " + "\r\n";

                queryString = queryString + "       IF  (" + this.paraPrimaryKey + " <> 0) " + "\r\n";
                queryString = queryString + "           " + this.BuildSQLDeliveryAdvice(true) + "\r\n";
                queryString = queryString + "       ELSE " + "\r\n";
                queryString = queryString + "           " + this.BuildSQLDeliveryAdvice(false) + "\r\n";

                queryString = queryString + "   END " + "\r\n";

                this.totalSmartCodingEntities.CreateStoredProcedure(functionName, queryString);
            }

            private string BuildSQLDeliveryAdvice(bool isDeliveryAdviceID)
            {
                string queryString = "";
                if (this.alwaysTrue)
                    queryString = queryString + "           " + this.BuildSQLDeliveryAdviceDeliveryAdviceDetailIDs(isDeliveryAdviceID, false) + "\r\n";
                else
                {
                    queryString = queryString + "   BEGIN " + "\r\n";
                    queryString = queryString + "       IF  (" + this.paraPrimaryKeyDetails + " <> '') " + "\r\n";
                    queryString = queryString + "           " + this.BuildSQLDeliveryAdviceDeliveryAdviceDetailIDs(isDeliveryAdviceID, true) + "\r\n";
                    queryString = queryString + "       ELSE " + "\r\n";
                    queryString = queryString + "           " + this.BuildSQLDeliveryAdviceDeliveryAdviceDetailIDs(isDeliveryAdviceID, false) + "\r\n";
                    queryString = queryString + "   END " + "\r\n";
                }
                return queryString;
            }

            private string BuildSQLDeliveryAdviceDeliveryAdviceDetailIDs(bool isDeliveryAdviceID, bool isDeliveryAdviceDetailIDs)
            {
                string queryString = "";
                queryString = queryString + "   BEGIN " + "\r\n";

                if (this.alwaysTrue)
                {
                    queryString = queryString + "               BEGIN " + "\r\n";
                    queryString = queryString + "                   " + this.BuildSQLNew(isDeliveryAdviceID, isDeliveryAdviceDetailIDs) + "\r\n";
                    queryString = queryString + "                   ORDER BY " + this.primaryTableDetails + ".EntryDate, " + this.primaryTableDetails + "." + this.primaryKey + ", " + this.primaryTableDetails + "." + this.primaryKeyDetail + " " + "\r\n";
                    queryString = queryString + "               END " + "\r\n";
                }
                else
                {
                    queryString = queryString + "       IF (@GoodsIssueID <= 0) " + "\r\n";
                    queryString = queryString + "               BEGIN " + "\r\n";
                    queryString = queryString + "                   " + this.BuildSQLNew(isDeliveryAdviceID, isDeliveryAdviceDetailIDs) + "\r\n";
                    queryString = queryString + "                   ORDER BY " + this.primaryTableDetails + ".EntryDate, " + this.primaryTableDetails + "." + this.primaryKey + ", " + this.primaryTableDetails + "." + this.primaryKeyDetail + " " + "\r\n";
                    queryString = queryString + "               END " + "\r\n";
                    queryString = queryString + "       ELSE " + "\r\n";

                    queryString = queryString + "               IF (@IsReadonly = 1) " + "\r\n";
                    queryString = queryString + "                   BEGIN " + "\r\n";
                    queryString = queryString + "                       " + this.BuildSQLEdit(isDeliveryAdviceID, isDeliveryAdviceDetailIDs) + "\r\n";
                    queryString = queryString + "                       ORDER BY " + this.primaryTableDetails + ".EntryDate, " + this.primaryTableDetails + "." + this.primaryKey + ", " + this.primaryTableDetails + "." + this.primaryKeyDetail + " " + "\r\n";
                    queryString = queryString + "                   END " + "\r\n";

                    queryString = queryString + "               ELSE " + "\r\n"; //FULL SELECT FOR EDIT MODE

                    queryString = queryString + "                   BEGIN " + "\r\n";
                    queryString = queryString + "                       " + this.BuildSQLNew(isDeliveryAdviceID, isDeliveryAdviceDetailIDs) + " WHERE " + this.primaryTableDetails + "." + this.primaryKeyDetail + " NOT IN (SELECT " + this.primaryKeyDetail + " FROM GoodsIssueDetails WHERE GoodsIssueID = @GoodsIssueID) " + "\r\n";
                    queryString = queryString + "                       UNION ALL " + "\r\n";
                    queryString = queryString + "                       " + this.BuildSQLEdit(isDeliveryAdviceID, isDeliveryAdviceDetailIDs) + "\r\n";
                    queryString = queryString + "                       ORDER BY " + this.primaryTableDetails + ".EntryDate, " + this.primaryTableDetails + "." + this.primaryKey + ", " + this.primaryTableDetails + "." + this.primaryKeyDetail + " " + "\r\n";
                    queryString = queryString + "                   END " + "\r\n";
                }

                queryString = queryString + "   END " + "\r\n";

                return queryString;
            }

            private string BuildSQLNew(bool isDeliveryAdviceID, bool isDeliveryAdviceDetailIDs)
            {
                string queryString = "";

                queryString = queryString + "       SELECT      " + this.primaryTableDetails + ".LocationID, " + this.primaryTableDetails + "." + this.primaryKey + ", " + this.primaryTableDetails + "." + this.primaryKeyDetail + ", " + this.primaryTableDetails + ".Reference AS " + this.primaryReference + ", " + this.primaryTableDetails + ".EntryDate AS " + this.primaryEntryDate + ", " + "\r\n";
                queryString = queryString + "                   Commodities.CommodityID, Commodities.Code AS CommodityCode, Commodities.Name AS CommodityName, Commodities.PackageSize, Commodities.Volume, Commodities.PackageVolume, " + this.primaryTableDetails + ".BatchID, Batches.Code AS BatchCode, " + "\r\n";
                queryString = queryString + "                   ROUND(" + this.primaryTableDetails + ".Quantity - " + this.primaryTableDetails + ".QuantityIssue,  " + (int)GlobalEnums.rndQuantity + ") AS QuantityRemains, CAST(0 AS decimal(18, 2)) AS Quantity, ROUND(" + this.primaryTableDetails + ".LineVolume - " + this.primaryTableDetails + ".LineVolumeIssue,  " + (int)GlobalEnums.rndVolume + ") AS LineVolumeRemains, CAST(0 AS decimal(18, 2)) AS LineVolume, " + this.primaryTableDetails + ".Remarks, CAST(0 AS bit) AS IsSelected " + "\r\n";

                queryString = queryString + "       FROM        " + this.primaryTableDetails + " " + "\r\n";
                queryString = queryString + "                   INNER JOIN Commodities ON " + (isDeliveryAdviceID ? " " + this.primaryTableDetails + "." + this.primaryKey + " = " + this.paraPrimaryKey + " " : this.primaryTableDetails + ".LocationID = @LocationID " + (this.isWarehouse ? " AND " + this.primaryTableDetails + ".WarehouseID = @WarehouseID " : "") + " AND " + this.primaryTableDetails + "." + this.filterKey + " = " + this.paraFilterKey + (this.isReceiverID ? " AND " + this.primaryTableDetails + ".ReceiverID = @ReceiverID " : "")) + " AND " + this.primaryTableDetails + ".Approved = 1 AND " + this.primaryTableDetails + ".InActive = 0 AND ROUND(" + this.primaryTableDetails + ".Quantity - " + this.primaryTableDetails + ".QuantityIssue, " + (int)GlobalEnums.rndQuantity + ") > 0 AND " + this.primaryTableDetails + ".CommodityID = Commodities.CommodityID" + (isDeliveryAdviceDetailIDs ? " AND " + this.primaryTableDetails + "." + this.primaryKeyDetail + " NOT IN (SELECT Id FROM dbo.SplitToIntList (" + this.paraPrimaryKeyDetails + "))" : "") + "\r\n";
                queryString = queryString + "                   LEFT JOIN Batches ON " + this.primaryTableDetails + ".BatchID = Batches.BatchID " + "\r\n";
                return queryString;
            }

            private string BuildSQLEdit(bool isDeliveryAdviceID, bool isDeliveryAdviceDetailIDs)
            {
                string queryString = "";

                queryString = queryString + "       SELECT      " + this.primaryTableDetails + ".LocationID, " + this.primaryTableDetails + "." + this.primaryKey + ", " + this.primaryTableDetails + "." + this.primaryKeyDetail + ", " + this.primaryTableDetails + ".Reference AS " + this.primaryReference + ", " + this.primaryTableDetails + ".EntryDate AS " + this.primaryEntryDate + ", " + "\r\n";
                queryString = queryString + "                   Commodities.CommodityID, Commodities.Code AS CommodityCode, Commodities.Name AS CommodityName, Commodities.PackageSize, Commodities.Volume, Commodities.PackageVolume, " + this.primaryTableDetails + ".BatchID, Batches.Code AS BatchCode, " + "\r\n";
                queryString = queryString + "                   ROUND(" + this.primaryTableDetails + ".Quantity - " + this.primaryTableDetails + ".QuantityIssue + GoodsIssueDetails.Quantity,  " + (int)GlobalEnums.rndQuantity + ") AS QuantityRemains, CAST(0 AS decimal(18, 2)) AS Quantity, ROUND(" + this.primaryTableDetails + ".LineVolume - " + this.primaryTableDetails + ".LineVolumeIssue + GoodsIssueDetails.LineVolume,  " + (int)GlobalEnums.rndVolume + ") AS LineVolumeRemains, CAST(0 AS decimal(18, 2)) AS LineVolume, " + this.primaryTableDetails + ".Remarks, CAST(0 AS bit) AS IsSelected " + "\r\n";

                queryString = queryString + "       FROM        " + this.primaryTableDetails + " " + "\r\n";
                queryString = queryString + "                   INNER JOIN GoodsIssueDetails ON GoodsIssueDetails.GoodsIssueID = @GoodsIssueID AND " + this.primaryTableDetails + "." + this.primaryKeyDetail + " = GoodsIssueDetails." + this.primaryKeyDetail + "" + (isDeliveryAdviceDetailIDs ? " AND " + this.primaryTableDetails + "." + this.primaryKeyDetail + " NOT IN (SELECT Id FROM dbo.SplitToIntList (" + this.paraPrimaryKeyDetails + "))" : "") + "\r\n";
                queryString = queryString + "                   INNER JOIN Commodities ON " + this.primaryTableDetails + ".CommodityID = Commodities.CommodityID " + "\r\n";
                queryString = queryString + "                   LEFT JOIN Batches ON " + this.primaryTableDetails + ".BatchID = Batches.BatchID " + "\r\n";

                return queryString;
            }
        }


        #endregion xyz

    }
}
