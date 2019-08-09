using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;


using TotalBase.Enums;
using TotalModel.Models;

using TotalDTO.Inventories;

using TotalCore.Repositories.Generals;
using TotalBase;
using System.Data.Entity.Core.Objects;

namespace TotalSmartCoding.Controllers.APIs.Generals
{
    public class UserAPIs
    {
        private readonly IUserAPIRepository userAPIRepository;

        public UserAPIs(IUserAPIRepository userAPIRepository)
        {
            this.userAPIRepository = userAPIRepository;
        }


        public ICollection<UserIndex> GetUserIndexes()
        {
            return this.userAPIRepository.GetEntityIndexes<UserIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate).ToList();
        }

        public IList<OrganizationalUnitIndex> GetOrganizationalUnitIndexes()
        {
            return this.userAPIRepository.GetOrganizationalUnitIndexes();
        }

        public IList<ActiveUser> GetActiveUsers(string securityIdentifier)
        {
            //this.userAPIRepository.ExecuteStoreCommand("UPDATE Users SET SecurityIdentifier = '" + securityIdentifier + "' WHERE UserID = 11 " , new ObjectParameter[] { });
            return this.userAPIRepository.GetActiveUsers(securityIdentifier);
        }

        public string GetPasswordHash(int userID)
        {
            return this.userAPIRepository.GetPasswordHash(userID);
        }

        public int SetPasswordHash(int userID, string passwordHash)
        {
            return this.userAPIRepository.SetPasswordHash(userID, passwordHash);
        }

        public IList<UserAccessControl> GetUserAccessControls(int? userID, int? nmvnTaskID)
        {
            return this.userAPIRepository.GetUserAccessControls(userID, nmvnTaskID);
        }

        public int UserAdd(int? organizationalUnitID, string firstName, string lastName, string userName, string securityIdentifier)
        {
            return this.userAPIRepository.UserAdd(organizationalUnitID, firstName, lastName, userName, securityIdentifier);
        }

        public int UserRemove(int? userID)
        {
            return this.userAPIRepository.UserRemove(userID);
        }

        public int SaveUserAccessControls(int? accessControlID, int? accessLevel, bool? approvalPermitted, bool? unApprovalPermitted, bool? voidablePermitted, bool? unVoidablePermitted, bool? showDiscount)
        {
            return this.userAPIRepository.SaveUserAccessControls(accessControlID, accessLevel, approvalPermitted, unApprovalPermitted, voidablePermitted, unVoidablePermitted, showDiscount);
        }
    }
}
