using System.Collections.Generic;

using TotalModel.Models;

namespace TotalCore.Repositories.Commons
{
    public interface ITransferOrderTypeRepository
    {

    }

    public interface ITransferOrderTypeAPIRepository : IGenericAPIRepository
    {
        IList<TransferOrderTypeBase> GetTransferOrderTypeBases();
    }
}
