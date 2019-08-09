using System.Collections.Generic;

using TotalModel.Models;

namespace TotalCore.Repositories.Commons
{
    public interface IEmployeeRepository
    {

    }

    public interface IEmployeeAPIRepository : IGenericAPIRepository
    {
        IList<EmployeeBase> GetEmployeeBases(int? userID, int? nmvnTaskID, int? roleID);
        IList<EmployeeTree> GetEmployeeTrees();
    }
}
