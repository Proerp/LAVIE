using TotalModel.Models;
using TotalDTO.Sales;

using TotalCore.Services.Sales;
using TotalSmartCoding.ViewModels.Sales;

namespace TotalSmartCoding.Controllers.Sales
{
    public class DeliveryAdviceController : GenericViewDetailController<DeliveryAdvice, DeliveryAdviceDetail, DeliveryAdviceViewDetail, DeliveryAdviceDTO, DeliveryAdvicePrimitiveDTO, DeliveryAdviceDetailDTO, DeliveryAdviceViewModel>
    {
        public DeliveryAdviceController(IDeliveryAdviceService deliveryAdviceService, DeliveryAdviceViewModel deliveryAdviceViewModel)
            : base(deliveryAdviceService, deliveryAdviceViewModel)
        {
        }
    }
}
