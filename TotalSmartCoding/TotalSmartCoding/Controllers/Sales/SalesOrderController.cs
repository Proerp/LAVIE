using TotalModel.Models;
using TotalDTO.Sales;

using TotalCore.Services.Sales;
using TotalSmartCoding.ViewModels.Sales;

namespace TotalSmartCoding.Controllers.Sales
{
    public class SalesOrderController : GenericViewDetailController<SalesOrder, SalesOrderDetail, SalesOrderViewDetail, SalesOrderDTO, SalesOrderPrimitiveDTO, SalesOrderDetailDTO, SalesOrderViewModel>
    {
        public SalesOrderController(ISalesOrderService salesOrderService, SalesOrderViewModel salesOrderViewModel)
            : base(salesOrderService, salesOrderViewModel)
        {
        }
    }
}
