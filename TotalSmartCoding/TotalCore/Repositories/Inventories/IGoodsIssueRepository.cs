using System;
using System.Collections.Generic;

using TotalModel.Models;

namespace TotalCore.Repositories.Inventories
{
    public interface IGoodsIssueRepository : IGenericWithDetailRepository<GoodsIssue, GoodsIssueDetail>
    {
    }

    public interface IGoodsIssueAPIRepository : IGenericAPIRepository
    {
        List<PendingDeliveryAdvice> GetPendingDeliveryAdvices(int? locationID);
        List<PendingDeliveryAdviceCustomer> GetPendingDeliveryAdviceCustomers(int? locationID);

        List<PendingTransferOrder> GetPendingTransferOrders(int? locationID);
        List<PendingTransferOrderWarehouse> GetPendingTransferOrderWarehouses(int? locationID);


        List<PendingDeliveryAdviceDetail> GetPendingDeliveryAdviceDetails(int? locationID, int? goodsIssueID, int? deliveryAdviceID, int? customerID, int? receiverID, string deliveryAdviceDetailIDs, bool isReadonly);
        List<PendingTransferOrderDetail> GetPendingTransferOrderDetails(int? locationID, int? goodsIssueID, int? warehouseID, int? transferOrderID, int? warehouseReceiptID, string transferOrderDetailIDs, bool isReadonly);
    }

}

