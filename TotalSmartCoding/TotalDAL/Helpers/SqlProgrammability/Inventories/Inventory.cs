using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Inventories
{
    public class Inventory
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public Inventory(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetExpiryDate();

            //this.ProductionJournals(); JUST COMMENT OUT, BECAUSE NO NEED TO RUN AGAIN
            ////-----------this.WarehouseJournals();

            this.DeletedBarcodeJournals();
        }


        private void GetExpiryDate()
        {
            string queryString = " (@ProductionDate Datetime, @Shelflife int) " + "\r\n";
            queryString = queryString + " RETURNS Datetime " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "   BEGIN " + "\r\n";
            queryString = queryString + "       RETURN DATEADD(DAY, -1 + DAY(@ProductionDate), DATEADD(MONTH, @Shelflife, DATEADD(DAY, 1 - DAY(@ProductionDate), @ProductionDate))) " + "\r\n"; // @ProductionDate.AddDays(1 - @ProductionDate.Day).AddMonths(@Shelflife).AddDays(-1 + @ProductionDate.Day) 
            queryString = queryString + "   END " + "\r\n";

            this.totalSmartCodingEntities.CreateUserDefinedFunction("GetExpiryDate", queryString);
        }

        private void WarehouseJournals()
        {
            string queryString = " @LocationID int, @WarehouseID int, @FromDate DateTime, @ToDate DateTime " + "\r\n"; //Filter by @LocalWarehouseID to make this stored procedure run faster, but it may be removed without any effect the algorithm

            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       DECLARE     @LocalLocationID int, @LocalWarehouseID int, @LocalFromDate DateTime, @LocalToDate DateTime " + "\r\n";
            queryString = queryString + "       SET         @LocalLocationID = @LocationID      SET         @LocalWarehouseID = @WarehouseID " + "\r\n";
            queryString = queryString + "       SET         @LocalFromDate = @FromDate          SET         @LocalToDate = @ToDate " + "\r\n";

            queryString = queryString + "       IF         (@LocalLocationID > 0 AND @LocalWarehouseID > 0) " + "\r\n";
            queryString = queryString + "                   " + this.WarehouseJournalBUILD(true, true) + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "           IF         (@LocalLocationID > 0 AND @LocalWarehouseID <= 0) " + "\r\n";
            queryString = queryString + "                       " + this.WarehouseJournalBUILD(true, false) + "\r\n";
            queryString = queryString + "           ELSE " + "\r\n";
            queryString = queryString + "               IF         (@LocalLocationID <= 0 AND @LocalWarehouseID > 0) " + "\r\n";
            queryString = queryString + "                           " + this.WarehouseJournalBUILD(false, true) + "\r\n";
            queryString = queryString + "               ELSE " + "\r\n";
            queryString = queryString + "                           " + this.WarehouseJournalBUILD(false, false) + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("WarehouseJournals", queryString);

        }


        private string WarehouseJournalBUILD(bool isLocationID, bool isWarehouseID)
        {
            string queryString = "" + "\r\n";

            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      WarehouseJournalDetails.LocationID, Locations.Name AS LocationName, WarehouseJournalDetails.WarehouseID, Warehouses.Name AS WarehouseName, WarehouseJournalDetails.BinLocationID, BinLocations.Code AS BinLocationCode, WarehouseJournalDetails.BatchEntryDate, " + "\r\n";
            queryString = queryString + "                   CommodityCategories.CommodityCategoryID, CommodityCategories.Name AS CommodityCategoryName, Commodities.CommodityID, Commodities.Code, Commodities.Name, Commodities.Unit, Commodities.PackageSize, ISNULL(Pallets.Code, ISNULL(Cartons.Code, ISNULL(Packs.Code, ''))) AS Barcode, CASE WHEN NOT WarehouseJournalDetails.PalletID IS NULL THEN N'Pallet' WHEN NOT WarehouseJournalDetails.CartonID IS NULL THEN N'Carton'  WHEN NOT WarehouseJournalDetails.PackID IS NULL THEN N'Pack' ELSE '' END AS BarcodeUnit, " + "\r\n";
            queryString = queryString + "                   WarehouseJournalDetails.GoodsReceiptDetailID, WarehouseJournalDetails.EntryDate, ISNULL('Production: ' + ' ' + Pickups.Reference, ISNULL('From: ' + ' ' + SourceWarehouses.Name + ', ' + GoodsIssues.VoucherCodes, ISNULL(WarehouseAdjustmentTypes.Name  + ' ' + WarehouseAdjustments.AdjustmentJobs, ''))) AS LineReferences, " + "\r\n";

            queryString = queryString + "                   WarehouseJournalDetails.QuantityBegin, WarehouseJournalDetails.QuantityReceiptPickup, WarehouseJournalDetails.QuantityReceiptPurchasing, WarehouseJournalDetails.QuantityReceiptTransfer, WarehouseJournalDetails.QuantityReceiptReturn, WarehouseJournalDetails.QuantityReceiptAdjustment, WarehouseJournalDetails.QuantityReceiptPickup + WarehouseJournalDetails.QuantityReceiptPurchasing + WarehouseJournalDetails.QuantityReceiptTransfer + WarehouseJournalDetails.QuantityReceiptReturn + WarehouseJournalDetails.QuantityReceiptAdjustment AS QuantityReceipt, " + "\r\n";
            queryString = queryString + "                   WarehouseJournalDetails.QuantityIssueSelling, WarehouseJournalDetails.QuantityIssueTransfer, WarehouseJournalDetails.QuantityIssueAdjustment, WarehouseJournalDetails.QuantityIssueSelling + WarehouseJournalDetails.QuantityIssueTransfer + WarehouseJournalDetails.QuantityIssueAdjustment AS QuantityIssue, " + "\r\n";
            queryString = queryString + "                   WarehouseJournalDetails.QuantityBegin + WarehouseJournalDetails.QuantityReceiptPickup + WarehouseJournalDetails.QuantityReceiptPurchasing + WarehouseJournalDetails.QuantityReceiptTransfer + WarehouseJournalDetails.QuantityReceiptReturn + WarehouseJournalDetails.QuantityReceiptAdjustment - WarehouseJournalDetails.QuantityIssueSelling - WarehouseJournalDetails.QuantityIssueTransfer - WarehouseJournalDetails.QuantityIssueAdjustment AS QuantityEnd, " + "\r\n";
            queryString = queryString + "                   WarehouseJournalDetails.QuantityOnPurchasing, WarehouseJournalDetails.QuantityOnPickup, WarehouseJournalDetails.QuantityOnTransit, " + "\r\n";
            queryString = queryString + "                   WarehouseJournalDetails.MovementMIN, WarehouseJournalDetails.MovementMAX, WarehouseJournalDetails.MovementAVG " + "\r\n";


            queryString = queryString + "       FROM       (" + "\r\n";

            //--BEGIN-INPUT-OUTPUT-END.END
            queryString = queryString + "                   SELECT  GoodsReceiptDetails.EntryDate, GoodsReceiptDetails.GoodsReceiptDetailID, GoodsReceiptDetails.CommodityID, GoodsReceiptDetails.BatchEntryDate, GoodsReceiptDetails.LocationID, GoodsReceiptDetails.WarehouseID, GoodsReceiptDetails.BinLocationID, GoodsReceiptDetails.PickupID, GoodsReceiptDetails.GoodsIssueID, GoodsReceiptDetails.WarehouseAdjustmentID, GoodsReceiptDetails.PackID, GoodsReceiptDetails.CartonID, GoodsReceiptDetails.PalletID, " + "\r\n";
            queryString = queryString + "                           GoodsReceiptDetailUnionMasters.QuantityBegin, GoodsReceiptDetailUnionMasters.QuantityReceiptPickup, GoodsReceiptDetailUnionMasters.QuantityReceiptPurchasing, GoodsReceiptDetailUnionMasters.QuantityReceiptTransfer, GoodsReceiptDetailUnionMasters.QuantityReceiptReturn, GoodsReceiptDetailUnionMasters.QuantityReceiptAdjustment, GoodsReceiptDetailUnionMasters.QuantityIssueSelling, GoodsReceiptDetailUnionMasters.QuantityIssueTransfer, GoodsReceiptDetailUnionMasters.QuantityIssueAdjustment, 0 AS QuantityOnPurchasing, 0 AS QuantityOnPickup, 0 AS QuantityOnTransit, GoodsReceiptDetailUnionMasters.MovementMIN, GoodsReceiptDetailUnionMasters.MovementMAX, GoodsReceiptDetailUnionMasters.MovementAVG " + "\r\n";


            queryString = queryString + "                   FROM   (" + "\r\n";
            queryString = queryString + "                           SELECT  GoodsReceiptDetailUnions.GoodsReceiptDetailID, " + "\r\n";
            queryString = queryString + "                                   SUM(QuantityBegin) AS QuantityBegin, SUM(QuantityReceiptPickup) AS QuantityReceiptPickup, SUM(QuantityReceiptPurchasing) AS QuantityReceiptPurchasing, SUM(QuantityReceiptTransfer) AS QuantityReceiptTransfer, SUM(QuantityReceiptReturn) AS QuantityReceiptReturn, SUM(QuantityReceiptAdjustment) AS QuantityReceiptAdjustment, " + "\r\n";
            queryString = queryString + "                                   SUM(QuantityIssueSelling) AS QuantityIssueSelling, SUM(QuantityIssueTransfer) AS QuantityIssueTransfer, SUM(QuantityIssueAdjustment) AS QuantityIssueAdjustment, " + "\r\n";
            queryString = queryString + "                                   MIN(MovementDate) AS MovementMIN, MAX(MovementDate) AS MovementMAX, SUM((QuantityIssueSelling + QuantityIssueTransfer + QuantityIssueAdjustment) * MovementDate) / SUM(QuantityIssueSelling + QuantityIssueTransfer + QuantityIssueAdjustment) AS MovementAVG " + "\r\n";
            queryString = queryString + "                           FROM    (" + "\r\n";


            //1.BEGINING
            //  BEGINING.GoodsReceipts
            queryString = queryString + "                                   SELECT      GoodsReceiptDetails.GoodsReceiptDetailID, ROUND(GoodsReceiptDetails.Quantity - GoodsReceiptDetails.QuantityIssue, " + (int)GlobalEnums.rndQuantity + ") AS QuantityBegin, 0 AS QuantityReceiptPickup, 0 AS QuantityReceiptPurchasing, 0 AS QuantityReceiptTransfer, 0 AS QuantityReceiptReturn, 0 AS QuantityReceiptAdjustment, 0 AS QuantityIssueSelling, 0 AS QuantityIssueTransfer, 0 AS QuantityIssueAdjustment, NULL AS MovementDate " + "\r\n";
            queryString = queryString + "                                   FROM        GoodsReceiptDetails " + "\r\n";
            queryString = queryString + "                                   WHERE       GoodsReceiptDetails.EntryDate < @LocalFromDate AND GoodsReceiptDetails.Quantity > GoodsReceiptDetails.QuantityIssue " + (isLocationID ? " AND GoodsReceiptDetails.LocationID = @LocalLocationID" : "") + (isWarehouseID ? " AND GoodsReceiptDetails.WarehouseID = @LocalWarehouseID" : "") + "\r\n";

            queryString = queryString + "                                   UNION ALL " + "\r\n";
            //  BEGINING.UNDO (CAC CAU SQL CHO GoodsIssues, WarehouseAdjustments LA HOAN TOAN GIONG NHAU. LUU Y T/H DAT BIET: WarehouseAdjustments.Quantity < 0)
            //  BEGINING.UNDO.GoodsIssues
            queryString = queryString + "                                   SELECT      GoodsReceiptDetails.GoodsReceiptDetailID, GoodsIssueDetails.Quantity AS QuantityBegin, 0 AS QuantityReceiptPickup, 0 AS QuantityReceiptPurchasing, 0 AS QuantityReceiptTransfer, 0 AS QuantityReceiptReturn, 0 AS QuantityReceiptAdjustment, 0 AS QuantityIssueSelling, 0 AS QuantityIssueTransfer, 0 AS QuantityIssueAdjustment, NULL AS MovementDate " + "\r\n";
            queryString = queryString + "                                   FROM        GoodsReceiptDetails INNER JOIN " + "\r\n";
            queryString = queryString + "                                               GoodsIssueDetails ON GoodsReceiptDetails.GoodsReceiptDetailID = GoodsIssueDetails.GoodsReceiptDetailID AND GoodsReceiptDetails.EntryDate < @LocalFromDate AND GoodsIssueDetails.EntryDate >= @LocalFromDate " + (isLocationID ? " AND GoodsReceiptDetails.LocationID = @LocalLocationID " : "") + (isWarehouseID ? " AND GoodsReceiptDetails.WarehouseID = @LocalWarehouseID" : "") + "\r\n";

            queryString = queryString + "                                   UNION ALL " + "\r\n";
            //  BEGINING.UNDO.WarehouseAdjustments
            queryString = queryString + "                                   SELECT      GoodsReceiptDetails.GoodsReceiptDetailID, -WarehouseAdjustmentDetails.Quantity AS QuantityBegin, 0 AS QuantityReceiptPickup, 0 AS QuantityReceiptPurchasing, 0 AS QuantityReceiptTransfer, 0 AS QuantityReceiptReturn, 0 AS QuantityReceiptAdjustment, 0 AS QuantityIssueSelling, 0 AS QuantityIssueTransfer, 0 AS QuantityIssueAdjustment, NULL AS MovementDate " + "\r\n";
            queryString = queryString + "                                   FROM        GoodsReceiptDetails INNER JOIN " + "\r\n";
            queryString = queryString + "                                               WarehouseAdjustmentDetails ON GoodsReceiptDetails.GoodsReceiptDetailID = WarehouseAdjustmentDetails.GoodsReceiptDetailID AND GoodsReceiptDetails.EntryDate < @LocalFromDate AND WarehouseAdjustmentDetails.EntryDate >= @LocalFromDate AND WarehouseAdjustmentDetails.Quantity < 0 " + (isLocationID ? " AND GoodsReceiptDetails.LocationID = @LocalLocationID " : "") + (isWarehouseID ? " AND GoodsReceiptDetails.WarehouseID = @LocalWarehouseID" : "") + "\r\n";



            //2.INTPUT
            queryString = queryString + "                                   UNION ALL " + "\r\n";
            queryString = queryString + "                                   SELECT      GoodsReceiptDetails.GoodsReceiptDetailID, 0 AS QuantityBegin, " + "\r\n";
            queryString = queryString + "                                               CASE WHEN GoodsReceiptDetails.GoodsReceiptTypeID = " + (int)GlobalEnums.GoodsReceiptTypeID.Pickup + " THEN GoodsReceiptDetails.Quantity ELSE 0 END AS QuantityReceiptPickup, " + "\r\n";
            queryString = queryString + "                                               CASE WHEN GoodsReceiptDetails.GoodsReceiptTypeID = " + (int)GlobalEnums.GoodsReceiptTypeID.PurchaseInvoice + " THEN GoodsReceiptDetails.Quantity ELSE 0 END AS QuantityReceiptPurchasing, " + "\r\n";
            queryString = queryString + "                                               CASE WHEN GoodsReceiptDetails.GoodsReceiptTypeID = " + (int)GlobalEnums.GoodsReceiptTypeID.GoodsIssueTransfer + " THEN GoodsReceiptDetails.Quantity ELSE 0 END AS QuantityReceiptTransfer, " + "\r\n";
            queryString = queryString + "                                               CASE WHEN GoodsReceiptDetails.GoodsReceiptTypeID = " + (int)GlobalEnums.GoodsReceiptTypeID.SalesReturn + " THEN GoodsReceiptDetails.Quantity ELSE 0 END AS QuantityReceiptReturn, " + "\r\n";
            queryString = queryString + "                                               CASE WHEN GoodsReceiptDetails.GoodsReceiptTypeID = " + (int)GlobalEnums.GoodsReceiptTypeID.WarehouseAdjustments + " THEN GoodsReceiptDetails.Quantity ELSE 0 END AS QuantityReceiptAdjustment, " + "\r\n";
            queryString = queryString + "                                               0 AS QuantityIssueSelling, 0 AS QuantityIssueTransfer, 0 AS QuantityIssueAdjustment, NULL AS MovementDate " + "\r\n";
            queryString = queryString + "                                   FROM        GoodsReceiptDetails " + "\r\n";
            queryString = queryString + "                                   WHERE       GoodsReceiptDetails.EntryDate >= @LocalFromDate AND GoodsReceiptDetails.EntryDate <= @LocalToDate " + (isLocationID ? " AND GoodsReceiptDetails.LocationID = @LocalLocationID " : "") + (isWarehouseID ? " AND GoodsReceiptDetails.WarehouseID = @LocalWarehouseID" : "") + "\r\n";



            //3.OUTPUT (CAC CAU SQL CHO GoodsIssues, WarehouseAdjustments LA HOAN TOAN GIONG NHAU. LUU Y T/H DAT BIET: WarehouseAdjustments.Quantity < 0)
            queryString = queryString + "                                   UNION ALL " + "\r\n";
            //GoodsIssueDetails + "\r\n";
            queryString = queryString + "                                   SELECT      GoodsIssueDetails.GoodsReceiptDetailID, 0 AS QuantityBegin, 0 AS QuantityReceiptPickup, 0 AS QuantityReceiptPurchasing, 0 AS QuantityReceiptTransfer, 0 AS QuantityReceiptReturn, 0 AS QuantityReceiptAdjustment, CASE WHEN DeliveryAdviceDetailID IS NULL THEN 0 ELSE GoodsIssueDetails.Quantity END AS QuantityIssueSelling, CASE WHEN TransferOrderDetailID IS NULL THEN 0 ELSE GoodsIssueDetails.Quantity END AS QuantityIssueTransfer, 0 AS QuantityIssueAdjustment, 0 AS MovementDate " + "\r\n"; //DATEDIFF(DAY, GoodsReceiptDetails.EntryDate, GoodsIssueDetails.EntryDate) AS MovementDate
            queryString = queryString + "                                   FROM        GoodsIssueDetails " + "\r\n";
            queryString = queryString + "                                   WHERE       GoodsIssueDetails.EntryDate >= @LocalFromDate AND GoodsIssueDetails.EntryDate <= @LocalToDate " + (isLocationID ? " AND GoodsIssueDetails.LocationID = @LocalLocationID " : "") + (isWarehouseID ? " AND GoodsIssueDetails.WarehouseID = @LocalWarehouseID" : "") + "\r\n";

            queryString = queryString + "                                   UNION ALL " + "\r\n";
            //WarehouseAdjustmentDetails
            queryString = queryString + "                                   SELECT      WarehouseAdjustmentDetails.GoodsReceiptDetailID, 0 AS QuantityBegin, 0 AS QuantityReceiptPickup, 0 AS QuantityReceiptPurchasing, 0 AS QuantityReceiptTransfer, 0 AS QuantityReceiptReturn, 0 AS QuantityReceiptAdjustment, 0 AS QuantityIssueSelling, 0 AS QuantityIssueTransfer, -WarehouseAdjustmentDetails.Quantity AS QuantityIssueAdjustment, 0 AS MovementDate " + "\r\n"; //DATEDIFF(DAY, GoodsReceiptDetails.EntryDate, WarehouseAdjustmentDetails.EntryDate) AS MovementDate
            queryString = queryString + "                                   FROM        WarehouseAdjustmentDetails " + "\r\n";
            queryString = queryString + "                                   WHERE       WarehouseAdjustmentDetails.EntryDate >= @LocalFromDate AND WarehouseAdjustmentDetails.EntryDate <= @LocalToDate AND WarehouseAdjustmentDetails.Quantity < 0 " + (isLocationID ? " AND WarehouseAdjustmentDetails.LocationID = @LocalLocationID " : "") + (isWarehouseID ? " AND WarehouseAdjustmentDetails.WarehouseID = @LocalWarehouseID" : "") + "\r\n";



            queryString = queryString + "                                   ) AS GoodsReceiptDetailUnions " + "\r\n";
            queryString = queryString + "                           GROUP BY GoodsReceiptDetailUnions.GoodsReceiptDetailID " + "\r\n";
            queryString = queryString + "                           ) AS GoodsReceiptDetailUnionMasters INNER JOIN " + "\r\n";
            queryString = queryString + "                           GoodsReceiptDetails ON GoodsReceiptDetailUnionMasters.GoodsReceiptDetailID = GoodsReceiptDetails.GoodsReceiptDetailID " + "\r\n";

            //--BEGIN-INPUT-OUTPUT-END.END

            queryString = queryString + "                   UNION ALL " + "\r\n";
            //--ON INPUT.BEGIN (CAC CAU SQL DUNG CHO EWHInputVoucherTypeID.EInvoice, EWHInputVoucherTypeID.EReturn, EWHInputVoucherTypeID.EWHTransfer, EWHInputVoucherTypeID.EWHAdjust, EWHInputVoucherTypeID.EWHAssemblyMaster, EWHInputVoucherTypeID.EWHAssemblyDetail LA HOAN TOAN GIONG NHAU)
            //EWHInputVoucherTypeID.EInvoice
            queryString = queryString + "                   SELECT  NULL AS EntryDate, NULL AS GoodsReceiptDetailID, PickupDetails.CommodityID, PickupDetails.BatchEntryDate, PickupDetails.LocationID, NULL AS WarehouseID, NULL AS BinLocationID, NULL AS PickupID, NULL AS GoodsIssueID, NULL AS WarehouseAdjustmentID, PickupDetails.PackID, PickupDetails.CartonID, PickupDetails.PalletID, " + "\r\n";
            queryString = queryString + "                           0 AS QuantityBegin, 0 AS QuantityReceiptPickup, 0 AS QuantityReceiptPurchasing, 0 AS QuantityReceiptTransfer, 0 AS QuantityReceiptReturn, 0 AS QuantityReceiptAdjustment, 0 AS QuantityIssueSelling, 0 AS QuantityIssueTransfer, 0 AS QuantityIssueAdjustment, 0 AS QuantityOnPurchasing, (PickupDetails.Quantity - PickupDetails.QuantityReceipt) AS QuantityOnPickup, 0 AS QuantityOnTransit, 0 AS MovementMIN, 0 AS MovementMAX, 0 AS MovementAVG " + "\r\n";
            queryString = queryString + "                   FROM    PickupDetails " + "\r\n";
            queryString = queryString + "                   WHERE   PickupDetails.EntryDate <= @LocalToDate AND PickupDetails.Quantity > PickupDetails.QuantityReceipt " + (isLocationID || isWarehouseID ? " AND PickupDetails.LocationID = @LocalLocationID" : "") + "\r\n";

            queryString = queryString + "                   UNION ALL " + "\r\n";

            queryString = queryString + "                   SELECT  NULL AS EntryDate, NULL AS GoodsReceiptDetailID, GoodsReceiptDetails.CommodityID, GoodsReceiptDetails.BatchEntryDate, GoodsReceiptDetails.LocationID, NULL AS WarehouseID, NULL AS BinLocationID, NULL AS PickupID, NULL AS GoodsIssueID, NULL AS WarehouseAdjustmentID, GoodsReceiptDetails.PackID, GoodsReceiptDetails.CartonID, GoodsReceiptDetails.PalletID, " + "\r\n";
            queryString = queryString + "                           0 AS QuantityBegin, 0 AS QuantityReceiptPickup, 0 AS QuantityReceiptPurchasing, 0 AS QuantityReceiptTransfer, 0 AS QuantityReceiptReturn, 0 AS QuantityReceiptAdjustment, 0 AS QuantityIssueSelling, 0 AS QuantityIssueTransfer, 0 AS QuantityIssueAdjustment, 0 AS QuantityOnPurchasing, GoodsReceiptDetails.Quantity AS QuantityOnPickup, 0 AS QuantityOnTransit, 0 AS MovementMIN, 0 AS MovementMAX, 0 AS MovementAVG " + "\r\n";
            queryString = queryString + "                   FROM    Pickups INNER JOIN " + "\r\n";
            queryString = queryString + "                           GoodsReceiptDetails ON Pickups.PickupID = GoodsReceiptDetails.PickupID AND Pickups.EntryDate <= @LocalToDate AND GoodsReceiptDetails.EntryDate > @LocalToDate " + (isLocationID || isWarehouseID ? " AND GoodsReceiptDetails.LocationID = @LocalLocationID" : "") + "\r\n";

            queryString = queryString + "                   UNION ALL " + "\r\n";
            //EWHInputVoucherTypeID.EWHTransfer
            queryString = queryString + "                   SELECT  NULL AS EntryDate, NULL AS GoodsReceiptDetailID, GoodsIssueTransferDetails.CommodityID, GoodsIssueTransferDetails.BatchEntryDate, GoodsIssueTransferDetails.LocationID, NULL AS WarehouseID, NULL AS BinLocationID, NULL AS PickupID, NULL AS GoodsIssueID, NULL AS WarehouseAdjustmentID, GoodsIssueTransferDetails.PackID, GoodsIssueTransferDetails.CartonID, GoodsIssueTransferDetails.PalletID, " + "\r\n";
            queryString = queryString + "                           0 AS QuantityBegin, 0 AS QuantityReceiptPickup, 0 AS QuantityReceiptPurchasing, 0 AS QuantityReceiptTransfer, 0 AS QuantityReceiptReturn, 0 AS QuantityReceiptAdjustment, 0 AS QuantityIssueSelling, 0 AS QuantityIssueTransfer, 0 AS QuantityIssueAdjustment, 0 AS QuantityOnPurchasing, 0 AS QuantityOnPickup, (GoodsIssueTransferDetails.Quantity - GoodsIssueTransferDetails.QuantityReceipt) AS QuantityOnTransit, 0 AS MovementMIN, 0 AS MovementMAX, 0 AS MovementAVG " + "\r\n";
            queryString = queryString + "                   FROM    GoodsIssues INNER JOIN " + "\r\n";
            queryString = queryString + "                           GoodsIssueTransferDetails ON GoodsIssues.GoodsIssueID = GoodsIssueTransferDetails.GoodsIssueID AND GoodsIssueTransferDetails.EntryDate <= @LocalToDate AND GoodsIssueTransferDetails.Quantity > GoodsIssueTransferDetails.QuantityReceipt " + (isLocationID || isWarehouseID ? " AND GoodsIssueTransferDetails.LocationID = @LocalLocationID" : "") + "\r\n";

            queryString = queryString + "                   UNION ALL " + "\r\n";

            queryString = queryString + "                   SELECT  NULL AS EntryDate, NULL AS GoodsReceiptDetailID, GoodsReceiptDetails.CommodityID, GoodsReceiptDetails.BatchEntryDate, GoodsReceiptDetails.LocationID, NULL AS WarehouseID, NULL AS BinLocationID, NULL AS PickupID, NULL AS GoodsIssueID, NULL AS WarehouseAdjustmentID, GoodsReceiptDetails.PackID, GoodsReceiptDetails.CartonID, GoodsReceiptDetails.PalletID, " + "\r\n";
            queryString = queryString + "                           0 AS QuantityBegin, 0 AS QuantityReceiptPickup, 0 AS QuantityReceiptPurchasing, 0 AS QuantityReceiptTransfer, 0 AS QuantityReceiptReturn, 0 AS QuantityReceiptAdjustment, 0 AS QuantityIssueSelling, 0 AS QuantityIssueTransfer, 0 AS QuantityIssueAdjustment, 0 AS QuantityOnPurchasing, 0 AS QuantityOnPickup, GoodsReceiptDetails.Quantity AS QuantityOnTransit, 0 AS MovementMIN, 0 AS MovementMAX, 0 AS MovementAVG " + "\r\n";
            queryString = queryString + "                   FROM    GoodsIssues INNER JOIN " + "\r\n";
            queryString = queryString + "                           GoodsReceiptDetails ON GoodsIssues.GoodsIssueID = GoodsReceiptDetails.GoodsIssueID AND GoodsIssues.EntryDate <= @LocalToDate AND GoodsReceiptDetails.EntryDate > @LocalToDate " + (isLocationID || isWarehouseID ? " AND GoodsReceiptDetails.LocationID = @LocalLocationID" : "") + "\r\n";
            //--ON INPUT.END

            queryString = queryString + "                   ) AS WarehouseJournalDetails " + "\r\n";

            queryString = queryString + "                   INNER JOIN Commodities ON WarehouseJournalDetails.CommodityID = Commodities.CommodityID " + "\r\n";
            queryString = queryString + "                   INNER JOIN CommodityCategories ON Commodities.CommodityCategoryID = CommodityCategories.CommodityCategoryID " + "\r\n";
            queryString = queryString + "                   INNER JOIN Locations ON WarehouseJournalDetails.LocationID = Locations.LocationID " + "\r\n";

            queryString = queryString + "                   LEFT JOIN Warehouses ON WarehouseJournalDetails.WarehouseID = Warehouses.WarehouseID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN BinLocations ON WarehouseJournalDetails.BinLocationID = BinLocations.BinLocationID " + "\r\n";

            queryString = queryString + "                   LEFT JOIN Pickups ON WarehouseJournalDetails.PickupID = Pickups.PickupID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN GoodsIssues ON WarehouseJournalDetails.GoodsIssueID = GoodsIssues.GoodsIssueID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Warehouses SourceWarehouses ON GoodsIssues.WarehouseID = SourceWarehouses.WarehouseID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN WarehouseAdjustments ON WarehouseJournalDetails.WarehouseAdjustmentID = WarehouseAdjustments.WarehouseAdjustmentID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN WarehouseAdjustmentTypes ON WarehouseAdjustments.WarehouseAdjustmentTypeID = WarehouseAdjustmentTypes.WarehouseAdjustmentTypeID " + "\r\n";

            queryString = queryString + "                   LEFT JOIN Packs ON WarehouseJournalDetails.PackID = Packs.PackID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Cartons ON WarehouseJournalDetails.CartonID = Cartons.CartonID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Pallets ON WarehouseJournalDetails.PalletID = Pallets.PalletID " + "\r\n";


            queryString = queryString + "    END " + "\r\n";

            return queryString;

        }

        private void ProductionJournals()
        {
            string queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime, @FillingLineIDs varchar(3999), @BatchMasterIDs varchar(3999), @BatchTypeIDs varchar(3999), @CommodityCategoryIDs varchar(3999), @CommodityTypeIDs varchar(3999), @CommodityIDs varchar(3999), @WithCartons bit, @WithPacks bit " + "\r\n";

            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";


            queryString = queryString + "       DECLARE     @LocalUserID Int, @LocalFromDate DateTime, @LocalToDate DateTime, @NullDate DateTime, @LocalFillingLineIDs varchar(3999), @LocalBatchMasterIDs varchar(3999), @LocalBatchTypeIDs varchar(3999), @LocalCommodityCategoryIDs varchar(3999), @LocalCommodityTypeIDs varchar(3999), @LocalCommodityIDs varchar(3999), @LocalWithCartons bit, @LocalWithPacks bit " + "\r\n";

            queryString = queryString + "       SET         @LocalUserID = @UserID          SET @LocalFromDate = @FromDate          SET @LocalToDate = @ToDate  " + "\r\n";
            queryString = queryString + "       SET         @LocalFillingLineIDs = @FillingLineIDs                                  SET @LocalBatchMasterIDs = @BatchMasterIDs                  SET @LocalBatchTypeIDs = @BatchTypeIDs " + "\r\n";
            queryString = queryString + "       SET         @LocalCommodityCategoryIDs = @CommodityCategoryIDs                      SET @LocalCommodityTypeIDs = @CommodityTypeIDs              SET @LocalCommodityIDs = @CommodityIDs          SET @LocalWithCartons = @WithCartons          SET @LocalWithPacks = @WithPacks" + "\r\n";


            queryString = queryString + "       IF         (@LocalFillingLineIDs <> '') " + "\r\n";
            queryString = queryString + "                   " + this.ProductionJournalSQL(true) + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "                   " + this.ProductionJournalSQL(false) + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("ProductionJournals", queryString);

        }

        private string ProductionJournalSQL(bool isFillingLineID)
        {
            string queryString = "";

            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       IF         (@LocalBatchMasterIDs <> '') " + "\r\n";
            queryString = queryString + "                   " + this.ProductionJournalSQL(isFillingLineID, true) + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "                   " + this.ProductionJournalSQL(isFillingLineID, false) + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            return queryString;
        }

        private string ProductionJournalSQL(bool isFillingLineID, bool isBatchMasterID)
        {
            string queryString = "";

            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       IF         (@LocalBatchTypeIDs <> '') " + "\r\n";
            queryString = queryString + "                   " + this.ProductionJournalSQL(isFillingLineID, isBatchMasterID, true) + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "                   " + this.ProductionJournalSQL(isFillingLineID, isBatchMasterID, false) + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            return queryString;
        }

        private string ProductionJournalSQL(bool isFillingLineID, bool isBatchMasterID, bool isBatchTypeID)
        {
            string queryString = "";

            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       IF         (@LocalCommodityCategoryIDs <> '') " + "\r\n";
            queryString = queryString + "                   " + this.ProductionJournalSQL(isFillingLineID, isBatchMasterID, isBatchTypeID, true) + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "                   " + this.ProductionJournalSQL(isFillingLineID, isBatchMasterID, isBatchTypeID, false) + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            return queryString;
        }

        private string ProductionJournalSQL(bool isFillingLineID, bool isBatchMasterID, bool isBatchTypeID, bool isCommodityCategoryID)
        {
            string queryString = "";

            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       IF         (@LocalCommodityTypeIDs <> '') " + "\r\n";
            queryString = queryString + "                   " + this.ProductionJournalSQL(isFillingLineID, isBatchMasterID, isBatchTypeID, isCommodityCategoryID, true) + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "                   " + this.ProductionJournalSQL(isFillingLineID, isBatchMasterID, isBatchTypeID, isCommodityCategoryID, false) + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            return queryString;
        }

        private string ProductionJournalSQL(bool isFillingLineID, bool isBatchMasterID, bool isBatchTypeID, bool isCommodityCategoryID, bool isCommodityTypeID)
        {
            string queryString = "";

            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       IF         (@LocalCommodityIDs <> '') " + "\r\n";
            queryString = queryString + "                   " + this.ProductionJournalSQL(isFillingLineID, isBatchMasterID, isBatchTypeID, isCommodityCategoryID, isCommodityTypeID, true) + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "                   " + this.ProductionJournalSQL(isFillingLineID, isBatchMasterID, isBatchTypeID, isCommodityCategoryID, isCommodityTypeID, false) + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            return queryString;
        }

        private string ProductionJournalSQL(bool isFillingLineID, bool isBatchMasterID, bool isBatchTypeID, bool isCommodityCategoryID, bool isCommodityTypeID, bool isCommodityID)
        {
            string queryString = "";

            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       IF         (@LocalWithCartons = 1) " + "\r\n";
            queryString = queryString + "                   " + this.ProductionJournalSQL(isFillingLineID, isBatchMasterID, isBatchTypeID, isCommodityCategoryID, isCommodityTypeID, isCommodityID, true) + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "                   " + this.ProductionJournalSQL(isFillingLineID, isBatchMasterID, isBatchTypeID, isCommodityCategoryID, isCommodityTypeID, isCommodityID, false) + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            return queryString;
        }

        private string ProductionJournalSQL(bool isFillingLineID, bool isBatchMasterID, bool isBatchTypeID, bool isCommodityCategoryID, bool isCommodityTypeID, bool isCommodityID, bool isWithCartons)
        {
            string queryString = "";

            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       IF         (@LocalWithPacks = 1) " + "\r\n";
            queryString = queryString + "                   " + this.ProductionJournalSQL(isFillingLineID, isBatchMasterID, isBatchTypeID, isCommodityCategoryID, isCommodityTypeID, isCommodityID, isWithCartons, true) + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "                   " + this.ProductionJournalSQL(isFillingLineID, isBatchMasterID, isBatchTypeID, isCommodityCategoryID, isCommodityTypeID, isCommodityID, isWithCartons, false) + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            return queryString;
        }

        private string ProductionJournalSQL(bool isFillingLineID, bool isBatchMasterID, bool isBatchTypeID, bool isCommodityCategoryID, bool isCommodityTypeID, bool isCommodityID, bool isWithCartons, bool isWithPacks)
        {
            string queryString = " " + "\r\n";

            queryString = queryString + "   SELECT      Batches.BatchMasterID, Batches.BatchID, Batches.EntryDate, dbo.GetExpiryDate(Batches.EntryDate, Commodities.Shelflife) AS ExpiryDate, Batches.Code AS BatchCode, Batches.LotID, Batches.LotCode, BatchTypes.BatchTypeID, BatchTypes.Code AS BatchTypeCode, Batches.FillingLineID, FillingLines.Code AS FillingLineCode, Commodities.Code AS CommodityCode, Commodities.Name AS CommodityName, Commodities.APICode, Commodities.Unit, Commodities.PackageSize, " + "\r\n";
            queryString = queryString + "               Pallets.PalletID, Pallets.EntryDate AS PalletDate, Pallets.Code AS PalletCode, Pallets.MinPackDate, Pallets.MaxPackDate, " + (isWithCartons ? "Cartons.CartonID, Cartons.EntryDate AS CartonDate, Cartons.Code AS CartonCode, " : "NULL AS CartonID, @NullDate AS CartonDate, NULL AS CartonCode, ") + (isWithCartons & isWithPacks ? "Packs.PackID, Packs.EntryDate AS PackDate, Packs.Code AS PackCode, " : "NULL AS PackID, @NullDate AS PackDate, NULL AS PackCode, ") + (isWithCartons & isWithPacks ? "CAST(1 AS Float) / Cartons.PackCounts AS CartonCounts" : (isWithCartons ? "1 AS CartonCounts" : "Pallets.CartonCounts")) + ", " + (isWithCartons & isWithPacks ? "1 AS PackCounts" : (isWithCartons ? "Cartons.PackCounts" : "Pallets.PackCounts")) + "\r\n";
            queryString = queryString + "   FROM        Batches " + "\r\n";
            queryString = queryString + "               INNER JOIN BatchTypes ON " + (isBatchMasterID ? "Batches.BatchMasterID IN (SELECT Id FROM dbo.SplitToIntList (@LocalBatchMasterIDs)) " : "Batches.EntryDate >= @LocalFromDate AND Batches.EntryDate <= @LocalToDate") + (isBatchTypeID ? " AND Batches.BatchTypeID IN (SELECT Id FROM dbo.SplitToIntList (@LocalBatchTypeIDs)) " : "") + " AND Batches.BatchTypeID = BatchTypes.BatchTypeID " + "\r\n";
            queryString = queryString + "               INNER JOIN FillingLines ON " + (isFillingLineID ? "Batches.FillingLineID IN (SELECT Id FROM dbo.SplitToIntList (@LocalFillingLineIDs)) AND " : "") + " Batches.FillingLineID = FillingLines.FillingLineID " + "\r\n";
            queryString = queryString + "               INNER JOIN Commodities ON " + (isCommodityID ? "Commodities.CommodityID IN (SELECT Id FROM dbo.SplitToIntList (@LocalCommodityIDs)) AND " : "") + (isCommodityTypeID ? "Commodities.CommodityTypeID IN (SELECT Id FROM dbo.SplitToIntList (@CommodityTypeIDs)) AND " : "") + " Batches.CommodityID = Commodities.CommodityID " + "\r\n";
            queryString = queryString + "               INNER JOIN CommodityCategories ON " + (isCommodityCategoryID ? "Commodities.CommodityCategoryID IN (SELECT Id FROM dbo.SplitToIntList (@LocalCommodityCategoryIDs)) AND " : "") + " Commodities.CommodityCategoryID = CommodityCategories.CommodityCategoryID " + "\r\n";
            queryString = queryString + "               INNER JOIN Pallets ON Batches.BatchID = Pallets.BatchID " + "\r\n";

            if (isWithCartons)
                queryString = queryString + "           INNER JOIN Cartons ON Pallets.PalletID = Cartons.PalletID " + "\r\n";

            if (isWithCartons && isWithPacks)
                queryString = queryString + "           INNER JOIN Packs ON Cartons.CartonID = Packs.CartonID " + "\r\n";

            return queryString;
        }


        #region DeletedBarcodes
        private void DeletedBarcodeJournals()
        {
            string queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime, @BatchMasterIDs varchar(3999), @BatchTypeIDs varchar(3999) " + "\r\n";

            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";


            queryString = queryString + "       DECLARE     @LocalUserID Int, @LocalFromDate DateTime, @LocalToDate DateTime, @LocalBatchMasterIDs varchar(3999), @LocalBatchTypeIDs varchar(3999) " + "\r\n";

            queryString = queryString + "       SET         @LocalUserID = @UserID          SET @LocalFromDate = @FromDate          SET @LocalToDate = @ToDate  " + "\r\n";
            queryString = queryString + "       SET         @LocalBatchMasterIDs = @BatchMasterIDs                                  SET @LocalBatchTypeIDs = @BatchTypeIDs " + "\r\n";

            queryString = queryString + "       IF         (@LocalBatchMasterIDs <> '') " + "\r\n";
            queryString = queryString + "                   " + this.DeletedBarcodeJournalsSQL(true) + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "                   " + this.DeletedBarcodeJournalsSQL(false) + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("DeletedBarcodeJournals", queryString);

        }


        private string DeletedBarcodeJournalsSQL(bool isBatchMasterID)
        {
            string queryString = "";

            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       IF         (@LocalBatchTypeIDs <> '') " + "\r\n";
            queryString = queryString + "                   " + this.DeletedBarcodeJournalsSQL(isBatchMasterID, true) + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "                   " + this.DeletedBarcodeJournalsSQL(isBatchMasterID, false) + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            return queryString;
        }

        private string DeletedBarcodeJournalsSQL(bool isBatchMasterID, bool isBatchTypeID)
        {
            string queryString = " " + "\r\n";

            queryString = queryString + "   SELECT     'PACKS' AS GroupType, Batches.BatchMasterID, Batches.BatchID, Batches.EntryDate, dbo.GetExpiryDate(Batches.EntryDate, Commodities.Shelflife) AS ExpiryDate, Batches.Code AS BatchCode, Batches.LotID, Batches.LotCode, BatchTypes.BatchTypeID, BatchTypes.Code AS BatchTypeCode, Batches.FillingLineID, FillingLines.Code AS FillingLineCode, Commodities.Code AS CommodityCode, Commodities.Name AS CommodityName, Commodities.APICode, Commodities.Unit, Commodities.PackageSize, " + "\r\n";
            queryString = queryString + "               DeletedPacks.PackID, NULL AS CartonID, NULL AS PalletID, DeletedPacks.EntryDate AS BarcodeDate, DeletedPacks.Code, 0 AS PackCounts, 0 AS CartonCounts, DeletedPacks.DeletedDate, DeletedPacks.Remarks " + "\r\n";
            queryString = queryString + "   FROM        Batches " + "\r\n";
            queryString = queryString + "               INNER JOIN BatchTypes ON " + (isBatchMasterID ? "Batches.BatchMasterID IN (SELECT Id FROM dbo.SplitToIntList (@LocalBatchMasterIDs)) " : "Batches.EntryDate >= @LocalFromDate AND Batches.EntryDate <= @LocalToDate") + (isBatchTypeID ? " AND Batches.BatchTypeID IN (SELECT Id FROM dbo.SplitToIntList (@LocalBatchTypeIDs)) " : "") + " AND Batches.BatchTypeID = BatchTypes.BatchTypeID " + "\r\n";
            queryString = queryString + "               INNER JOIN FillingLines ON Batches.FillingLineID = FillingLines.FillingLineID " + "\r\n";
            queryString = queryString + "               INNER JOIN Commodities ON Batches.CommodityID = Commodities.CommodityID " + "\r\n";
            queryString = queryString + "               INNER JOIN DeletedPacks ON Batches.BatchID = DeletedPacks.BatchID " + "\r\n";
            queryString = queryString + "   UNION ALL " + "\r\n";
            queryString = queryString + "   SELECT     'CARTONS' AS GroupType, Batches.BatchMasterID, Batches.BatchID, Batches.EntryDate, dbo.GetExpiryDate(Batches.EntryDate, Commodities.Shelflife) AS ExpiryDate, Batches.Code AS BatchCode, Batches.LotID, Batches.LotCode, BatchTypes.BatchTypeID, BatchTypes.Code AS BatchTypeCode, Batches.FillingLineID, FillingLines.Code AS FillingLineCode, Commodities.Code AS CommodityCode, Commodities.Name AS CommodityName, Commodities.APICode, Commodities.Unit, Commodities.PackageSize, " + "\r\n";
            queryString = queryString + "               NULL AS PackID, DeletedCartons.CartonID, NULL AS PalletID, DeletedCartons.EntryDate AS BarcodeDate, DeletedCartons.Code, DeletedCartons.PackCounts, 0 AS CartonCounts, DeletedCartons.DeletedDate, DeletedCartons.Remarks " + "\r\n";
            queryString = queryString + "   FROM        Batches " + "\r\n";
            queryString = queryString + "               INNER JOIN BatchTypes ON " + (isBatchMasterID ? "Batches.BatchMasterID IN (SELECT Id FROM dbo.SplitToIntList (@LocalBatchMasterIDs)) " : "Batches.EntryDate >= @LocalFromDate AND Batches.EntryDate <= @LocalToDate") + (isBatchTypeID ? " AND Batches.BatchTypeID IN (SELECT Id FROM dbo.SplitToIntList (@LocalBatchTypeIDs)) " : "") + " AND Batches.BatchTypeID = BatchTypes.BatchTypeID " + "\r\n";
            queryString = queryString + "               INNER JOIN FillingLines ON Batches.FillingLineID = FillingLines.FillingLineID " + "\r\n";
            queryString = queryString + "               INNER JOIN Commodities ON Batches.CommodityID = Commodities.CommodityID " + "\r\n";
            queryString = queryString + "               INNER JOIN DeletedCartons ON Batches.BatchID = DeletedCartons.BatchID " + "\r\n";
            queryString = queryString + "   UNION ALL " + "\r\n";
            queryString = queryString + "   SELECT     ' PALLETS' AS GroupType, Batches.BatchMasterID, Batches.BatchID, Batches.EntryDate, dbo.GetExpiryDate(Batches.EntryDate, Commodities.Shelflife) AS ExpiryDate, Batches.Code AS BatchCode, Batches.LotID, Batches.LotCode, BatchTypes.BatchTypeID, BatchTypes.Code AS BatchTypeCode, Batches.FillingLineID, FillingLines.Code AS FillingLineCode, Commodities.Code AS CommodityCode, Commodities.Name AS CommodityName, Commodities.APICode, Commodities.Unit, Commodities.PackageSize, " + "\r\n";
            queryString = queryString + "               NULL AS PackID, NULL AS CartonID, DeletedPallets.PalletID, DeletedPallets.EntryDate AS BarcodeDate, DeletedPallets.Code, DeletedPallets.PackCounts, DeletedPallets.CartonCounts, DeletedPallets.DeletedDate, DeletedPallets.Remarks " + "\r\n";
            queryString = queryString + "   FROM        Batches " + "\r\n";
            queryString = queryString + "               INNER JOIN BatchTypes ON " + (isBatchMasterID ? "Batches.BatchMasterID IN (SELECT Id FROM dbo.SplitToIntList (@LocalBatchMasterIDs)) " : "Batches.EntryDate >= @LocalFromDate AND Batches.EntryDate <= @LocalToDate") + (isBatchTypeID ? " AND Batches.BatchTypeID IN (SELECT Id FROM dbo.SplitToIntList (@LocalBatchTypeIDs)) " : "") + " AND Batches.BatchTypeID = BatchTypes.BatchTypeID " + "\r\n";
            queryString = queryString + "               INNER JOIN FillingLines ON Batches.FillingLineID = FillingLines.FillingLineID " + "\r\n";
            queryString = queryString + "               INNER JOIN Commodities ON Batches.CommodityID = Commodities.CommodityID " + "\r\n";
            queryString = queryString + "               INNER JOIN DeletedPallets ON Batches.BatchID = DeletedPallets.BatchID " + "\r\n";

            return queryString;
        }
        #endregion DeletedBarcodes

    }
}
