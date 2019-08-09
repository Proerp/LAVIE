using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

using TotalDTO.Inventories;
using TotalCore.Repositories.Commons;

namespace TotalSmartCoding.Controllers.APIs.Commons
{
    public class CommodityTypeAPIs
    {
        private readonly ICommodityTypeAPIRepository commodityCategoryAPIRepository;

        public CommodityTypeAPIs(ICommodityTypeAPIRepository commodityCategoryAPIRepository)
        {
            this.commodityCategoryAPIRepository = commodityCategoryAPIRepository;
        }


        public ICollection<CommodityTypeIndex> GetCommodityTypeIndexes()
        {
            return this.commodityCategoryAPIRepository.GetEntityIndexes<CommodityTypeIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate).ToList();
        }

        public IList<CommodityTypeBase> GetCommodityTypeBases()
        {
            return this.commodityCategoryAPIRepository.GetCommodityTypeBases();
        }

        public IList<CommodityTypeTree> GetCommodityTypeTrees()
        {
            return this.commodityCategoryAPIRepository.GetCommodityTypeTrees();
        }
    }
}
