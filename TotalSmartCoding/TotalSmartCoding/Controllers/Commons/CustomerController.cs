using TotalModel.Models;
using TotalDTO.Commons;

using TotalCore.Services.Commons;
using TotalSmartCoding.ViewModels.Commons;

namespace TotalSmartCoding.Controllers.Commons
{
    public class CustomerController : GenericSimpleController<Customer, CustomerDTO, CustomerPrimitiveDTO, CustomerViewModel>
    {
        public CustomerController(ICustomerService customerService, CustomerViewModel customerViewModel)
            : base(customerService, customerViewModel)
        {
        }
    }
}
