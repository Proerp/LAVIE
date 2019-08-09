using TotalModel.Models;
using TotalDTO.Sales;

using TotalCore.Services.Sales;
using TotalSmartCoding.ViewModels.Sales;

namespace TotalSmartCoding.Controllers.Sales
{
    public class TransferOrderController : GenericViewDetailController<TransferOrder, TransferOrderDetail, TransferOrderViewDetail, TransferOrderDTO, TransferOrderPrimitiveDTO, TransferOrderDetailDTO, TransferOrderViewModel>
    {
        public TransferOrderController(ITransferOrderService transferOrderService, TransferOrderViewModel transferOrderViewModel)
            : base(transferOrderService, transferOrderViewModel)
        {
        }
    }
}
