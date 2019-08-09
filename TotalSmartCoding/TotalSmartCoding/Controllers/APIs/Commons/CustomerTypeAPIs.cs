using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;


using TotalBase.Enums;
using TotalModel.Models;

using TotalDTO.Inventories;

using TotalCore.Repositories.Commons;
using TotalBase;

namespace TotalSmartCoding.Controllers.APIs.Commons
{
    public class CustomerTypeAPIs
    {
        private readonly ICustomerTypeAPIRepository customerTypeAPIRepository;

        public CustomerTypeAPIs(ICustomerTypeAPIRepository customerTypeAPIRepository)
        {
            this.customerTypeAPIRepository = customerTypeAPIRepository;
        }


        public ICollection<CustomerTypeIndex> GetCustomerTypeIndexes()
        {
            return this.customerTypeAPIRepository.GetEntityIndexes<CustomerTypeIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate).ToList();
        }

        public IList<CustomerTypeBase> GetCustomerTypeBases()
        {
            return this.customerTypeAPIRepository.GetCustomerTypeBases();
        }

    }
}
