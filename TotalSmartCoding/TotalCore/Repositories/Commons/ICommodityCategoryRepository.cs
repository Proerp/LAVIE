using System.Collections.Generic;

using TotalModel.Models;

namespace TotalCore.Repositories.Commons
{
    public interface ICommodityCategoryRepository : IGenericRepository<CommodityCategory>
    {

    }

    public interface ICommodityCategoryAPIRepository : IGenericAPIRepository
    {
        IList<CommodityCategoryBase> GetCommodityCategoryBases();
    }
}
