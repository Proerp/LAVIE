using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

using TotalBase.Enums;
using TotalModel.Models;
using TotalCore.Repositories.Sales;

namespace TotalDAL.Repositories.Sales
{
    public class TransferOrderRepository : GenericWithDetailRepository<TransferOrder, TransferOrderDetail>, ITransferOrderRepository
    {
        public TransferOrderRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "TransferOrderEditable", "TransferOrderApproved", null, "TransferOrderVoidable")
        {
        }
    }








    public class TransferOrderAPIRepository : GenericAPIRepository, ITransferOrderAPIRepository
    {
        public TransferOrderAPIRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "GetTransferOrderIndexes")
        {
        }
    }


}
