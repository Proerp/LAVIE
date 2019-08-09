using System.Collections.Generic;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;
using TotalCore.Repositories.Inventories;

namespace TotalSmartCoding.Controllers.APIs.Inventories
{
    public class GoodsReceiptAPIs
    {
        private readonly IGoodsReceiptAPIRepository goodsReceiptAPIRepository;

        public GoodsReceiptAPIs(IGoodsReceiptAPIRepository goodsReceiptAPIRepository)
        {
            this.goodsReceiptAPIRepository = goodsReceiptAPIRepository;
        }


        public ICollection<GoodsReceiptIndex> GetGoodsReceiptIndexes()
        {
            return this.goodsReceiptAPIRepository.GetEntityIndexes<GoodsReceiptIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate);
        }



        public List<PendingPickup> GetPendingPickups(int? locationID)
        {
            return this.goodsReceiptAPIRepository.GetPendingPickups(locationID);
        }


        public List<PendingPickupWarehouse> GetPendingPickupWarehouses(int? locationID)
        {
            return this.goodsReceiptAPIRepository.GetPendingPickupWarehouses(locationID);
        }

        public List<PendingPickupDetail> GetPendingPickupDetails(int? locationID, int? goodsReceiptID, int? pickupID, int? warehouseID, string pickupDetailIDs, bool isReadonly)
        {
            return this.goodsReceiptAPIRepository.GetPendingPickupDetails(locationID, goodsReceiptID, pickupID, warehouseID, pickupDetailIDs, isReadonly);
        }





        public List<PendingGoodsIssueTransfer> GetPendingGoodsIssueTransfers(int? locationID)
        {
            return this.goodsReceiptAPIRepository.GetPendingGoodsIssueTransfers(locationID);
        }


        public List<PendingGoodsIssueTransferWarehouse> GetPendingGoodsIssueTransferWarehouses(int? locationID)
        {
            return this.goodsReceiptAPIRepository.GetPendingGoodsIssueTransferWarehouses(locationID);
        }

        public List<PendingGoodsIssueTransferDetail> GetPendingGoodsIssueTransferDetails(int? locationID, int? goodsReceiptID, int? goodsIssueID, int? warehouseID, string goodsIssueTransferDetailIDs, bool isReadonly)
        {
            return this.goodsReceiptAPIRepository.GetPendingGoodsIssueTransferDetails(locationID, goodsReceiptID, goodsIssueID, warehouseID, goodsIssueTransferDetailIDs, isReadonly);
        }




        public List<PendingWarehouseAdjustmentDetail> GetPendingWarehouseAdjustmentDetails(int? locationID, int? goodsReceiptID, int? warehouseAdjustmentID, int? warehouseID, string warehouseAdjustmentDetailIDs, bool isReadonly)
        {
            return this.goodsReceiptAPIRepository.GetPendingWarehouseAdjustmentDetails(locationID, goodsReceiptID, warehouseAdjustmentID, warehouseID, warehouseAdjustmentDetailIDs, isReadonly);
        }



        public List<GoodsReceiptDetailAvailable> GetGoodsReceiptDetailAvailables(int? locationID, int? warehouseID, int? commodityID, string commodityIDs, int? batchID, string goodsReceiptDetailIDs, bool onlyIssuable)
        {
            return this.goodsReceiptAPIRepository.GetGoodsReceiptDetailAvailables(locationID, warehouseID, commodityID, commodityIDs, batchID, goodsReceiptDetailIDs, onlyIssuable);
        }
    }
}
