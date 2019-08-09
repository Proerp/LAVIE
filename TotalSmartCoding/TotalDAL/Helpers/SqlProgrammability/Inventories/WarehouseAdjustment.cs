using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Inventories
{
    public class WarehouseAdjustment
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public WarehouseAdjustment(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetWarehouseAdjustmentIndexes();


            this.GetWarehouseAdjustmentViewDetails();

            this.WarehouseAdjustmentSaveRelative();
            this.WarehouseAdjustmentPostSaveValidate();

            this.WarehouseAdjustmentApproved();
            this.WarehouseAdjustmentEditable();

            this.WarehouseAdjustmentToggleApproved();

            this.WarehouseAdjustmentInitReference();
        }


        private void GetWarehouseAdjustmentIndexes()
        {
            string queryString;

            queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      WarehouseAdjustments.WarehouseAdjustmentID, CAST(WarehouseAdjustments.EntryDate AS DATE) AS EntryDate, WarehouseAdjustments.Reference, Locations.Code AS LocationCode, WarehouseAdjustments.WarehouseAdjustmentTypeID, WarehouseAdjustmentTypes.Name AS WarehouseAdjustmentTypeName, WarehouseAdjustments.AdjustmentJobs, WarehouseAdjustments.Description, WarehouseAdjustments.TotalQuantity, WarehouseAdjustments.TotalLineVolume, WarehouseAdjustments.Approved " + "\r\n";
            queryString = queryString + "       FROM        WarehouseAdjustments " + "\r\n";
            queryString = queryString + "                   INNER JOIN Locations ON WarehouseAdjustments.EntryDate >= @FromDate AND WarehouseAdjustments.EntryDate <= @ToDate AND WarehouseAdjustments.OrganizationalUnitID IN (SELECT OrganizationalUnitID FROM AccessControls WHERE UserID = @UserID AND NMVNTaskID = " + (int)TotalBase.Enums.GlobalEnums.NmvnTaskID.WarehouseAdjustment + " AND AccessControls.AccessLevel > 0) AND Locations.LocationID = WarehouseAdjustments.LocationID " + "\r\n";
            queryString = queryString + "                   INNER JOIN WarehouseAdjustmentTypes ON WarehouseAdjustments.WarehouseAdjustmentTypeID = WarehouseAdjustmentTypes.WarehouseAdjustmentTypeID " + "\r\n";
            queryString = queryString + "       " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetWarehouseAdjustmentIndexes", queryString);
        }


        #region X


        private void GetWarehouseAdjustmentViewDetails()
        {
            string queryString;

            queryString = " @WarehouseAdjustmentID Int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      WarehouseAdjustmentDetails.WarehouseAdjustmentDetailID, WarehouseAdjustmentDetails.WarehouseAdjustmentID, WarehouseAdjustmentDetails.GoodsReceiptID, WarehouseAdjustmentDetails.GoodsReceiptDetailID, GoodsReceiptDetails.Reference AS GoodsReceiptReference, GoodsReceiptDetails.EntryDate AS GoodsReceiptEntryDate," + "\r\n";
            queryString = queryString + "                   Commodities.CommodityID, Commodities.Code AS CommodityCode, Commodities.Name AS CommodityName, BinLocations.BinLocationID, BinLocations.Code AS BinLocationCode, " + "\r\n";
            queryString = queryString + "                   WarehouseAdjustmentDetails.PackID, Packs.Code AS PackCode, WarehouseAdjustmentDetails.CartonID, Cartons.Code AS CartonCode, WarehouseAdjustmentDetails.PalletID, Pallets.Code AS PalletCode, " + "\r\n";
            queryString = queryString + "                   ROUND(GoodsReceiptDetails.Quantity - GoodsReceiptDetails.QuantityIssue + (-WarehouseAdjustmentDetails.Quantity), " + (int)GlobalEnums.rndQuantity + ") AS QuantityAvailable, ROUND(GoodsReceiptDetails.LineVolume - GoodsReceiptDetails.LineVolumeIssue + (-WarehouseAdjustmentDetails.LineVolume), " + (int)GlobalEnums.rndVolume + ") AS LineVolumeAvailable, WarehouseAdjustmentDetails.Quantity, WarehouseAdjustmentDetails.LineVolume, WarehouseAdjustmentDetails.Remarks " + "\r\n";
            queryString = queryString + "       FROM        WarehouseAdjustmentDetails " + "\r\n";
            queryString = queryString + "                   INNER JOIN Commodities ON WarehouseAdjustmentDetails.WarehouseAdjustmentID = @WarehouseAdjustmentID AND WarehouseAdjustmentDetails.CommodityID = Commodities.CommodityID " + "\r\n";
            queryString = queryString + "                   INNER JOIN BinLocations ON WarehouseAdjustmentDetails.BinLocationID = BinLocations.BinLocationID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN GoodsReceiptDetails ON WarehouseAdjustmentDetails.GoodsReceiptDetailID = GoodsReceiptDetails.GoodsReceiptDetailID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Packs ON WarehouseAdjustmentDetails.PackID = Packs.PackID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Cartons ON WarehouseAdjustmentDetails.CartonID = Cartons.CartonID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN Pallets ON WarehouseAdjustmentDetails.PalletID = Pallets.PalletID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetWarehouseAdjustmentViewDetails", queryString);
        }



        private void WarehouseAdjustmentSaveRelative()
        {
            string queryString = " @EntityID int, @SaveRelativeOption int " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            //queryString = queryString + "   IF (SELECT HasDeliveryAdvice FROM WarehouseAdjustments WHERE WarehouseAdjustmentID = @EntityID) = 1 " + "\r\n";
            queryString = queryString + "       BEGIN " + "\r\n";

            queryString = queryString + "           IF (@SaveRelativeOption = 1) ";
            queryString = queryString + "               BEGIN ";
            queryString = queryString + "                   UPDATE          WarehouseAdjustmentDetails " + "\r\n";
            queryString = queryString + "                   SET             WarehouseAdjustmentDetails.Reference = WarehouseAdjustments.Reference " + "\r\n";
            queryString = queryString + "                   FROM            WarehouseAdjustments INNER JOIN WarehouseAdjustmentDetails ON WarehouseAdjustments.WarehouseAdjustmentID = @EntityID AND WarehouseAdjustments.WarehouseAdjustmentID = WarehouseAdjustmentDetails.WarehouseAdjustmentID " + "\r\n";
            queryString = queryString + "               END ";

            queryString = queryString + "           UPDATE          GoodsReceiptDetails" + "\r\n";
            queryString = queryString + "           SET             GoodsReceiptDetails.QuantityIssue = ROUND(GoodsReceiptDetails.QuantityIssue + WarehouseAdjustmentDetails.Quantity * @SaveRelativeOption, " + (int)GlobalEnums.rndQuantity + "), GoodsReceiptDetails.LineVolumeIssue = ROUND(GoodsReceiptDetails.LineVolumeIssue + WarehouseAdjustmentDetails.LineVolume * @SaveRelativeOption, " + (int)GlobalEnums.rndVolume + ") " + "\r\n";
            queryString = queryString + "           FROM            (SELECT GoodsReceiptDetailID, SUM(-Quantity) AS Quantity, SUM(-LineVolume) AS LineVolume FROM WarehouseAdjustmentDetails WHERE WarehouseAdjustmentID = @EntityID AND Quantity < 0 GROUP BY GoodsReceiptDetailID) WarehouseAdjustmentDetails " + "\r\n";
            queryString = queryString + "                           INNER JOIN GoodsReceiptDetails ON WarehouseAdjustmentDetails.GoodsReceiptDetailID = GoodsReceiptDetails.GoodsReceiptDetailID" + "\r\n";

            queryString = queryString + "           IF @@ROWCOUNT > (SELECT COUNT(*) FROM WarehouseAdjustmentDetails WHERE WarehouseAdjustmentID = @EntityID) " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   DECLARE     @msg NVARCHAR(300) = N'Phiếu giao hàng đã hủy, hoặc chưa duyệt' ; " + "\r\n";
            queryString = queryString + "                   THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "               END " + "\r\n";
            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("WarehouseAdjustmentSaveRelative", queryString);
        }

        private void WarehouseAdjustmentPostSaveValidate()
        {
            string[] queryArray = new string[2];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = N'Ngày điều chỉnh kho: ' + CAST(GoodsReceipts.EntryDate AS nvarchar) FROM WarehouseAdjustmentDetails INNER JOIN GoodsReceipts ON WarehouseAdjustmentDetails.WarehouseAdjustmentID = @EntityID AND WarehouseAdjustmentDetails.GoodsReceiptID = GoodsReceipts.GoodsReceiptID AND WarehouseAdjustmentDetails.EntryDate < GoodsReceipts.EntryDate ";
            queryArray[1] = " SELECT TOP 1 @FoundEntity = N'Số lượng điều chỉnh giảm vượt quá số lượng tồn kho: ' + CAST(ROUND(Quantity - QuantityIssue, " + (int)GlobalEnums.rndQuantity + ") AS nvarchar) + N' Hoặc khối lượng: ' + CAST(ROUND(LineVolume - LineVolumeIssue, " + (int)GlobalEnums.rndVolume + ") AS nvarchar) FROM GoodsReceiptDetails WHERE (ROUND(Quantity - QuantityIssue, " + (int)GlobalEnums.rndQuantity + ") < 0) OR (ROUND(LineVolume - LineVolumeIssue, " + (int)GlobalEnums.rndVolume + ") < 0) ";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("WarehouseAdjustmentPostSaveValidate", queryArray);
        }




        private void WarehouseAdjustmentApproved()
        {
            string[] queryArray = new string[1];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = WarehouseAdjustmentID FROM WarehouseAdjustments WHERE WarehouseAdjustmentID = @EntityID AND Approved = 1";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("WarehouseAdjustmentApproved", queryArray);
        }


        private void WarehouseAdjustmentEditable()
        {
            string[] queryArray = new string[2]; //IMPORTANT: THESE QUERIES ARE COPIED FROM GoodsReceiptEditable

            string queryString = "       DECLARE @GoodsReceiptID int " + "\r\n";
            queryString = queryString + "       IF ((SELECT HasPositiveLine FROM WarehouseAdjustments WHERE WarehouseAdjustmentID = @EntityID) = 0) BEGIN SELECT @FoundEntity AS FoundEntity    RETURN 0 END " + "\r\n";

            queryString = queryString + "       SELECT TOP 1 @GoodsReceiptID = GoodsReceiptID FROM GoodsReceipts WHERE WarehouseAdjustmentID = @EntityID " + "\r\n";
            queryString = queryString + "       IF (@GoodsReceiptID IS NULL) BEGIN SELECT @FoundEntity AS FoundEntity    RETURN 0 END " + "\r\n";

            queryArray[0] = " SELECT TOP 1 @FoundEntity = GoodsReceiptID FROM GoodsIssueDetails WHERE GoodsReceiptID = @GoodsReceiptID ";
            queryArray[1] = " SELECT TOP 1 @FoundEntity = GoodsReceiptID FROM WarehouseAdjustmentDetails WHERE GoodsReceiptID = @GoodsReceiptID ";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("WarehouseAdjustmentEditable", queryArray, queryString);
        }

        private void WarehouseAdjustmentToggleApproved()
        {
            string queryString = " @EntityID int, @Approved bit " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       UPDATE      WarehouseAdjustments  SET Approved = @Approved, ApprovedDate = GetDate() WHERE WarehouseAdjustmentID = @EntityID AND Approved = ~@Approved" + "\r\n";

            queryString = queryString + "       IF @@ROWCOUNT = 1 " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";
            queryString = queryString + "               UPDATE          WarehouseAdjustmentDetails  SET Approved = @Approved WHERE WarehouseAdjustmentID = @EntityID ; " + "\r\n";

            queryString = queryString + "               UPDATE          GoodsReceipts  SET Approved = @Approved, ApprovedDate = GetDate() WHERE WarehouseAdjustmentID = @EntityID " + "\r\n";
            queryString = queryString + "               UPDATE          GoodsReceiptDetails  SET Approved = @Approved WHERE WarehouseAdjustmentID = @EntityID ; " + "\r\n";

            queryString = queryString + "           END " + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";
            queryString = queryString + "               DECLARE     @msg NVARCHAR(300) = N'Dữ liệu không tồn tại hoặc đã ' + iif(@Approved = 0, 'hủy', '')  + ' duyệt' ; " + "\r\n";
            queryString = queryString + "               THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "           END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("WarehouseAdjustmentToggleApproved", queryString);
        }

        private void WarehouseAdjustmentInitReference()
        {
            SimpleInitReference simpleInitReference = new SimpleInitReference("WarehouseAdjustments", "WarehouseAdjustmentID", "Reference", ModelSettingManager.ReferenceLength, ModelSettingManager.ReferencePrefix(GlobalEnums.NmvnTaskID.WarehouseAdjustment));
            this.totalSmartCodingEntities.CreateTrigger("WarehouseAdjustmentInitReference", simpleInitReference.CreateQuery());
        }


        #endregion
    }
}
