using System.Collections.Generic;

using TotalModel.Models;

namespace TotalCore.Repositories.Generals
{
    public interface IUserRepository : IGenericRepository<User>
    {

    }

    public interface IUserAPIRepository : IGenericAPIRepository
    {
        IList<OrganizationalUnitIndex> GetOrganizationalUnitIndexes();

        List<UserTree> GetUserTrees(int? activeOption);

        IList<ActiveUser> GetActiveUsers(string securityIdentifier);
        string GetPasswordHash(int userID);
        int SetPasswordHash(int userID, string passwordHash);

        IList<UserAccessControl> GetUserAccessControls(int? userID, int? nmvnTaskID);

        int UserRemove(int? userID);
        int UserAdd(int? organizationalUnitID, string firstName, string lastName, string userName, string securityIdentifier);
        int SaveUserAccessControls(int? accessControlID, int? accessLevel, bool? approvalPermitted, bool? unApprovalPermitted, bool? voidablePermitted, bool? unVoidablePermitted, bool? showDiscount);
    }
}
