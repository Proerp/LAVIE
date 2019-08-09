using TotalModel.Models;

using TotalDTO.Sales;


namespace TotalCore.Services.Sales
{
    public interface IDeliveryAdviceService : IGenericWithViewDetailService<DeliveryAdvice, DeliveryAdviceDetail, DeliveryAdviceViewDetail, DeliveryAdviceDTO, DeliveryAdvicePrimitiveDTO, DeliveryAdviceDetailDTO>
    {
    }
}

