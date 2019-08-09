using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;


using TotalBase.Enums;
using TotalModel.Models;

using TotalDTO.Inventories;

using TotalCore.Repositories.Generals;
using TotalBase;

namespace TotalSmartCoding.Controllers.APIs.Generals
{
    public class ModuleAPIs
    {
        private readonly IModuleAPIRepository moduleAPIRepository;

        public ModuleAPIs(IModuleAPIRepository moduleAPIRepository)
        {
            this.moduleAPIRepository = moduleAPIRepository;
        }


        public ICollection<ModuleIndex> GetModuleIndexes()
        {
            return this.moduleAPIRepository.GetEntityIndexes<ModuleIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate).ToList();
        }

        public IList<ModuleDetailIndex> GetModuleDetailIndexes()
        {
            return this.moduleAPIRepository.GetModuleDetailIndexes();
        }

        public IList<ModuleViewDetail> GetModuleViewDetails(int? moduleID)
        {
            return this.moduleAPIRepository.GetModuleViewDetails(moduleID);
        }

    }
}
