using System.Linq;
using System.Collections.Generic;

using TotalModel.Models;
using TotalCore.Repositories.Commons;


namespace TotalDAL.Repositories.Commons
{
    public class CommodityTypeRepository : GenericRepository<CommodityType>, ICommodityTypeRepository
    {
        public CommodityTypeRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "CommodityTypeEditable")
        {
        }
    }





    public class CommodityTypeAPIRepository : GenericAPIRepository, ICommodityTypeAPIRepository
    {
        public CommodityTypeAPIRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "GetCommodityTypeIndexes")
        {
        }

        public IList<CommodityTypeBase> GetCommodityTypeBases()
        {
            return this.TotalSmartCodingEntities.GetCommodityTypeBases().ToList();
        }

        public IList<CommodityTypeTree> GetCommodityTypeTrees()
        {
            return this.TotalSmartCodingEntities.GetCommodityTypeTrees().ToList();
        }
    }
}
