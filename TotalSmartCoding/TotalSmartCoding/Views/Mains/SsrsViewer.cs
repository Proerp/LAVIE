using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TotalSmartCoding.ViewModels.Helpers;

namespace TotalSmartCoding.Views.Mains
{
    public partial class SsrsViewer : Form
    {
        PrintViewModel printViewModel;

        public SsrsViewer(PrintViewModel printViewModel)
        {
            InitializeComponent();

            this.printViewModel = printViewModel;
        }

        private void SsrsViewer_Load(object sender, EventArgs e)
        {
            try
            {
                this.ssrsMainViewer.ProcessingMode = ProcessingMode.Remote;

                ServerReport serverReport = this.ssrsMainViewer.ServerReport;

                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("Administrator", "Nuti123@#", "SERVER3");
                ////////WINDOWS AUTHENTICATION!!! System.Net.ICredentials credentials = System.Net.CredentialCache.DefaultCredentials;// Get a reference to the default credentials  
                
                ReportServerCredentials rsCredentials = serverReport.ReportServerCredentials;// Get a reference to the report server credentials  
                rsCredentials.NetworkCredentials = credentials; // Set the credentials for the server report  

                serverReport.ReportServerUrl = new Uri(this.printViewModel.ReportServerUrl); //// Set the report server URL and report path  
                serverReport.ReportPath = "/" + this.printViewModel.ReportFolder + "/" + this.printViewModel.ReportPath;

                if (this.printViewModel.ReportParameters != null && this.printViewModel.ReportParameters.Count > 0)
                    this.ssrsMainViewer.ServerReport.SetParameters(this.printViewModel.ReportParameters); // Set the report parameters for the report  

                this.ssrsMainViewer.ShowPromptAreaButton = this.printViewModel.ShowPromptAreaButton;

                this.ssrsMainViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
