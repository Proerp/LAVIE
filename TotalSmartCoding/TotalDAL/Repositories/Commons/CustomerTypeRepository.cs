using System.Linq;
using System.Collections.Generic;

using TotalModel.Models;
using TotalCore.Repositories.Commons;


namespace TotalDAL.Repositories.Commons
{
    public class CustomerTypeRepository : GenericRepository<CustomerType>, ICustomerTypeRepository
    {
        public CustomerTypeRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "CustomerTypeEditable")
        {
        }
    }





    public class CustomerTypeAPIRepository : GenericAPIRepository, ICustomerTypeAPIRepository
    {
        public CustomerTypeAPIRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "GetCustomerTypeIndexes")
        {
        }

        public IList<CustomerTypeBase> GetCustomerTypeBases()
        {
            return this.TotalSmartCodingEntities.GetCustomerTypeBases().ToList();
        }
    }
}
