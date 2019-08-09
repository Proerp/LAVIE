using System;
using System.Collections.Generic;

using TotalModel.Models;

namespace TotalCore.Repositories.Inventories
{
    public interface IGoodsReceiptRepository : IGenericWithDetailRepository<GoodsReceipt, GoodsReceiptDetail>
    {
    }

    public interface IGoodsReceiptAPIRepository : IGenericAPIRepository
    {
        List<PendingPickup> GetPendingPickups(int? locationID);
        List<PendingPickupWarehouse> GetPendingPickupWarehouses(int? locationID);
        List<PendingPickupDetail> GetPendingPickupDetails(int? locationID, int? goodsReceiptID, int? pickupID, int? warehouseID, string pickupDetailIDs, bool isReadonly);

        List<PendingGoodsIssueTransfer> GetPendingGoodsIssueTransfers(int? locationID);
        List<PendingGoodsIssueTransferWarehouse> GetPendingGoodsIssueTransferWarehouses(int? locationID);
        List<PendingGoodsIssueTransferDetail> GetPendingGoodsIssueTransferDetails(int? locationID, int? goodsReceiptID, int? goodsIssueID, int? warehouseID, string goodsIssueTransferDetailIDs, bool isReadonly);
        
        List<PendingWarehouseAdjustmentDetail> GetPendingWarehouseAdjustmentDetails(int? locationID, int? goodsReceiptID, int? warehouseAdjustmentID, int? warehouseID, string warehouseAdjustmentDetailIDs, bool isReadonly);
        int? GetGoodsReceiptIDofWarehouseAdjustment(int? warehouseAdjustmentID);

        List<GoodsReceiptDetailAvailable> GetGoodsReceiptDetailAvailables(int? locationID, int? warehouseID, int? commodityID, string commodityIDs, int? batchID, string goodsReceiptDetailIDs, bool onlyIssuable);
    }

}

