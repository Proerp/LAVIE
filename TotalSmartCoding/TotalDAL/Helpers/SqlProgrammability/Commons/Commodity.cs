using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

using TotalDAL.Helpers.SqlProgrammability.Sales;

namespace TotalDAL.Helpers.SqlProgrammability.Commons
{
    public class Commodity
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public Commodity(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetCommodityIndexes();

            this.CommodityEditable();
            this.CommoditySaveRelative();

            this.GetCommodityBases();
            this.GetCommodityTrees();

            this.SearchCommodities();

            this.SearchBarcodes();
        }


        private void GetCommodityIndexes()
        {
            string queryString;

            queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      Commodities.CommodityID, Commodities.Code AS CommodityCode, Commodities.APICode AS CommodityAPICode, Commodities.Name AS CommodityName, CommodityCategories.CommodityCategoryID, CommodityCategories.Name AS CommodityCategoryName, Commodities.PackageSize, Commodities.PackageVolume, Commodities.PackPerCarton, Commodities.CartonPerPallet, Commodities.Shelflife, Commodities.InActive, Commodities.Remarks " + "\r\n";
            queryString = queryString + "       FROM        CommodityCategories " + "\r\n";
            queryString = queryString + "                   INNER JOIN Commodities ON CommodityCategories.CommodityCategoryID = Commodities.CommodityCategoryID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetCommodityIndexes", queryString);
        }


        private void CommoditySaveRelative()
        {
            string queryString = " @EntityID int, @SaveRelativeOption int " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("CommoditySaveRelative", queryString);
        }


        private void CommodityEditable()
        {
            string[] queryArray = new string[14];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = CommodityID FROM Commodities WHERE CommodityID = @EntityID AND InActive = 1 ";
            queryArray[1] = " SELECT TOP 1 @FoundEntity = CommodityID FROM BatchMasters WHERE CommodityID = @EntityID ";
            queryArray[2] = " SELECT TOP 1 @FoundEntity = CommodityID FROM Batches WHERE CommodityID = @EntityID ";
            queryArray[3] = " SELECT TOP 1 @FoundEntity = CommodityID FROM TransferOrderDetails WHERE CommodityID = @EntityID ";
            queryArray[4] = " SELECT TOP 1 @FoundEntity = CommodityID FROM SalesOrderDetails WHERE CommodityID = @EntityID ";
            queryArray[5] = " SELECT TOP 1 @FoundEntity = CommodityID FROM DeliveryAdviceDetails WHERE CommodityID = @EntityID ";
            queryArray[6] = " SELECT TOP 1 @FoundEntity = CommodityID FROM WarehouseAdjustmentDetails WHERE CommodityID = @EntityID ";
            queryArray[7] = " SELECT TOP 1 @FoundEntity = CommodityID FROM PickupDetails WHERE CommodityID = @EntityID ";
            queryArray[8] = " SELECT TOP 1 @FoundEntity = CommodityID FROM GoodsReceiptDetails WHERE CommodityID = @EntityID ";
            queryArray[9] = " SELECT TOP 1 @FoundEntity = CommodityID FROM GoodsIssueDetails WHERE CommodityID = @EntityID ";
            queryArray[10] = " SELECT TOP 1 @FoundEntity = CommodityID FROM GoodsIssueTransferDetails WHERE CommodityID = @EntityID ";
            queryArray[11] = " SELECT TOP 1 @FoundEntity = CommodityID FROM Pallets WHERE CommodityID = @EntityID ";
            queryArray[12] = " SELECT TOP 1 @FoundEntity = CommodityID FROM Cartons WHERE CommodityID = @EntityID ";
            queryArray[13] = " SELECT TOP 1 @FoundEntity = CommodityID FROM Packs WHERE CommodityID = @EntityID ";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("CommodityEditable", queryArray);
        }


        private void GetCommodityBases()
        {            
            this.totalSmartCodingEntities.CreateStoredProcedure("GetCommodityBases", this.GetCommodityBUILD(0));
            this.totalSmartCodingEntities.CreateStoredProcedure("GetCommodityBase", this.GetCommodityBUILD(1));
            this.totalSmartCodingEntities.CreateStoredProcedure("GetCommodityBaseByCode", this.GetCommodityBUILD(2));
        }

        private string GetCommodityBUILD(int switchID)
        {
            string queryString;

            queryString = (switchID == 0 ? "" : (switchID == 1 ? "@CommodityID int" : "@Code nvarchar(50)")) + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      CommodityID, Code, Name, Unit, APICode, Volume, PackageSize, PackageVolume, FillingLineIDs " + "\r\n";
            queryString = queryString + "       FROM        Commodities " + "\r\n";
            queryString = queryString + "       WHERE       " + (switchID == 0 ? "InActive = 0" : (switchID == 1 ? "CommodityID = @CommodityID" : "Code = @Code")) + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            return queryString;
        }

        private void GetCommodityTrees()
        {
            string queryString;

            queryString = " " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      " + GlobalEnums.RootNode + " AS NodeID, 0 AS ParentNodeID, NULL AS PrimaryID, NULL AS AncestorID, '[All]' AS Code, NULL AS Name, NULL AS ParameterName, CAST(1 AS bit) AS Selected " + "\r\n";
            queryString = queryString + "       UNION ALL " + "\r\n";
            queryString = queryString + "       SELECT      " + GlobalEnums.AncestorNode + " + CommodityCategoryID AS NodeID, " + GlobalEnums.RootNode + " AS ParentNodeID, CommodityCategoryID AS PrimaryID, NULL AS AncestorID, Name AS Code, NULL AS Name, 'CommodityCategoryID' AS ParameterName, CAST(0 AS bit) AS Selected " + "\r\n";
            queryString = queryString + "       FROM        CommodityCategories " + "\r\n";
            queryString = queryString + "       UNION ALL " + "\r\n";
            queryString = queryString + "       SELECT      CommodityID AS NodeID, " + GlobalEnums.AncestorNode + " + CommodityCategoryID AS ParentNodeID, CommodityID AS PrimaryID, CommodityCategoryID AS AncestorID, '[' + APICode + ']  ' + Code AS Code, Name, 'CommodityID' AS ParameterName, CAST(0 AS bit) AS Selected " + "\r\n";
            queryString = queryString + "       FROM        Commodities " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetCommodityTrees", queryString);
        }

        private void SearchCommodities()
        {
            string queryString;

            //THIS SearchCommodities NOW IS USED BY Add new item in SALES ORDER, and new TRANSFER ORDER. 
            //IT ALSO MAY BE USED BY Add new item in DELIVERY ADVICE. BUT, AT CURRENT: We only convert from SALES ORDER to DELIVERY ADVICE
            queryString = " @CommodityID int, @LocationID int, @BatchID int, @DeliveryAdviceID int, @TransferOrderID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       DECLARE     @SearchCommodities TABLE (LocationID int NOT NULL, BatchID int NULL, CommodityID int NOT NULL, Code nvarchar(50) NOT NULL, Name nvarchar(200) NOT NULL, CommodityCategoryID int NOT NULL, CommodityTypeID int NOT NULL, Unit nvarchar(10) NULL, PackageSize nvarchar(60) NULL, Volume decimal(18, 3) NOT NULL, PackageVolume decimal(18, 3) NOT NULL) " + "\r\n";
            queryString = queryString + "                   " + GenerateSQLCommoditiesAvailable.BuildSQL("@SalesOrderDetails", false, false, true, false, false, true, false) + "\r\n";

            queryString = queryString + "       INSERT INTO @SearchCommodities SELECT @LocationID, @BatchID, CommodityID, Code, Name, CommodityCategoryID, CommodityTypeID, Unit, PackageSize, Volume, PackageVolume FROM Commodities WHERE CommodityID = @CommodityID " + "\r\n";

            queryString = queryString + "       IF (@BatchID > 0) " + "\r\n";
            queryString = queryString + "           " + this.BuildSQLSearchCommodities(true) + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "           " + this.BuildSQLSearchCommodities(false) + "\r\n";

            queryString = queryString + "       SELECT      SearchCommodities.LocationID, SearchCommodities.BatchID, SearchCommodities.CommodityID, SearchCommodities.Code, SearchCommodities.Name, SearchCommodities.CommodityCategoryID, SearchCommodities.CommodityTypeID, SearchCommodities.Unit, SearchCommodities.PackageSize, SearchCommodities.Volume, SearchCommodities.PackageVolume, " + "\r\n";
            queryString = queryString + "                   ISNULL(CommoditiesAvailable.QuantityAvailable, 0) AS QuantityAvailable, ISNULL(CommoditiesAvailable.LineVolumeAvailable, 0) AS LineVolumeAvailable, ISNULL(CommoditiesAvailableByBatches.QuantityAvailable, 0) AS QuantityBatchAvailable, ISNULL(CommoditiesAvailableByBatches.LineVolumeAvailable, 0) AS LineVolumeBatchAvailable " + "\r\n";

            queryString = queryString + "       FROM        @SearchCommodities SearchCommodities " + "\r\n";
            queryString = queryString + "                   LEFT JOIN (SELECT CommodityID, SUM(QuantityAvailable) AS QuantityAvailable, SUM(LineVolumeAvailable) AS LineVolumeAvailable FROM @CommoditiesAvailable GROUP BY CommodityID) CommoditiesAvailable ON SearchCommodities.CommodityID = CommoditiesAvailable.CommodityID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN (SELECT CommodityID, BatchID, SUM(QuantityAvailable) AS QuantityAvailable, SUM(LineVolumeAvailable) AS LineVolumeAvailable FROM @CommoditiesAvailableByBatches GROUP BY CommodityID, BatchID) CommoditiesAvailableByBatches ON SearchCommodities.BatchID = CommoditiesAvailableByBatches.BatchID AND SearchCommodities.CommodityID = CommoditiesAvailableByBatches.CommodityID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("SearchCommodities", queryString);
        }

        private string BuildSQLSearchCommodities(bool byBatchID)
        {
            string queryString = "";
            queryString = queryString + "   BEGIN " + "\r\n";

            queryString = queryString + "       IF (@DeliveryAdviceID > 0) " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   " + GenerateSQLCommoditiesAvailable.BuildSQL("@SearchCommodities", true, false, false, true, true, byBatchID, false) + "\r\n";
            queryString = queryString + "               END " + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "           IF (@TransferOrderID > 0) " + "\r\n";
            queryString = queryString + "                   BEGIN " + "\r\n";
            queryString = queryString + "                       " + GenerateSQLCommoditiesAvailable.BuildSQL("@SearchCommodities", false, true, false, true, true, byBatchID, false) + "\r\n";
            queryString = queryString + "                   END " + "\r\n";
            queryString = queryString + "           ELSE " + "\r\n";
            queryString = queryString + "                   BEGIN " + "\r\n";
            queryString = queryString + "                       " + GenerateSQLCommoditiesAvailable.BuildSQL("@SearchCommodities", false, false, false, true, false, byBatchID, false) + "\r\n";
            queryString = queryString + "                   END " + "\r\n";

            queryString = queryString + "   END " + "\r\n";

            return queryString;
        }


        private void SearchBarcodes()
        {
            string queryString;

            queryString = " @Barcode varchar(50) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       DECLARE     @PackID int, @CartonID int, @PalletID int, @PackCode varchar(50), @CartonCode varchar(50), @PalletCode varchar(50) " + "\r\n";
            queryString = queryString + "       DECLARE     @BarcodeResults TABLE (LocationID int NOT NULL, CommodityID int NOT NULL, PackID int NULL, CartonID int NULL, PalletID int NULL, EntryDate datetime NOT NULL, EntryOrderID int NOT NULL, Quantity decimal(18, 3) NOT NULL, LineVolume decimal(18, 3) NOT NULL, Description nvarchar(1000) NULL) " + "\r\n";

            queryString = queryString + "       IF LEN(@Barcode) > 10 " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";


            //FIND ID
            queryString = queryString + "               IF SUBSTRING (@Barcode, 7, 1 ) = 'B' " + "\r\n";
            queryString = queryString + "                   BEGIN " + "\r\n";
            queryString = queryString + "                       SELECT TOP 1 @PackID = PackID, @CartonID = CartonID, @PackCode = Code FROM Packs WHERE Code = @Barcode " + "\r\n";
            queryString = queryString + "                       IF NOT @CartonID IS NULL " + "\r\n";
            queryString = queryString + "                           SELECT TOP 1 @PalletID = PalletID, @CartonCode = Code FROM Cartons WHERE CartonID = @CartonID " + "\r\n";
            queryString = queryString + "                       IF NOT @PalletID IS NULL " + "\r\n";
            queryString = queryString + "                           SELECT TOP 1 @PalletCode = Code FROM Pallets WHERE PalletID = @PalletID " + "\r\n";
            queryString = queryString + "                   END " + "\r\n";

            queryString = queryString + "               IF SUBSTRING (@Barcode, 7, 1 ) = 'C' " + "\r\n";
            queryString = queryString + "                   BEGIN " + "\r\n";
            queryString = queryString + "                       SELECT TOP 1 @CartonID = CartonID, @PalletID = PalletID, @CartonCode = Code FROM Cartons WHERE Code = @Barcode " + "\r\n";
            queryString = queryString + "                       IF NOT @PalletID IS NULL " + "\r\n";
            queryString = queryString + "                           SELECT TOP 1 @PalletCode = Code FROM Pallets WHERE PalletID = @PalletID " + "\r\n";
            queryString = queryString + "                   END " + "\r\n";

            queryString = queryString + "               IF SUBSTRING (@Barcode, 7, 1 ) = 'P' " + "\r\n";
            queryString = queryString + "                       SELECT TOP 1 @PalletID = PalletID, @PalletCode = Code FROM Pallets WHERE Code = @Barcode " + "\r\n";


            //GET TRANSACTION
            queryString = queryString + "               IF (NOT @PackID IS NULL) OR (NOT @CartonID IS NULL) OR (NOT @PalletID IS NULL) " + "\r\n";
            queryString = queryString + "                   BEGIN " + "\r\n";
            queryString = queryString + "                       IF (NOT @PackID IS NULL) " + "\r\n";
            queryString = queryString + "                           INSERT INTO @BarcodeResults SELECT LocationID, CommodityID, PackID, NULL AS CartonID, NULL AS PalletID, EntryDate, 0 AS EntryOrderID, 0 AS Quantity, 0 AS LineVolume, 'Production' FROM Packs WHERE PackID = @PackID " + "\r\n";
            queryString = queryString + "                       IF (NOT @CartonID IS NULL) " + "\r\n";
            queryString = queryString + "                           INSERT INTO @BarcodeResults SELECT LocationID, CommodityID, NULL AS PackID, CartonID, NULL AS PalletID, EntryDate, 0 AS EntryOrderID, Quantity, LineVolume, 'Production' FROM Cartons WHERE CartonID = @CartonID " + "\r\n";
            queryString = queryString + "                       IF (NOT @PalletID IS NULL) " + "\r\n";
            queryString = queryString + "                           INSERT INTO @BarcodeResults SELECT LocationID, CommodityID, NULL AS PackID, NULL AS CartonID, PalletID, EntryDate, 0 AS EntryOrderID, Quantity, LineVolume, 'Production' FROM Pallets WHERE PalletID = @PalletID " + "\r\n";

            queryString = queryString + "                       INSERT INTO @BarcodeResults SELECT LocationID, CommodityID, PackID, CartonID, PalletID, EntryDate, 0 AS EntryOrderID, Quantity, LineVolume, 'Pickup ' + Reference FROM PickupDetails WHERE PackID = @PackID OR CartonID = @CartonID OR PalletID = @PalletID " + "\r\n";
            queryString = queryString + "                       INSERT INTO @BarcodeResults SELECT GoodsReceiptDetails.LocationID, GoodsReceiptDetails.CommodityID, GoodsReceiptDetails.PackID, GoodsReceiptDetails.CartonID, GoodsReceiptDetails.PalletID, GoodsReceiptDetails.EntryDate, GoodsReceiptDetails.GoodsReceiptTypeID AS EntryOrderID, GoodsReceiptDetails.Quantity, GoodsReceiptDetails.LineVolume, 'Receipt from ' +  GoodsReceiptTypes.Name + ' ' + ISNULL(WarehouseAdjustments.AdjustmentJobs, '') + ISNULL(Warehouses.Name, '') FROM GoodsReceiptDetails INNER JOIN GoodsReceiptTypes ON GoodsReceiptDetails.GoodsReceiptTypeID = GoodsReceiptTypes.GoodsReceiptTypeID LEFT JOIN WarehouseAdjustments ON GoodsReceiptDetails.WarehouseAdjustmentID = WarehouseAdjustments.WarehouseAdjustmentID LEFT JOIN GoodsIssues ON GoodsReceiptDetails.GoodsIssueID = GoodsIssues.GoodsIssueID LEFT JOIN Warehouses ON GoodsIssues.WarehouseID = Warehouses.WarehouseID AND GoodsIssues.WarehouseReceiptID = Warehouses.WarehouseID WHERE PackID = @PackID OR CartonID = @CartonID OR PalletID = @PalletID " + "\r\n";
            queryString = queryString + "                       INSERT INTO @BarcodeResults SELECT GoodsIssueDetails.LocationID, GoodsIssueDetails.CommodityID, GoodsIssueDetails.PackID, GoodsIssueDetails.CartonID, GoodsIssueDetails.PalletID, GoodsIssueDetails.EntryDate, 0 AS EntryOrderID, GoodsIssueDetails.Quantity, GoodsIssueDetails.LineVolume, 'Issue for ' + ISNULL(GoodsIssues.VoucherCodes, '') + ', ' + ISNULL(Customers.Name, '') + ISNULL(Warehouses.Name, '') FROM GoodsIssueDetails INNER JOIN GoodsIssues ON GoodsIssueDetails.GoodsIssueID = GoodsIssues.GoodsIssueID LEFT JOIN Customers ON GoodsIssueDetails.CustomerID = Customers.CustomerID LEFT JOIN Warehouses ON GoodsIssueDetails.WarehouseReceiptID = Warehouses.WarehouseID WHERE PackID = @PackID OR CartonID = @CartonID OR PalletID = @PalletID " + "\r\n";
            queryString = queryString + "                       INSERT INTO @BarcodeResults SELECT WarehouseAdjustmentDetails.LocationID, WarehouseAdjustmentDetails.CommodityID, WarehouseAdjustmentDetails.PackID, WarehouseAdjustmentDetails.CartonID, WarehouseAdjustmentDetails.PalletID, WarehouseAdjustmentDetails.EntryDate, 0 AS EntryOrderID, -WarehouseAdjustmentDetails.Quantity, -WarehouseAdjustmentDetails.LineVolume, WarehouseAdjustmentTypes.Name + ' ' + ISNULL(WarehouseAdjustments.AdjustmentJobs, '') FROM WarehouseAdjustmentDetails INNER JOIN WarehouseAdjustments ON WarehouseAdjustmentDetails.WarehouseAdjustmentID = WarehouseAdjustments.WarehouseAdjustmentID INNER JOIN WarehouseAdjustmentTypes ON WarehouseAdjustmentDetails.WarehouseAdjustmentTypeID = WarehouseAdjustmentTypes.WarehouseAdjustmentTypeID WHERE (PackID = @PackID OR CartonID = @CartonID OR PalletID = @PalletID) AND Quantity < 0 " + "\r\n";
            queryString = queryString + "                   END " + "\r\n";

            queryString = queryString + "           END " + "\r\n";

            //RETURN RESULT
            queryString = queryString + "       SELECT      BarcodeResults.EntryDate, BarcodeResults.EntryOrderID, Locations.Name AS LocationName, Commodities.CommodityID, Commodities.Code, Commodities.Name, Commodities.PackageSize, Commodities.Volume, BarcodeResults.Quantity, BarcodeResults.LineVolume, " + "\r\n";
            queryString = queryString + "                   PackID, CASE WHEN NOT PackID IS NULL THEN @PackCode ELSE NULL END AS PackCode, CartonID, CASE WHEN NOT CartonID IS NULL THEN @CartonCode ELSE NULL END AS CartonCode, PalletID, CASE WHEN NOT PalletID IS NULL THEN @PalletCode ELSE NULL END AS PalletCode, BarcodeResults.Description " + "\r\n";
            queryString = queryString + "       FROM        @BarcodeResults BarcodeResults " + "\r\n";
            queryString = queryString + "                   INNER JOIN Commodities ON BarcodeResults.CommodityID = Commodities.CommodityID " + "\r\n";
            queryString = queryString + "                   INNER JOIN Locations ON BarcodeResults.LocationID = Locations.LocationID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("SearchBarcodes", queryString);
        }

    }
}
