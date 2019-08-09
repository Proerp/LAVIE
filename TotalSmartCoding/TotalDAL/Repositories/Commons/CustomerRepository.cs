using System.Linq;
using System.Collections.Generic;

using TotalModel.Models;
using TotalCore.Repositories.Commons;


namespace TotalDAL.Repositories.Commons
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "CustomerEditable")
        {
        }
    }








    public class CustomerAPIRepository : GenericAPIRepository, ICustomerAPIRepository
    {
        public CustomerAPIRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "GetCustomerIndexes")
        {
        }

        public IList<CustomerBase> GetCustomerBases()
        {
            return this.TotalSmartCodingEntities.GetCustomerBases().ToList();
        }

        public IList<CustomerTree> GetCustomerTrees()
        {
            return this.TotalSmartCodingEntities.GetCustomerTrees().ToList();
        }
    }

}
