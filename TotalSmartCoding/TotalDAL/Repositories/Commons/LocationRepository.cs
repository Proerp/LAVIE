using System.Linq;
using System.Collections.Generic;

using TotalModel.Models;
using TotalCore.Repositories.Commons;


namespace TotalDAL.Repositories.Commons
{
    //public class LocationRepository : GenericRepository<Location>, ILocationRepository
    //{
    //    public LocationRepository(TotalSmartCodingEntities totalSmartCodingEntities)
    //        : base(totalSmartCodingEntities, "LocationEditable")
    //    {
    //    }
    //}





    public class LocationAPIRepository : GenericAPIRepository, ILocationAPIRepository
    {
        public LocationAPIRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "GetLocationIndexes")
        {
        }

        public IList<LocationBase> GetLocationBases()
        {
            return this.TotalSmartCodingEntities.GetLocationBases().OrderBy(o => o.Name).ToList();
        }
    }
}
