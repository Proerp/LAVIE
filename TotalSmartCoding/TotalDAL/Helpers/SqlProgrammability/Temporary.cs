using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability
{
    public class Temporary
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public Temporary(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
        }


        #region Backup for GetPendingDeliveryAdviceDetails, GetPendingTransferOrderDetails
        //????By Transfer Order? By Warehouse Issue, receipt   =>> WarehouseID, WarehouseReceiptID

        private bool alwaysTrue = true;
        //HERE: WE ALWAYS AND ONLY CALL GetPendingDeliveryAdviceDetails AFTER SAVE SUCCESSFUL
        //AT GoodsIssues VIEWS: WE DON'T ALLOW TO USE CURRENT RESULT FROM GetPendingDeliveryAdviceDetails IF THE LAST SAVE IS NOT SUCCESSFULLY. WHEN SAVE SUCCESSFUL, THE GetPendingDeliveryAdviceDetails IS CALL IMMEDIATELY
        //SO => HERE: WE DON'T CARE BOTH: @DeliveryAdviceDetailIDs AND BuildSQLEdit
        private void GetPendingDeliveryAdviceDetails()
        {
            string queryString;

            queryString = " @LocationID Int, @GoodsIssueID Int, @DeliveryAdviceID Int, @CustomerID Int, @DeliveryAdviceDetailIDs varchar(3999), @IsReadonly bit " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "   BEGIN " + "\r\n";

            queryString = queryString + "       IF  (@DeliveryAdviceID <> 0) " + "\r\n";
            queryString = queryString + "           " + this.BuildSQLDeliveryAdvice(true) + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "           " + this.BuildSQLDeliveryAdvice(false) + "\r\n";

            queryString = queryString + "   END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetPendingDeliveryAdviceDetails", queryString);
        }

        private string BuildSQLDeliveryAdvice(bool isDeliveryAdviceID)
        {
            string queryString = "";
            if (this.alwaysTrue)
                queryString = queryString + "           " + this.BuildSQLDeliveryAdviceDeliveryAdviceDetailIDs(isDeliveryAdviceID, false) + "\r\n";
            else
            {
                queryString = queryString + "   BEGIN " + "\r\n";
                queryString = queryString + "       IF  (@DeliveryAdviceDetailIDs <> '') " + "\r\n";
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
                queryString = queryString + "                   ORDER BY DeliveryAdviceDetails.EntryDate, DeliveryAdviceDetails.DeliveryAdviceID, DeliveryAdviceDetails.DeliveryAdviceDetailID " + "\r\n";
                queryString = queryString + "               END " + "\r\n";
            }
            else
            {
                queryString = queryString + "       IF (@GoodsIssueID <= 0) " + "\r\n";
                queryString = queryString + "               BEGIN " + "\r\n";
                queryString = queryString + "                   " + this.BuildSQLNew(isDeliveryAdviceID, isDeliveryAdviceDetailIDs) + "\r\n";
                queryString = queryString + "                   ORDER BY DeliveryAdviceDetails.EntryDate, DeliveryAdviceDetails.DeliveryAdviceID, DeliveryAdviceDetails.DeliveryAdviceDetailID " + "\r\n";
                queryString = queryString + "               END " + "\r\n";
                queryString = queryString + "       ELSE " + "\r\n";

                queryString = queryString + "               IF (@IsReadonly = 1) " + "\r\n";
                queryString = queryString + "                   BEGIN " + "\r\n";
                queryString = queryString + "                       " + this.BuildSQLEdit(isDeliveryAdviceID, isDeliveryAdviceDetailIDs) + "\r\n";
                queryString = queryString + "                       ORDER BY DeliveryAdviceDetails.EntryDate, DeliveryAdviceDetails.DeliveryAdviceID, DeliveryAdviceDetails.DeliveryAdviceDetailID " + "\r\n";
                queryString = queryString + "                   END " + "\r\n";

                queryString = queryString + "               ELSE " + "\r\n"; //FULL SELECT FOR EDIT MODE

                queryString = queryString + "                   BEGIN " + "\r\n";
                queryString = queryString + "                       " + this.BuildSQLNew(isDeliveryAdviceID, isDeliveryAdviceDetailIDs) + " WHERE DeliveryAdviceDetails.DeliveryAdviceDetailID NOT IN (SELECT DeliveryAdviceDetailID FROM GoodsIssueDetails WHERE GoodsIssueID = @GoodsIssueID) " + "\r\n";
                queryString = queryString + "                       UNION ALL " + "\r\n";
                queryString = queryString + "                       " + this.BuildSQLEdit(isDeliveryAdviceID, isDeliveryAdviceDetailIDs) + "\r\n";
                queryString = queryString + "                       ORDER BY DeliveryAdviceDetails.EntryDate, DeliveryAdviceDetails.DeliveryAdviceID, DeliveryAdviceDetails.DeliveryAdviceDetailID " + "\r\n";
                queryString = queryString + "                   END " + "\r\n";
            }

            queryString = queryString + "   END " + "\r\n";

            return queryString;
        }

        private string BuildSQLNew(bool isDeliveryAdviceID, bool isDeliveryAdviceDetailIDs)
        {
            string queryString = "";

            queryString = queryString + "       SELECT      DeliveryAdviceDetails.LocationID, DeliveryAdviceDetails.DeliveryAdviceID, DeliveryAdviceDetails.DeliveryAdviceDetailID, DeliveryAdviceDetails.Reference AS DeliveryAdviceReference, DeliveryAdviceDetails.EntryDate AS DeliveryAdviceEntryDate, " + "\r\n";
            queryString = queryString + "                   Commodities.CommodityID, Commodities.Code AS CommodityCode, Commodities.Name AS CommodityName, Commodities.PackageSize, Commodities.Volume, Commodities.PackageVolume, DeliveryAdviceDetails.BatchID, Batches.Code AS BatchCode, " + "\r\n";
            queryString = queryString + "                   ROUND(DeliveryAdviceDetails.Quantity - DeliveryAdviceDetails.QuantityIssue,  " + (int)GlobalEnums.rndQuantity + ") AS QuantityRemains, CAST(0 AS decimal(18, 2)) AS Quantity, ROUND(DeliveryAdviceDetails.LineVolume - DeliveryAdviceDetails.LineVolumeIssue,  " + (int)GlobalEnums.rndVolume + ") AS LineVolumeRemains, CAST(0 AS decimal(18, 2)) AS LineVolume, DeliveryAdviceDetails.Remarks, CAST(0 AS bit) AS IsSelected " + "\r\n";

            queryString = queryString + "       FROM        DeliveryAdviceDetails " + "\r\n";
            queryString = queryString + "                   INNER JOIN Commodities ON " + (isDeliveryAdviceID ? " DeliveryAdviceDetails.DeliveryAdviceID = @DeliveryAdviceID " : "DeliveryAdviceDetails.LocationID = @LocationID AND DeliveryAdviceDetails.CustomerID = @CustomerID ") + " AND DeliveryAdviceDetails.Approved = 1 AND ROUND(DeliveryAdviceDetails.Quantity - DeliveryAdviceDetails.QuantityIssue, " + (int)GlobalEnums.rndQuantity + ") > 0 AND DeliveryAdviceDetails.CommodityID = Commodities.CommodityID" + (isDeliveryAdviceDetailIDs ? " AND DeliveryAdviceDetails.DeliveryAdviceDetailID NOT IN (SELECT Id FROM dbo.SplitToIntList (@DeliveryAdviceDetailIDs))" : "") + "\r\n";
            queryString = queryString + "                   LEFT JOIN Batches ON DeliveryAdviceDetails.BatchID = Batches.BatchID " + "\r\n";
            return queryString;
        }

        private string BuildSQLEdit(bool isDeliveryAdviceID, bool isDeliveryAdviceDetailIDs)
        {
            string queryString = "";

            queryString = queryString + "       SELECT      DeliveryAdviceDetails.LocationID, DeliveryAdviceDetails.DeliveryAdviceID, DeliveryAdviceDetails.DeliveryAdviceDetailID, DeliveryAdviceDetails.Reference AS DeliveryAdviceReference, DeliveryAdviceDetails.EntryDate AS DeliveryAdviceEntryDate, " + "\r\n";
            queryString = queryString + "                   Commodities.CommodityID, Commodities.Code AS CommodityCode, Commodities.Name AS CommodityName, Commodities.PackageSize, Commodities.Volume, Commodities.PackageVolume, DeliveryAdviceDetails.BatchID, Batches.Code AS BatchCode, " + "\r\n";
            queryString = queryString + "                   ROUND(DeliveryAdviceDetails.Quantity - DeliveryAdviceDetails.QuantityIssue + GoodsIssueDetails.Quantity,  " + (int)GlobalEnums.rndQuantity + ") AS QuantityRemains, CAST(0 AS decimal(18, 2)) AS Quantity, ROUND(DeliveryAdviceDetails.LineVolume - DeliveryAdviceDetails.LineVolumeIssue + GoodsIssueDetails.LineVolume,  " + (int)GlobalEnums.rndVolume + ") AS LineVolumeRemains, CAST(0 AS decimal(18, 2)) AS LineVolume, DeliveryAdviceDetails.Remarks, CAST(0 AS bit) AS IsSelected " + "\r\n";

            queryString = queryString + "       FROM        DeliveryAdviceDetails " + "\r\n";
            queryString = queryString + "                   INNER JOIN GoodsIssueDetails ON GoodsIssueDetails.GoodsIssueID = @GoodsIssueID AND DeliveryAdviceDetails.DeliveryAdviceDetailID = GoodsIssueDetails.DeliveryAdviceDetailID" + (isDeliveryAdviceDetailIDs ? " AND DeliveryAdviceDetails.DeliveryAdviceDetailID NOT IN (SELECT Id FROM dbo.SplitToIntList (@DeliveryAdviceDetailIDs))" : "") + "\r\n";
            queryString = queryString + "                   INNER JOIN Commodities ON DeliveryAdviceDetails.CommodityID = Commodities.CommodityID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Batches ON DeliveryAdviceDetails.BatchID = Batches.BatchID " + "\r\n";

            return queryString;
        }

        #endregion Backup for GetPendingDeliveryAdviceDetails, GetPendingTransferOrderDetails





        #region Backup for GoodsReceipts

        private void GetPendingPickups()
        {
            string queryString = " @LocationID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       SELECT          Pickups.PickupID, Pickups.Reference AS PickupReference, Pickups.EntryDate AS PickupEntryDate, Pickups.WarehouseID, Warehouses.Code AS WarehouseCode, Warehouses.Name AS WarehouseName, " + (int)GlobalEnums.GoodsReceiptTypeID.Pickup + " AS GoodsReceiptTypeID, (SELECT TOP 1 Name FROM GoodsReceiptTypes WHERE GoodsReceiptTypeID = " + (int)GlobalEnums.GoodsReceiptTypeID.Pickup + ") AS GoodsReceiptTypeName, Pickups.Description, Pickups.Remarks " + "\r\n";
            queryString = queryString + "       FROM            Pickups " + "\r\n";
            queryString = queryString + "                       INNER JOIN Warehouses ON Pickups.PickupID IN (SELECT PickupID FROM PickupDetails WHERE LocationID = @LocationID AND Approved = 1 AND ROUND(Quantity - QuantityReceipt, " + (int)GlobalEnums.rndQuantity + ") > 0) AND Pickups.WarehouseID = Warehouses.WarehouseID " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetPendingPickups", queryString);
        }

        private void GetPendingPickupWarehouses()
        {
            string queryString = " @LocationID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       SELECT          Warehouses.WarehouseID, Warehouses.Code AS WarehouseCode, Warehouses.Name AS WarehouseName, " + (int)GlobalEnums.GoodsReceiptTypeID.Pickup + " AS GoodsReceiptTypeID, (SELECT TOP 1 Name FROM GoodsReceiptTypes WHERE GoodsReceiptTypeID = " + (int)GlobalEnums.GoodsReceiptTypeID.Pickup + ") AS GoodsReceiptTypeName " + "\r\n";
            queryString = queryString + "       FROM           (SELECT DISTINCT WarehouseID FROM PickupDetails WHERE LocationID = @LocationID AND Approved = 1 AND ROUND(Quantity - QuantityReceipt, " + (int)GlobalEnums.rndQuantity + ") > 0) WarehousePENDING " + "\r\n";
            queryString = queryString + "                       INNER JOIN Warehouses ON WarehousePENDING.WarehouseID = Warehouses.WarehouseID " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetPendingPickupWarehouses", queryString);
        }



        private void GetPendingPickupDetails()
        {
            string queryString;

            queryString = " @LocationID Int, @GoodsReceiptID Int, @PickupID Int, @WarehouseID Int, @PickupDetailIDs varchar(3999), @IsReadonly bit " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "   BEGIN " + "\r\n";

            queryString = queryString + "       IF  (@PickupID <> 0) " + "\r\n";
            queryString = queryString + "           " + this.BuildSQLPickup(true) + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "           " + this.BuildSQLPickup(false) + "\r\n";

            queryString = queryString + "   END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetPendingPickupDetails", queryString);
        }

        private string BuildSQLPickup(bool isPickupID)
        {
            string queryString = "";
            queryString = queryString + "   BEGIN " + "\r\n";
            queryString = queryString + "       IF  (@PickupDetailIDs <> '') " + "\r\n";
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
            queryString = queryString + "                   " + this.BuildSQLPickupNew(isPickupID, isPickupDetailIDs) + "\r\n";
            queryString = queryString + "                   ORDER BY PickupDetails.EntryDate, PickupDetails.PickupID, PickupDetails.PickupDetailID " + "\r\n";
            queryString = queryString + "               END " + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";

            queryString = queryString + "               IF (@IsReadonly = 1) " + "\r\n";
            queryString = queryString + "                   BEGIN " + "\r\n";
            queryString = queryString + "                       " + this.BuildSQLPickupEdit(isPickupID, isPickupDetailIDs) + "\r\n";
            queryString = queryString + "                       ORDER BY PickupDetails.EntryDate, PickupDetails.PickupID, PickupDetails.PickupDetailID " + "\r\n";
            queryString = queryString + "                   END " + "\r\n";

            queryString = queryString + "               ELSE " + "\r\n"; //FULL SELECT FOR EDIT MODE

            queryString = queryString + "                   BEGIN " + "\r\n";
            queryString = queryString + "                       " + this.BuildSQLPickupNew(isPickupID, isPickupDetailIDs) + " WHERE PickupDetails.PickupDetailID NOT IN (SELECT PickupDetailID FROM GoodsReceiptDetails WHERE GoodsReceiptID = @GoodsReceiptID) " + "\r\n";
            queryString = queryString + "                       UNION ALL " + "\r\n";
            queryString = queryString + "                       " + this.BuildSQLPickupEdit(isPickupID, isPickupDetailIDs) + "\r\n";
            queryString = queryString + "                       ORDER BY PickupDetails.EntryDate, PickupDetails.PickupID, PickupDetails.PickupDetailID " + "\r\n";
            queryString = queryString + "                   END " + "\r\n";

            queryString = queryString + "   END " + "\r\n";

            return queryString;
        }

        private string BuildSQLPickupNew(bool isPickupID, bool isPickupDetailIDs)
        {
            string queryString = "";

            queryString = queryString + "       SELECT      PickupDetails.PickupID, PickupDetails.PickupDetailID, PickupDetails.Reference AS PickupReference, PickupDetails.EntryDate AS PickupEntryDate, " + "\r\n";
            queryString = queryString + "                   Commodities.CommodityID, Commodities.Code AS CommodityCode, Commodities.Name AS CommodityName, PickupDetails.BatchID, PickupDetails.BatchEntryDate, PickupDetails.BinLocationID, BinLocations.Code AS BinLocationCode, PickupDetails.PackID, Packs.Code AS PackCode, PickupDetails.CartonID, Cartons.Code AS CartonCode, PickupDetails.PalletID, Pallets.Code AS PalletCode, " + "\r\n";
            queryString = queryString + "                   ROUND(PickupDetails.Quantity - PickupDetails.QuantityReceipt,  " + (int)GlobalEnums.rndQuantity + ") AS QuantityRemains, CAST(0 AS decimal(18, 2)) AS Quantity, ROUND(PickupDetails.LineVolume - PickupDetails.LineVolumeReceipt,  " + (int)GlobalEnums.rndVolume + ") AS LineVolumeRemains, PickupDetails.LineVolume, PickupDetails.PackCounts, PickupDetails.CartonCounts, PickupDetails.PalletCounts, PickupDetails.Remarks, CAST(1 AS bit) AS IsSelected " + "\r\n";

            queryString = queryString + "       FROM        PickupDetails " + "\r\n";
            queryString = queryString + "                   INNER JOIN Commodities ON " + (isPickupID ? " PickupDetails.PickupID = @PickupID " : "PickupDetails.LocationID = @LocationID AND PickupDetails.WarehouseID = @WarehouseID ") + " AND PickupDetails.Approved = 1 AND ROUND(PickupDetails.Quantity - PickupDetails.QuantityReceipt, " + (int)GlobalEnums.rndQuantity + ") > 0 AND PickupDetails.CommodityID = Commodities.CommodityID " + (isPickupDetailIDs ? " AND PickupDetails.PickupDetailID NOT IN (SELECT Id FROM dbo.SplitToIntList (@PickupDetailIDs))" : "") + "\r\n";
            queryString = queryString + "                   INNER JOIN BinLocations ON PickupDetails.BinLocationID = BinLocations.BinLocationID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Packs ON PickupDetails.PackID = Packs.PackID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Cartons ON PickupDetails.CartonID = Cartons.CartonID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Pallets ON PickupDetails.PalletID = Pallets.PalletID " + "\r\n";

            return queryString;
        }

        private string BuildSQLPickupEdit(bool isPickupID, bool isPickupDetailIDs)
        {
            string queryString = "";

            queryString = queryString + "       SELECT      PickupDetails.PickupID, PickupDetails.PickupDetailID, PickupDetails.Reference AS PickupReference, PickupDetails.EntryDate AS PickupEntryDate, " + "\r\n";
            queryString = queryString + "                   Commodities.CommodityID, Commodities.Code AS CommodityCode, Commodities.Name AS CommodityName, PickupDetails.BatchID, PickupDetails.BatchEntryDate, PickupDetails.BinLocationID, BinLocations.Code AS BinLocationCode, PickupDetails.PackID, Packs.Code AS PackCode, PickupDetails.CartonID, Cartons.Code AS CartonCode, PickupDetails.PalletID, Pallets.Code AS PalletCode, " + "\r\n";
            queryString = queryString + "                   ROUND(PickupDetails.Quantity - PickupDetails.QuantityReceipt + GoodsReceiptDetails.Quantity,  " + (int)GlobalEnums.rndQuantity + ") AS QuantityRemains, CAST(0 AS decimal(18, 2)) AS Quantity, ROUND(PickupDetails.LineVolume - PickupDetails.LineVolumeReceipt + GoodsReceiptDetails.LineVolume,  " + (int)GlobalEnums.rndVolume + ") AS LineVolumeRemains, PickupDetails.LineVolume, PickupDetails.PackCounts, PickupDetails.CartonCounts, PickupDetails.PalletCounts, PickupDetails.Remarks, CAST(1 AS bit) AS IsSelected " + "\r\n";

            queryString = queryString + "       FROM        PickupDetails " + "\r\n";
            queryString = queryString + "                   INNER JOIN GoodsReceiptDetails ON GoodsReceiptDetails.GoodsReceiptID = @GoodsReceiptID AND PickupDetails.PickupDetailID = GoodsReceiptDetails.PickupDetailID" + (isPickupDetailIDs ? " AND PickupDetails.PickupDetailID NOT IN (SELECT Id FROM dbo.SplitToIntList (@PickupDetailIDs))" : "") + "\r\n";
            queryString = queryString + "                   INNER JOIN Commodities ON PickupDetails.CommodityID = Commodities.CommodityID " + "\r\n";
            queryString = queryString + "                   INNER JOIN BinLocations ON PickupDetails.BinLocationID = BinLocations.BinLocationID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Packs ON PickupDetails.PackID = Packs.PackID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Cartons ON PickupDetails.CartonID = Cartons.CartonID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Pallets ON PickupDetails.PalletID = Pallets.PalletID " + "\r\n";

            return queryString;
        }



        #region WarehouseAdjustment

        private void GetPendingWarehouseAdjustmentDetails()
        {
            string queryString;

            queryString = " @LocationID Int, @GoodsReceiptID Int, @WarehouseAdjustmentID Int, @WarehouseID Int, @WarehouseAdjustmentDetailIDs varchar(3999), @IsReadonly bit " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "   BEGIN " + "\r\n";

            queryString = queryString + "       IF  (@WarehouseAdjustmentID <> 0) " + "\r\n";
            queryString = queryString + "           " + this.BuildSQLWarehouseAdjustment(true) + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "           " + this.BuildSQLWarehouseAdjustment(false) + "\r\n";

            queryString = queryString + "   END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetPendingWarehouseAdjustmentDetails", queryString);
        }

        private string BuildSQLWarehouseAdjustment(bool isWarehouseAdjustmentID)
        {
            string queryString = "";
            queryString = queryString + "   BEGIN " + "\r\n";
            queryString = queryString + "       IF  (@WarehouseAdjustmentDetailIDs <> '') " + "\r\n";
            queryString = queryString + "           " + this.BuildSQLWarehouseAdjustmentWarehouseAdjustmentDetailIDs(isWarehouseAdjustmentID, true) + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "           " + this.BuildSQLWarehouseAdjustmentWarehouseAdjustmentDetailIDs(isWarehouseAdjustmentID, false) + "\r\n";
            queryString = queryString + "   END " + "\r\n";

            return queryString;
        }

        private string BuildSQLWarehouseAdjustmentWarehouseAdjustmentDetailIDs(bool isWarehouseAdjustmentID, bool isWarehouseAdjustmentDetailIDs)
        {
            string queryString = "";
            queryString = queryString + "   BEGIN " + "\r\n";

            queryString = queryString + "       IF (@GoodsReceiptID <= 0) " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   " + this.BuildSQLWarehouseAdjustmentNew(isWarehouseAdjustmentID, isWarehouseAdjustmentDetailIDs) + "\r\n";
            queryString = queryString + "                   ORDER BY WarehouseAdjustmentDetails.EntryDate, WarehouseAdjustmentDetails.WarehouseAdjustmentID, WarehouseAdjustmentDetails.WarehouseAdjustmentDetailID " + "\r\n";
            queryString = queryString + "               END " + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";

            queryString = queryString + "               IF (@IsReadonly = 1) " + "\r\n";
            queryString = queryString + "                   BEGIN " + "\r\n";
            queryString = queryString + "                       " + this.BuildSQLWarehouseAdjustmentEdit(isWarehouseAdjustmentID, isWarehouseAdjustmentDetailIDs) + "\r\n";
            queryString = queryString + "                       ORDER BY WarehouseAdjustmentDetails.EntryDate, WarehouseAdjustmentDetails.WarehouseAdjustmentID, WarehouseAdjustmentDetails.WarehouseAdjustmentDetailID " + "\r\n";
            queryString = queryString + "                   END " + "\r\n";

            queryString = queryString + "               ELSE " + "\r\n"; //FULL SELECT FOR EDIT MODE

            queryString = queryString + "                   BEGIN " + "\r\n";
            queryString = queryString + "                       " + this.BuildSQLWarehouseAdjustmentNew(isWarehouseAdjustmentID, isWarehouseAdjustmentDetailIDs) + " WHERE WarehouseAdjustmentDetails.WarehouseAdjustmentDetailID NOT IN (SELECT WarehouseAdjustmentDetailID FROM GoodsReceiptDetails WHERE GoodsReceiptID = @GoodsReceiptID) " + "\r\n";
            queryString = queryString + "                       UNION ALL " + "\r\n";
            queryString = queryString + "                       " + this.BuildSQLWarehouseAdjustmentEdit(isWarehouseAdjustmentID, isWarehouseAdjustmentDetailIDs) + "\r\n";
            queryString = queryString + "                       ORDER BY WarehouseAdjustmentDetails.EntryDate, WarehouseAdjustmentDetails.WarehouseAdjustmentID, WarehouseAdjustmentDetails.WarehouseAdjustmentDetailID " + "\r\n";
            queryString = queryString + "                   END " + "\r\n";

            queryString = queryString + "   END " + "\r\n";

            return queryString;
        }

        private string BuildSQLWarehouseAdjustmentNew(bool isWarehouseAdjustmentID, bool isWarehouseAdjustmentDetailIDs)
        {
            string queryString = "";

            queryString = queryString + "       SELECT      WarehouseAdjustmentDetails.WarehouseAdjustmentID, WarehouseAdjustmentDetails.WarehouseAdjustmentDetailID, WarehouseAdjustmentDetails.Reference AS WarehouseAdjustmentReference, WarehouseAdjustmentDetails.EntryDate AS WarehouseAdjustmentEntryDate, " + "\r\n";
            queryString = queryString + "                   Commodities.CommodityID, Commodities.Code AS CommodityCode, Commodities.Name AS CommodityName, WarehouseAdjustmentDetails.BatchID, WarehouseAdjustmentDetails.BatchEntryDate, WarehouseAdjustmentDetails.BinLocationID, BinLocations.Code AS BinLocationCode, WarehouseAdjustmentDetails.PackID, Packs.Code AS PackCode, WarehouseAdjustmentDetails.CartonID, Cartons.Code AS CartonCode, WarehouseAdjustmentDetails.PalletID, Pallets.Code AS PalletCode, " + "\r\n";
            queryString = queryString + "                   ROUND(WarehouseAdjustmentDetails.Quantity - WarehouseAdjustmentDetails.QuantityReceipt,  " + (int)GlobalEnums.rndQuantity + ") AS QuantityRemains, CAST(0 AS decimal(18, 2)) AS Quantity, ROUND(WarehouseAdjustmentDetails.LineVolume - WarehouseAdjustmentDetails.LineVolumeReceipt,  " + (int)GlobalEnums.rndVolume + ") AS LineVolumeRemains, WarehouseAdjustmentDetails.LineVolume, WarehouseAdjustmentDetails.PackCounts, WarehouseAdjustmentDetails.CartonCounts, WarehouseAdjustmentDetails.PalletCounts, WarehouseAdjustmentDetails.Remarks, CAST(1 AS bit) AS IsSelected " + "\r\n";

            queryString = queryString + "       FROM        WarehouseAdjustmentDetails " + "\r\n";
            queryString = queryString + "                   INNER JOIN Commodities ON " + (isWarehouseAdjustmentID ? " WarehouseAdjustmentDetails.WarehouseAdjustmentID = @WarehouseAdjustmentID " : "WarehouseAdjustmentDetails.LocationID = @LocationID AND WarehouseAdjustmentDetails.WarehouseID = @WarehouseID ") + " AND WarehouseAdjustmentDetails.Quantity > 0 AND ROUND(WarehouseAdjustmentDetails.Quantity - WarehouseAdjustmentDetails.QuantityReceipt, " + (int)GlobalEnums.rndQuantity + ") > 0 AND WarehouseAdjustmentDetails.CommodityID = Commodities.CommodityID " + (isWarehouseAdjustmentDetailIDs ? " AND WarehouseAdjustmentDetails.WarehouseAdjustmentDetailID NOT IN (SELECT Id FROM dbo.SplitToIntList (@WarehouseAdjustmentDetailIDs))" : "") + "\r\n";
            queryString = queryString + "                   INNER JOIN BinLocations ON WarehouseAdjustmentDetails.BinLocationID = BinLocations.BinLocationID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Packs ON WarehouseAdjustmentDetails.PackID = Packs.PackID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Cartons ON WarehouseAdjustmentDetails.CartonID = Cartons.CartonID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Pallets ON WarehouseAdjustmentDetails.PalletID = Pallets.PalletID " + "\r\n";

            return queryString;
        }

        private string BuildSQLWarehouseAdjustmentEdit(bool isWarehouseAdjustmentID, bool isWarehouseAdjustmentDetailIDs)
        {
            string queryString = "";

            queryString = queryString + "       SELECT      WarehouseAdjustmentDetails.WarehouseAdjustmentID, WarehouseAdjustmentDetails.WarehouseAdjustmentDetailID, WarehouseAdjustmentDetails.Reference AS WarehouseAdjustmentReference, WarehouseAdjustmentDetails.EntryDate AS WarehouseAdjustmentEntryDate, " + "\r\n";
            queryString = queryString + "                   Commodities.CommodityID, Commodities.Code AS CommodityCode, Commodities.Name AS CommodityName, WarehouseAdjustmentDetails.BatchID, WarehouseAdjustmentDetails.BatchEntryDate, WarehouseAdjustmentDetails.BinLocationID, BinLocations.Code AS BinLocationCode, WarehouseAdjustmentDetails.PackID, Packs.Code AS PackCode, WarehouseAdjustmentDetails.CartonID, Cartons.Code AS CartonCode, WarehouseAdjustmentDetails.PalletID, Pallets.Code AS PalletCode, " + "\r\n";
            queryString = queryString + "                   ROUND(WarehouseAdjustmentDetails.Quantity - WarehouseAdjustmentDetails.QuantityReceipt + GoodsReceiptDetails.Quantity,  " + (int)GlobalEnums.rndQuantity + ") AS QuantityRemains, CAST(0 AS decimal(18, 2)) AS Quantity, ROUND(WarehouseAdjustmentDetails.LineVolume - WarehouseAdjustmentDetails.LineVolumeReceipt + GoodsReceiptDetails.LineVolume,  " + (int)GlobalEnums.rndVolume + ") AS LineVolumeRemains, WarehouseAdjustmentDetails.LineVolume, WarehouseAdjustmentDetails.PackCounts, WarehouseAdjustmentDetails.CartonCounts, WarehouseAdjustmentDetails.PalletCounts, WarehouseAdjustmentDetails.Remarks, CAST(1 AS bit) AS IsSelected " + "\r\n";

            queryString = queryString + "       FROM        WarehouseAdjustmentDetails " + "\r\n";
            queryString = queryString + "                   INNER JOIN GoodsReceiptDetails ON GoodsReceiptDetails.GoodsReceiptID = @GoodsReceiptID AND WarehouseAdjustmentDetails.WarehouseAdjustmentDetailID = GoodsReceiptDetails.WarehouseAdjustmentDetailID" + (isWarehouseAdjustmentDetailIDs ? " AND WarehouseAdjustmentDetails.WarehouseAdjustmentDetailID NOT IN (SELECT Id FROM dbo.SplitToIntList (@WarehouseAdjustmentDetailIDs))" : "") + "\r\n";
            queryString = queryString + "                   INNER JOIN Commodities ON WarehouseAdjustmentDetails.CommodityID = Commodities.CommodityID " + "\r\n";
            queryString = queryString + "                   INNER JOIN BinLocations ON WarehouseAdjustmentDetails.BinLocationID = BinLocations.BinLocationID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Packs ON WarehouseAdjustmentDetails.PackID = Packs.PackID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Cartons ON WarehouseAdjustmentDetails.CartonID = Cartons.CartonID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Pallets ON WarehouseAdjustmentDetails.PalletID = Pallets.PalletID " + "\r\n";

            return queryString;
        }
        
        #endregion WarehouseAdjustment

        #endregion Y




    }

}
