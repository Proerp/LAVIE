using System.Collections.Generic;

using TotalModel.Models;

namespace TotalCore.Repositories.Generals
{
    public interface IModuleRepository
    {

    }

    public interface IModuleAPIRepository : IGenericAPIRepository
    {
        IList<ModuleDetailIndex> GetModuleDetailIndexes();
        IList<ModuleViewDetail> GetModuleViewDetails(int? moduleID);
    }
}
