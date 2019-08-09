using System.Collections.Generic;

using TotalModel.Models;

namespace TotalCore.Repositories.Commons
{
    public interface IBatchTypeRepository
    {

    }

    public interface IBatchTypeAPIRepository : IGenericAPIRepository
    {
        IList<BatchTypeBase> GetBatchTypeBases();
        IList<BatchTypeTree> GetBatchTypeTrees();
    }
}
