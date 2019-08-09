using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalBase
{
    public class UserInformation
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string SecurityIdentifier { get; set; }
        public string FullyQualifiedUserName { get; set; }
        public int LocationID { get; set; }
        public string LocationName { get; set; }
        public int OrganizationalUnitID { get; set; }
        public bool IsDatabaseAdmin { get; set; }
        public DateTime UserDate { get; set; }

        public UserInformation() : this(-1, -1, -1, "", "", "", "", false, DateTime.MinValue) { }

        public UserInformation(int userID, int organizationalUnitID, int locationID, string locationName, string userName, string securityIdentifier, string fullyQualifiedUserName, bool isDatabaseAdmin, DateTime userDate)
        {
            this.UserID = userID;
            this.UserName = userName;
            this.SecurityIdentifier = securityIdentifier;
            this.FullyQualifiedUserName = fullyQualifiedUserName;
            
            this.OrganizationalUnitID = organizationalUnitID;
            this.LocationID = locationID;
            this.LocationName = locationName;

            this.IsDatabaseAdmin = isDatabaseAdmin;
            this.UserDate = userDate;
        }
    }
}
