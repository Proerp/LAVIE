namespace TotalSmartCoding.Views.Productions
{
    partial class BatchRepackWizard
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
            this.toolStripBottom = new System.Windows.Forms.ToolStrip();
            this.buttonESC = new System.Windows.Forms.ToolStripButton();
            this.buttonAddExit = new System.Windows.Forms.ToolStripButton();
            this.fastBatchRepacks = new BrightIdeasSoftware.FastObjectListView();
            this.olvLineIndex = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvSerialID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvPackCode = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvBatchCode = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvBatchEntryDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvLotCode = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvPrintedDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvPrintedHour = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvPrintedMinute = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.panelMaster = new System.Windows.Forms.Panel();
            this.fastMismatchedBarcodes = new BrightIdeasSoftware.FastObjectListView();
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvMismatchedSerialID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvScannedBarcode = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvItemAPICode = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvDescription = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvItemName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.toolStripBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastBatchRepacks)).BeginInit();
            this.panelMaster.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastMismatchedBarcodes)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripBottom
            // 
            this.toolStripBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripBottom.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripBottom.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripBottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonESC,
            this.buttonAddExit});
            this.toolStripBottom.Location = new System.Drawing.Point(0, 629);
            this.toolStripBottom.Name = "toolStripBottom";
            this.toolStripBottom.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStripBottom.Size = new System.Drawing.Size(1297, 55);
            this.toolStripBottom.TabIndex = 0;
            this.toolStripBottom.Text = "toolStrip1";
            // 
            // buttonESC
            // 
            this.buttonESC.Image = global::TotalSmartCoding.Properties.Resources.signout_icon_24;
            this.buttonESC.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonESC.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonESC.Name = "buttonESC";
            this.buttonESC.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonESC.Size = new System.Drawing.Size(73, 52);
            this.buttonESC.Text = "Close";
            this.buttonESC.Click += new System.EventHandler(this.buttonAddESC_Click);
            // 
            // buttonAddExit
            // 
            this.buttonAddExit.Image = global::TotalSmartCoding.Properties.Resources.Add_continue;
            this.buttonAddExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonAddExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAddExit.Name = "buttonAddExit";
            this.buttonAddExit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonAddExit.Size = new System.Drawing.Size(158, 52);
            this.buttonAddExit.Text = "Add and Close";
            this.buttonAddExit.Click += new System.EventHandler(this.buttonAddESC_Click);
            // 
            // fastBatchRepacks
            // 
            this.fastBatchRepacks.AllColumns.Add(this.olvLineIndex);
            this.fastBatchRepacks.AllColumns.Add(this.olvSerialID);
            this.fastBatchRepacks.AllColumns.Add(this.olvPackCode);
            this.fastBatchRepacks.AllColumns.Add(this.olvBatchCode);
            this.fastBatchRepacks.AllColumns.Add(this.olvBatchEntryDate);
            this.fastBatchRepacks.AllColumns.Add(this.olvLotCode);
            this.fastBatchRepacks.AllColumns.Add(this.olvPrintedDate);
            this.fastBatchRepacks.AllColumns.Add(this.olvPrintedHour);
            this.fastBatchRepacks.AllColumns.Add(this.olvPrintedMinute);
            this.fastBatchRepacks.CheckedAspectName = "";
            this.fastBatchRepacks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvLineIndex,
            this.olvSerialID,
            this.olvPackCode,
            this.olvBatchCode,
            this.olvBatchEntryDate,
            this.olvLotCode,
            this.olvPrintedDate,
            this.olvPrintedHour,
            this.olvPrintedMinute});
            this.fastBatchRepacks.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastBatchRepacks.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fastBatchRepacks.FullRowSelect = true;
            this.fastBatchRepacks.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.fastBatchRepacks.HideSelection = false;
            this.fastBatchRepacks.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastBatchRepacks.Location = new System.Drawing.Point(0, 25);
            this.fastBatchRepacks.Name = "fastBatchRepacks";
            this.fastBatchRepacks.OwnerDraw = true;
            this.fastBatchRepacks.ShowGroups = false;
            this.fastBatchRepacks.ShowImagesOnSubItems = true;
            this.fastBatchRepacks.Size = new System.Drawing.Size(1079, 198);
            this.fastBatchRepacks.TabIndex = 69;
            this.fastBatchRepacks.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastBatchRepacks.UseCompatibleStateImageBehavior = false;
            this.fastBatchRepacks.UseFiltering = true;
            this.fastBatchRepacks.View = System.Windows.Forms.View.Details;
            this.fastBatchRepacks.VirtualMode = true;
            // 
            // olvLineIndex
            // 
            this.olvLineIndex.AspectName = "LineIndex";
            this.olvLineIndex.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvLineIndex.Text = "";
            this.olvLineIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvLineIndex.Width = 39;
            // 
            // olvSerialID
            // 
            this.olvSerialID.AspectName = "SerialID";
            this.olvSerialID.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvSerialID.Text = "Serial #";
            this.olvSerialID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // olvPackCode
            // 
            this.olvPackCode.AspectName = "Code";
            this.olvPackCode.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvPackCode.Sortable = false;
            this.olvPackCode.Text = "Pack";
            this.olvPackCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvPackCode.Width = 290;
            // 
            // olvBatchCode
            // 
            this.olvBatchCode.AspectName = "BatchCode";
            this.olvBatchCode.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvBatchCode.Sortable = false;
            this.olvBatchCode.Text = "Batch";
            this.olvBatchCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvBatchCode.Width = 98;
            // 
            // olvBatchEntryDate
            // 
            this.olvBatchEntryDate.AspectName = "EntryDate";
            this.olvBatchEntryDate.AspectToStringFormat = "{0:d}";
            this.olvBatchEntryDate.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvBatchEntryDate.Sortable = false;
            this.olvBatchEntryDate.Text = "Date";
            this.olvBatchEntryDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvBatchEntryDate.Width = 128;
            // 
            // olvLotCode
            // 
            this.olvLotCode.AspectName = "LotCode";
            this.olvLotCode.FillsFreeSpace = true;
            this.olvLotCode.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvLotCode.Sortable = false;
            this.olvLotCode.Text = "Lot";
            this.olvLotCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvLotCode.Width = 88;
            // 
            // olvPrintedDate
            // 
            this.olvPrintedDate.AspectName = "dd";
            this.olvPrintedDate.AspectToStringFormat = "{0:#,#}";
            this.olvPrintedDate.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvPrintedDate.Text = "Printed Date";
            this.olvPrintedDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvPrintedDate.Width = 98;
            // 
            // olvPrintedHour
            // 
            this.olvPrintedHour.AspectName = "HH";
            this.olvPrintedHour.AspectToStringFormat = "{0:#,#}";
            this.olvPrintedHour.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvPrintedHour.Text = "Hour";
            this.olvPrintedHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvPrintedHour.Width = 68;
            // 
            // olvPrintedMinute
            // 
            this.olvPrintedMinute.AspectName = "mm";
            this.olvPrintedMinute.AspectToStringFormat = "{0:#,#}";
            this.olvPrintedMinute.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvPrintedMinute.Text = "Minute";
            this.olvPrintedMinute.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvPrintedMinute.Width = 68;
            // 
            // panelMaster
            // 
            this.panelMaster.Controls.Add(this.fastMismatchedBarcodes);
            this.panelMaster.Controls.Add(this.fastBatchRepacks);
            this.panelMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMaster.Location = new System.Drawing.Point(0, 0);
            this.panelMaster.Name = "panelMaster";
            this.panelMaster.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.panelMaster.Size = new System.Drawing.Size(1297, 629);
            this.panelMaster.TabIndex = 71;
            // 
            // fastMismatchedBarcodes
            // 
            this.fastMismatchedBarcodes.AllColumns.Add(this.olvColumn5);
            this.fastMismatchedBarcodes.AllColumns.Add(this.olvMismatchedSerialID);
            this.fastMismatchedBarcodes.AllColumns.Add(this.olvScannedBarcode);
            this.fastMismatchedBarcodes.AllColumns.Add(this.olvItemAPICode);
            this.fastMismatchedBarcodes.AllColumns.Add(this.olvDescription);
            this.fastMismatchedBarcodes.AllColumns.Add(this.olvItemName);
            this.fastMismatchedBarcodes.CheckedAspectName = "";
            this.fastMismatchedBarcodes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn5,
            this.olvMismatchedSerialID,
            this.olvScannedBarcode,
            this.olvItemAPICode,
            this.olvDescription,
            this.olvItemName});
            this.fastMismatchedBarcodes.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastMismatchedBarcodes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fastMismatchedBarcodes.FullRowSelect = true;
            this.fastMismatchedBarcodes.HideSelection = false;
            this.fastMismatchedBarcodes.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastMismatchedBarcodes.Location = new System.Drawing.Point(-3, 250);
            this.fastMismatchedBarcodes.Name = "fastMismatchedBarcodes";
            this.fastMismatchedBarcodes.OwnerDraw = true;
            this.fastMismatchedBarcodes.ShowGroups = false;
            this.fastMismatchedBarcodes.ShowImagesOnSubItems = true;
            this.fastMismatchedBarcodes.Size = new System.Drawing.Size(1082, 303);
            this.fastMismatchedBarcodes.TabIndex = 72;
            this.fastMismatchedBarcodes.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastMismatchedBarcodes.UseCompatibleStateImageBehavior = false;
            this.fastMismatchedBarcodes.UseFiltering = true;
            this.fastMismatchedBarcodes.View = System.Windows.Forms.View.Details;
            this.fastMismatchedBarcodes.VirtualMode = true;
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "LineIndex";
            this.olvColumn5.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn5.Text = "";
            this.olvColumn5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn5.Width = 39;
            // 
            // olvMismatchedSerialID
            // 
            this.olvMismatchedSerialID.AspectName = "SerialID";
            this.olvMismatchedSerialID.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvMismatchedSerialID.Text = "Serial #";
            this.olvMismatchedSerialID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // olvScannedBarcode
            // 
            this.olvScannedBarcode.AspectName = "Barcode";
            this.olvScannedBarcode.FillsFreeSpace = true;
            this.olvScannedBarcode.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvScannedBarcode.Sortable = false;
            this.olvScannedBarcode.Text = "Barcode";
            this.olvScannedBarcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvScannedBarcode.Width = 290;
            // 
            // olvItemAPICode
            // 
            this.olvItemAPICode.AspectName = "APICode";
            this.olvItemAPICode.AspectToStringFormat = "";
            this.olvItemAPICode.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvItemAPICode.Text = "Item";
            this.olvItemAPICode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvItemAPICode.Width = 98;
            // 
            // olvDescription
            // 
            this.olvDescription.AspectName = "Description";
            this.olvDescription.Text = "Description";
            this.olvDescription.Width = 0;
            // 
            // olvItemName
            // 
            this.olvItemName.AspectName = "CommodityName";
            this.olvItemName.AspectToStringFormat = "";
            this.olvItemName.FillsFreeSpace = true;
            this.olvItemName.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvItemName.Text = "Name";
            this.olvItemName.Width = 308;
            // 
            // BatchRepackWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1297, 684);
            this.Controls.Add(this.panelMaster);
            this.Controls.Add(this.toolStripBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "BatchRepackWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Packs to repack";
            this.Load += new System.EventHandler(this.BatchRepackWizard_Load);
            this.toolStripBottom.ResumeLayout(false);
            this.toolStripBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastBatchRepacks)).EndInit();
            this.panelMaster.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fastMismatchedBarcodes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBottom;
        private System.Windows.Forms.ToolStripButton buttonESC;
        private System.Windows.Forms.ToolStripButton buttonAddExit;
        private BrightIdeasSoftware.FastObjectListView fastBatchRepacks;
        private System.Windows.Forms.Panel panelMaster;
        private BrightIdeasSoftware.OLVColumn olvPackCode;
        private BrightIdeasSoftware.OLVColumn olvLotCode;
        private BrightIdeasSoftware.OLVColumn olvLineIndex;
        private BrightIdeasSoftware.OLVColumn olvBatchCode;
        private BrightIdeasSoftware.OLVColumn olvBatchEntryDate;
        private BrightIdeasSoftware.FastObjectListView fastMismatchedBarcodes;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private BrightIdeasSoftware.OLVColumn olvScannedBarcode;
        private BrightIdeasSoftware.OLVColumn olvDescription;
        private BrightIdeasSoftware.OLVColumn olvItemAPICode;
        private BrightIdeasSoftware.OLVColumn olvItemName;
        private BrightIdeasSoftware.OLVColumn olvPrintedDate;
        private BrightIdeasSoftware.OLVColumn olvPrintedHour;
        private BrightIdeasSoftware.OLVColumn olvPrintedMinute;
        private BrightIdeasSoftware.OLVColumn olvSerialID;
        private BrightIdeasSoftware.OLVColumn olvMismatchedSerialID;
    }
}