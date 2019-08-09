using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;

using TotalCore.Repositories;
using TotalModel.Models;


namespace TotalDAL.Repositories
{
    public class GenericAPIRepository : BaseRepository, IGenericAPIRepository
    {
        private readonly string functionNameGetEntityIndexes;

        public GenericAPIRepository(TotalSmartCodingEntities totalSmartCodingEntities, string functionNameGetEntityIndexes)
            : base(totalSmartCodingEntities)
        {
            this.functionNameGetEntityIndexes = functionNameGetEntityIndexes;
        }

        public virtual ICollection<TEntityIndex> GetEntityIndexes<TEntityIndex>(int userID, DateTime fromDate, DateTime toDate)
        {
            return base.ExecuteFunction<TEntityIndex>(this.functionNameGetEntityIndexes, this.GetEntityIndexParameters(userID, fromDate, toDate));
        }

        protected virtual ObjectParameter[] GetEntityIndexParameters(int userID, DateTime fromDate, DateTime toDate)
        {
            return new ObjectParameter[] { new ObjectParameter("UserID", userID), new ObjectParameter("FromDate", fromDate), new ObjectParameter("ToDate", toDate) };
        }

    }
}
