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
    public class CustomerAPIs
    {
        private readonly ICustomerAPIRepository customerAPIRepository;

        public CustomerAPIs(ICustomerAPIRepository customerAPIRepository)
        {
            this.customerAPIRepository = customerAPIRepository;
        }


        public ICollection<CustomerIndex> GetCustomerIndexes()
        {
            return this.customerAPIRepository.GetEntityIndexes<CustomerIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate).ToList();
        }

        public IList<CustomerBase> GetCustomerBases()
        {
            return this.customerAPIRepository.GetCustomerBases();
        }

        public IList<CustomerTree> GetCustomerTrees()
        {
            return this.customerAPIRepository.GetCustomerTrees();
        }
    }
}
