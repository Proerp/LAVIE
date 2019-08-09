using System.Collections.Generic;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;
using TotalCore.Repositories.Sales;

namespace TotalSmartCoding.Controllers.APIs.Sales
{
    public class SalesOrderAPIs
    {
        private readonly ISalesOrderAPIRepository salesOrderAPIRepository;

        public SalesOrderAPIs(ISalesOrderAPIRepository salesOrderAPIRepository)
        {
            this.salesOrderAPIRepository = salesOrderAPIRepository;
        }


        public ICollection<SalesOrderIndex> GetSalesOrderIndexes()
        {
            return this.salesOrderAPIRepository.GetEntityIndexes<SalesOrderIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate);
        }
    }
}
