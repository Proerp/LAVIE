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
    public class CommodityCategoryAPIs
    {
        private readonly ICommodityCategoryAPIRepository commodityCategoryAPIRepository;

        public CommodityCategoryAPIs(ICommodityCategoryAPIRepository commodityCategoryAPIRepository)
        {
            this.commodityCategoryAPIRepository = commodityCategoryAPIRepository;
        }


        public ICollection<CommodityCategoryIndex> GetCommodityCategoryIndexes()
        {
            return this.commodityCategoryAPIRepository.GetEntityIndexes<CommodityCategoryIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate).ToList();
        }

        public IList<CommodityCategoryBase> GetCommodityCategoryBases()
        {
            return this.commodityCategoryAPIRepository.GetCommodityCategoryBases();
        }

    }
}
