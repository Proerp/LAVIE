using System;
using System.Collections.Generic;

using TotalModel.Models;

namespace TotalCore.Repositories.Sales
{
    public interface ITransferOrderRepository : IGenericWithDetailRepository<TransferOrder, TransferOrderDetail>
    {
    }

    public interface ITransferOrderAPIRepository : IGenericAPIRepository
    {
    }
}