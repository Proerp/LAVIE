namespace TotalSmartCoding.Views.Inventories.GoodsReceipts
{
    partial class WizardMaster
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonESC = new System.Windows.Forms.ToolStripButton();
            this.buttonOK = new System.Windows.Forms.ToolStripButton();
            this.fastPendingPickups = new BrightIdeasSoftware.FastObjectListView();
            this.olvPickupEntryDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvPickupReference = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvWarehouseName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.fastPendingPickupWarehouses = new BrightIdeasSoftware.FastObjectListView();
            this.olvWarehouseName1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.panelMaster = new System.Windows.Forms.Panel();
            this.fastPendingGoodsIssueTransfers = new BrightIdeasSoftware.FastObjectListView();
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.fastPendingGoodsIssueTransferWarehouses = new BrightIdeasSoftware.FastObjectListView();
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvVoucherCodes = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvDescription = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastPendingPickups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastPendingPickupWarehouses)).BeginInit();
            this.panelMaster.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastPendingGoodsIssueTransfers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastPendingGoodsIssueTransferWarehouses)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonESC,
            this.buttonOK});
            this.toolStrip1.Location = new System.Drawing.Point(0, 548);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(1147, 55);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // buttonESC
            // 
            this.buttonESC.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonESC.Image = global::TotalSmartCoding.Properties.Resources.signout_icon_24;
            this.buttonESC.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonESC.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonESC.Name = "buttonESC";
            this.buttonESC.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonESC.Size = new System.Drawing.Size(83, 52);
            this.buttonESC.Text = "Cancel";
            this.buttonESC.Click += new System.EventHandler(this.buttonOKESC_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOK.Image = global::TotalSmartCoding.Properties.Resources.Oxygen_Icons_org_Oxygen_Actions_go_next_view;
            this.buttonOK.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonOK.Size = new System.Drawing.Size(94, 52);
            this.buttonOK.Text = "Next";
            this.buttonOK.Click += new System.EventHandler(this.buttonOKESC_Click);
            // 
            // fastPendingPickups
            // 
            this.fastPendingPickups.AllColumns.Add(this.olvPickupEntryDate);
            this.fastPendingPickups.AllColumns.Add(this.olvPickupReference);
            this.fastPendingPickups.AllColumns.Add(this.olvWarehouseName);
            this.fastPendingPickups.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvPickupEntryDate,
            this.olvPickupReference,
            this.olvWarehouseName});
            this.fastPendingPickups.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastPendingPickups.Dock = System.Windows.Forms.DockStyle.Top;
            this.fastPendingPickups.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fastPendingPickups.FullRowSelect = true;
            this.fastPendingPickups.HideSelection = false;
            this.fastPendingPickups.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastPendingPickups.Location = new System.Drawing.Point(0, 87);
            this.fastPendingPickups.Name = "fastPendingPickups";
            this.fastPendingPickups.OwnerDraw = true;
            this.fastPendingPickups.ShowGroups = false;
            this.fastPendingPickups.Size = new System.Drawing.Size(1147, 124);
            this.fastPendingPickups.TabIndex = 69;
            this.fastPendingPickups.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastPendingPickups.UseCompatibleStateImageBehavior = false;
            this.fastPendingPickups.UseFiltering = true;
            this.fastPendingPickups.View = System.Windows.Forms.View.Details;
            this.fastPendingPickups.VirtualMode = true;
            // 
            // olvPickupEntryDate
            // 
            this.olvPickupEntryDate.AspectName = "PrimaryEntryDate";
            this.olvPickupEntryDate.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvPickupEntryDate.Text = "Entry Date";
            this.olvPickupEntryDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvPickupEntryDate.Width = 170;
            // 
            // olvPickupReference
            // 
            this.olvPickupReference.AspectName = "PrimaryReference";
            this.olvPickupReference.Text = "Reference";
            this.olvPickupReference.Width = 137;
            // 
            // olvWarehouseName
            // 
            this.olvWarehouseName.AspectName = "WarehouseName";
            this.olvWarehouseName.FillsFreeSpace = true;
            this.olvWarehouseName.Text = "Warehouse";
            this.olvWarehouseName.Width = 192;
            // 
            // fastPendingPickupWarehouses
            // 
            this.fastPendingPickupWarehouses.AllColumns.Add(this.olvWarehouseName1);
            this.fastPendingPickupWarehouses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvWarehouseName1});
            this.fastPendingPickupWarehouses.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastPendingPickupWarehouses.Dock = System.Windows.Forms.DockStyle.Top;
            this.fastPendingPickupWarehouses.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fastPendingPickupWarehouses.FullRowSelect = true;
            this.fastPendingPickupWarehouses.HideSelection = false;
            this.fastPendingPickupWarehouses.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastPendingPickupWarehouses.Location = new System.Drawing.Point(0, 9);
            this.fastPendingPickupWarehouses.Name = "fastPendingPickupWarehouses";
            this.fastPendingPickupWarehouses.OwnerDraw = true;
            this.fastPendingPickupWarehouses.ShowGroups = false;
            this.fastPendingPickupWarehouses.Size = new System.Drawing.Size(1147, 78);
            this.fastPendingPickupWarehouses.TabIndex = 70;
            this.fastPendingPickupWarehouses.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastPendingPickupWarehouses.UseCompatibleStateImageBehavior = false;
            this.fastPendingPickupWarehouses.UseFiltering = true;
            this.fastPendingPickupWarehouses.View = System.Windows.Forms.View.Details;
            this.fastPendingPickupWarehouses.VirtualMode = true;
            // 
            // olvWarehouseName1
            // 
            this.olvWarehouseName1.AspectName = "WarehouseName";
            this.olvWarehouseName1.FillsFreeSpace = true;
            this.olvWarehouseName1.Text = "Warehouse";
            this.olvWarehouseName1.Width = 263;
            // 
            // panelMaster
            // 
            this.panelMaster.BackColor = System.Drawing.Color.Ivory;
            this.panelMaster.Controls.Add(this.fastPendingGoodsIssueTransfers);
            this.panelMaster.Controls.Add(this.fastPendingGoodsIssueTransferWarehouses);
            this.panelMaster.Controls.Add(this.fastPendingPickups);
            this.panelMaster.Controls.Add(this.fastPendingPickupWarehouses);
            this.panelMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMaster.Location = new System.Drawing.Point(0, 0);
            this.panelMaster.Name = "panelMaster";
            this.panelMaster.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
            this.panelMaster.Size = new System.Drawing.Size(1147, 548);
            this.panelMaster.TabIndex = 71;
            // 
            // fastPendingGoodsIssueTransfers
            // 
            this.fastPendingGoodsIssueTransfers.AllColumns.Add(this.olvColumn3);
            this.fastPendingGoodsIssueTransfers.AllColumns.Add(this.olvColumn4);
            this.fastPendingGoodsIssueTransfers.AllColumns.Add(this.olvVoucherCodes);
            this.fastPendingGoodsIssueTransfers.AllColumns.Add(this.olvColumn5);
            this.fastPendingGoodsIssueTransfers.AllColumns.Add(this.olvDescription);
            this.fastPendingGoodsIssueTransfers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn3,
            this.olvColumn4,
            this.olvVoucherCodes,
            this.olvColumn5,
            this.olvDescription});
            this.fastPendingGoodsIssueTransfers.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastPendingGoodsIssueTransfers.Dock = System.Windows.Forms.DockStyle.Top;
            this.fastPendingGoodsIssueTransfers.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fastPendingGoodsIssueTransfers.FullRowSelect = true;
            this.fastPendingGoodsIssueTransfers.HideSelection = false;
            this.fastPendingGoodsIssueTransfers.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastPendingGoodsIssueTransfers.Location = new System.Drawing.Point(0, 328);
            this.fastPendingGoodsIssueTransfers.Name = "fastPendingGoodsIssueTransfers";
            this.fastPendingGoodsIssueTransfers.OwnerDraw = true;
            this.fastPendingGoodsIssueTransfers.ShowGroups = false;
            this.fastPendingGoodsIssueTransfers.Size = new System.Drawing.Size(1147, 124);
            this.fastPendingGoodsIssueTransfers.TabIndex = 72;
            this.fastPendingGoodsIssueTransfers.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastPendingGoodsIssueTransfers.UseCompatibleStateImageBehavior = false;
            this.fastPendingGoodsIssueTransfers.UseFiltering = true;
            this.fastPendingGoodsIssueTransfers.View = System.Windows.Forms.View.Details;
            this.fastPendingGoodsIssueTransfers.VirtualMode = true;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "PrimaryEntryDate";
            this.olvColumn3.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn3.Text = "Entry Date";
            this.olvColumn3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn3.Width = 139;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "PrimaryReference";
            this.olvColumn4.Text = "Reference";
            this.olvColumn4.Width = 110;
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "SourceWarehouseName";
            this.olvColumn5.Text = "Source Warehouse";
            this.olvColumn5.Width = 110;
            // 
            // fastPendingGoodsIssueTransferWarehouses
            // 
            this.fastPendingGoodsIssueTransferWarehouses.AllColumns.Add(this.olvColumn2);
            this.fastPendingGoodsIssueTransferWarehouses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn2});
            this.fastPendingGoodsIssueTransferWarehouses.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastPendingGoodsIssueTransferWarehouses.Dock = System.Windows.Forms.DockStyle.Top;
            this.fastPendingGoodsIssueTransferWarehouses.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fastPendingGoodsIssueTransferWarehouses.FullRowSelect = true;
            this.fastPendingGoodsIssueTransferWarehouses.HideSelection = false;
            this.fastPendingGoodsIssueTransferWarehouses.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastPendingGoodsIssueTransferWarehouses.Location = new System.Drawing.Point(0, 211);
            this.fastPendingGoodsIssueTransferWarehouses.Name = "fastPendingGoodsIssueTransferWarehouses";
            this.fastPendingGoodsIssueTransferWarehouses.OwnerDraw = true;
            this.fastPendingGoodsIssueTransferWarehouses.ShowGroups = false;
            this.fastPendingGoodsIssueTransferWarehouses.Size = new System.Drawing.Size(1147, 117);
            this.fastPendingGoodsIssueTransferWarehouses.TabIndex = 71;
            this.fastPendingGoodsIssueTransferWarehouses.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastPendingGoodsIssueTransferWarehouses.UseCompatibleStateImageBehavior = false;
            this.fastPendingGoodsIssueTransferWarehouses.UseFiltering = true;
            this.fastPendingGoodsIssueTransferWarehouses.View = System.Windows.Forms.View.Details;
            this.fastPendingGoodsIssueTransferWarehouses.VirtualMode = true;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "WarehouseName";
            this.olvColumn2.FillsFreeSpace = true;
            this.olvColumn2.Text = "Warehouse";
            this.olvColumn2.Width = 263;
            // 
            // olvVoucherCodes
            // 
            this.olvVoucherCodes.AspectName = "VoucherCodes";
            this.olvVoucherCodes.Text = "Vouchers";
            this.olvVoucherCodes.Width = 192;
            // 
            // olvDescription
            // 
            this.olvDescription.AspectName = "Description";
            this.olvDescription.FillsFreeSpace = true;
            this.olvDescription.Text = "Description";
            // 
            // WizardMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1147, 603);
            this.Controls.Add(this.panelMaster);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WizardMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Wizard [New Goods Receipt]";
            this.Load += new System.EventHandler(this.Wizard_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastPendingPickups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastPendingPickupWarehouses)).EndInit();
            this.panelMaster.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fastPendingGoodsIssueTransfers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastPendingGoodsIssueTransferWarehouses)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton buttonESC;
        private System.Windows.Forms.ToolStripButton buttonOK;
        private BrightIdeasSoftware.FastObjectListView fastPendingPickups;
        private BrightIdeasSoftware.FastObjectListView fastPendingPickupWarehouses;
        private System.Windows.Forms.Panel panelMaster;
        private BrightIdeasSoftware.OLVColumn olvPickupEntryDate;
        private BrightIdeasSoftware.OLVColumn olvWarehouseName;
        private BrightIdeasSoftware.OLVColumn olvWarehouseName1;
        private BrightIdeasSoftware.OLVColumn olvPickupReference;
        private BrightIdeasSoftware.FastObjectListView fastPendingGoodsIssueTransfers;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private BrightIdeasSoftware.FastObjectListView fastPendingGoodsIssueTransferWarehouses;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvVoucherCodes;
        private BrightIdeasSoftware.OLVColumn olvDescription;
    }
}