using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

using TotalBase.Enums;
using TotalModel.Models;
using TotalCore.Repositories.Sales;

namespace TotalDAL.Repositories.Sales
{
    public class SalesOrderRepository : GenericWithDetailRepository<SalesOrder, SalesOrderDetail>, ISalesOrderRepository
    {
        public SalesOrderRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "SalesOrderEditable", "SalesOrderApproved", null, "SalesOrderVoidable")            
        {
        }
    }








    public class SalesOrderAPIRepository : GenericAPIRepository, ISalesOrderAPIRepository
    {
        public SalesOrderAPIRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "GetSalesOrderIndexes")
        {
        }
    }


}
