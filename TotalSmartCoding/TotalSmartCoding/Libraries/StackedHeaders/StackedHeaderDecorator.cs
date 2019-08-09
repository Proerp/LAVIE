using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace TotalSmartCoding.Libraries.StackedHeaders
{
    public class StackedHeaderDecorator
    {
        private readonly IStackedHeaderGenerator objStackedHeaderGenerator = StackedHeaderGenerator.Instance;
        private Graphics objGraphics;
        private readonly DataGridView objDataGrid;
        private Header objHeaderTree;
        private int iNoOfLevels;
        private readonly StringFormat objFormat;

        public StackedHeaderDecorator(DataGridView objDataGrid)
        {
            this.objDataGrid = objDataGrid;
            objFormat = new StringFormat();
            objFormat.Alignment = StringAlignment.Center;
            objFormat.LineAlignment = StringAlignment.Center;

            Type dgvType = objDataGrid.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(objDataGrid, true, null);

            objDataGrid.Scroll += (objDataGrid_Scroll);
            objDataGrid.Paint += objDataGrid_Paint;
            objDataGrid.ColumnRemoved += objDataGrid_ColumnRemoved;
            objDataGrid.ColumnAdded += objDataGrid_ColumnAdded;
            objDataGrid.ColumnWidthChanged += objDataGrid_ColumnWidthChanged;
            objHeaderTree = objStackedHeaderGenerator.GenerateStackedHeader(objDataGrid);
        }

        public StackedHeaderDecorator(IStackedHeaderGenerator objStackedHeaderGenerator, DataGridView objDataGrid)
            : this(objDataGrid)
        {
            this.objStackedHeaderGenerator = objStackedHeaderGenerator;
        }

        void objDataGrid_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            Refresh();
        }

        void objDataGrid_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            RegenerateHeaders();
            Refresh();
        }

        void objDataGrid_ColumnRemoved(object sender, DataGridViewColumnEventArgs e)
        {
            RegenerateHeaders();
            Refresh();
        }

        void objDataGrid_Paint(object sender, PaintEventArgs e)
        {
            iNoOfLevels = NoOfLevels(objHeaderTree);
            objGraphics = e.Graphics;
            objDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            objDataGrid.ColumnHeadersHeight = iNoOfLevels * 15; //LEMINHHIEP: ORIGINAL: iNoOfLevels * 20;
            if (null != objHeaderTree)
            {
                RenderColumnHeaders();
            }
        }

        void objDataGrid_Scroll(object sender, ScrollEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            Rectangle rtHeader = objDataGrid.DisplayRectangle;
            objDataGrid.Invalidate(rtHeader);
        }

        private void RegenerateHeaders()
        {
            objHeaderTree = objStackedHeaderGenerator.GenerateStackedHeader(objDataGrid);
        }

        private void RenderColumnHeaders()
        {
            objGraphics.FillRectangle(new SolidBrush(SystemColors.InactiveBorder), //LEMINHHIEP: CHANGE FROM: objDataGrid.ColumnHeadersDefaultCellStyle.BackColor TO: SystemColors.InactiveBorder
                                      new Rectangle(objDataGrid.DisplayRectangle.X, objDataGrid.DisplayRectangle.Y,
                                                    objDataGrid.DisplayRectangle.Width, objDataGrid.ColumnHeadersHeight - 2)); //LEMINHHIEP: CHANGE FROM: objDataGrid.ColumnHeadersHeight TO: objDataGrid.ColumnHeadersHeight - 2

            foreach (Header objChild in objHeaderTree.Children)
            {
                objChild.Measure(objDataGrid, 0, objDataGrid.ColumnHeadersHeight / (iNoOfLevels - 1)); //LEMINHHIEP: CHANGE FROM: iNoOfLevels TO: (iNoOfLevels - 1)
                objChild.AcceptRenderer(this);
            }
        }

        public void Render(Header objHeader)
        {
            if (objHeader.Children.Count == 0)
            {
                Rectangle r1 = objDataGrid.GetColumnDisplayRectangle(objHeader.ColumnId, true);
                if (r1.Width == 0)
                {
                    return;
                }
                r1.Y = objHeader.Y;
                r1.Width += 1;
                r1.X -= 1;
                r1.Height = objHeader.Height;
                objGraphics.SetClip(r1);

                if (r1.X + objDataGrid.Columns[objHeader.ColumnId].Width < objDataGrid.DisplayRectangle.Width)
                {
                    r1.X -= (objDataGrid.Columns[objHeader.ColumnId].Width - r1.Width);
                }
                r1.X -= 1;
                r1.Width = objDataGrid.Columns[objHeader.ColumnId].Width;
                objGraphics.DrawRectangle(Pens.Lavender, r1);
                objGraphics.DrawString(objHeader.Name, objDataGrid.ColumnHeadersDefaultCellStyle.Font,
                                       new SolidBrush(Color.FromArgb(188, objDataGrid.ColumnHeadersDefaultCellStyle.ForeColor)), //LEMINHHIEP: ADD Color.FromArgb
                                       r1,
                                       objFormat);
                objGraphics.ResetClip();
            }
            else
            {
                int x = objDataGrid.RowHeadersWidth;
                for (int i = 0; i < objHeader.Children[0].ColumnId; ++i)
                {
                    if (objDataGrid.Columns[i].Visible)
                    {
                        x += objDataGrid.Columns[i].Width;
                    }
                }
                if (x > (objDataGrid.HorizontalScrollingOffset + objDataGrid.DisplayRectangle.Width - 5))
                {
                    return;
                }

                //Rectangle r1 = objDataGrid.GetCellDisplayRectangle(objHeader.Children[0].ColumnId, -1, true);
                Rectangle r1 = objDataGrid.GetCellDisplayRectangle(objHeader.ColumnId, -1, true);
                r1.Y = objHeader.Y;
                r1.Height = objHeader.Height;
                r1.Width = objHeader.Width + 1;
                if (r1.X < objDataGrid.RowHeadersWidth)
                {
                    r1.X = objDataGrid.RowHeadersWidth;
                }
                r1.X -= 1;
                objGraphics.SetClip(r1);
                r1.X = x - objDataGrid.HorizontalScrollingOffset - 1; //LEMINHHIEP: CHANGE FROM: x - objDataGrid.HorizontalScrollingOffset TO: x - objDataGrid.HorizontalScrollingOffset - 1
                r1.Width -= 1;
                objGraphics.DrawRectangle(Pens.Lavender, r1);
                r1.X -= 1;
                objGraphics.DrawString(objHeader.Name, objDataGrid.ColumnHeadersDefaultCellStyle.Font,
                                       new SolidBrush(Color.FromArgb(188, objDataGrid.ColumnHeadersDefaultCellStyle.ForeColor)), //LEMINHHIEP: ADD Color.FromArgb
                                       r1, objFormat);
                objGraphics.ResetClip();
            }
        }

        private int NoOfLevels(Header header)
        {
            int level = 0;
            foreach (Header child in header.Children)
            {
                int temp = NoOfLevels(child);
                level = temp > level ? temp : level;
            }
            return level + 1;
        }
    }
}
