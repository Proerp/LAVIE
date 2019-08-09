using System.Linq;
using System.Collections.Generic;

using TotalModel.Models;
using TotalCore.Repositories.Commons;


namespace TotalDAL.Repositories.Commons
{
    public class TransferOrderTypeRepository : GenericRepository<TransferOrderType>, ITransferOrderTypeRepository
    {
        public TransferOrderTypeRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "TransferOrderTypeEditable")
        {
        }
    }





    public class TransferOrderTypeAPIRepository : GenericAPIRepository, ITransferOrderTypeAPIRepository
    {
        public TransferOrderTypeAPIRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "GetTransferOrderTypeIndexes")
        {
        }

        public IList<TransferOrderTypeBase> GetTransferOrderTypeBases()
        {
            return this.TotalSmartCodingEntities.GetTransferOrderTypeBases().ToList();
        }
    }
}
