using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using Ninject;

using TotalDAL;
using TotalBase;
using TotalSmartCoding.Libraries.Helpers;
using TotalSmartCoding.Controllers.APIs.Commons;
using TotalSmartCoding.Libraries;
using TotalCore.Repositories.Commons;
using TotalModel.Models;
using TotalDAL.Repositories;
using TotalCore.Repositories;
using System.Data.Entity.Core.Objects;
using System.DirectoryServices.AccountManagement;
using TotalSmartCoding.Controllers.APIs.Generals;
using TotalCore.Repositories.Generals;
using TotalCore.Extensions;
using TotalBase.Enums;

namespace TotalSmartCoding.Views.Mains
{
    public partial class Logon : Form
    {
        IBaseRepository baseRepository;

        public Logon()
        {
            InitializeComponent();
        }


        public int EmployeeID { get; set; }
        private Binding employeeIDBinding;
        //private CommonMetaList commonMetaList;


        private void PublicApplicationLogon_Load(object sender, EventArgs e)
        {
            #region TEST
            //// List of strings for your names
            //List<string> allUsers = new List<string>();

            //// create your domain context and define the OU container to search in
            //PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "DOMAINNAME",
            //                                            "OU=SomeOU,dc=YourCompany,dc=com");

            //// define a "query-by-example" principal - here, we search for a UserPrincipal (user)
            //UserPrincipal qbeUser = new UserPrincipal(ctx);

            //// create your principal searcher passing in the QBE principal    
            //PrincipalSearcher srch = new PrincipalSearcher(qbeUser);

            //// find all matches
            //foreach (var found in srch.FindAll())
            //{
            //    // do whatever here - "found" is of type "Principal" - it could be user, group, computer.....          
            //    allUsers.Add(found.DisplayName);
            //}







            //using (var context = new PrincipalContext(ContextType.Domain, "yourdomain.com"))
            //{
            //    using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
            //    {
            //        foreach (var result in searcher.FindAll())
            //        {
            //            DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;
            //            Console.WriteLine("First Name: " + de.Properties["givenName"].Value);
            //            Console.WriteLine("Last Name : " + de.Properties["sn"].Value);
            //            Console.WriteLine("SAM account name   : " + de.Properties["samAccountName"].Value);
            //            Console.WriteLine("User principal name: " + de.Properties["userPrincipalName"].Value);
            //            Console.WriteLine();
            //        }
            //    }
            //}
            //Console.ReadLine();




            ////////string plainText = "Lê Minh Hiệp";
            ////////// Convert the plain string pwd into bytes
            //////////byte[] plainTextBytes = UnicodeEncoding.Unicode.GetBytes(plainText);
            //////////System.Security.Cryptography.HashAlgorithm hashAlgo = new System.Security.Cryptography.SHA256Managed();
            //////////byte[] hash = hashAlgo.ComputeHash(plainTextBytes);

            ////////byte[] data = UnicodeEncoding.Unicode.GetBytes(plainText);
            ////////data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            ////////String hash = UnicodeEncoding.Unicode.GetString(data);
            //////////CustomMsgBox.Show(hash);

            #endregion TEST

            try
            {
                int.TryParse(CommonConfigs.ReadSetting("ConfigID"), out GlobalVariables.ConfigID);

                UserPrincipal currentUserPrincipal = UserPrincipal.Current;
                if (currentUserPrincipal == null || currentUserPrincipal.Sid == null) throw new Exception("Sorry, can not get current user principal!");

                this.baseRepository = CommonNinject.Kernel.Get<IBaseRepository>();

                UserAPIs userAPIs = new UserAPIs(CommonNinject.Kernel.Get<IUserAPIRepository>());
                IList<ActiveUser> activeUsers = userAPIs.GetActiveUsers(GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Smallpack || GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Pail ? "S-1-5-21-705987929-4194781227-356911358-1001" : "S-1-5-21-2907738014-1953812902-1740135539-2132-NTF"); //currentUserPrincipal.Sid.Value

                //throw new Exception(currentUserPrincipal.Sid.Value);
                if (activeUsers.Count > 0)
                {
                    this.comboBoxEmployeeID.DataSource = activeUsers;
                    this.comboBoxEmployeeID.DisplayMember = CommonExpressions.PropertyName<ActiveUser>(p => p.FullyQualifiedUserName);
                    this.comboBoxEmployeeID.ValueMember = CommonExpressions.PropertyName<ActiveUser>(p => p.UserID);

                    FillingLineAPIs fillingLineAPIs = new FillingLineAPIs(CommonNinject.Kernel.Get<IFillingLineAPIRepository>());

                    this.comboFillingLineID.DataSource = fillingLineAPIs.GetFillingLineBases();
                    this.comboFillingLineID.DisplayMember = CommonExpressions.PropertyName<FillingLineBase>(p => p.Name);
                    this.comboFillingLineID.ValueMember = CommonExpressions.PropertyName<FillingLineBase>(p => p.FillingLineID);


                    if (GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Smallpack || GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Pail || GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Drum)
                        this.comboFillingLineID.SelectedValue = GlobalVariables.ConfigID;

                    if (!(GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Smallpack || GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Pail || GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Drum))
                    {
                        this.lbProductionLineID.Visible = false;
                        this.comboFillingLineID.Visible = false;

                        this.labelPassword.Top = this.lbEmployeeID.Top;
                        this.textPassword.Top = this.comboBoxEmployeeID.Top;

                        this.lbEmployeeID.Top = this.lbProductionLineID.Top;
                        this.comboBoxEmployeeID.Top = this.comboFillingLineID.Top;
                    }


                    this.comboBoxAutonicsPortName.DataSource = System.IO.Ports.SerialPort.GetPortNames();
                    if (this.comboBoxAutonicsPortName.Items.Count == 0)
                    {
                        this.comboBoxAutonicsPortName.DataSource = null;
                        this.comboBoxAutonicsPortName.Items.Add("COM0");
                    }

                    string comportName = CommonConfigs.ReadSetting("ComportName");
                    if (this.comboBoxAutonicsPortName.Items.IndexOf(comportName) >= 0)
                        this.comboBoxAutonicsPortName.SelectedIndex = this.comboBoxAutonicsPortName.Items.IndexOf(comportName);


                    this.buttonDownload.Visible = true;
                    this.buttonLoginRestore.Visible = activeUsers[0].IsDatabaseAdmin;
                }
                else
                {
                    this.comboFillingLineID.Visible = false;
                    this.comboBoxEmployeeID.Visible = false;
                    this.lbEmployeeID.Visible = false;
                    this.lbProductionLineID.Text = "\r\n" + "Sorry, user: " + currentUserPrincipal.Name + "\r\n" + "Don't have permission to run this program." + "\r\n" + "\r\n" + "Contact your admin for more information. Thank you!" + "\r\n" + "\r\n" + "\r\n" + "Xin lỗi, bạn chưa được cấp quyền sử dụng phần mềm này.";

                    this.buttonLogin.Visible = false;
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void CommonControl_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            if (e.BindingCompleteState == BindingCompleteState.Exception)
            { ExceptionHandlers.ShowExceptionMessageBox(this, e.ErrorText); e.Cancel = true; }
        }

        private void pictureBoxIcon_DoubleClick(object sender, EventArgs e)
        {
            this.labelPortAutonis.Visible = true;
            this.comboBoxAutonicsPortName.Visible = true;
        }

        private void buttonLoginExit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(GlobalVariables.ConfigID == (int)GlobalEnums.NmvnTaskID.Lavie || GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Smallpack || GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Pail || GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.BatchMaster))
                {
                    CustomMsgBox.Show(this, "Sorry, can not open this application." + "\r\n" + "\r\n" + "Call 0919 878 329 for more information. Thanks!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.DialogResult = DialogResult.Cancel; return;
                }

                if (sender.Equals(this.buttonExit)) { this.DialogResult = DialogResult.Cancel; return; }

                if (this.comboBoxEmployeeID.SelectedIndex >= 0)
                {
                    ActiveUser activeUser = this.comboBoxEmployeeID.SelectedItem as ActiveUser;
                    if (activeUser != null)
                    {

                        UserAPIs userAPIs = new UserAPIs(CommonNinject.Kernel.Get<IUserAPIRepository>());
                        string passwordHash = userAPIs.GetPasswordHash(activeUser.UserID);
                        if (passwordHash != "") passwordHash = SecurePassword.Decrypt(passwordHash);
                        if (this.textPassword.Text != passwordHash) throw new Exception("Sai mật khẩu. Vui lòng nhập lại mật khẩu trước khi tiếp tục.");


                        ContextAttributes.User = new UserInformation(activeUser.UserID, activeUser.OrganizationalUnitID, activeUser.LocationID, activeUser.LocationName, activeUser.UserName, activeUser.SecurityIdentifier, activeUser.FullyQualifiedUserName, activeUser.IsDatabaseAdmin, new DateTime());

                        if (this.comboFillingLineID.Visible && (this.comboFillingLineID.SelectedIndex < 0 || this.comboBoxAutonicsPortName.SelectedIndex < 0)) throw new System.ArgumentException("Vui lòng chọn chuyền sản xuất (NOF1, NOF2, NOF...), và chọn đúng cổng COM để chạy phần mềm"); // || (this.comboFillingLineID.Enabled && (GlobalVariables.ProductionLine)this.comboFillingLineID.SelectedValue == GlobalVariables.ProductionLine.SERVER)

                        if (this.comboFillingLineID.Visible)
                        {
                            GlobalVariables.FillingLineID = (GlobalVariables.FillingLine)this.comboFillingLineID.SelectedValue;
                            GlobalVariables.FillingLineCode = ((FillingLineBase)this.comboFillingLineID.SelectedItem).Code;
                            GlobalVariables.FillingLineName = ((FillingLineBase)this.comboFillingLineID.SelectedItem).Name;
                            GlobalVariables.FillingLineFactoryCode = ((FillingLineBase)this.comboFillingLineID.SelectedItem).FactoryCode;
                        }
                        else
                            GlobalVariables.FillingLineID = GlobalVariables.FillingLine.None;

                        GlobalVariables.ComportName = (string)this.comboBoxAutonicsPortName.SelectedValue;

                        CommonConfigs.AddUpdateAppSetting("ConfigID", (GlobalVariables.ConfigID).ToString());
                        CommonConfigs.AddUpdateAppSetting("ComportName", GlobalVariables.ComportName);

                        //CommonConfigs.AddUpdateAppSetting("ReportServerUrl", GlobalVariables.ReportServerUrl); //WILL BE REMOVE THIS LINE
                        GlobalVariables.ReportServerUrl = CommonConfigs.ReadSetting("ReportServerUrl");

                        this.VersionValidate();

                        #region EMPTY DATABASE
                        if (false && this.checkEmptyData.Checked)
                        {
                            this.baseRepository.ExecuteStoreCommand("UPDATE GoodsReceipts SET PickupID = NULL, GoodsIssueID = NULL, WarehouseAdjustmentID = NULL", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("UPDATE GoodsReceiptDetails SET PickupDetailID = NULL, PickupID = NULL, GoodsIssueTransferDetailID = NULL, GoodsIssueID = NULL, WarehouseAdjustmentDetailID = NULL, WarehouseAdjustmentID = NULL", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     WarehouseAdjustmentDetails", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('WarehouseAdjustmentDetails', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     WarehouseAdjustments", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('WarehouseAdjustments', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     GoodsIssueTransferDetails", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('GoodsIssueTransferDetails', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     GoodsIssueDetails", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('GoodsIssueDetails', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     GoodsIssues", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('GoodsIssues', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     TransferOrderDetails", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('TransferOrderDetails', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     TransferOrders", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('TransferOrders', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     DeliveryAdviceDetails", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('DeliveryAdviceDetails', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     DeliveryAdvices", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('DeliveryAdvices', RESEED, 0)", new ObjectParameter[] { });


                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     SalesOrderDetails", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('SalesOrderDetails', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     SalesOrders", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('SalesOrders', RESEED, 0)", new ObjectParameter[] { });



                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     GoodsReceiptDetails", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('GoodsReceiptDetails', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     GoodsReceipts", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('GoodsReceipts', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     PickupDetails", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('PickupDetails', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     Pickups", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('Pickups', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     Packs", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('Packs', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     Cartons", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('Cartons', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     Pallets", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('Pallets', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     Batches", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('Batches', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     Lots", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('Lots', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     BatchMasters", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('BatchMasters', RESEED, 0)", new ObjectParameter[] { });

                            this.baseRepository.ExecuteStoreCommand("DELETE FROM     Commodities", new ObjectParameter[] { });
                            this.baseRepository.ExecuteStoreCommand("DBCC CHECKIDENT ('Commodities', RESEED, 0)", new ObjectParameter[] { });
                        }
                        #endregion

                        if (this.baseRepository.AutoUpdates(sender.Equals(this.buttonLoginRestore)))
                            this.DialogResult = DialogResult.OK;
                        else
                        {
                            CustomMsgBox.Show(this, "The program on this computer must be updated to the latest version." + "\r\n" + "\r\n" + "Contact your administrator for more information.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            this.buttonDownload_Click(this.buttonDownload, new EventArgs());
                        }

                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);

                this.DialogResult = DialogResult.None;
            }
        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            try
            {
                ChangePassword changePassword = new ChangePassword(); //new ChangePassword(true): checkPasswordOnly 
                DialogResult dialogResult = changePassword.ShowDialog(); changePassword.Dispose();
                if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                {
                    CustomMsgBox.Show(this, "Mật khẩu vừa thay đổi thành công." + "\r\n" + "\r\n" + "Vui lòng mở lại phần mềm để đăng nhập bằng mật khẩu mới.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.DialogResult = DialogResult.Yes;
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }

            //try
            //{
            //    throw new Exception("Please open your program again in order to update new version." + "\r\n" + "\r\n" + "Contact your admin for more information. Thank you!" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "Vui lòng mở lại phần mềm để cập nhật phiên bản mới nhất. Cám ơn.");
            //}
            //catch (Exception exception)
            //{
            //    CommonConfigs.AddUpdateAppSetting("VersionID", "-9");
            //    ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            //    this.DialogResult = DialogResult.Cancel;
            //}
        }

        private bool VersionValidate()
        {
            try
            {
                foreach (GlobalVariables.FillingLine fillingLine in Enum.GetValues(typeof(GlobalVariables.FillingLine)))
                {
                    this.baseRepository.ExecuteStoreCommand("UPDATE Configs SET VersionID = " + GlobalVariables.ConfigVersionID((int)fillingLine) + " WHERE ConfigID = " + (int)fillingLine + " AND VersionID < " + GlobalVariables.ConfigVersionID((int)fillingLine), new ObjectParameter[] { });
                }


                if (this.baseRepository.VersionValidate(GlobalVariables.ConfigID, GlobalVariables.ConfigVersionID(GlobalVariables.ConfigID)))
                    CommonConfigs.AddUpdateAppSetting("VersionID", GlobalVariables.ConfigVersionID(GlobalVariables.ConfigID).ToString());

                return true;
            }
            catch (Exception exception)
            {
                CommonConfigs.AddUpdateAppSetting("VersionID", "-9");
                throw exception;
            }
        }

        private void comboFillingLineID_Validated(object sender, EventArgs e)
        {
            if (this.comboFillingLineID.SelectedIndex < 0) this.comboFillingLineID.Text = "";
        }

        private void labelNoDomino_DoubleClick(object sender, EventArgs e)
        {
            //if ((int)this.comboFillingLineID.SelectedValue != (int)GlobalVariables.ProductionLine.SERVER)
            //{
            //    this.labelNoDomino.Visible = false;
            //    this.checkBoxNoDomino.Visible = true;
            //}
        }

        private void lbProductionLineID_DoubleClick(object sender, EventArgs e)
        {
            //try
            //{
            //    string textInput = "";
            //    if (CustomInputBox.Show("NMVN", "Vui lòng nhập mật khẩu đổi chuyền", ref textInput) == System.Windows.Forms.DialogResult.OK)
            //        this.comboFillingLineID.Enabled = (textInput == "9876543210");
            //}
            //catch (Exception exception)
            //{
            //    GlobalExceptionHandler.ShowExceptionMessageBox(this, exception);
            //}
        }

        private void labelChangePassword_Click(object sender, EventArgs e)
        {

        }

        private void SetButtonEnabled()
        {
            this.buttonLogin.Enabled = this.textPassword.Text == (string)this.textPassword.Tag;
        }

        private void comboBoxEmployeeID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxEmployeeID.SelectedIndex >= 0)
            {
                ActiveUser activeUser = this.comboBoxEmployeeID.SelectedItem as ActiveUser;
                if (activeUser != null)
                {
                    //JUST SET IN ORDER TO CHANGE PASSWORD
                    ContextAttributes.User = new UserInformation(activeUser.UserID, activeUser.OrganizationalUnitID, activeUser.LocationID, activeUser.LocationName, activeUser.UserName, activeUser.SecurityIdentifier, activeUser.FullyQualifiedUserName, activeUser.IsDatabaseAdmin, new DateTime());


                    string passwordHash = activeUser.PasswordHash;
                    if (passwordHash != "") passwordHash = SecurePassword.Decrypt(passwordHash);

                    this.textPassword.Tag = passwordHash;
                }
                this.SetButtonEnabled();
            }
        }

        private void textPassword_TextChanged(object sender, EventArgs e)
        {
            this.SetButtonEnabled();
        }




    }
}
