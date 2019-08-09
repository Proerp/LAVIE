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
    public class TerritoryAPIs
    {
        private readonly ITerritoryAPIRepository territoryAPIRepository;

        public TerritoryAPIs(ITerritoryAPIRepository territoryAPIRepository)
        {
            this.territoryAPIRepository = territoryAPIRepository;
        }


        public ICollection<TerritoryIndex> GetTerritoryIndexes()
        {
            return this.territoryAPIRepository.GetEntityIndexes<TerritoryIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate).ToList();
        }

        public IList<TerritoryBase> GetTerritoryBases()
        {
            return this.territoryAPIRepository.GetTerritoryBases();
        }

    }
}
