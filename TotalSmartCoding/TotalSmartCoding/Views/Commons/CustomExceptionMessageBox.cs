using System.Windows.Forms;

using TotalModel.Helpers;
using BrightIdeasSoftware;

namespace TotalSmartCoding.Views.Commons
{
    public partial class CustomExceptionMessageBox : Form
    {
        private CustomException customException;

        public CustomExceptionMessageBox()
        {
            InitializeComponent();
        }

        public CustomExceptionMessageBox(CustomException customException)
            : this()
        {
            this.customException = customException;

            this.labelExceptionMessage.Text = this.customException.Message;

            this.dataListViewExceptionTable.AboutToCreateGroups += dataListViewExceptionTable_AboutToCreateGroups;

            this.dataListViewExceptionTable.ShowGroups = customException.ShowGroups;
            this.dataListViewExceptionTable.DataSource = customException.ExceptionTable;

            this.dataListViewExceptionTable.AutoResizeColumns();

            if (customException.ShowGroups && this.dataListViewExceptionTable.Columns.Count > 1)
            {
                this.dataListViewExceptionTable.Columns[0].Width = 0;
                if (this.dataListViewExceptionTable.Columns.Count == 2) this.dataListViewExceptionTable.Columns[1].Width = this.dataListViewExceptionTable.Width - 30;
            }
        }

        private void dataListViewExceptionTable_AboutToCreateGroups(object sender, CreateGroupsEventArgs e)
        {
            if (e.Groups != null && e.Groups.Count > 0)
            {
                foreach (OLVGroup olvGroup in e.Groups)
                {
                    olvGroup.Collapsed = true;
                }
            }
        }

        private void buttonOK_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
