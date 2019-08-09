using System.Collections.Generic;

using TotalModel.Models;

namespace TotalCore.Repositories.Commons
{
    public interface ICustomerTypeRepository
    {

    }

    public interface ICustomerTypeAPIRepository : IGenericAPIRepository
    {
        IList<CustomerTypeBase> GetCustomerTypeBases();
    }
}
