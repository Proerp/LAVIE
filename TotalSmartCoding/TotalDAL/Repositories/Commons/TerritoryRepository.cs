using System.Linq;
using System.Collections.Generic;

using TotalModel.Models;
using TotalCore.Repositories.Commons;


namespace TotalDAL.Repositories.Commons
{
    public class TerritoryRepository : GenericRepository<Territory>, ITerritoryRepository
    {
        public TerritoryRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "TerritoryEditable")
        {
        }
    }





    public class TerritoryAPIRepository : GenericAPIRepository, ITerritoryAPIRepository
    {
        public TerritoryAPIRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "GetTerritoryIndexes")
        {
        }

        public IList<TerritoryBase> GetTerritoryBases()
        {
            return this.TotalSmartCodingEntities.GetTerritoryBases().ToList();
        }
    }
}
