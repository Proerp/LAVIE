using System.Linq;
using System.Collections.Generic;

using TotalModel.Models;
using TotalCore.Repositories.Commons;


namespace TotalDAL.Repositories.Commons
{
    public class BatchTypeRepository : GenericRepository<BatchType>, IBatchTypeRepository
    {
        public BatchTypeRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "BatchTypeEditable")
        {
        }
    }





    public class BatchTypeAPIRepository : GenericAPIRepository, IBatchTypeAPIRepository
    {
        public BatchTypeAPIRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "GetBatchTypeIndexes")
        {
        }

        public IList<BatchTypeBase> GetBatchTypeBases()
        {
            return this.TotalSmartCodingEntities.GetBatchTypeBases().ToList();
        }

        public IList<BatchTypeTree> GetBatchTypeTrees()
        {
            return this.TotalSmartCodingEntities.GetBatchTypeTrees().ToList();
        }
    }
}
