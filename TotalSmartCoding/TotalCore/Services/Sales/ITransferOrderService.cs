using TotalModel.Models;

using TotalDTO.Sales;

namespace TotalCore.Services.Sales
{
    public interface ITransferOrderService : IGenericWithViewDetailService<TransferOrder, TransferOrderDetail, TransferOrderViewDetail, TransferOrderDTO, TransferOrderPrimitiveDTO, TransferOrderDetailDTO>
    {
    }
}