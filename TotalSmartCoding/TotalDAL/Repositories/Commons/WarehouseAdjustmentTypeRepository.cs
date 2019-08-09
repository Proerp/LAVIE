using System.Linq;
using System.Collections.Generic;

using TotalModel.Models;
using TotalCore.Repositories.Commons;


namespace TotalDAL.Repositories.Commons
{
    public class WarehouseAdjustmentTypeRepository : GenericRepository<WarehouseAdjustmentType>, IWarehouseAdjustmentTypeRepository
    {
        public WarehouseAdjustmentTypeRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "WarehouseAdjustmentTypeEditable")
        {
        }
    }





    public class WarehouseAdjustmentTypeAPIRepository : GenericAPIRepository, IWarehouseAdjustmentTypeAPIRepository
    {
        public WarehouseAdjustmentTypeAPIRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "GetWarehouseAdjustmentTypeIndexes")
        {
        }

        public IList<WarehouseAdjustmentTypeBase> GetWarehouseAdjustmentTypeBases()
        {
            return this.TotalSmartCodingEntities.GetWarehouseAdjustmentTypeBases().ToList();
        }

        public IList<WarehouseAdjustmentTypeTree> GetWarehouseAdjustmentTypeTrees()
        {
            return this.TotalSmartCodingEntities.GetWarehouseAdjustmentTypeTrees().ToList();
        }
    }
}
