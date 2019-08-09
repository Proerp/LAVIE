using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

using TotalBase.Enums;
using TotalModel.Models;
using TotalCore.Repositories.Sales;

namespace TotalDAL.Repositories.Sales
{
    public class DeliveryAdviceRepository : GenericWithDetailRepository<DeliveryAdvice, DeliveryAdviceDetail>, IDeliveryAdviceRepository
    {
        public DeliveryAdviceRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "DeliveryAdviceEditable", "DeliveryAdviceApproved", null, "DeliveryAdviceVoidable")
        {
        }
    }








    public class DeliveryAdviceAPIRepository : GenericAPIRepository, IDeliveryAdviceAPIRepository
    {
        public DeliveryAdviceAPIRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "GetDeliveryAdviceIndexes")
        {
        }

        public List<PendingSalesOrder> GetPendingSalesOrders(int? locationID)
        {
            return base.TotalSmartCodingEntities.GetPendingSalesOrders(locationID).ToList();
        }

        public List<PendingSalesOrderCustomer> GetPendingSalesOrderCustomers(int? locationID)
        {
            return base.TotalSmartCodingEntities.GetPendingSalesOrderCustomers(locationID).ToList();
        }

        public List<PendingSalesOrderDetail> GetPendingSalesOrderDetails(int? locationID, int? deliveryAdviceID, int? salesOrderID, int? customerID, int? receiverID, string salesOrderDetailIDs, bool isReadonly)
        {
            return base.TotalSmartCodingEntities.GetPendingSalesOrderDetails(locationID, deliveryAdviceID, salesOrderID, customerID, receiverID, salesOrderDetailIDs, isReadonly).ToList();
        }        
    }


}
