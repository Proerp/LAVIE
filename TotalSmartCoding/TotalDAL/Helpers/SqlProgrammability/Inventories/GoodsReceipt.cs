using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Inventories
{
    public class GoodsReceipt
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public GoodsReceipt(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetGoodsReceiptIndexes();


            this.GetGoodsReceiptViewDetails();


            GenerateSQLPendingDetails generatePendingPickupDetails = new GenerateSQLPendingDetails(totalSmartCodingEntities, GlobalEnums.GoodsReceiptTypeID.Pickup, "Pickups", "PickupDetails", "PickupID", "@PickupID", "PickupDetailID", "@PickupDetailIDs", "WarehouseID", "PrimaryReference", "PrimaryEntryDate");
            generatePendingPickupDetails.GetPendingPickups("GetPendingPickups");
            generatePendingPickupDetails.GetPendingPickupWarehouses("GetPendingPickupWarehouses");
            generatePendingPickupDetails.GetPendingPickupDetails("GetPendingPickupDetails");

            GenerateSQLPendingDetails generatePendingGoodsIssueTransferDetails = new GenerateSQLPendingDetails(totalSmartCodingEntities, GlobalEnums.GoodsReceiptTypeID.GoodsIssueTransfer, "GoodsIssues", "GoodsIssueTransferDetails", "GoodsIssueID", "@GoodsIssueID", "GoodsIssueTransferDetailID", "@GoodsIssueTransferDetailIDs", "WarehouseReceiptID", "PrimaryReference", "PrimaryEntryDate");
            generatePendingGoodsIssueTransferDetails.GetPendingPickups("GetPendingGoodsIssueTransfers");
            generatePendingGoodsIssueTransferDetails.GetPendingPickupWarehouses("GetPendingGoodsIssueTransferWarehouses");
            generatePendingGoodsIssueTransferDetails.GetPendingPickupDetails("GetPendingGoodsIssueTransferDetails");

            GenerateSQLPendingDetails generatePendingWarehouseAdjustmentDetails = new GenerateSQLPendingDetails(totalSmartCodingEntities, GlobalEnums.GoodsReceiptTypeID.WarehouseAdjustments, "WarehouseAdjustments", "WarehouseAdjustmentDetails", "WarehouseAdjustmentID", "@WarehouseAdjustmentID", "WarehouseAdjustmentDetailID", "@WarehouseAdjustmentDetailIDs", "WarehouseID", "PrimaryReference", "PrimaryEntryDate");
            generatePendingWarehouseAdjustmentDetails.GetPendingPickupDetails("GetPendingWarehouseAdjustmentDetails");

            this.GetGoodsReceiptIDofWarehouseAdjustment();

            this.GoodsReceiptSaveRelative();
            this.GoodsReceiptPostSaveValidate();

            this.GoodsReceiptApproved();
            this.GoodsReceiptEditable();

            this.GoodsReceiptToggleApproved();

            this.GoodsReceiptInitReference();
            this.GetGoodsReceiptSheet();

            this.GetGoodsReceiptDetailAvailables();
        }


        private void GetGoodsReceiptIndexes()
        {
            string queryString;

            queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      GoodsReceipts.GoodsReceiptID, CAST(GoodsReceipts.EntryDate AS DATE) AS EntryDate, GoodsReceipts.Reference, GoodsReceiptTypes.Code AS GoodsReceiptTypeCode, GoodsReceipts.PrimaryReferences, Locations.Code AS LocationCode, Warehouses.Name AS WarehouseName, GoodsReceipts.Description, GoodsReceipts.TotalQuantity, GoodsReceipts.TotalLineVolume, GoodsReceipts.Approved " + "\r\n";
            queryString = queryString + "       FROM        GoodsReceipts " + "\r\n";
            queryString = queryString + "                   INNER JOIN Locations ON GoodsReceipts.EntryDate >= @FromDate AND GoodsReceipts.EntryDate <= @ToDate AND GoodsReceipts.OrganizationalUnitID IN (SELECT OrganizationalUnitID FROM AccessControls WHERE UserID = @UserID AND NMVNTaskID = " + (int)TotalBase.Enums.GlobalEnums.NmvnTaskID.GoodsReceipt + " AND AccessControls.AccessLevel > 0) AND Locations.LocationID = GoodsReceipts.LocationID " + "\r\n";
            queryString = queryString + "                   INNER JOIN Warehouses ON GoodsReceipts.WarehouseID = Warehouses.WarehouseID " + "\r\n";
            queryString = queryString + "                   INNER JOIN GoodsReceiptTypes ON GoodsReceipts.GoodsReceiptTypeID = GoodsReceiptTypes.GoodsReceiptTypeID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetGoodsReceiptIndexes", queryString);
        }


        #region X


        private void GetGoodsReceiptViewDetails()
        {
            string queryString;

            queryString = " @GoodsReceiptID Int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      GoodsReceiptDetails.GoodsReceiptDetailID, GoodsReceiptDetails.GoodsReceiptID, GoodsReceiptDetails.PickupID, GoodsReceiptDetails.PickupDetailID, PickupDetails.Reference AS PickupReference, PickupDetails.EntryDate AS PickupEntryDate, " + "\r\n";
            queryString = queryString + "                   GoodsReceiptDetails.GoodsIssueID, GoodsReceiptDetails.GoodsIssueTransferDetailID, GoodsIssueTransferDetails.Reference AS GoodsIssueReference, GoodsIssueTransferDetails.EntryDate AS GoodsIssueEntryDate, GoodsReceiptDetails.WarehouseAdjustmentID, GoodsReceiptDetails.WarehouseAdjustmentDetailID, WarehouseAdjustmentDetails.Reference AS WarehouseAdjustmentReference, WarehouseAdjustmentDetails.EntryDate AS WarehouseAdjustmentEntryDate, " + "\r\n";
            queryString = queryString + "                   Commodities.CommodityID, Commodities.Code AS CommodityCode, Commodities.Name AS CommodityName, GoodsReceiptDetails.BatchID, GoodsReceiptDetails.BatchEntryDate, GoodsReceiptDetails.BinLocationID, BinLocations.Code AS BinLocationCode, " + "\r\n";
            queryString = queryString + "                   GoodsReceiptDetails.PackID, Packs.Code AS PackCode, GoodsReceiptDetails.CartonID, Cartons.Code AS CartonCode, GoodsReceiptDetails.PalletID, Pallets.Code AS PalletCode, " + "\r\n";
            queryString = queryString + "                   GoodsReceiptDetails.Quantity, GoodsReceiptDetails.LineVolume, GoodsReceiptDetails.PackCounts, GoodsReceiptDetails.CartonCounts, GoodsReceiptDetails.PalletCounts, GoodsReceiptDetails.Remarks " + "\r\n";

            queryString = queryString + "       FROM        GoodsReceiptDetails " + "\r\n";
            queryString = queryString + "                   INNER JOIN Commodities ON GoodsReceiptDetails.GoodsReceiptID = @GoodsReceiptID AND GoodsReceiptDetails.CommodityID = Commodities.CommodityID " + "\r\n";
            queryString = queryString + "                   INNER JOIN BinLocations ON GoodsReceiptDetails.BinLocationID = BinLocations.BinLocationID " + "\r\n";

            queryString = queryString + "                   LEFT JOIN PickupDetails ON GoodsReceiptDetails.PickupDetailID = PickupDetails.PickupDetailID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN GoodsIssueTransferDetails ON GoodsReceiptDetails.GoodsIssueTransferDetailID = GoodsIssueTransferDetails.GoodsIssueTransferDetailID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN WarehouseAdjustmentDetails ON GoodsReceiptDetails.WarehouseAdjustmentDetailID = WarehouseAdjustmentDetails.WarehouseAdjustmentDetailID " + "\r\n";

            queryString = queryString + "                   LEFT JOIN Packs ON GoodsReceiptDetails.PackID = Packs.PackID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Cartons ON GoodsReceiptDetails.CartonID = Cartons.CartonID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Pallets ON GoodsReceiptDetails.PalletID = Pallets.PalletID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetGoodsReceiptViewDetails", queryString);
        }


        private void GoodsReceiptSaveRelative()
        {
            string queryString = " @EntityID int, @SaveRelativeOption int " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       BEGIN " + "\r\n";

            queryString = queryString + "           IF (@SaveRelativeOption = 1) ";
            queryString = queryString + "               BEGIN ";
            queryString = queryString + "                   UPDATE          GoodsReceiptDetails " + "\r\n";
            queryString = queryString + "                   SET             GoodsReceiptDetails.Reference = GoodsReceipts.Reference " + "\r\n";
            queryString = queryString + "                   FROM            GoodsReceipts INNER JOIN GoodsReceiptDetails ON GoodsReceipts.GoodsReceiptID = @EntityID AND GoodsReceipts.GoodsReceiptID = GoodsReceiptDetails.GoodsReceiptID " + "\r\n";
            queryString = queryString + "               END ";

            queryString = queryString + "           DECLARE @GoodsReceiptTypeID int, @AffectedROWCOUNT int ";
            queryString = queryString + "           SELECT @GoodsReceiptTypeID = GoodsReceiptTypeID FROM GoodsReceipts WHERE GoodsReceiptID = @EntityID ";
            queryString = queryString + "           IF (@GoodsReceiptTypeID > 0) " + "\r\n";
            queryString = queryString + "               BEGIN  " + "\r\n";

            queryString = queryString + "                   IF (@GoodsReceiptTypeID = " + (int)GlobalEnums.GoodsReceiptTypeID.Pickup + ") " + "\r\n";
            queryString = queryString + "                       BEGIN  " + "\r\n";
            queryString = queryString + "                           UPDATE          PickupDetails " + "\r\n";
            queryString = queryString + "                           SET             PickupDetails.QuantityReceipt = ROUND(PickupDetails.QuantityReceipt + GoodsReceiptDetails.Quantity * @SaveRelativeOption, " + (int)GlobalEnums.rndQuantity + "), PickupDetails.LineVolumeReceipt = ROUND(PickupDetails.LineVolumeReceipt + GoodsReceiptDetails.LineVolume * @SaveRelativeOption, " + (int)GlobalEnums.rndVolume + ") " + "\r\n";
            queryString = queryString + "                           FROM            GoodsReceiptDetails " + "\r\n";
            queryString = queryString + "                                           INNER JOIN PickupDetails ON GoodsReceiptDetails.GoodsReceiptID = @EntityID AND PickupDetails.Approved = 1 AND GoodsReceiptDetails.PickupDetailID = PickupDetails.PickupDetailID " + "\r\n";
            queryString = queryString + "                           SET @AffectedROWCOUNT = @@ROWCOUNT " + "\r\n";
            queryString = queryString + "                       END " + "\r\n";


            queryString = queryString + "                   IF (@GoodsReceiptTypeID = " + (int)GlobalEnums.GoodsReceiptTypeID.GoodsIssueTransfer + ") " + "\r\n";
            queryString = queryString + "                       BEGIN  " + "\r\n";
            queryString = queryString + "                           UPDATE          GoodsIssueTransferDetails " + "\r\n";
            queryString = queryString + "                           SET             GoodsIssueTransferDetails.QuantityReceipt = ROUND(GoodsIssueTransferDetails.QuantityReceipt + GoodsReceiptDetails.Quantity * @SaveRelativeOption, " + (int)GlobalEnums.rndQuantity + "), GoodsIssueTransferDetails.LineVolumeReceipt = ROUND(GoodsIssueTransferDetails.LineVolumeReceipt + GoodsReceiptDetails.LineVolume * @SaveRelativeOption, " + (int)GlobalEnums.rndVolume + ") " + "\r\n";
            queryString = queryString + "                           FROM            GoodsReceiptDetails " + "\r\n";
            queryString = queryString + "                                           INNER JOIN GoodsIssueTransferDetails ON GoodsReceiptDetails.GoodsReceiptID = @EntityID AND GoodsIssueTransferDetails.Approved = 1 AND GoodsReceiptDetails.GoodsIssueTransferDetailID = GoodsIssueTransferDetails.GoodsIssueTransferDetailID " + "\r\n";
            queryString = queryString + "                           SET @AffectedROWCOUNT = @@ROWCOUNT " + "\r\n";
            queryString = queryString + "                       END " + "\r\n";


            queryString = queryString + "                   IF (@GoodsReceiptTypeID = " + (int)GlobalEnums.GoodsReceiptTypeID.WarehouseAdjustments + ") " + "\r\n";
            queryString = queryString + "                       BEGIN  " + "\r\n";
            queryString = queryString + "                           UPDATE          WarehouseAdjustmentDetails " + "\r\n";
            queryString = queryString + "                           SET             WarehouseAdjustmentDetails.QuantityReceipt = ROUND(WarehouseAdjustmentDetails.QuantityReceipt + GoodsReceiptDetails.Quantity * @SaveRelativeOption, " + (int)GlobalEnums.rndQuantity + "), WarehouseAdjustmentDetails.LineVolumeReceipt = ROUND(WarehouseAdjustmentDetails.LineVolumeReceipt + GoodsReceiptDetails.LineVolume * @SaveRelativeOption, " + (int)GlobalEnums.rndVolume + ") " + "\r\n";
            queryString = queryString + "                           FROM            GoodsReceiptDetails " + "\r\n";
            queryString = queryString + "                                           INNER JOIN WarehouseAdjustmentDetails ON GoodsReceiptDetails.GoodsReceiptID = @EntityID AND WarehouseAdjustmentDetails.Quantity > 0 AND GoodsReceiptDetails.WarehouseAdjustmentDetailID = WarehouseAdjustmentDetails.WarehouseAdjustmentDetailID " + "\r\n";
            queryString = queryString + "                           SET @AffectedROWCOUNT = @@ROWCOUNT " + "\r\n";
            //------------------------------------------------------SHOULD UPDATE GoodsReceiptID, GoodsReceiptDetailID BACK TO WarehouseAdjustmentDetails FOR GoodsReceipts OF WarehouseAdjustmentDetails? THE ANSWER: WE CAN DO IT HERE, BUT IT BREAK THE RELATIONSHIP (cyclic redundancy relationship: GoodsReceiptDetails => WarehouseAdjustmentDetails => THUS: WE SHOULD NOT MAKE ANOTHER RELATIONSHIP FROM WarehouseAdjustmentDetails => GoodsReceiptDetails ) => SO: SHOULD NOT!!!
            queryString = queryString + "                       END " + "\r\n";

            queryString = queryString + "                   IF @AffectedROWCOUNT <> (SELECT COUNT(*) FROM GoodsReceiptDetails WHERE GoodsReceiptID = @EntityID) " + "\r\n";
            queryString = queryString + "                       BEGIN " + "\r\n";
            queryString = queryString + "                           DECLARE     @msg NVARCHAR(300) = N'Phiếu giao hàng đã hủy, hoặc chưa duyệt' ; " + "\r\n";
            queryString = queryString + "                           THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "                       END " + "\r\n";

            queryString = queryString + "               END  " + "\r\n";

            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GoodsReceiptSaveRelative", queryString);
        }

        private void GoodsReceiptPostSaveValidate()
        {
            string[] queryArray = new string[4];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = N'Ngày giao hàng: ' + CAST(Pickups.EntryDate AS nvarchar) FROM GoodsReceiptDetails INNER JOIN Pickups ON GoodsReceiptDetails.GoodsReceiptID = @EntityID AND GoodsReceiptDetails.PickupID = Pickups.PickupID AND GoodsReceiptDetails.EntryDate < Pickups.EntryDate ";
            queryArray[1] = " SELECT TOP 1 @FoundEntity = N'Ngày chuyển kho: ' + CAST(GoodsIssues.EntryDate AS nvarchar) FROM GoodsReceiptDetails INNER JOIN GoodsIssues ON GoodsReceiptDetails.GoodsReceiptID = @EntityID AND GoodsReceiptDetails.GoodsIssueID = GoodsIssues.GoodsIssueID AND GoodsReceiptDetails.EntryDate < GoodsIssues.EntryDate ";
            queryArray[2] = " SELECT TOP 1 @FoundEntity = N'Số lượng nhập kho vượt quá số lượng giao: ' + CAST(ROUND(Quantity - QuantityReceipt, " + (int)GlobalEnums.rndQuantity + ") AS nvarchar) FROM PickupDetails WHERE (ROUND(Quantity - QuantityReceipt, " + (int)GlobalEnums.rndQuantity + ") < 0) ";
            queryArray[3] = " SELECT TOP 1 @FoundEntity = N'Số lượng nhập kho vượt quá số lượng giao: ' + CAST(ROUND(Quantity - QuantityReceipt, " + (int)GlobalEnums.rndQuantity + ") AS nvarchar) FROM GoodsIssueTransferDetails WHERE (ROUND(Quantity - QuantityReceipt, " + (int)GlobalEnums.rndQuantity + ") < 0) ";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("GoodsReceiptPostSaveValidate", queryArray);
        }




        private void GoodsReceiptApproved()
        {
            string[] queryArray = new string[1];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = GoodsReceiptID FROM GoodsReceipts WHERE GoodsReceiptID = @EntityID AND Approved = 1";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("GoodsReceiptApproved", queryArray);
        }


        private void GoodsReceiptEditable()
        {
            string[] queryArray = new string[2]; //IMPORTANT: THESE QUERIES SHOULD BE COPIED TO WarehouseAdjustmentEditable

            queryArray[0] = " SELECT TOP 1 @FoundEntity = GoodsReceiptID FROM GoodsIssueDetails WHERE GoodsReceiptID = @EntityID ";
            queryArray[1] = " SELECT TOP 1 @FoundEntity = GoodsReceiptID FROM WarehouseAdjustmentDetails WHERE GoodsReceiptID = @EntityID ";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("GoodsReceiptEditable", queryArray);
        }

        private void GoodsReceiptToggleApproved()
        {
            string queryString = " @EntityID int, @Approved bit " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       UPDATE      GoodsReceipts  SET Approved = @Approved, ApprovedDate = GetDate() WHERE GoodsReceiptID = @EntityID AND Approved = ~@Approved" + "\r\n";

            queryString = queryString + "       IF @@ROWCOUNT = 1 " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";
            queryString = queryString + "               UPDATE          GoodsReceiptDetails  SET Approved = @Approved WHERE GoodsReceiptID = @EntityID ; " + "\r\n";
            queryString = queryString + "           END " + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";
            queryString = queryString + "               DECLARE     @msg NVARCHAR(300) = N'Dữ liệu không tồn tại hoặc đã ' + iif(@Approved = 0, 'hủy', '')  + ' duyệt' ; " + "\r\n";
            queryString = queryString + "               THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "           END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GoodsReceiptToggleApproved", queryString);
        }

        private void GoodsReceiptInitReference()
        {
            SimpleInitReference simpleInitReference = new SimpleInitReference("GoodsReceipts", "GoodsReceiptID", "Reference", ModelSettingManager.ReferenceLength, ModelSettingManager.ReferencePrefix(GlobalEnums.NmvnTaskID.GoodsReceipt));
            this.totalSmartCodingEntities.CreateTrigger("GoodsReceiptInitReference", simpleInitReference.CreateQuery());
        }


        private void GetGoodsReceiptIDofWarehouseAdjustment()
        {
            string queryString;

            queryString = " @WarehouseAdjustmentID Int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "   SELECT TOP 1 GoodsReceiptID FROM GoodsReceipts WHERE WarehouseAdjustmentID = @WarehouseAdjustmentID " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetGoodsReceiptIDofWarehouseAdjustment", queryString);
        }

        private void GetGoodsReceiptDetailAvailables()
        {
            string queryString = " @LocationID Int, @WarehouseID Int, @CommodityID Int, @CommodityIDs varchar(3999), @BatchID Int, @GoodsReceiptDetailIDs varchar(3999), @OnlyIssuable bit " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "   BEGIN " + "\r\n";

            queryString = queryString + "       IF  (@WarehouseID <> 0) " + "\r\n";
            queryString = queryString + "           " + this.AvailableBuildSQLWarehouse(true) + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "           " + this.AvailableBuildSQLWarehouse(false) + "\r\n";

            queryString = queryString + "   END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetGoodsReceiptDetailAvailables", queryString);
        }

        private string AvailableBuildSQLWarehouse(bool isWarehouseID)
        {
            string queryString = "";
            queryString = queryString + "   BEGIN " + "\r\n";

            queryString = queryString + "       IF  (@CommodityID <> 0) " + "\r\n";
            queryString = queryString + "           " + this.AvailableBuildSQLCommmodity(isWarehouseID, true, false) + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";
            queryString = queryString + "               IF  (@CommodityIDs <> '') " + "\r\n";
            queryString = queryString + "                   " + this.AvailableBuildSQLCommmodity(isWarehouseID, false, true) + "\r\n";
            queryString = queryString + "               ELSE " + "\r\n";
            queryString = queryString + "                   " + this.AvailableBuildSQLCommmodity(isWarehouseID, false, false) + "\r\n";
            queryString = queryString + "           END " + "\r\n";
            queryString = queryString + "   END " + "\r\n";

            return queryString;
        }

        private string AvailableBuildSQLCommmodity(bool isWarehouseID, bool isCommodityID, bool isCommodityIDs)
        {
            string queryString = "";
            queryString = queryString + "   BEGIN " + "\r\n";
            queryString = queryString + "       IF  (NOT @BatchID IS NULL) " + "\r\n";
            queryString = queryString + "           " + this.AvailableBuildSQLCommmodityBatch(isWarehouseID, isCommodityID, isCommodityIDs, true) + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "           " + this.AvailableBuildSQLCommmodityBatch(isWarehouseID, isCommodityID, isCommodityIDs, false) + "\r\n";
            queryString = queryString + "   END " + "\r\n";

            return queryString;
        }

        private string AvailableBuildSQLCommmodityBatch(bool isWarehouseID, bool isCommodityID, bool isCommodityIDs, bool isBatchID)
        {
            string queryString = "";
            queryString = queryString + "   BEGIN " + "\r\n";
            queryString = queryString + "       IF  (@GoodsReceiptDetailIDs <> '') " + "\r\n";
            queryString = queryString + "           " + this.AvailableBuildSQLGoodsReceiptDetailIDs(isWarehouseID, isCommodityID, isCommodityIDs, isBatchID, true) + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "           " + this.AvailableBuildSQLGoodsReceiptDetailIDs(isWarehouseID, isCommodityID, isCommodityIDs, isBatchID, false) + "\r\n";
            queryString = queryString + "   END " + "\r\n";

            return queryString;
        }

        private string AvailableBuildSQLGoodsReceiptDetailIDs(bool isWarehouseID, bool isCommodityID, bool isCommodityIDs, bool isBatchID, bool isGoodsReceiptDetailIDs)
        {
            string queryString = "";
            queryString = queryString + "   BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      GoodsReceiptDetails.GoodsReceiptID, GoodsReceiptDetails.GoodsReceiptDetailID, GoodsReceiptDetails.Reference AS GoodsReceiptReference, GoodsReceiptDetails.EntryDate AS GoodsReceiptEntryDate, GoodsReceiptDetails.BatchID, GoodsReceiptDetails.BatchEntryDate, GoodsReceiptDetails.WarehouseID, Warehouses.Code AS WarehouseCode, Commodities.CommodityID, Commodities.Code AS CommodityCode, Commodities.Name AS CommodityName, Commodities.PackageSize, Commodities.Volume, Commodities.PackageVolume, GoodsReceiptDetails.BinLocationID, BinLocations.Code AS BinLocationCode, GoodsReceiptDetails.PackID, Packs.Code AS PackCode, GoodsReceiptDetails.CartonID, Cartons.Code AS CartonCode, GoodsReceiptDetails.PalletID, Pallets.Code AS PalletCode, GoodsReceiptDetails.Remarks, " + "\r\n";
            queryString = queryString + "                   ISNULL('Production: ' + ' ' + Pickups.Reference, ISNULL('From: ' + ' ' + SourceWarehouses.Name + ', ' + GoodsIssues.VoucherCodes, ISNULL(WarehouseAdjustmentTypes.Name  + ' ' + WarehouseAdjustments.AdjustmentJobs, ''))) AS LineReferences, " + "\r\n";
            queryString = queryString + "                   GoodsReceiptDetails.PackCounts, GoodsReceiptDetails.CartonCounts, GoodsReceiptDetails.PalletCounts, ROUND(GoodsReceiptDetails.Quantity - GoodsReceiptDetails.QuantityIssue, " + GlobalEnums.rndQuantity + ") AS QuantityAvailable, ROUND(GoodsReceiptDetails.LineVolume - GoodsReceiptDetails.LineVolumeIssue, " + GlobalEnums.rndVolume + ") AS LineVolumeAvailable, ISNULL(CAST(0 AS bit), CAST(0 AS bit)) AS IsSelected " + "\r\n";

            queryString = queryString + "       FROM        GoodsReceiptDetails " + "\r\n";
            queryString = queryString + "                   INNER JOIN Warehouses ON ROUND(GoodsReceiptDetails.Quantity - GoodsReceiptDetails.QuantityIssue, " + GlobalEnums.rndQuantity + ") > 0 AND GoodsReceiptDetails.LocationID = @LocationID " + (isWarehouseID ? " AND GoodsReceiptDetails.WarehouseID = @WarehouseID" : "") + (isCommodityID ? " AND GoodsReceiptDetails.CommodityID = @CommodityID" : "") + (isCommodityIDs ? " AND GoodsReceiptDetails.CommodityID IN (SELECT Id FROM dbo.SplitToIntList (@CommodityIDs))" : "") + " AND GoodsReceiptDetails.Approved = 1 AND (@OnlyIssuable = 0 OR Warehouses.Issuable = 1) AND GoodsReceiptDetails.WarehouseID = Warehouses.WarehouseID " + (isBatchID ? " AND GoodsReceiptDetails.BatchID = @BatchID" : "") + (isGoodsReceiptDetailIDs ? " AND GoodsReceiptDetails.GoodsReceiptDetailID NOT IN (SELECT Id FROM dbo.SplitToIntList (@GoodsReceiptDetailIDs))" : "") + "\r\n";
            queryString = queryString + "                   INNER JOIN Commodities ON GoodsReceiptDetails.CommodityID = Commodities.CommodityID " + "\r\n";
            queryString = queryString + "                   INNER JOIN BinLocations ON GoodsReceiptDetails.BinLocationID = BinLocations.BinLocationID " + "\r\n";

            queryString = queryString + "                   LEFT JOIN Pickups ON GoodsReceiptDetails.PickupID = Pickups.PickupID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN GoodsIssues ON GoodsReceiptDetails.GoodsIssueID = GoodsIssues.GoodsIssueID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Warehouses SourceWarehouses ON GoodsIssues.WarehouseID = SourceWarehouses.WarehouseID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN WarehouseAdjustments ON GoodsReceiptDetails.WarehouseAdjustmentID = WarehouseAdjustments.WarehouseAdjustmentID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN WarehouseAdjustmentTypes ON WarehouseAdjustments.WarehouseAdjustmentTypeID = WarehouseAdjustmentTypes.WarehouseAdjustmentTypeID " + "\r\n";

            queryString = queryString + "                   LEFT JOIN Packs ON GoodsReceiptDetails.PackID = Packs.PackID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Cartons ON GoodsReceiptDetails.CartonID = Cartons.CartonID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Pallets ON GoodsReceiptDetails.PalletID = Pallets.PalletID " + "\r\n";

            queryString = queryString + "       ORDER BY    GoodsReceiptDetails.BatchEntryDate, GoodsReceiptDetails.GoodsReceiptDetailID, BinLocations.Code " + "\r\n";

            queryString = queryString + "   END " + "\r\n";

            return queryString;
        }

        #endregion


        private void GetGoodsReceiptSheet()
        {
            string queryString;

            queryString = " @GoodsReceiptID Int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      GoodsReceipts.GoodsReceiptID, GoodsReceipts.EntryDate, GoodsReceipts.Reference, GoodsReceipts.GoodsReceiptTypeID, GoodsReceiptTypes.Name AS GoodsReceiptTypeName, GoodsReceipts.PrimaryReferences, GoodsReceipts.LocationID, Locations.OfficialName AS LocationName, GoodsReceipts.StorekeeperID, Storekeepers.Name AS StorekeeperName, GoodsReceipts.ForkliftDriverID, ForkliftDrivers.Name AS ForkliftDriverName, " + "\r\n";
            queryString = queryString + "                   GoodsReceipts.GoodsIssueID, GoodsIssues.Vehicle, GoodsReceipts.VehicleDriver, GoodsReceipts.CreatedDate AS LoadingStart, GoodsReceipts.ApprovedDate AS LoadingCompletion, GoodsReceipts.Description, GoodsReceipts.Remarks, " + "\r\n";
            queryString = queryString + "                   GoodsReceiptDetails.WarehouseID, GoodsReceiptDetails.CommodityID, Commodities.Code AS CommodityCode, Commodities.Name AS CommodityName, Commodities.OfficialName AS CommodityOfficialName, Commodities.PackageSize, 1 AS PackageQuantity, Commodities.PackageVolume, GoodsReceiptDetails.BatchID, Batches.Code AS BatchCode, Batches.EntryDate AS BatchEntryDate, GoodsReceiptDetails.BinLocationID, BinLocations.Code AS BinLocationCode, GoodsReceiptDetails.PalletID, Pallets.Code AS PalletCode, GoodsReceiptDetails.CartonID, Cartons.Code AS CartonCode, PalletCartons.Code AS PalletCartonCode, " + "\r\n";
            queryString = queryString + "                   CASE WHEN NOT GoodsReceiptDetails.CartonID IS NULL THEN Cartons.Code ELSE Pallets.Code END AS LineBarcode, CASE WHEN NOT GoodsReceiptDetails.CartonID IS NULL THEN Cartons.Code ELSE PalletCartons.Code END AS AllCartonCode, GoodsReceiptDetails.Quantity, GoodsReceiptDetails.LineVolume, GoodsReceipts.TotalQuantity, GoodsReceipts.TotalLineVolume " + "\r\n";
            queryString = queryString + "       FROM        GoodsReceipts " + "\r\n";
            queryString = queryString + "                   INNER JOIN GoodsReceiptTypes ON GoodsReceipts.GoodsReceiptID = @GoodsReceiptID AND GoodsReceipts.GoodsReceiptTypeID = GoodsReceiptTypes.GoodsReceiptTypeID " + "\r\n";
            queryString = queryString + "                   INNER JOIN Locations ON GoodsReceipts.LocationID = Locations.LocationID " + "\r\n";
            queryString = queryString + "                   INNER JOIN Employees AS Storekeepers ON GoodsReceipts.StorekeeperID = Storekeepers.EmployeeID " + "\r\n";
            queryString = queryString + "                   INNER JOIN Employees AS ForkliftDrivers ON GoodsReceipts.ForkliftDriverID = ForkliftDrivers.EmployeeID " + "\r\n";
            queryString = queryString + "                   INNER JOIN GoodsReceiptDetails ON GoodsReceipts.GoodsReceiptID = GoodsReceiptDetails.GoodsReceiptID " + "\r\n";
            queryString = queryString + "                   INNER JOIN Batches ON GoodsReceiptDetails.BatchID = Batches.BatchID " + "\r\n";
            queryString = queryString + "                   INNER JOIN Commodities ON GoodsReceiptDetails.CommodityID = Commodities.CommodityID " + "\r\n";
            queryString = queryString + "                   INNER JOIN BinLocations ON GoodsReceiptDetails.BinLocationID = BinLocations.BinLocationID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Pallets ON GoodsReceiptDetails.PalletID = Pallets.PalletID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Cartons PalletCartons ON Pallets.PalletID = PalletCartons.PalletID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Cartons ON GoodsReceiptDetails.CartonID = Cartons.CartonID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN GoodsIssues ON GoodsReceipts.GoodsIssueID = GoodsIssues.GoodsIssueID " + "\r\n";
            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetGoodsReceiptSheet", queryString);
        }



        #region Generate Pending

        private class GenerateSQLPendingDetails
        {
            private readonly GlobalEnums.GoodsReceiptTypeID goodsReceiptTypeID;

            private readonly string primaryTables;
            private readonly string primaryTableDetails;

            private readonly string primaryKey;
            private readonly string paraPrimaryKey;

            private readonly string primaryKeyDetail;
            private readonly string paraPrimaryKeyDetails;

            private readonly string fieldNameWarehouseID;

            private readonly string primaryReference;
            private readonly string primaryEntryDate;

            private readonly TotalSmartCodingEntities totalSmartCodingEntities;

            public GenerateSQLPendingDetails(TotalSmartCodingEntities totalSmartCodingEntities, GlobalEnums.GoodsReceiptTypeID goodsReceiptTypeID, string primaryTables, string primaryTableDetails, string primaryKey, string paraPrimaryKey, string primaryKeyDetails, string paraPrimaryKeyDetails, string fieldNameWarehouseID, string primaryReference, string primaryEntryDate)
            {
                this.totalSmartCodingEntities = totalSmartCodingEntities;

                this.goodsReceiptTypeID = goodsReceiptTypeID;

                this.primaryTables = primaryTables;
                this.primaryTableDetails = primaryTableDetails;

                this.primaryKey = primaryKey;
                this.paraPrimaryKey = paraPrimaryKey;

                this.primaryKeyDetail = primaryKeyDetails;
                this.paraPrimaryKeyDetails = paraPrimaryKeyDetails;

                this.fieldNameWarehouseID = fieldNameWarehouseID;

                this.primaryReference = primaryReference;
                this.primaryEntryDate = primaryEntryDate;
            }





            public void GetPendingPickups(string myfunctionName)
            {
                string queryString = " @LocationID int " + "\r\n";
                queryString = queryString + " WITH ENCRYPTION " + "\r\n";
                queryString = queryString + " AS " + "\r\n";

                queryString = queryString + "       SELECT          " + this.primaryTables + "." + this.primaryKey + ", " + this.primaryTables + ".Reference AS " + this.primaryReference + ", " + this.primaryTables + ".EntryDate AS " + this.primaryEntryDate + ", Warehouses.WarehouseID, Warehouses.Code AS WarehouseCode, Warehouses.Name AS WarehouseName, " + (int)this.goodsReceiptTypeID + " AS GoodsReceiptTypeID, (SELECT TOP 1 Name FROM GoodsReceiptTypes WHERE GoodsReceiptTypeID = " + (int)this.goodsReceiptTypeID + ") AS GoodsReceiptTypeName, " + this.primaryTables + ".Description, " + this.primaryTables + ".Remarks " + (this.goodsReceiptTypeID == GlobalEnums.GoodsReceiptTypeID.GoodsIssueTransfer ? ", GoodsIssues.VoucherCodes, SourceWarehouses.Name AS SourceWarehouseName " : "") + "\r\n";
                queryString = queryString + "       FROM            " + this.primaryTables + " " + "\r\n";
                queryString = queryString + "                       INNER JOIN Warehouses ON " + this.primaryTables + "." + this.primaryKey + " IN (SELECT " + this.primaryKey + " FROM " + this.primaryTableDetails + " WHERE LocationID = @LocationID AND Approved = 1 AND ROUND(Quantity - QuantityReceipt, " + (int)GlobalEnums.rndQuantity + ") > 0) AND " + this.primaryTables + "." + fieldNameWarehouseID + " = Warehouses.WarehouseID " + "\r\n";
                if (this.goodsReceiptTypeID == GlobalEnums.GoodsReceiptTypeID.GoodsIssueTransfer)
                    queryString = queryString + "                   INNER JOIN Warehouses SourceWarehouses ON GoodsIssues.WarehouseID = SourceWarehouses.WarehouseID " + "\r\n";

                this.totalSmartCodingEntities.CreateStoredProcedure(myfunctionName, queryString);
            }

            public void GetPendingPickupWarehouses(string myfunctionName)
            {
                string queryString = " @LocationID int " + "\r\n";
                queryString = queryString + " WITH ENCRYPTION " + "\r\n";
                queryString = queryString + " AS " + "\r\n";

                queryString = queryString + "       SELECT          Warehouses.WarehouseID, Warehouses.Code AS WarehouseCode, Warehouses.Name AS WarehouseName, " + (int)this.goodsReceiptTypeID + " AS GoodsReceiptTypeID, (SELECT TOP 1 Name FROM GoodsReceiptTypes WHERE GoodsReceiptTypeID = " + (int)this.goodsReceiptTypeID + ") AS GoodsReceiptTypeName " + "\r\n";
                queryString = queryString + "       FROM           (SELECT DISTINCT " + fieldNameWarehouseID + " FROM " + this.primaryTableDetails + " WHERE LocationID = @LocationID AND Approved = 1 AND ROUND(Quantity - QuantityReceipt, " + (int)GlobalEnums.rndQuantity + ") > 0) WarehousePENDING " + "\r\n";
                queryString = queryString + "                       INNER JOIN Warehouses ON WarehousePENDING." + fieldNameWarehouseID + " = Warehouses.WarehouseID " + "\r\n";

                this.totalSmartCodingEntities.CreateStoredProcedure(myfunctionName, queryString);
            }




















            public void GetPendingPickupDetails(string myfunctionName)
            {
                string queryString;

                queryString = " @LocationID Int, @GoodsReceiptID Int, " + this.paraPrimaryKey + " Int, @WarehouseID Int, " + this.paraPrimaryKeyDetails + " varchar(3999), @IsReadonly bit " + "\r\n";
                queryString = queryString + " WITH ENCRYPTION " + "\r\n";
                queryString = queryString + " AS " + "\r\n";

                queryString = queryString + "   BEGIN " + "\r\n";

                queryString = queryString + "       IF  (" + this.paraPrimaryKey + " <> 0) " + "\r\n";
                queryString = queryString + "           " + this.BuildSQLPickup(true) + "\r\n";
                queryString = queryString + "       ELSE " + "\r\n";
                queryString = queryString + "           " + this.BuildSQLPickup(false) + "\r\n";

                queryString = queryString + "   END " + "\r\n";

                this.totalSmartCodingEntities.CreateStoredProcedure(myfunctionName, queryString);
            }

            private string BuildSQLPickup(bool isPickupID)
            {
                string queryString = "";
                queryString = queryString + "   BEGIN " + "\r\n";
                queryString = queryString + "       IF  (" + this.paraPrimaryKeyDetails + " <> '') " + "\r\n";
                queryString = queryString + "           " + this.BuildSQLPickupPickupDetailIDs(isPickupID, true) + "\r\n";
                queryString = queryString + "       ELSE " + "\r\n";
                queryString = queryString + "           " + this.BuildSQLPickupPickupDetailIDs(isPickupID, false) + "\r\n";
                queryString = queryString + "   END " + "\r\n";

                return queryString;
            }

            private string BuildSQLPickupPickupDetailIDs(bool isPickupID, bool isPickupDetailIDs)
            {
                string queryString = "";
                queryString = queryString + "   BEGIN " + "\r\n";

                queryString = queryString + "       IF (@GoodsReceiptID <= 0) " + "\r\n";
                queryString = queryString + "               BEGIN " + "\r\n";
                queryString = queryString + "                   " + this.BuildSQLNew(isPickupID, isPickupDetailIDs) + "\r\n";
                queryString = queryString + "                   ORDER BY " + this.primaryTableDetails + ".EntryDate, " + this.primaryTableDetails + "." + this.primaryKey + ", " + this.primaryTableDetails + "." + this.primaryKeyDetail + " " + "\r\n";
                queryString = queryString + "               END " + "\r\n";
                queryString = queryString + "       ELSE " + "\r\n";

                queryString = queryString + "               IF (@IsReadonly = 1) " + "\r\n";
                queryString = queryString + "                   BEGIN " + "\r\n";
                queryString = queryString + "                       " + this.BuildSQLEdit(isPickupID, isPickupDetailIDs) + "\r\n";
                queryString = queryString + "                       ORDER BY " + this.primaryTableDetails + ".EntryDate, " + this.primaryTableDetails + "." + this.primaryKey + ", " + this.primaryTableDetails + "." + this.primaryKeyDetail + " " + "\r\n";
                queryString = queryString + "                   END " + "\r\n";

                queryString = queryString + "               ELSE " + "\r\n"; //FULL SELECT FOR EDIT MODE

                queryString = queryString + "                   BEGIN " + "\r\n";
                queryString = queryString + "                       " + this.BuildSQLNew(isPickupID, isPickupDetailIDs) + " WHERE " + this.primaryTableDetails + "." + this.primaryKeyDetail + " NOT IN (SELECT " + this.primaryKeyDetail + " FROM GoodsReceiptDetails WHERE GoodsReceiptID = @GoodsReceiptID) " + "\r\n";
                queryString = queryString + "                       UNION ALL " + "\r\n";
                queryString = queryString + "                       " + this.BuildSQLEdit(isPickupID, isPickupDetailIDs) + "\r\n";
                queryString = queryString + "                       ORDER BY " + this.primaryTableDetails + ".EntryDate, " + this.primaryTableDetails + "." + this.primaryKey + ", " + this.primaryTableDetails + "." + this.primaryKeyDetail + " " + "\r\n";
                queryString = queryString + "                   END " + "\r\n";

                queryString = queryString + "   END " + "\r\n";

                return queryString;
            }

            private string BuildSQLNew(bool isPickupID, bool isPickupDetailIDs)
            {
                string queryString = "";

                queryString = queryString + "       SELECT      " + this.primaryTableDetails + "." + this.primaryKey + ", " + this.primaryTableDetails + "." + this.primaryKeyDetail + ", " + this.primaryTableDetails + ".Reference " + (this.goodsReceiptTypeID == GlobalEnums.GoodsReceiptTypeID.GoodsIssueTransfer ? " + ' [' + GoodsIssues.VoucherCodes + ']' " : "") + " AS " + this.primaryReference + ", " + this.primaryTableDetails + ".EntryDate AS " + this.primaryEntryDate + ", " + "\r\n";
                queryString = queryString + "                   Commodities.CommodityID, Commodities.Code AS CommodityCode, Commodities.Name AS CommodityName, " + this.primaryTableDetails + ".BatchID, " + this.primaryTableDetails + ".BatchEntryDate, " + (this.goodsReceiptTypeID == GlobalEnums.GoodsReceiptTypeID.Pickup || this.goodsReceiptTypeID == GlobalEnums.GoodsReceiptTypeID.WarehouseAdjustments ? this.primaryTableDetails + ".BinLocationID" : "0") + " AS BinLocationID, " + (this.goodsReceiptTypeID == GlobalEnums.GoodsReceiptTypeID.Pickup || this.goodsReceiptTypeID == GlobalEnums.GoodsReceiptTypeID.WarehouseAdjustments ? "BinLocations.Code" : "N''") + " AS BinLocationCode, " + this.primaryTableDetails + ".PackID, Packs.Code AS PackCode, " + this.primaryTableDetails + ".CartonID, Cartons.Code AS CartonCode, " + this.primaryTableDetails + ".PalletID, Pallets.Code AS PalletCode, " + "\r\n";
                queryString = queryString + "                   ROUND(" + this.primaryTableDetails + ".Quantity - " + this.primaryTableDetails + ".QuantityReceipt,  " + (int)GlobalEnums.rndQuantity + ") AS QuantityRemains, CAST(0 AS decimal(18, 2)) AS Quantity, ROUND(" + this.primaryTableDetails + ".LineVolume - " + this.primaryTableDetails + ".LineVolumeReceipt,  " + (int)GlobalEnums.rndVolume + ") AS LineVolumeRemains, " + this.primaryTableDetails + ".LineVolume, " + this.primaryTableDetails + ".PackCounts, " + this.primaryTableDetails + ".CartonCounts, " + this.primaryTableDetails + ".PalletCounts, " + this.primaryTableDetails + ".Remarks, CAST(" + (this.goodsReceiptTypeID == GlobalEnums.GoodsReceiptTypeID.Pickup ? "1" : "0") + " AS bit) AS IsSelected " + "\r\n";

                queryString = queryString + "       FROM        " + this.primaryTableDetails + " " + "\r\n";
                queryString = queryString + "                   INNER JOIN Commodities ON " + (isPickupID ? " " + this.primaryTableDetails + "." + this.primaryKey + " = " + this.paraPrimaryKey + " " : "" + this.primaryTableDetails + ".LocationID = @LocationID AND " + this.primaryTableDetails + "." + fieldNameWarehouseID + " = @WarehouseID ") + " AND " + this.primaryTableDetails + (this.primaryTableDetails == "WarehouseAdjustmentDetails" ? ".Quantity > 0" : ".Approved = 1") + " AND ROUND(" + this.primaryTableDetails + ".Quantity - " + this.primaryTableDetails + ".QuantityReceipt, " + (int)GlobalEnums.rndQuantity + ") > 0 AND " + this.primaryTableDetails + ".CommodityID = Commodities.CommodityID " + (isPickupDetailIDs ? " AND " + this.primaryTableDetails + "." + this.primaryKeyDetail + " NOT IN (SELECT Id FROM dbo.SplitToIntList (" + this.paraPrimaryKeyDetails + "))" : "") + "\r\n";

                if (this.goodsReceiptTypeID == GlobalEnums.GoodsReceiptTypeID.GoodsIssueTransfer)
                    queryString = queryString + "               INNER JOIN GoodsIssues ON GoodsIssueTransferDetails.GoodsIssueID = GoodsIssues.GoodsIssueID " + "\r\n";

                if (this.goodsReceiptTypeID == GlobalEnums.GoodsReceiptTypeID.Pickup || this.goodsReceiptTypeID == GlobalEnums.GoodsReceiptTypeID.WarehouseAdjustments)
                    queryString = queryString + "               INNER JOIN BinLocations ON " + this.primaryTableDetails + ".BinLocationID = BinLocations.BinLocationID " + "\r\n";

                queryString = queryString + "                   LEFT JOIN Packs ON " + this.primaryTableDetails + ".PackID = Packs.PackID " + "\r\n";
                queryString = queryString + "                   LEFT JOIN Cartons ON " + this.primaryTableDetails + ".CartonID = Cartons.CartonID " + "\r\n";
                queryString = queryString + "                   LEFT JOIN Pallets ON " + this.primaryTableDetails + ".PalletID = Pallets.PalletID " + "\r\n";

                return queryString;
            }

            private string BuildSQLEdit(bool isPickupID, bool isPickupDetailIDs)
            {
                string queryString = "";

                queryString = queryString + "       SELECT      " + this.primaryTableDetails + "." + this.primaryKey + ", " + this.primaryTableDetails + "." + this.primaryKeyDetail + ", " + this.primaryTableDetails + ".Reference " + (this.goodsReceiptTypeID == GlobalEnums.GoodsReceiptTypeID.GoodsIssueTransfer ? " + ' [' + GoodsIssues.VoucherCodes + ']' " : "") + " AS " + this.primaryReference + ", " + this.primaryTableDetails + ".EntryDate AS " + this.primaryEntryDate + ", " + "\r\n";
                queryString = queryString + "                   Commodities.CommodityID, Commodities.Code AS CommodityCode, Commodities.Name AS CommodityName, " + this.primaryTableDetails + ".BatchID, " + this.primaryTableDetails + ".BatchEntryDate, " + this.primaryTableDetails + ".BinLocationID, BinLocations.Code AS BinLocationCode, " + this.primaryTableDetails + ".PackID, Packs.Code AS PackCode, " + this.primaryTableDetails + ".CartonID, Cartons.Code AS CartonCode, " + this.primaryTableDetails + ".PalletID, Pallets.Code AS PalletCode, " + "\r\n";
                queryString = queryString + "                   ROUND(" + this.primaryTableDetails + ".Quantity - " + this.primaryTableDetails + ".QuantityReceipt + GoodsReceiptDetails.Quantity,  " + (int)GlobalEnums.rndQuantity + ") AS QuantityRemains, CAST(0 AS decimal(18, 2)) AS Quantity, ROUND(" + this.primaryTableDetails + ".LineVolume - " + this.primaryTableDetails + ".LineVolumeReceipt + GoodsReceiptDetails.LineVolume,  " + (int)GlobalEnums.rndVolume + ") AS LineVolumeRemains, " + this.primaryTableDetails + ".LineVolume, " + this.primaryTableDetails + ".PackCounts, " + this.primaryTableDetails + ".CartonCounts, " + this.primaryTableDetails + ".PalletCounts, " + this.primaryTableDetails + ".Remarks, CAST(" + (this.goodsReceiptTypeID == GlobalEnums.GoodsReceiptTypeID.Pickup ? "1" : "0") + " AS bit) AS IsSelected " + "\r\n";

                queryString = queryString + "       FROM        " + this.primaryTableDetails + " " + "\r\n";
                queryString = queryString + "                   INNER JOIN GoodsReceiptDetails ON GoodsReceiptDetails.GoodsReceiptID = @GoodsReceiptID AND " + this.primaryTableDetails + "." + this.primaryKeyDetail + " = GoodsReceiptDetails." + this.primaryKeyDetail + "" + (isPickupDetailIDs ? " AND " + this.primaryTableDetails + "." + this.primaryKeyDetail + " NOT IN (SELECT Id FROM dbo.SplitToIntList (" + this.paraPrimaryKeyDetails + "))" : "") + "\r\n";

                if (this.goodsReceiptTypeID == GlobalEnums.GoodsReceiptTypeID.GoodsIssueTransfer)
                    queryString = queryString + "               INNER JOIN GoodsIssues ON GoodsIssueTransferDetails.GoodsIssueID = GoodsIssues.GoodsIssueID " + "\r\n";

                queryString = queryString + "                   INNER JOIN Commodities ON GoodsReceiptDetails.CommodityID = Commodities.CommodityID " + "\r\n";
                queryString = queryString + "                   INNER JOIN BinLocations ON GoodsReceiptDetails.BinLocationID = BinLocations.BinLocationID " + "\r\n";
                queryString = queryString + "                   LEFT JOIN Packs ON " + this.primaryTableDetails + ".PackID = Packs.PackID " + "\r\n";
                queryString = queryString + "                   LEFT JOIN Cartons ON " + this.primaryTableDetails + ".CartonID = Cartons.CartonID " + "\r\n";
                queryString = queryString + "                   LEFT JOIN Pallets ON " + this.primaryTableDetails + ".PalletID = Pallets.PalletID " + "\r\n";

                return queryString;
            }
        }
        #endregion Generate Pending
    }
}
