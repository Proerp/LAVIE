using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Generals
{
    public class UserReference
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public UserReference(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetUserIndexes();
            this.GetOrganizationalUnitIndexes();

            this.GetActiveUsers();
            this.GetPasswordHash();
            this.SetPasswordHash();

            this.UserAdd();
            this.UserRemove();

            this.GetUserAccessControls();
            this.SaveUserAccessControls();

            this.GetUserTrees();
        }


        private void GetUserIndexes()
        {
            string queryString;

            queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      Users.UserID, Users.FirstName, Users.LastName, Users.UserName, Users.SecurityIdentifier, Users.IsDatabaseAdmin, OrganizationalUnits.Name AS OrganizationalUnitName, Locations.Name AS LocationName " + "\r\n";
            queryString = queryString + "       FROM        Users " + "\r\n";
            queryString = queryString + "                   INNER JOIN OrganizationalUnitUsers ON Users.UserID = OrganizationalUnitUsers.UserID AND OrganizationalUnitUsers.InActive = 0 " + "\r\n";
            queryString = queryString + "                   INNER JOIN OrganizationalUnits ON OrganizationalUnitUsers.OrganizationalUnitID = OrganizationalUnits.OrganizationalUnitID " + "\r\n";
            queryString = queryString + "                   INNER JOIN Locations ON OrganizationalUnits.LocationID = Locations.LocationID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetUserIndexes", queryString);
        }

        private void GetOrganizationalUnitIndexes()
        {
            string queryString;

            queryString = " " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      OrganizationalUnits.OrganizationalUnitID, OrganizationalUnits.Name AS OrganizationalUnitName, Locations.Name AS LocationName, Locations.Name + '\\' + OrganizationalUnits.Name AS LocationOrganizationalUnitName " + "\r\n";
            queryString = queryString + "       FROM        Locations INNER JOIN OrganizationalUnits ON Locations.LocationID = OrganizationalUnits.LocationID " + "\r\n";
            queryString = queryString + "       ORDER BY    Locations.Name, OrganizationalUnits.Name " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetOrganizationalUnitIndexes", queryString);
        }

        private void GetActiveUsers()
        {
            string queryString;

            queryString = " @SecurityIdentifier nvarchar(256) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      Users.UserID, Users.FirstName, Users.LastName, Users.UserName, Users.SecurityIdentifier, Users.IsDatabaseAdmin, Users.OrganizationalUnitID, OrganizationalUnits.Name AS OrganizationalUnitName, OrganizationalUnits.LocationID, Locations.Name AS LocationName, Users.PasswordHash " + "\r\n";
            queryString = queryString + "       FROM        Users " + "\r\n";
            queryString = queryString + "                   INNER JOIN OrganizationalUnits ON Users.SecurityIdentifier = @SecurityIdentifier AND Users.InActive = 0 AND Users.OrganizationalUnitID = OrganizationalUnits.OrganizationalUnitID " + "\r\n";
            queryString = queryString + "                   INNER JOIN Locations ON OrganizationalUnits.LocationID = Locations.LocationID " + "\r\n";
            queryString = queryString + "       ORDER BY    Users.UserName DESC " + "\r\n";
            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetActiveUsers", queryString);
        }

        private void GetPasswordHash()
        {
            string queryString;

            queryString = " @UserID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";
            queryString = queryString + "       SELECT      PasswordHash FROM Users WHERE UserID = @UserID " + "\r\n";
            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetPasswordHash", queryString);
        }

        private void SetPasswordHash()
        {
            string queryString;

            queryString = " @UserID int, @PasswordHash nvarchar(1000) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";
            queryString = queryString + "       UPDATE      Users SET PasswordHash = @PasswordHash WHERE UserID = @UserID " + "\r\n";
            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("SetPasswordHash", queryString);
        }

        private void UserAdd()
        {
            string queryString = " @OrganizationalUnitID int, @FirstName nvarchar(60), @LastName nvarchar(60), @UserName nvarchar(256), @SecurityIdentifier nvarchar(256) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            //AT NOW: OrganizationalUnitUsers KHONG CON PHU HOP NUA, TUY NHIEN, VAN PHAI DUY TRI VI KHONG CO THOI GIAN MODIFY
            //YEU CAU LUC NAY LA: LAM SAO DAM BAO Users(UserID, OrganizationalUnitID) VA OrganizationalUnitUsers(OrganizationalUnitID, UserID) PHAI MATCH 1-1
            //DO DO: KHI ADD, REMOVE, EDIT, INACTIVE, ... PHAI DAM BAO YEU CAU NAY THI MOI THU SE OK
            queryString = queryString + "       BEGIN " + "\r\n";

            queryString = queryString + "           DECLARE         @UserID Int" + "\r\n";
            queryString = queryString + "           INSERT INTO     Users (OrganizationalUnitID, FirstName, LastName, UserName, SecurityIdentifier, IsDatabaseAdmin, InActive) VALUES (@OrganizationalUnitID, @FirstName, @LastName, @UserName, @SecurityIdentifier, 0, 0) " + "\r\n";
            queryString = queryString + "           SELECT          @UserID = SCOPE_IDENTITY() " + "\r\n";
            queryString = queryString + "           INSERT INTO     OrganizationalUnitUsers (OrganizationalUnitID, UserID, InActive) VALUES (@OrganizationalUnitID, @UserID, 0) " + "\r\n";

            queryString = queryString + "           INSERT INTO     AccessControls (UserID, NMVNTaskID, OrganizationalUnitID, AccessLevel, ApprovalPermitted, UnApprovalPermitted, VoidablePermitted, UnVoidablePermitted, ShowDiscount) " + "\r\n";
            queryString = queryString + "           SELECT          @UserID, ModuleDetails.ModuleDetailID, OrganizationalUnits.OrganizationalUnitID, 0 AS AccessLevel, 0 AS ApprovalPermitted, 0 AS UnApprovalPermitted, 0 AS VoidablePermitted, 0 AS UnVoidablePermitted, 0 AS ShowDiscount " + "\r\n";
            queryString = queryString + "           FROM            ModuleDetails CROSS JOIN OrganizationalUnits" + "\r\n";
            queryString = queryString + "           WHERE           ModuleDetails.InActive = 0 " + "\r\n";

            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("UserAdd", queryString);
        }

        private void UserRemove()
        {
            string queryString = " @UserID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       BEGIN " + "\r\n";

            queryString = queryString + "           DELETE FROM     AccessControls WHERE UserID = @UserID " + "\r\n";
            queryString = queryString + "           DELETE FROM     OrganizationalUnitUsers WHERE UserID = @UserID " + "\r\n";
            queryString = queryString + "           DELETE FROM     Users WHERE UserID = @UserID " + "\r\n";

            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("UserRemove", queryString);
        }

        private void GetUserAccessControls()
        {
            string queryString;

            queryString = " @UserID int, @NMVNTaskID int" + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";
           
            queryString = queryString + "       SELECT      AccessControls.AccessControlID, Locations.Name AS LocationName, OrganizationalUnits.Name AS OrganizationalUnitName, AccessControls.OrganizationalUnitID, AccessControls.AccessLevel, AccessControls.ApprovalPermitted, AccessControls.UnApprovalPermitted, AccessControls.VoidablePermitted, AccessControls.UnVoidablePermitted, AccessControls.ShowDiscount " + "\r\n";
            queryString = queryString + "       FROM        AccessControls INNER JOIN OrganizationalUnits ON AccessControls.OrganizationalUnitID = OrganizationalUnits.OrganizationalUnitID INNER JOIN Locations ON OrganizationalUnits.LocationID = Locations.LocationID " + "\r\n";
            queryString = queryString + "       WHERE       AccessControls.UserID = @UserID AND AccessControls.NMVNTaskID = @NMVNTaskID " + "\r\n";
            queryString = queryString + "       ORDER BY    Locations.Name, OrganizationalUnits.Name " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetUserAccessControls", queryString);
        }

        private void SaveUserAccessControls()
        {
            string queryString = " @AccessControlID int, @AccessLevel Int, @ApprovalPermitted bit, @UnApprovalPermitted bit, @VoidablePermitted bit, @UnVoidablePermitted bit, @ShowDiscount bit " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       BEGIN " + "\r\n";

            queryString = queryString + "           UPDATE          AccessControls " + "\r\n";
            queryString = queryString + "           SET             AccessLevel = @AccessLevel, ApprovalPermitted = @ApprovalPermitted, UnApprovalPermitted = @UnApprovalPermitted, VoidablePermitted = @VoidablePermitted, UnVoidablePermitted = @UnVoidablePermitted, ShowDiscount = @ShowDiscount " + "\r\n";
            queryString = queryString + "           WHERE           AccessControlID = @AccessControlID " + "\r\n";

            queryString = queryString + "           IF @@ROWCOUNT <> 1 " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   DECLARE     @msg NVARCHAR(300) = N'Unknow error: SaveUserAccessControls. Please exit then open and try again.' ; " + "\r\n";
            queryString = queryString + "                   THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "               END " + "\r\n";
            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("SaveUserAccessControls", queryString);
        }

        private void GetUserTrees()
        {
            string queryString;

            queryString = " @ActiveOption int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      " + GlobalEnums.RootNode + " + LocationID AS NodeID, 0 AS ParentNodeID, LocationID AS PrimaryID, NULL AS AncestorID, ItemNumber, Name, 'LocationID' AS ParameterName, CAST(0 AS bit) AS Selected " + "\r\n";
            queryString = queryString + "       FROM        Locations " + "\r\n";

            queryString = queryString + "       UNION ALL " + "\r\n";
            queryString = queryString + "       SELECT      " + GlobalEnums.AncestorNode + " + OrganizationalUnitID AS NodeID, " + GlobalEnums.RootNode + " + LocationID AS ParentNodeID, OrganizationalUnitID AS PrimaryID, LocationID AS AncestorID, ItemNumber, Name, 'OrganizationalUnitID' AS ParameterName, CAST(0 AS bit) AS Selected " + "\r\n";
            queryString = queryString + "       FROM        OrganizationalUnits " + "\r\n";
            queryString = queryString + "       UNION ALL " + "\r\n";
            queryString = queryString + "       SELECT      UserID AS NodeID, " + GlobalEnums.AncestorNode + " + OrganizationalUnitID AS ParentNodeID, UserID AS PrimaryID, OrganizationalUnitID AS AncestorID, SecurityIdentifier AS ItemNumber, UserName AS Name, 'UserID' AS ParameterName, InActive AS Selected " + "\r\n";
            queryString = queryString + "       FROM        Users " + "\r\n";
            queryString = queryString + "       WHERE       (@ActiveOption = " + (int)GlobalEnums.ActiveOption.Both + " OR Users.InActive = @ActiveOption) " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetUserTrees", queryString);
        }
    }
}
