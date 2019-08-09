using System;
using System.Collections.Generic;

using TotalBase.Enums;

namespace TotalCore.Repositories
{
    public interface IGenericAPIRepository : IBaseRepository
    {
        ICollection<TEntityIndex> GetEntityIndexes<TEntityIndex>(int userID, DateTime fromDate, DateTime toDate);
    }
}
