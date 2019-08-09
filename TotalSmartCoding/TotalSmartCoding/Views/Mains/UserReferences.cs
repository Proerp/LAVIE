using System;
using System.Windows.Forms;
using System.Collections.Generic;

using Ninject;
using BrightIdeasSoftware;

using TotalModel.Models;

using TotalCore.Repositories.Generals;
using TotalSmartCoding.Libraries;
using TotalSmartCoding.Libraries.Helpers;
using TotalSmartCoding.Controllers.APIs.Generals;
using TotalBase;
using TotalSmartCoding.Libraries.StackedHeaders;
using System.ComponentModel;
using AutoMapper;
using TotalDTO.Generals;


namespace TotalSmartCoding.Views.Mains
{
    public partial class UserReferences : Form
    {
        private Binding bindingUserID;
        private UserAPIs userAPIs { get; set; }

        private BindingList<UserAccessControlDTO> bindingListUserAccessControls;

        public UserReferences()
        {
            InitializeComponent();
            try
            {
                this.UserID = ContextAttributes.User.UserID;

                ModuleAPIs moduleAPIs = new ModuleAPIs(CommonNinject.Kernel.Get<IModuleAPIRepository>());

                this.fastNMVNTasks.ShowGroups = true;
                this.fastNMVNTasks.AboutToCreateGroups += fastNMVNTasks_AboutToCreateGroups;
                this.fastNMVNTasks.SetObjects(moduleAPIs.GetModuleDetailIndexes());
                this.fastNMVNTasks.Sort(this.olvModuleName, SortOrder.Ascending);

                this.userAPIs = new UserAPIs(CommonNinject.Kernel.Get<IUserAPIRepository>());
                this.comboUserID.ComboBox.DataSource = this.userAPIs.GetUserIndexes();
                this.comboUserID.ComboBox.DisplayMember = CommonExpressions.PropertyName<UserIndex>(p => p.FullyQualifiedUserName);
                this.comboUserID.ComboBox.ValueMember = CommonExpressions.PropertyName<UserIndex>(p => p.UserID);
                this.bindingUserID = this.comboUserID.ComboBox.DataBindings.Add("SelectedValue", this, CommonExpressions.PropertyName<UserIndex>(p => p.UserID), true, DataSourceUpdateMode.OnPropertyChanged);


                this.gridexUserAccessControl.AutoGenerateColumns = false;
                this.gridexUserAccessControl.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                this.bindingListUserAccessControls = new BindingList<UserAccessControlDTO>();
                this.gridexUserAccessControl.DataSource = this.bindingListUserAccessControls;
                this.bindingListUserAccessControls.ListChanged += bindingListUserAccessControls_ListChanged;

                StackedHeaderDecorator stackedHeaderDecorator = new StackedHeaderDecorator(this.gridexUserAccessControl);
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void comboUserID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboUserID.SelectedItem != null)
            {
                UserIndex userIndex = this.comboUserID.SelectedItem as UserIndex;
                if (userIndex != null)
                    this.labelCaption.Text = "            " + userIndex.LocationName + "\\" + userIndex.OrganizationalUnitName;
            }
        }

        private void fastNMVNTasks_AboutToCreateGroups(object sender, BrightIdeasSoftware.CreateGroupsEventArgs e)
        {
            if (e.Groups != null && e.Groups.Count > 0)
            {
                foreach (OLVGroup olvGroup in e.Groups)
                {
                    olvGroup.TitleImage = "Assembly-32";
                    olvGroup.Subtitle = "Count: " + olvGroup.Contents.Count.ToString() + " Task" + (olvGroup.Contents.Count > 1 ? "s" : "");
                }
            }
        }

        private int userID;
        public int UserID
        {
            get { return this.userID; }
            set
            {
                if (this.userID != value)
                {
                    this.userID = value;
                    this.GetUserAccessControls();
                }
            }
        }


        private void fastNMVNTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetUserAccessControls();
        }

        private void GetUserAccessControls()
        {
            try
            {
                if (this.UserID > 0 && this.fastNMVNTasks.SelectedObject != null)
                {
                    ModuleDetailIndex moduleDetailIndex = (ModuleDetailIndex)this.fastNMVNTasks.SelectedObject;
                    if (moduleDetailIndex != null)
                    {
                        IList<UserAccessControl> userAccessControls = this.userAPIs.GetUserAccessControls(this.UserID, moduleDetailIndex.ModuleDetailID);
                        this.bindingListUserAccessControls.RaiseListChangedEvents = false;
                        Mapper.Map<ICollection<UserAccessControl>, ICollection<UserAccessControlDTO>>(userAccessControls, this.bindingListUserAccessControls);
                        this.bindingListUserAccessControls.RaiseListChangedEvents = true;
                        this.bindingListUserAccessControls.ResetBindings();
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void gridexAccessControls_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.gridexUserAccessControl.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void bindingListUserAccessControls_ListChanged(object sender, ListChangedEventArgs e)
        {
            try
            {
                if (e.PropertyDescriptor != null && e.NewIndex >= 0 && e.NewIndex < this.bindingListUserAccessControls.Count)
                {
                    UserAccessControlDTO userAccessControlDTO = this.bindingListUserAccessControls[e.NewIndex];
                    if (userAccessControlDTO != null)
                        this.userAPIs.SaveUserAccessControls(userAccessControlDTO.AccessControlID, userAccessControlDTO.AccessLevel, userAccessControlDTO.ApprovalPermitted, userAccessControlDTO.UnApprovalPermitted, userAccessControlDTO.VoidablePermitted, userAccessControlDTO.UnVoidablePermitted, userAccessControlDTO.ShowDiscount);
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void buttonUserAdd_Click(object sender, EventArgs e)
        {
            UserAdd wizardUserAdd = new UserAdd(this.userAPIs);
            DialogResult dialogResult = wizardUserAdd.ShowDialog();

            wizardUserAdd.Dispose();
            if (dialogResult == DialogResult.OK) this.comboUserID.ComboBox.DataSource = this.userAPIs.GetUserIndexes();
        }

        private void buttonUserRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.UserID > 0)
                {
                    if (CustomMsgBox.Show(this, "Are you sure you want to delete " + this.comboUserID.Text + "?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
                    {
                        this.userAPIs.UserRemove(this.UserID);
                        this.comboUserID.ComboBox.DataSource = this.userAPIs.GetUserIndexes();
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }
    }
}
