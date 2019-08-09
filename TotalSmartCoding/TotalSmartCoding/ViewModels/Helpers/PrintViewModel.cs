using Microsoft.Reporting.WinForms;
using System.Collections.Generic;
using TotalBase;

namespace TotalSmartCoding.ViewModels.Helpers
{
    public class PrintViewModel
    {
        public int Id { get; set; }

        public string ServerName { get { return "vnhpbcvmsql01"; } }
        public string CatalogName { get { return "TotalSmartCoding"; } }

        public string ReportFolder { get { return "TotalSmartCoding"; } }
        public string ReportPath { get; set; }

        public int PrintOptionID { get; set; }

        public string ReportServerUrl = GlobalVariables.ReportServerUrl;
        public bool ShowPromptAreaButton { get; set; }

        public List<ReportParameter> ReportParameters { get; set; }

        public PrintViewModel() { this.ReportParameters = new List<ReportParameter>(); }
    }
}