using TotalModel.Models;

using TotalDTO.Sales;

namespace TotalCore.Services.Sales
{
    public interface ISalesOrderService : IGenericWithViewDetailService<SalesOrder, SalesOrderDetail, SalesOrderViewDetail, SalesOrderDTO, SalesOrderPrimitiveDTO, SalesOrderDetailDTO>
    {
    }
}