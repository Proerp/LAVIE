using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Productions
{
    public class BatchMaster
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public BatchMaster(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetBatchMasterIndexes();

            this.BatchMasterApproved();
            this.BatchMasterEditable();

            this.BatchMasterSaveRelative();
            this.BatchMasterPostSaveValidate();

            this.BatchMasterToggleApproved();
            this.BatchMasterToggleVoid();

            this.BatchMasterInitReference();
            this.BatchMasterAddLot();
            this.BatchMasterRemoveLot();

            this.GetBatchMasterBases();
            this.GetBatchMasterTrees();
        }


        private void GetBatchMasterIndexes()
        {
            string queryString;

            queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime, @ShowCummulativePacks bit, @ActiveOption int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       IF  (@ActiveOption <> -1) " + "\r\n";
            queryString = queryString + "           " + this.GetBatchMasterIndexSQL(true) + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "           " + this.GetBatchMasterIndexSQL(false) + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetBatchMasterIndexes", queryString);
        }

        private string GetBatchMasterIndexSQL(bool isActiveOption)
        {
            string queryString = "";

            queryString = queryString + "   BEGIN " + "\r\n";
            queryString = queryString + "       IF  (@ShowCummulativePacks = 1) " + "\r\n";
            queryString = queryString + "           " + this.GetBatchMasterIndexSQL(isActiveOption, true) + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "           " + this.GetBatchMasterIndexSQL(isActiveOption, false) + "\r\n";
            queryString = queryString + "   END " + "\r\n";

            return queryString;
        }

        private string GetBatchMasterIndexSQL(bool isActiveOption, bool showCummulativePacks)
        {
            string queryString = "";

            queryString = queryString + "   BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      BatchMasters.BatchMasterID, CAST(ISNULL(Lots.EntryDate, BatchMasters.EntryDate) AS DATE) AS EntryDate, BatchMasters.Reference, BatchMasters.ItemNumber AS BatchMasterItemNumber, BatchMasters.BatchStatusID, BatchStatuses.ItemNumber AS BatchStatusItemNumber, BatchMasters.LavieID, Lavies.ItemNumber AS LavieItemNumber, Lavies.OfficialItemNumber AS LavieOfficialItemNumber, Lavies.Name AS LavieName, Lavies.APIItemNumber AS LavieAPIItemNumber, Lavies.CartonItemNumber AS LavieCartonItemNumber, Lavies.Volume, Lavies.PackPerCarton, Lavies.CartonPerPallet, Lavies.Shelflife, " + "\r\n";
            queryString = queryString + "                   Lots.LotID, Lots.EntryDate AS LotEntryDate, Lots.ItemNumber AS LotItemNumber, BatchMasters.Description, BatchMasters.Remarks, BatchMasters.PlannedQuantity, " + (showCummulativePacks ? "CummulativePacks.PackQuantity" : "CAST(0 AS int) AS PackQuantity") + ", " + (showCummulativePacks ? "CummulativePacks.PackLineVolume" : "CAST(0 AS decimal(18, 2)) AS PackLineVolume") + ", BatchMasters.CreatedDate, BatchMasters.EditedDate, BatchMasters.IsDefault, BatchMasters.InActive " + "\r\n";
            queryString = queryString + "       FROM        BatchMasters " + "\r\n";
            queryString = queryString + "                   INNER JOIN Lavies ON " + (isActiveOption ? "BatchMasters.InActive = @ActiveOption AND " : "") + "((BatchMasters.EntryDate >= @FromDate AND BatchMasters.EntryDate <= @ToDate) OR BatchMasters.EntryDate = CONVERT(DATETIME, '2000-01-01 00:00:00', 102)) AND BatchMasters.LavieID = Lavies.LavieID " + "\r\n";
            queryString = queryString + "                   INNER JOIN BatchStatuses ON BatchMasters.BatchStatusID = BatchStatuses.BatchStatusID " + "\r\n";

            queryString = queryString + "                   LEFT JOIN Lots ON BatchMasters.BatchMasterID = Lots.BatchMasterID " + "\r\n";
            if (showCummulativePacks)
                queryString = queryString + "               LEFT JOIN (SELECT Batches.BatchMasterID, Batches.LotID, SUM(1) AS PackQuantity, SUM(Packs.LineVolume) AS PackLineVolume FROM Packs INNER JOIN Batches ON Packs.BatchID = Batches.BatchID GROUP BY Batches.BatchMasterID, Batches.LotID) CummulativePacks ON Lots.LotID = CummulativePacks.LotID " + "\r\n";
            
            queryString = queryString + "   END " + "\r\n";

            return queryString;
        }


        private void BatchMasterApproved()
        {
            string[] queryArray = new string[1];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = BatchMasterID FROM BatchMasters WHERE BatchMasterID = @EntityID AND Approved = 1";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("BatchMasterApproved", queryArray);
        }

        private void BatchMasterEditable()
        {
            string[] queryArray = new string[3];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = BatchMasterID FROM BatchMasters WHERE BatchMasterID = @EntityID AND InActive = 1 "; //Don't allow edit after void
            queryArray[1] = " SELECT TOP 1 @FoundEntity = BatchMasterID FROM Lots WHERE BatchMasterID = @EntityID ";
            queryArray[2] = " SELECT TOP 1 @FoundEntity = BatchMasterID FROM Batches WHERE BatchMasterID = @EntityID ";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("BatchMasterEditable", queryArray);
        }

        private void BatchMasterSaveRelative()
        {
            string queryString = " @EntityID int, @SaveRelativeOption int " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       BEGIN " + "\r\n";
            queryString = queryString + "           DELETE FROM     UniquePacks     WHERE EntryDate < DATEADD(day, -10, GETDATE()) " + "\r\n";
            queryString = queryString + "           DELETE FROM     UniqueCartons   WHERE EntryDate < DATEADD(day, -10, GETDATE()) " + "\r\n";
            queryString = queryString + "           DELETE FROM     UniquePallets   WHERE EntryDate < DATEADD(day, -10, GETDATE()) " + "\r\n";
            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("BatchMasterSaveRelative", queryString);
        }

        private void BatchMasterPostSaveValidate()
        {
            string[] queryArray = new string[0];

            //queryArray[0] = " SELECT TOP 1 @FoundEntity = N'Ngày xuất kho: ' + CAST(GoodsIssueDetails.EntryDate AS nvarchar) FROM BatchMasterDetails INNER JOIN GoodsIssueDetails ON BatchMasterDetails.BatchMasterID = @EntityID AND BatchMasterDetails.GoodsIssueDetailID = GoodsIssueDetails.GoodsIssueDetailID AND BatchMasterDetails.EntryDate < GoodsIssueDetails.EntryDate ";
            //queryArray[1] = " SELECT TOP 1 @FoundEntity = N'Ngày xuất kho: ' + CAST(CAST(GoodsIssueDetails.EntryDate AS Date) AS nvarchar) + N' (Ngày HĐ phải sau ngày xuất kho)' FROM BatchMasterDetails INNER JOIN GoodsIssueDetails ON BatchMasterDetails.BatchMasterID = @EntityID AND BatchMasterDetails.GoodsIssueDetailID = GoodsIssueDetails.GoodsIssueDetailID AND BatchMasterDetails.VATInvoiceDate < CAST(GoodsIssueDetails.EntryDate AS Date) ";
            //queryArray[2] = " SELECT TOP 1 @FoundEntity = N'Số lượng xuất hóa đơn vượt quá số lượng xuất kho: ' + CAST(ROUND(GoodsIssueDetails.Quantity - GoodsIssueDetails.QuantityInvoice, " + (int)GlobalEnums.rndQuantity + ") AS nvarchar) + ' OR free quantity: ' + CAST(ROUND(GoodsIssueDetails.FreeQuantity - GoodsIssueDetails.FreeQuantityInvoice, " + (int)GlobalEnums.rndQuantity + ") AS nvarchar) FROM BatchMasterDetails INNER JOIN GoodsIssueDetails ON BatchMasterDetails.BatchMasterID = @EntityID AND BatchMasterDetails.GoodsIssueDetailID = GoodsIssueDetails.GoodsIssueDetailID AND (ROUND(GoodsIssueDetails.Quantity - GoodsIssueDetails.QuantityInvoice, " + (int)GlobalEnums.rndQuantity + ") < 0 OR ROUND(GoodsIssueDetails.FreeQuantity - GoodsIssueDetails.FreeQuantityInvoice, " + (int)GlobalEnums.rndQuantity + ") < 0) ";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("BatchMasterPostSaveValidate", queryArray);
        }

        private void BatchMasterToggleApproved()
        {
            string queryString = " @EntityID int, @Approved bit " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       UPDATE      BatchMasters  SET Approved = @Approved, ApprovedDate = GetDate() WHERE BatchMasterID = @EntityID AND Approved = ~@Approved" + "\r\n";

            queryString = queryString + "       IF @@ROWCOUNT <> 1 " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";
            queryString = queryString + "               DECLARE     @msg NVARCHAR(300) = N'Dữ liệu không tồn tại hoặc đã ' + iif(@Approved = 0, 'hủy', '')  + ' duyệt' ; " + "\r\n";
            queryString = queryString + "               THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "           END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("BatchMasterToggleApproved", queryString);
        }

        private void BatchMasterToggleVoid()
        {
            string queryString = " @EntityID int, @InActive bit, @VoidTypeID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       UPDATE      BatchMasters  SET InActive = @InActive, InActiveDate = GetDate() WHERE BatchMasterID = @EntityID AND InActive = ~@InActive" + "\r\n";

            queryString = queryString + "       IF @@ROWCOUNT <> 1 " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";
            queryString = queryString + "               DECLARE     @msg NVARCHAR(300) = N'Batch không tồn tại hoặc ' + iif(@InActive = 0, 'đang', 'dừng')  + ' sản xuất' ; " + "\r\n";
            queryString = queryString + "               THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "           END " + "\r\n";


            this.totalSmartCodingEntities.CreateStoredProcedure("BatchMasterToggleVoid", queryString);
        }

        private void BatchMasterInitReference()
        {
            SimpleInitReference simpleInitReference = new SimpleInitReference("BatchMasters", "BatchMasterID", "Reference", ModelSettingManager.ReferenceLength, ModelSettingManager.ReferencePrefix(GlobalEnums.NmvnTaskID.BatchMaster));
            this.totalSmartCodingEntities.CreateTrigger("BatchMasterInitReference", simpleInitReference.CreateQuery());
        }

        private void BatchMasterAddLot()
        {
            string queryString = " @BatchMasterID int, @EntryDate DateTime" + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       DECLARE     @CreatedDate DateTime, @ItemNumber nvarchar(10) ";
            queryString = queryString + "       SET         @CreatedDate = GetDate() ";
            queryString = queryString + "       SELECT      @ItemNumber = MAX(ItemNumber) FROM Lots WHERE BatchMasterID = @BatchMasterID ";

            queryString = queryString + "       SELECT      @ItemNumber = CHAR(CASE WHEN @ItemNumber IS NULL THEN 48 WHEN (ASCII(@ItemNumber) >= 48 AND ASCII(@ItemNumber) < 57) OR (ASCII(@ItemNumber) >= 65 AND ASCII(@ItemNumber) < 90) THEN ASCII(@ItemNumber) + 1 WHEN ASCII(@ItemNumber) = 57 THEN 65 ELSE 97 END) " + "\r\n";

            queryString = queryString + "       INSERT INTO Lots (EntryDate, Reference, ItemNumber, BatchMasterID, LocationID, Description, Remarks, CreatedDate, EditedDate, Approved, ApprovedDate, InActive, InActiveDate) " + "\r\n";
            queryString = queryString + "       SELECT      @EntryDate AS EntryDate, @ItemNumber AS Reference, @ItemNumber, BatchMasterID, LocationID, NULL AS Description, NULL AS Remarks, @CreatedDate AS CreatedDate, @CreatedDate AS EditedDate, 0 AS Approved, NULL AS ApprovedDate, 0 AS InActive, NULL AS InActiveDate " + "\r\n";
            queryString = queryString + "       FROM        BatchMasters " + "\r\n";
            queryString = queryString + "       WHERE       BatchMasterID = @BatchMasterID " + "\r\n";

            queryString = queryString + "       UPDATE      BatchMasters SET BatchStatusID = " + (int)GlobalVariables.BatchStatuses.WIP + " WHERE BatchMasterID = @BatchMasterID " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("BatchMasterAddLot", queryString);
        }

        private void BatchMasterRemoveLot()
        {
            string queryString = " @LotID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       DECLARE @FoundBatchID int " + "\r\n";
            queryString = queryString + "       SELECT TOP 1 @FoundBatchID = BatchID FROM Batches WHERE LotID = @LotID " + "\r\n";

            queryString = queryString + "       IF NOT @FoundBatchID IS NULL " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";
            queryString = queryString + "               DECLARE     @msg NVARCHAR(300) = N'Lot này đã sản xuất rồi, vui lòng kiểm tra lại' ; " + "\r\n";
            queryString = queryString + "               THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "           END " + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";
            queryString = queryString + "               DELETE FROM Lots WHERE LotID = @LotID " + "\r\n";
            queryString = queryString + "               UPDATE BatchMasters SET BatchStatusID = " + (int)GlobalVariables.BatchStatuses.Pending + " WHERE BatchMasterID NOT IN (SELECT BatchMasterID FROM Lots)" + "\r\n";
            queryString = queryString + "           END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("BatchMasterRemoveLot", queryString);
        }

        private void GetBatchMasterBases()
        {
            this.totalSmartCodingEntities.CreateStoredProcedure("GetBatchMasterBases", this.GetBatchMasterBUILD(0));
            this.totalSmartCodingEntities.CreateStoredProcedure("GetBatchMasterBase", this.GetBatchMasterBUILD(1));
            this.totalSmartCodingEntities.CreateStoredProcedure("GetBatchMasterBaseByItemNumber", this.GetBatchMasterBUILD(2));
        }

        private string GetBatchMasterBUILD(int switchID)
        {
            string queryString;

            queryString = (switchID == 0 ? "" : (switchID == 1 ? "@BatchMasterID int" : "@ItemNumber nvarchar(50)")) + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      BatchMasters.BatchMasterID, BatchMasters.EntryDate, BatchMasters.ItemNumber, ISNULL(BatchMasters.PlannedQuantity, 0) AS PlannedQuantity, ISNULL(CummulativePacks.PackQuantity, 0) AS PackQuantity, ISNULL(CummulativePacks.PackLineVolume, 0) AS PackLineVolume, BatchStatuses.ItemNumber AS BatchStatusItemNumber, BatchMasters.Remarks, " + "\r\n";
            queryString = queryString + "                   BatchMasters.LavieID, Lavies.ItemNumber AS LavieItemNumber, Lavies.Name AS LavieName, Lavies.APIItemNumber AS LavieAPIItemNumber, Lavies.CartonItemNumber AS LavieCartonItemNumber, Lavies.Volume, Lavies.Shelflife, Lavies.PackPerCarton, Lavies.CartonPerPallet " + "\r\n";
            queryString = queryString + "       FROM        BatchMasters " + "\r\n";
            queryString = queryString + "                   INNER JOIN Lavies ON BatchMasters." + (switchID == 0 ? "Approved = 1" : (switchID == 1 ? "BatchMasterID = @BatchMasterID" : "ItemNumber = @ItemNumber")) + " AND BatchMasters.LavieID = Lavies.LavieID " + "\r\n";
            queryString = queryString + "                   INNER JOIN BatchStatuses ON BatchMasters.BatchStatusID = BatchStatuses.BatchStatusID " + "\r\n";
            queryString = queryString + "                   LEFT JOIN (SELECT Batches.BatchMasterID, SUM(1) AS PackQuantity, SUM(Packs.LineVolume) AS PackLineVolume FROM Packs INNER JOIN Batches ON Packs.BatchID = Batches.BatchID GROUP BY Batches.BatchMasterID) CummulativePacks ON BatchMasters.BatchMasterID = CummulativePacks.BatchMasterID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            return queryString;
        }


        private void GetBatchMasterTrees()
        {
            string queryString;

            queryString = " @FromDate DateTime, @ToDate DateTime " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      " + GlobalEnums.RootNode + " AS NodeID, 0 AS ParentNodeID, NULL AS PrimaryID, NULL AS AncestorID, '[All]' AS ItemNumber, NULL AS Name, NULL AS ParameterName, CAST(1 AS bit) AS Selected " + "\r\n";
            queryString = queryString + "       UNION ALL " + "\r\n";

            queryString = queryString + "       SELECT      " + GlobalEnums.AncestorNode + " - DATEDIFF(day, CONVERT(DATETIME, '2000-01-01 00:00:00', 102), EntryDate) AS NodeID, " + GlobalEnums.RootNode + " AS ParentNodeID, NULL AS PrimaryID, NULL AS AncestorID, LEFT(CONVERT(VARCHAR, MIN(EntryDate), 103), 10) AS ItemNumber, NULL AS Name, '' AS ParameterName, CAST(0 AS bit) AS Selected " + "\r\n";
            queryString = queryString + "       FROM        BatchMasters WHERE EntryDate >= @FromDate AND EntryDate <= @ToDate GROUP BY DATEDIFF(day, CONVERT(DATETIME, '2000-01-01 00:00:00', 102), EntryDate) " + "\r\n";
            queryString = queryString + "       UNION ALL " + "\r\n";
            queryString = queryString + "       SELECT      BatchMasterID AS NodeID, " + GlobalEnums.AncestorNode + " - DATEDIFF(day, CONVERT(DATETIME, '2000-01-01 00:00:00', 102), EntryDate) AS ParentNodeID, BatchMasters.BatchMasterID AS PrimaryID, NULL AS AncestorID, BatchMasters.ItemNumber, '[' + Lavies.ItemNumber + ']    ' + Lavies.Name AS Name, 'BatchMasterID' AS ParameterName, CAST(0 AS bit) AS Selected " + "\r\n";
            queryString = queryString + "       FROM        BatchMasters INNER JOIN Lavies ON BatchMasters.EntryDate >= @FromDate AND BatchMasters.EntryDate <= @ToDate AND BatchMasters.LavieID = Lavies.LavieID " + "\r\n";

            queryString = queryString + "       ORDER BY    NodeID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetBatchMasterTrees", queryString);

        }
    }
}
