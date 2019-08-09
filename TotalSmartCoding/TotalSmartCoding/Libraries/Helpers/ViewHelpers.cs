using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

using BrightIdeasSoftware;
using Guifreaks.Navisuite;

namespace TotalSmartCoding.Libraries.Helpers
{
    public class ViewHelpers
    {
        public static List<Control> GetAllControls(Control controlContainer, List<Control> controlList)
        {
            foreach (Control control in controlContainer.Controls)
            {
                controlList.Add(control);
                if (control.Controls.Count > 0) controlList = GetAllControls(control, controlList);
            }

            return controlList;
        }
        public static List<Control> GetAllControls(Control controlContainer)
        {
            return GetAllControls(controlContainer, new List<Control>());
        }


        public static void SetFont(Control controlContainer, Font titleFont, Font font, Font toolbarFont)
        {
            List<Control> controls = ViewHelpers.GetAllControls(controlContainer);
            foreach (Control control in controls)
            {
                if (control is Label || control is CustomTabControl || control is NaviGroup) control.Font = titleFont;
                else if (control is TextBox || control is ComboBox || control is DateTimePicker) control.Font = font;
                else if (control is FastObjectListView)
                {
                    control.Font = font;
                    FastObjectListView fastObjectListView = control as FastObjectListView;
                    fastObjectListView.RowHeight = 36;
                    foreach (OLVColumn olvColumn in fastObjectListView.Columns)
                    {
                        olvColumn.HeaderFont = titleFont;
                    }
                }
                else if (control is DataGridView)
                {
                    DataGridView dataGridView = control as DataGridView;
                    dataGridView.ColumnHeadersDefaultCellStyle.Font = titleFont;
                    dataGridView.RowsDefaultCellStyle.Font = font;
                    dataGridView.RowTemplate.Height = 36;
                }
                else if (control is ToolStrip)
                {
                    foreach (ToolStripItem item in ((ToolStrip)control).Items)
                    {
                        if (item is ToolStripLabel || item is ToolStripTextBox || item is ToolStripComboBox || item is ToolStripButton)
                            item.Font = toolbarFont;
                    }
                }
            }
        }

    }
}
