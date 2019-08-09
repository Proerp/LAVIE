using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TotalSmartCoding.Views.Commons
{
    public partial class CustomMessageBox : Form
    {
        public CustomMessageBox(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, string promptText, IList<string> voidTypeNames, string inputValue)
        {
            InitializeComponent();

            this.labelText.Text = text;
            this.Text = caption;

            this.labelPromptText.Text = promptText;
            this.comboVoidTypeNames.DataSource = voidTypeNames;

            this.labelPromptText.Visible = promptText != null && promptText != "";
            this.textRemarks.Visible = promptText != null && promptText != "";
            this.comboVoidTypeNames.Visible = promptText != null && promptText != "";

            switch (buttons)
            {
                case MessageBoxButtons.OK:
                    this.button1.Visible = true;
                    this.button2.Visible = false;
                    this.button3.Visible = false;

                    this.button1.Text = "OK";
                    this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.button1.Image = this.button2.Image;

                    break;
                case MessageBoxButtons.OKCancel:
                    this.button1.Visible = true;
                    this.button2.Visible = true;
                    this.button3.Visible = false;

                    this.button1.Text = "Cancel";
                    this.button2.Text = "OK";

                    this.button1.Image = global::TotalSmartCoding.Properties.Resources.signout_icon_24;

                    this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;

                    break;
                case MessageBoxButtons.YesNo:
                    this.button1.Visible = true;
                    this.button2.Visible = true;
                    this.button3.Visible = false;

                    this.button1.Text = "No";
                    this.button2.Text = "Yes";

                    this.button1.DialogResult = System.Windows.Forms.DialogResult.No;
                    this.button2.DialogResult = System.Windows.Forms.DialogResult.Yes;

                    break;
                case MessageBoxButtons.YesNoCancel:
                    this.button1.Visible = true;
                    this.button2.Visible = true;
                    this.button3.Visible = true;

                    this.button1.Text = "Cancel";
                    this.button2.Text = "No";
                    this.button3.Text = "Yes";

                    this.button2.Image = global::TotalSmartCoding.Properties.Resources.Yellow_cross;
                    this.button1.Image = global::TotalSmartCoding.Properties.Resources.signout_icon_24;

                    this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.button2.DialogResult = System.Windows.Forms.DialogResult.No;
                    this.button3.DialogResult = System.Windows.Forms.DialogResult.Yes;

                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }

            this.pictureIcon.Image = icon == MessageBoxIcon.Warning ? TotalSmartCoding.Properties.Resources.Martz90_Circle_Addon2_Warning : TotalSmartCoding.Properties.Resources.Kyo_Tux_Phuzion_Sign_Info;

            if (promptText != null && promptText != "")
                comboVoidTypeNames_TextChanged(this.comboVoidTypeNames, new EventArgs());
            else
                this.AcceptButton = defaultButton == MessageBoxDefaultButton.Button1 ? this.button1 : (defaultButton == MessageBoxDefaultButton.Button1 ? this.button2 : this.button3);
        }

        private void comboVoidTypeNames_TextChanged(object sender, EventArgs e)
        {
            if (this.button1.DialogResult != System.Windows.Forms.DialogResult.No && this.button1.DialogResult != System.Windows.Forms.DialogResult.Cancel) this.button1.Enabled = this.comboVoidTypeNames.Text.Trim().Length > 15;
            if (this.button2.DialogResult != System.Windows.Forms.DialogResult.No && this.button2.DialogResult != System.Windows.Forms.DialogResult.Cancel) this.button2.Enabled = this.comboVoidTypeNames.Text.Trim().Length > 15;
            if (this.button3.DialogResult != System.Windows.Forms.DialogResult.No && this.button3.DialogResult != System.Windows.Forms.DialogResult.Cancel) this.button3.Enabled = this.comboVoidTypeNames.Text.Trim().Length > 15;
        }
    }
}
