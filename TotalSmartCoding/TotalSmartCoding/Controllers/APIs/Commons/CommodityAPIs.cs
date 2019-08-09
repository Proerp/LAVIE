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
    public class CommodityAPIs
    {
        private readonly ICommodityAPIRepository commodityAPIRepository;

        public CommodityAPIs(ICommodityAPIRepository commodityAPIRepository)
        {
            this.commodityAPIRepository = commodityAPIRepository;
        }


        public ICollection<CommodityIndex> GetCommodityIndexes()
        {
            return this.commodityAPIRepository.GetEntityIndexes<CommodityIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate).ToList();
        }

        public CommodityBase GetCommodityBase(int commodityID)
        {
            return this.commodityAPIRepository.GetCommodityBase(commodityID);
        }

        public CommodityBase GetCommodityBase(string code)
        {
            return this.commodityAPIRepository.GetCommodityBase(code);
        }

        public IList<CommodityBase> GetCommodityBases()
        {
            return this.GetCommodityBases(false);
        }

        public IList<CommodityBase> GetCommodityBases(bool withNullRow)
        {
            return this.commodityAPIRepository.GetCommodityBases(withNullRow);
        }

        public IList<CommodityTree> GetCommodityTrees()
        {
            return this.commodityAPIRepository.GetCommodityTrees();
        }

        public IList<SearchCommodity> SearchCommodities(int? commodityID, int? locationID, int? batchID, int? deliveryAdviceID, int? transferOrderID)
        {
            return this.commodityAPIRepository.SearchCommodities(commodityID, locationID, batchID, deliveryAdviceID, transferOrderID);
        }
    }
}
