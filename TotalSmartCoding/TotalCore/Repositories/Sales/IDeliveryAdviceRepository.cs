using System;
using System.Collections.Generic;

using TotalModel.Models;

namespace TotalCore.Repositories.Sales
{
    public interface IDeliveryAdviceRepository : IGenericWithDetailRepository<DeliveryAdvice, DeliveryAdviceDetail>
    {
    }

    public interface IDeliveryAdviceAPIRepository : IGenericAPIRepository
    {
        List<PendingSalesOrder> GetPendingSalesOrders(int? locationID);
        List<PendingSalesOrderCustomer> GetPendingSalesOrderCustomers(int? locationID);
        List<PendingSalesOrderDetail> GetPendingSalesOrderDetails(int? locationID, int? deliveryAdviceID, int? salesOrderID, int? customerID, int? receiverID, string salesOrderDetailIDs, bool isReadonly);        
    }

}

