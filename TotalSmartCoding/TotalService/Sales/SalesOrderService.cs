using System.Collections.Generic;
using System.Data.Entity.Core.Objects;

using TotalModel.Models;
using TotalDTO.Sales;
using TotalCore.Repositories.Sales;
using TotalCore.Services.Sales;

namespace TotalService.Sales
{
    public class SalesOrderService : GenericWithViewDetailService<SalesOrder, SalesOrderDetail, SalesOrderViewDetail, SalesOrderDTO, SalesOrderPrimitiveDTO, SalesOrderDetailDTO>, ISalesOrderService
    {
        public SalesOrderService(ISalesOrderRepository salesOrderRepository)
            : base(salesOrderRepository, "SalesOrderPostSaveValidate", "SalesOrderSaveRelative", "SalesOrderToggleApproved", "SalesOrderToggleVoid", null, "GetSalesOrderViewDetails")            
        {
        }

        public override ICollection<SalesOrderViewDetail> GetViewDetails(int salesOrderID)
        {
            ObjectParameter[] parameters = new ObjectParameter[] { new ObjectParameter("SalesOrderID", salesOrderID) };
            return this.GetViewDetails(parameters);
        }
    }
}
