using System.Collections.Generic;

using TotalBase;
using TotalModel.Models;
using TotalCore.Repositories.Sales;
using TotalBase.Enums;

namespace TotalSmartCoding.Controllers.APIs.Sales
{
    public class TransferOrderAPIs
    {
        private readonly ITransferOrderAPIRepository transferOrderAPIRepository;

        public TransferOrderAPIs(ITransferOrderAPIRepository transferOrderAPIRepository)
        {
            this.transferOrderAPIRepository = transferOrderAPIRepository;
        }


        public ICollection<TransferOrderIndex> GetTransferOrderIndexes()
        {
            return this.transferOrderAPIRepository.GetEntityIndexes<TransferOrderIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate);
        }
    }
}
