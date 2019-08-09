using System.Collections.Generic;

using TotalModel.Models;

namespace TotalCore.Repositories.Commons
{
    public interface ILocationRepository
    {

    }

    public interface ILocationAPIRepository : IGenericAPIRepository
    {
        IList<LocationBase> GetLocationBases();
    }
}
