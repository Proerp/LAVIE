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
    public class EmployeeAPIs
    {
        private readonly IEmployeeAPIRepository employeeAPIRepository;

        public EmployeeAPIs(IEmployeeAPIRepository employeeAPIRepository)
        {
            this.employeeAPIRepository = employeeAPIRepository;
        }


        public ICollection<EmployeeIndex> GetEmployeeIndexes()
        {
            return this.employeeAPIRepository.GetEntityIndexes<EmployeeIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate).ToList();
        }

        public IList<EmployeeBase> GetEmployeeBases(int? userID, int? nmvnTaskID, int? roleID)
        {
            return this.employeeAPIRepository.GetEmployeeBases(userID, nmvnTaskID, roleID);
        }

        public IList<EmployeeTree> GetEmployeeTrees()
        {
            return this.employeeAPIRepository.GetEmployeeTrees();
        }
    }
}
