using System.Collections.Generic;

using TotalModel.Models;

namespace TotalCore.Repositories.Commons
{
    public interface IBatchStatusRepository
    {

    }

    public interface IBatchStatusAPIRepository : IGenericAPIRepository
    {
        BatchStatusBase GetBatchStatusBase(string code);
        BatchStatusBase GetBatchStatusBase(int batchStatusID);
        IList<BatchStatusBase> GetBatchStatusBases();
    }
}
