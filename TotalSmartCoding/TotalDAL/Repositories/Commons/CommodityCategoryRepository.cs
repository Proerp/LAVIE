using System.Linq;
using System.Collections.Generic;

using TotalModel.Models;
using TotalCore.Repositories.Commons;


namespace TotalDAL.Repositories.Commons
{
    public class CommodityCategoryRepository : GenericRepository<CommodityCategory>, ICommodityCategoryRepository
    {
        public CommodityCategoryRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "CommodityCategoryEditable")
        {
        }
    }





    public class CommodityCategoryAPIRepository : GenericAPIRepository, ICommodityCategoryAPIRepository
    {
        public CommodityCategoryAPIRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "GetCommodityCategoryIndexes")
        {
        }

        public IList<CommodityCategoryBase> GetCommodityCategoryBases()
        {
            return this.TotalSmartCodingEntities.GetCommodityCategoryBases().ToList();
        }
    }
}
