using System.Collections.Generic;
using System.Data.Entity.Core.Objects;

using TotalModel.Models;
using TotalDTO.Sales;
using TotalCore.Repositories.Sales;
using TotalCore.Services.Sales;

namespace TotalService.Sales
{
    public class TransferOrderService : GenericWithViewDetailService<TransferOrder, TransferOrderDetail, TransferOrderViewDetail, TransferOrderDTO, TransferOrderPrimitiveDTO, TransferOrderDetailDTO>, ITransferOrderService
    {
        public TransferOrderService(ITransferOrderRepository transferOrderRepository)
            : base(transferOrderRepository, "TransferOrderPostSaveValidate", "TransferOrderSaveRelative", "TransferOrderToggleApproved", "TransferOrderToggleVoid", null, "GetTransferOrderViewDetails")
        {
        }

        public override ICollection<TransferOrderViewDetail> GetViewDetails(int transferOrderID)
        {
            ObjectParameter[] parameters = new ObjectParameter[] { new ObjectParameter("TransferOrderID", transferOrderID) };
            return this.GetViewDetails(parameters);
        }
    }
}
