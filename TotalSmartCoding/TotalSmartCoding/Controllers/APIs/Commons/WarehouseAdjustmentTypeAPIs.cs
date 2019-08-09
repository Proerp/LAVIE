using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;


using TotalBase.Enums;
using TotalModel.Models;

using TotalDTO.Inventories;

using TotalCore.Repositories.Commons;
using TotalBase;

namespace TotalSmartCoding.Controllers.APIs.Commons
{
    public class WarehouseAdjustmentTypeAPIs
    {
        private readonly IWarehouseAdjustmentTypeAPIRepository warehouseAdjustmentTypeAPIRepository;

        public WarehouseAdjustmentTypeAPIs(IWarehouseAdjustmentTypeAPIRepository warehouseAdjustmentTypeAPIRepository)
        {
            this.warehouseAdjustmentTypeAPIRepository = warehouseAdjustmentTypeAPIRepository;
        }


        public ICollection<WarehouseAdjustmentTypeIndex> GetWarehouseAdjustmentTypeIndexes()
        {
            return this.warehouseAdjustmentTypeAPIRepository.GetEntityIndexes<WarehouseAdjustmentTypeIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate).ToList();
        }

        public IList<WarehouseAdjustmentTypeBase> GetWarehouseAdjustmentTypeBases()
        {
            return this.warehouseAdjustmentTypeAPIRepository.GetWarehouseAdjustmentTypeBases();
        }

        public IList<WarehouseAdjustmentTypeTree> GetWarehouseAdjustmentTypeTrees()
        {
            return this.warehouseAdjustmentTypeAPIRepository.GetWarehouseAdjustmentTypeTrees();
        }

    }
}
