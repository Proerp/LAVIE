using System.Collections.Generic;

using TotalModel.Models;

namespace TotalCore.Repositories.Commons
{
    public interface ITerritoryRepository
    {

    }

    public interface ITerritoryAPIRepository : IGenericAPIRepository
    {
        IList<TerritoryBase> GetTerritoryBases();
    }
}
