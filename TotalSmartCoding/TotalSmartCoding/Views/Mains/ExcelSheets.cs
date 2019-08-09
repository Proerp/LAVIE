using System;
using System.Windows.Forms;
using System.Collections.Generic;

using Ninject;

using TotalBase.Enums;
using TotalCore.Repositories.Generals;
using TotalSmartCoding.Libraries;
using TotalSmartCoding.Libraries.Helpers;
using TotalSmartCoding.Controllers.APIs.Generals;


namespace TotalSmartCoding.Views.Mains
{
    public partial class ExcelSheets : Form
    {
        public ExcelSheets(GlobalEnums.MappingTaskID mappingTaskID, string excelFile)
        {
            InitializeComponent();
            try
            {
                OleDbAPIs oleDbAPIs = new OleDbAPIs(CommonNinject.Kernel.Get<IOleDbAPIRepository>(), mappingTaskID);
                List<string> excelSheets = oleDbAPIs.GetExcelSheets(excelFile);
                if (excelSheets != null) this.combexSheetName.Items.AddRange(excelSheets.ToArray());
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void combexSheetName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.buttonOK.Enabled = this.combexSheetName.SelectedIndex >= 0;
        }

        private void OKCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender.Equals(this.buttonOK)) this.Tag = this.combexSheetName.Text;
                this.DialogResult = sender.Equals(this.buttonOK) ? DialogResult.OK : DialogResult.Cancel;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }
    }
}
