using System.Collections.Generic;

using TotalModel.Models;

namespace TotalCore.Repositories.Commons
{
    public interface ICommodityTypeRepository : IGenericRepository<CommodityType>
    {

    }

    public interface ICommodityTypeAPIRepository : IGenericAPIRepository
    {
        IList<CommodityTypeBase> GetCommodityTypeBases();
        IList<CommodityTypeTree> GetCommodityTypeTrees();
    }
}
