using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Ninject;
using TotalSmartCoding.Controllers.APIs.Generals;
using TotalSmartCoding.Libraries;
using TotalCore.Repositories.Generals;
using TotalBase;
using TotalSmartCoding.Libraries.Helpers;
using TotalCore.Extensions;


namespace TotalSmartCoding.Views.Mains
{
    public partial class ChangePassword : Form
    {
        public ChangePassword(bool checkPasswordOnly)
            : this()
        {
            this.buttonChange.Visible = false;
        }

        public ChangePassword()
        {
            InitializeComponent();
        }

        private void PublicAuthenticationPassword_Load(object sender, EventArgs e)
        {
            try
            {
                UserAPIs userAPIs = new UserAPIs(CommonNinject.Kernel.Get<IUserAPIRepository>());
                string passwordHash = userAPIs.GetPasswordHash(ContextAttributes.User.UserID);
                if (passwordHash != "") passwordHash = SecurePassword.Decrypt(passwordHash);

                this.textBoxCurrentPassword.Tag = passwordHash;

                this.SetButtonEnabled();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private bool ValidPassword { get { return this.textBoxCurrentPassword.Text == (string)this.textBoxCurrentPassword.Tag; } }

        private void SetButtonEnabled()
        {
            //this.buttonExit.Enabled = this.ValidPassword && this.textBoxCurrentPassword.Visible;
            this.buttonChange.Enabled = this.ValidPassword && this.textBoxNewPassword.Text == this.textBoxConfirmPassword.Text;
        }

        private void textBoxCurrentPassword_TextChanged(object sender, EventArgs e)
        {
            this.SetButtonEnabled();
        }

        private void textBoxNewPassword_TextChanged(object sender, EventArgs e)
        {
            this.SetButtonEnabled();
        }

        private void textBoxConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            this.SetButtonEnabled();
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.textBoxCurrentPassword.Visible)
                {
                    this.textBoxCurrentPassword.Visible = false;
                    this.textBoxNewPassword.Visible = true;
                    this.textBoxConfirmPassword.Visible = true;
                    this.labelCurrentPassword.Visible = false;
                    this.labelNewPassword.Visible = true;
                    this.labelConfirmPassword.Visible = true;
                    this.SetButtonEnabled();
                    this.DialogResult = DialogResult.None;
                }
                else
                {
                    string passwordHash = this.textBoxNewPassword.Text;
                    if (passwordHash != "") passwordHash = SecurePassword.Encrypt(passwordHash);

                    UserAPIs userAPIs = new UserAPIs(CommonNinject.Kernel.Get<IUserAPIRepository>());
                    if (userAPIs.SetPasswordHash(ContextAttributes.User.UserID, passwordHash) == 1)
                        this.DialogResult = DialogResult.Yes;
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }



    }
}
