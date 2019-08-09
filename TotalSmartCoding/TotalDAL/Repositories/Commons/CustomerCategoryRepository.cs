using System.Linq;
using System.Collections.Generic;

using TotalModel.Models;
using TotalCore.Repositories.Commons;


namespace TotalDAL.Repositories.Commons
{
    public class CustomerCategoryRepository : GenericRepository<CustomerCategory>, ICustomerCategoryRepository
    {
        public CustomerCategoryRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "CustomerCategoryEditable")
        {
        }
    }





    public class CustomerCategoryAPIRepository : GenericAPIRepository, ICustomerCategoryAPIRepository
    {
        public CustomerCategoryAPIRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "GetCustomerCategoryIndexes")
        {
        }

        public IList<CustomerCategoryBase> GetCustomerCategoryBases()
        {
            return this.TotalSmartCodingEntities.GetCustomerCategoryBases().ToList();
        }
    }
}
