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
    public class WarehouseAPIs
    {
        private readonly IWarehouseAPIRepository warehouseAPIRepository;

        public WarehouseAPIs(IWarehouseAPIRepository warehouseAPIRepository)
        {
            this.warehouseAPIRepository = warehouseAPIRepository;
        }


        public ICollection<WarehouseIndex> GetWarehouseIndexes()
        {
            return this.warehouseAPIRepository.GetEntityIndexes<WarehouseIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate).ToList();
        }

        public IList<WarehouseBase> GetWarehouseBases()
        {
            return this.warehouseAPIRepository.GetWarehouseBases();
        }

        public IList<WarehouseTree> GetWarehouseTrees(int? locationID)
        {
            return this.warehouseAPIRepository.GetWarehouseTrees(locationID);
        }

        public int? GetWarehouseLocationID(int? warehouseID)
        {
            return this.warehouseAPIRepository.GetWarehouseLocationID(warehouseID);
        }

    }
}
