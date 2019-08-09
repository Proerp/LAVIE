using System.Collections.Generic;
using System.Data.Entity.Core.Objects;

using TotalModel.Models;
using TotalDTO.Sales;
using TotalCore.Repositories.Sales;
using TotalCore.Services.Sales;


namespace TotalService.Sales
{
    public class DeliveryAdviceService : GenericWithViewDetailService<DeliveryAdvice, DeliveryAdviceDetail, DeliveryAdviceViewDetail, DeliveryAdviceDTO, DeliveryAdvicePrimitiveDTO, DeliveryAdviceDetailDTO>, IDeliveryAdviceService
    {
        public DeliveryAdviceService(IDeliveryAdviceRepository deliveryAdviceRepository)
            : base(deliveryAdviceRepository, "DeliveryAdvicePostSaveValidate", "DeliveryAdviceSaveRelative", "DeliveryAdviceToggleApproved", "DeliveryAdviceToggleVoid", null, "GetDeliveryAdviceViewDetails")
        {
        }

        public override ICollection<DeliveryAdviceViewDetail> GetViewDetails(int deliveryAdviceID)
        {
            ObjectParameter[] parameters = new ObjectParameter[] { new ObjectParameter("DeliveryAdviceID", deliveryAdviceID) };
            return this.GetViewDetails(parameters);
        }
    }
}
