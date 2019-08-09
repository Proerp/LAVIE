using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;

using TotalBase;
using TotalModel.Models;
using TotalSmartCoding.Libraries.Helpers;
using TotalSmartCoding.Controllers.APIs.Generals;
using TotalSmartCoding.Libraries;


namespace TotalSmartCoding.Views.Mains
{
    public partial class UserAdd : Form
    {

        private UserAPIs userAPIs { get; set; }

        public string UserName { get; set; }
        public int? OrganizationalUnitID { get; set; }

        private Binding bindingUserName;
        private Binding bindingOrganizationalUnitID;

        public UserAdd(UserAPIs userAPIs)
        {
            InitializeComponent();

            try
            {
                List<DomainUser> allUsers = new List<DomainUser>();
                PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "chevronvn.com"); //, "OU=SomeOU,dc=YourCompany,dc=com"// create your domain context and define the OU container to search in
                UserPrincipal qbeUser = new UserPrincipal(ctx);// define a "query-by-example" principal - here, we search for a UserPrincipal (user)
                PrincipalSearcher srch = new PrincipalSearcher(qbeUser); // create your principal searcher passing in the QBE principal    

                foreach (var found in srch.FindAll())// find all matches
                {// do whatever here - "found" is of type "Principal" - it could be user, group, computer.....          
                    allUsers.Add(new DomainUser() { FirstName = found.DisplayName, LastName = found.Name, UserName = this.GetWindowsIdentityName(found.DistinguishedName), SecurityIdentifier = found.Sid.Value });
                }

                this.combexUserID.DataSource = allUsers;
                this.combexUserID.DisplayMember = CommonExpressions.PropertyName<DomainUser>(p => p.UserName);
                this.combexUserID.ValueMember = CommonExpressions.PropertyName<DomainUser>(p => p.UserName);
                this.bindingUserName = this.combexUserID.DataBindings.Add("SelectedValue", this, CommonExpressions.PropertyName<DomainUser>(p => p.UserName), true, DataSourceUpdateMode.OnPropertyChanged);

                this.userAPIs = userAPIs;
                this.combexOrganizationalUnitID.DataSource = this.userAPIs.GetOrganizationalUnitIndexes();
                this.combexOrganizationalUnitID.DisplayMember = CommonExpressions.PropertyName<OrganizationalUnitIndex>(p => p.LocationOrganizationalUnitName);
                this.combexOrganizationalUnitID.ValueMember = CommonExpressions.PropertyName<OrganizationalUnitIndex>(p => p.OrganizationalUnitID);
                this.bindingOrganizationalUnitID = this.combexOrganizationalUnitID.DataBindings.Add("SelectedValue", this, CommonExpressions.PropertyName<OrganizationalUnitIndex>(p => p.OrganizationalUnitID), true, DataSourceUpdateMode.OnPropertyChanged);
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private string GetWindowsIdentityName(string distinguishedName)
        {
            string windowsIdentityName = "";

            string[] arrayName = distinguishedName.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string stringName in arrayName)
            {
                if (stringName.IndexOf("CN=") >= 0)
                    windowsIdentityName = windowsIdentityName + "\\" + stringName.Substring(stringName.IndexOf("CN=") + "CN=".Length);
                if (stringName.IndexOf("DC=") >= 0 && stringName.IndexOf("DC=com") < 0)
                    windowsIdentityName = (stringName.Substring(stringName.IndexOf("DC=") + "DC=".Length)).ToUpper() + windowsIdentityName;
            }

            return windowsIdentityName;
        }

        private void buttonOKESC_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender.Equals(this.buttonOK))
                {
                    if (this.combexUserID.SelectedIndex >= 0 && this.UserName != null && this.OrganizationalUnitID != null)
                    {
                        DomainUser domainUser = this.combexUserID.SelectedItem as DomainUser;
                        if (domainUser != null)
                        {
                            this.userAPIs.UserAdd(this.OrganizationalUnitID, domainUser.FirstName, domainUser.LastName, domainUser.UserName, domainUser.SecurityIdentifier);
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                }

                if (sender.Equals(this.buttonESC))
                    this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

    }

    public class DomainUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string SecurityIdentifier { get; set; }
    }

}
