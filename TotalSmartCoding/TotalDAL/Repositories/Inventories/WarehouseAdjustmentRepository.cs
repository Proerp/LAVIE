using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

using TotalBase.Enums;
using TotalModel.Models;
using TotalCore.Repositories.Inventories;

namespace TotalDAL.Repositories.Inventories
{
    public class WarehouseAdjustmentRepository : GenericWithDetailRepository<WarehouseAdjustment, WarehouseAdjustmentDetail>, IWarehouseAdjustmentRepository
    {
        public WarehouseAdjustmentRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "WarehouseAdjustmentEditable", "WarehouseAdjustmentApproved")
        {
        }
    }








    public class WarehouseAdjustmentAPIRepository : GenericAPIRepository, IWarehouseAdjustmentAPIRepository
    {
        public WarehouseAdjustmentAPIRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "GetWarehouseAdjustmentIndexes")
        {
        }
    }


}
