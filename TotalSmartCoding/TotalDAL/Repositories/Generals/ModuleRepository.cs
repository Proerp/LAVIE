using System.Linq;
using System.Collections.Generic;

using TotalModel.Models;
using TotalCore.Repositories.Generals;

namespace TotalDAL.Repositories.Generals
{
    public class ModuleRepository : GenericRepository<Module>, IModuleRepository
    {
        public ModuleRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "ModuleEditable")
        {
        }
    }





    public class ModuleAPIRepository : GenericAPIRepository, IModuleAPIRepository
    {
        public ModuleAPIRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "GetModuleIndexes")
        {
        }

        public IList<ModuleDetailIndex> GetModuleDetailIndexes()
        {
            return this.TotalSmartCodingEntities.GetModuleDetailIndexes().ToList();
        }

        public IList<ModuleViewDetail> GetModuleViewDetails(int? moduleID)
        {
            return this.TotalSmartCodingEntities.GetModuleViewDetails(moduleID).ToList();
        }
    }
}
