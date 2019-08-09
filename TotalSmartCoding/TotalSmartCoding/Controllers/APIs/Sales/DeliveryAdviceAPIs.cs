using System.Collections.Generic;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;
using TotalCore.Repositories.Sales;

namespace TotalSmartCoding.Controllers.APIs.Sales
{
    public class DeliveryAdviceAPIs
    {
        private readonly IDeliveryAdviceAPIRepository deliveryAdviceAPIRepository;

        public DeliveryAdviceAPIs(IDeliveryAdviceAPIRepository deliveryAdviceAPIRepository)
        {
            this.deliveryAdviceAPIRepository = deliveryAdviceAPIRepository;
        }


        public ICollection<DeliveryAdviceIndex> GetDeliveryAdviceIndexes()
        {
            return this.deliveryAdviceAPIRepository.GetEntityIndexes<DeliveryAdviceIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate);
        }

        public List<PendingSalesOrder> GetPendingSalesOrders(int? locationID)
        {
            return this.deliveryAdviceAPIRepository.GetPendingSalesOrders(locationID);
        }


        public List<PendingSalesOrderCustomer> GetPendingSalesOrderCustomers(int? locationID)
        {
            return this.deliveryAdviceAPIRepository.GetPendingSalesOrderCustomers(locationID);
        }

        public List<PendingSalesOrderDetail> GetPendingSalesOrderDetails(int? locationID, int? deliveryAdviceID, int? salesOrderID, int? customerID, int? receiverID, string salesOrderDetailIDs, bool isReadonly)
        {
            return this.deliveryAdviceAPIRepository.GetPendingSalesOrderDetails(locationID, deliveryAdviceID, salesOrderID, customerID, receiverID, salesOrderDetailIDs, isReadonly);
        }

    }
}
