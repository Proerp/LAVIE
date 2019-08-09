using System.Collections.Generic;

using TotalModel.Models;

namespace TotalCore.Repositories.Commons
{
    public interface ICustomerCategoryRepository
    {

    }

    public interface ICustomerCategoryAPIRepository : IGenericAPIRepository
    {
        IList<CustomerCategoryBase> GetCustomerCategoryBases();
    }
}
