using System.Collections.Generic;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;
using TotalCore.Repositories.Inventories;

namespace TotalSmartCoding.Controllers.APIs.Inventories
{
    public class GoodsIssueAPIs
    {
        private readonly IGoodsIssueAPIRepository goodsIssueAPIRepository;

        public GoodsIssueAPIs(IGoodsIssueAPIRepository goodsIssueAPIRepository)
        {
            this.goodsIssueAPIRepository = goodsIssueAPIRepository;
        }


        public ICollection<GoodsIssueIndex> GetGoodsIssueIndexes()
        {
            return this.goodsIssueAPIRepository.GetEntityIndexes<GoodsIssueIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate);
        }



        public List<PendingDeliveryAdvice> GetPendingDeliveryAdvices(int? locationID)
        {
            return this.goodsIssueAPIRepository.GetPendingDeliveryAdvices(locationID);
        }

        public List<PendingDeliveryAdviceCustomer> GetPendingDeliveryAdviceCustomers(int? locationID)
        {
            return this.goodsIssueAPIRepository.GetPendingDeliveryAdviceCustomers(locationID);
        }



        public List<PendingTransferOrder> GetPendingTransferOrders(int? locationID)
        {
            return this.goodsIssueAPIRepository.GetPendingTransferOrders(locationID);
        }

        public List<PendingTransferOrderWarehouse> GetPendingTransferOrderWarehouses(int? locationID)
        {
            return this.goodsIssueAPIRepository.GetPendingTransferOrderWarehouses(locationID);
        }


        public List<PendingDeliveryAdviceDetail> GetPendingDeliveryAdviceDetails(int? locationID, int? goodsIssueID, int? deliveryAdviceID, int? customerID, int? receiverID, string deliveryAdviceDetailIDs, bool isReadonly)
        {
            return this.goodsIssueAPIRepository.GetPendingDeliveryAdviceDetails(locationID, goodsIssueID, deliveryAdviceID, customerID, receiverID, deliveryAdviceDetailIDs, isReadonly);
        }

        public List<PendingTransferOrderDetail> GetPendingTransferOrderDetails(int? locationID, int? goodsIssueID, int? warehouseID, int? transferOrderID, int? warehouseReceiptID, string transferOrderDetailIDs, bool isReadonly)
        {
            return this.goodsIssueAPIRepository.GetPendingTransferOrderDetails(locationID, goodsIssueID, warehouseID, transferOrderID, warehouseReceiptID, transferOrderDetailIDs, isReadonly);
        }
    }
}
