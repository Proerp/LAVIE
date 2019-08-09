namespace TotalSmartCoding.Views.Mains
{
    partial class ColumnMappings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.fastColumnAvailable = new BrightIdeasSoftware.FastObjectListView();
            this.olvAvailableColumnAvailableName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvAvailableColumnMappingName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.fastColumnMapping = new BrightIdeasSoftware.FastObjectListView();
            this.olvMappingColumnDisplayName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvMappingColumnMappingName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.buttonMap = new System.Windows.Forms.Button();
            this.buttonUnMap = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.ToolStripButton();
            this.buttonOK = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastColumnAvailable)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastColumnMapping)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonCancel,
            this.buttonOK});
            this.toolStrip2.Location = new System.Drawing.Point(0, 365);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip2.Size = new System.Drawing.Size(824, 55);
            this.toolStrip2.TabIndex = 70;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // fastColumnAvailable
            // 
            this.fastColumnAvailable.AllColumns.Add(this.olvAvailableColumnAvailableName);
            this.fastColumnAvailable.AllColumns.Add(this.olvAvailableColumnMappingName);
            this.fastColumnAvailable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvAvailableColumnAvailableName,
            this.olvAvailableColumnMappingName});
            this.fastColumnAvailable.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastColumnAvailable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastColumnAvailable.FullRowSelect = true;
            this.fastColumnAvailable.HideSelection = false;
            this.fastColumnAvailable.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastColumnAvailable.Location = new System.Drawing.Point(3, 3);
            this.fastColumnAvailable.Name = "fastColumnAvailable";
            this.fastColumnAvailable.OwnerDraw = true;
            this.tableLayoutPanel1.SetRowSpan(this.fastColumnAvailable, 2);
            this.fastColumnAvailable.ShowGroups = false;
            this.fastColumnAvailable.Size = new System.Drawing.Size(389, 359);
            this.fastColumnAvailable.TabIndex = 71;
            this.fastColumnAvailable.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastColumnAvailable.UseCompatibleStateImageBehavior = false;
            this.fastColumnAvailable.UseFiltering = true;
            this.fastColumnAvailable.View = System.Windows.Forms.View.Details;
            this.fastColumnAvailable.VirtualMode = true;
            // 
            // olvAvailableColumnAvailableName
            // 
            this.olvAvailableColumnAvailableName.AspectName = "ColumnAvailableName";
            this.olvAvailableColumnAvailableName.Text = "File excel";
            this.olvAvailableColumnAvailableName.Width = 168;
            // 
            // olvAvailableColumnMappingName
            // 
            this.olvAvailableColumnMappingName.AspectName = "ColumnMappingName";
            this.olvAvailableColumnMappingName.FillsFreeSpace = true;
            this.olvAvailableColumnMappingName.Text = "Phần mềm";
            this.olvAvailableColumnMappingName.Width = 200;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.fastColumnMapping, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonMap, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.fastColumnAvailable, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonUnMap, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(824, 365);
            this.tableLayoutPanel1.TabIndex = 72;
            // 
            // fastColumnMapping
            // 
            this.fastColumnMapping.AllColumns.Add(this.olvMappingColumnDisplayName);
            this.fastColumnMapping.AllColumns.Add(this.olvMappingColumnMappingName);
            this.fastColumnMapping.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvMappingColumnDisplayName,
            this.olvMappingColumnMappingName});
            this.fastColumnMapping.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastColumnMapping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastColumnMapping.FullRowSelect = true;
            this.fastColumnMapping.HideSelection = false;
            this.fastColumnMapping.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastColumnMapping.Location = new System.Drawing.Point(432, 3);
            this.fastColumnMapping.Name = "fastColumnMapping";
            this.fastColumnMapping.OwnerDraw = true;
            this.tableLayoutPanel1.SetRowSpan(this.fastColumnMapping, 2);
            this.fastColumnMapping.ShowGroups = false;
            this.fastColumnMapping.Size = new System.Drawing.Size(389, 359);
            this.fastColumnMapping.TabIndex = 73;
            this.fastColumnMapping.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastColumnMapping.UseCompatibleStateImageBehavior = false;
            this.fastColumnMapping.UseFiltering = true;
            this.fastColumnMapping.View = System.Windows.Forms.View.Details;
            this.fastColumnMapping.VirtualMode = true;
            // 
            // olvMappingColumnDisplayName
            // 
            this.olvMappingColumnDisplayName.AspectName = "ColumnDisplayName";
            this.olvMappingColumnDisplayName.Text = "Phần mềm";
            this.olvMappingColumnDisplayName.Width = 168;
            // 
            // olvMappingColumnMappingName
            // 
            this.olvMappingColumnMappingName.AspectName = "ColumnMappingName";
            this.olvMappingColumnMappingName.FillsFreeSpace = true;
            this.olvMappingColumnMappingName.Text = "File excel";
            this.olvMappingColumnMappingName.Width = 200;
            // 
            // buttonMap
            // 
            this.buttonMap.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonMap.Image = global::TotalSmartCoding.Properties.Resources.Navigate_right;
            this.buttonMap.Location = new System.Drawing.Point(398, 79);
            this.buttonMap.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.buttonMap.Name = "buttonMap";
            this.buttonMap.Size = new System.Drawing.Size(28, 30);
            this.buttonMap.TabIndex = 13;
            this.buttonMap.UseVisualStyleBackColor = true;
            this.buttonMap.Click += new System.EventHandler(this.Mapping_Click);
            // 
            // buttonUnMap
            // 
            this.buttonUnMap.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonUnMap.Image = global::TotalSmartCoding.Properties.Resources.Navigate_left;
            this.buttonUnMap.Location = new System.Drawing.Point(398, 112);
            this.buttonUnMap.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.buttonUnMap.Name = "buttonUnMap";
            this.buttonUnMap.Size = new System.Drawing.Size(28, 30);
            this.buttonUnMap.TabIndex = 14;
            this.buttonUnMap.UseVisualStyleBackColor = true;
            this.buttonUnMap.Click += new System.EventHandler(this.Mapping_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Image = global::TotalSmartCoding.Properties.Resources.signout_icon_24;
            this.buttonCancel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonCancel.Size = new System.Drawing.Size(81, 52);
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.OKCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Image = global::TotalSmartCoding.Properties.Resources.Oxygen_Icons_org_Oxygen_Actions_go_next_view;
            this.buttonOK.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonOK.Size = new System.Drawing.Size(92, 52);
            this.buttonOK.Text = "Next";
            this.buttonOK.Click += new System.EventHandler(this.OKCancel_Click);
            // 
            // ColumnMappings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 420);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolStrip2);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColumnMappings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mapping column for import";
            this.Load += new System.EventHandler(this.ColumnMappings_Load);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastColumnAvailable)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fastColumnMapping)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonMap;
        private System.Windows.Forms.Button buttonUnMap;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton buttonCancel;
        private System.Windows.Forms.ToolStripButton buttonOK;
        private BrightIdeasSoftware.FastObjectListView fastColumnAvailable;
        private BrightIdeasSoftware.OLVColumn olvAvailableColumnAvailableName;
        private BrightIdeasSoftware.OLVColumn olvAvailableColumnMappingName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private BrightIdeasSoftware.FastObjectListView fastColumnMapping;
        private BrightIdeasSoftware.OLVColumn olvMappingColumnDisplayName;
        private BrightIdeasSoftware.OLVColumn olvMappingColumnMappingName;
    }
}