using System.Collections.Generic;

using TotalModel.Models;

namespace TotalCore.Repositories.Commons
{
    public interface IWarehouseAdjustmentTypeRepository
    {

    }

    public interface IWarehouseAdjustmentTypeAPIRepository : IGenericAPIRepository
    {
        IList<WarehouseAdjustmentTypeBase> GetWarehouseAdjustmentTypeBases();
        IList<WarehouseAdjustmentTypeTree> GetWarehouseAdjustmentTypeTrees();
    }
}
