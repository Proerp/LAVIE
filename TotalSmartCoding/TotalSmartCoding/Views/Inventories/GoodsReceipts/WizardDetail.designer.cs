namespace TotalSmartCoding.Views.Inventories.GoodsReceipts
{
    partial class WizardDetail
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
            this.components = new System.ComponentModel.Container();
            this.fastPendingPallets = new BrightIdeasSoftware.FastObjectListView();
            this.olvPalletSelected = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvCommodityCode = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvCommodityName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvPalletBinLocationCode = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvPalletQuantityRemains = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvPalletLineVolumeRemains = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvPalletCode = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuDetails = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuOptionBinLocations = new System.Windows.Forms.ToolStripMenuItem();
            this.panelMaster = new System.Windows.Forms.Panel();
            this.fastPendingCartons = new BrightIdeasSoftware.FastObjectListView();
            this.olvCartonSelected = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvCartonQuantityRemains = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvCartonLineVolumeRemains = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvCartonCode = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.panelBottom = new System.Windows.Forms.Panel();
            this.panelBottomRight = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonESC = new System.Windows.Forms.ToolStripButton();
            this.buttonAddExit = new System.Windows.Forms.ToolStripButton();
            this.buttonAdd = new System.Windows.Forms.ToolStripButton();
            this.panelBottomLeft = new System.Windows.Forms.Panel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.textexFilters = new System.Windows.Forms.ToolStripTextBox();
            this.buttonClearFilters = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.fastPendingPallets)).BeginInit();
            this.contextMenuDetails.SuspendLayout();
            this.panelMaster.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastPendingCartons)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.panelBottomRight.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panelBottomLeft.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // fastPendingPallets
            // 
            this.fastPendingPallets.AllColumns.Add(this.olvPalletSelected);
            this.fastPendingPallets.AllColumns.Add(this.olvCommodityCode);
            this.fastPendingPallets.AllColumns.Add(this.olvCommodityName);
            this.fastPendingPallets.AllColumns.Add(this.olvPalletBinLocationCode);
            this.fastPendingPallets.AllColumns.Add(this.olvPalletQuantityRemains);
            this.fastPendingPallets.AllColumns.Add(this.olvPalletLineVolumeRemains);
            this.fastPendingPallets.AllColumns.Add(this.olvPalletCode);
            this.fastPendingPallets.CheckBoxes = true;
            this.fastPendingPallets.CheckedAspectName = "IsSelected";
            this.fastPendingPallets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvPalletSelected,
            this.olvCommodityCode,
            this.olvCommodityName,
            this.olvPalletBinLocationCode,
            this.olvPalletQuantityRemains,
            this.olvPalletLineVolumeRemains,
            this.olvPalletCode});
            this.fastPendingPallets.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastPendingPallets.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fastPendingPallets.FullRowSelect = true;
            this.fastPendingPallets.HideSelection = false;
            this.fastPendingPallets.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastPendingPallets.Location = new System.Drawing.Point(0, 192);
            this.fastPendingPallets.Name = "fastPendingPallets";
            this.fastPendingPallets.OwnerDraw = true;
            this.fastPendingPallets.ShowGroups = false;
            this.fastPendingPallets.ShowImagesOnSubItems = true;
            this.fastPendingPallets.Size = new System.Drawing.Size(1386, 245);
            this.fastPendingPallets.TabIndex = 69;
            this.fastPendingPallets.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastPendingPallets.UseCompatibleStateImageBehavior = false;
            this.fastPendingPallets.UseFiltering = true;
            this.fastPendingPallets.View = System.Windows.Forms.View.Details;
            this.fastPendingPallets.VirtualMode = true;
            this.fastPendingPallets.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.fastPendingBarcodes_MouseDoubleClick);
            // 
            // olvPalletSelected
            // 
            this.olvPalletSelected.HeaderCheckBox = true;
            this.olvPalletSelected.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvPalletSelected.Sortable = false;
            this.olvPalletSelected.Text = "";
            this.olvPalletSelected.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvPalletSelected.Width = 20;
            // 
            // olvCommodityCode
            // 
            this.olvCommodityCode.AspectName = "CommodityCode";
            this.olvCommodityCode.Text = "Item";
            this.olvCommodityCode.Width = 90;
            // 
            // olvCommodityName
            // 
            this.olvCommodityName.AspectName = "CommodityName";
            this.olvCommodityName.Text = "Item Name";
            this.olvCommodityName.Width = 290;
            // 
            // olvPalletBinLocationCode
            // 
            this.olvPalletBinLocationCode.AspectName = "BinLocationCode";
            this.olvPalletBinLocationCode.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvPalletBinLocationCode.Text = "Bin Location";
            this.olvPalletBinLocationCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvPalletBinLocationCode.Width = 110;
            // 
            // olvPalletQuantityRemains
            // 
            this.olvPalletQuantityRemains.AspectName = "QuantityRemains";
            this.olvPalletQuantityRemains.AspectToStringFormat = "{0:#,#}";
            this.olvPalletQuantityRemains.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvPalletQuantityRemains.Text = "Quantity";
            this.olvPalletQuantityRemains.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvPalletQuantityRemains.Width = 80;
            // 
            // olvPalletLineVolumeRemains
            // 
            this.olvPalletLineVolumeRemains.AspectName = "LineVolumeRemains";
            this.olvPalletLineVolumeRemains.AspectToStringFormat = "{0:#,##0.00}";
            this.olvPalletLineVolumeRemains.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvPalletLineVolumeRemains.Text = "Volume";
            this.olvPalletLineVolumeRemains.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvPalletLineVolumeRemains.Width = 90;
            // 
            // olvPalletCode
            // 
            this.olvPalletCode.AspectName = "PalletCode";
            this.olvPalletCode.FillsFreeSpace = true;
            this.olvPalletCode.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvPalletCode.Text = "Pallet Code";
            this.olvPalletCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvPalletCode.Width = 150;
            // 
            // contextMenuDetails
            // 
            this.contextMenuDetails.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuDetails.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuOptionBinLocations});
            this.contextMenuDetails.Name = "contextMenuDetails";
            this.contextMenuDetails.Size = new System.Drawing.Size(267, 30);
            this.contextMenuDetails.Text = "Context MenuDetails";
            // 
            // menuOptionBinLocations
            // 
            this.menuOptionBinLocations.Name = "menuOptionBinLocations";
            this.menuOptionBinLocations.Size = new System.Drawing.Size(266, 26);
            this.menuOptionBinLocations.Text = "Set bin location for this line";
            this.menuOptionBinLocations.Click += new System.EventHandler(this.menuOptionBinLocations_Click);
            // 
            // panelMaster
            // 
            this.panelMaster.BackColor = System.Drawing.Color.Ivory;
            this.panelMaster.Controls.Add(this.fastPendingPallets);
            this.panelMaster.Controls.Add(this.fastPendingCartons);
            this.panelMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMaster.Location = new System.Drawing.Point(0, 0);
            this.panelMaster.Name = "panelMaster";
            this.panelMaster.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
            this.panelMaster.Size = new System.Drawing.Size(1386, 548);
            this.panelMaster.TabIndex = 71;
            // 
            // fastPendingCartons
            // 
            this.fastPendingCartons.AllColumns.Add(this.olvCartonSelected);
            this.fastPendingCartons.AllColumns.Add(this.olvColumn4);
            this.fastPendingCartons.AllColumns.Add(this.olvColumn6);
            this.fastPendingCartons.AllColumns.Add(this.olvColumn2);
            this.fastPendingCartons.AllColumns.Add(this.olvCartonQuantityRemains);
            this.fastPendingCartons.AllColumns.Add(this.olvCartonLineVolumeRemains);
            this.fastPendingCartons.AllColumns.Add(this.olvCartonCode);
            this.fastPendingCartons.CheckBoxes = true;
            this.fastPendingCartons.CheckedAspectName = "IsSelected";
            this.fastPendingCartons.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvCartonSelected,
            this.olvColumn4,
            this.olvColumn6,
            this.olvColumn2,
            this.olvCartonQuantityRemains,
            this.olvCartonLineVolumeRemains,
            this.olvCartonCode});
            this.fastPendingCartons.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastPendingCartons.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fastPendingCartons.FullRowSelect = true;
            this.fastPendingCartons.HideSelection = false;
            this.fastPendingCartons.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastPendingCartons.Location = new System.Drawing.Point(0, 52);
            this.fastPendingCartons.Name = "fastPendingCartons";
            this.fastPendingCartons.OwnerDraw = true;
            this.fastPendingCartons.ShowGroups = false;
            this.fastPendingCartons.ShowImagesOnSubItems = true;
            this.fastPendingCartons.Size = new System.Drawing.Size(1386, 245);
            this.fastPendingCartons.TabIndex = 70;
            this.fastPendingCartons.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastPendingCartons.UseCompatibleStateImageBehavior = false;
            this.fastPendingCartons.UseFiltering = true;
            this.fastPendingCartons.View = System.Windows.Forms.View.Details;
            this.fastPendingCartons.VirtualMode = true;
            this.fastPendingCartons.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.fastPendingBarcodes_MouseDoubleClick);
            // 
            // olvCartonSelected
            // 
            this.olvCartonSelected.HeaderCheckBox = true;
            this.olvCartonSelected.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvCartonSelected.Sortable = false;
            this.olvCartonSelected.Text = "";
            this.olvCartonSelected.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvCartonSelected.Width = 20;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "CommodityCode";
            this.olvColumn4.Text = "Item";
            this.olvColumn4.Width = 90;
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "CommodityName";
            this.olvColumn6.Text = "Item Name";
            this.olvColumn6.Width = 290;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "BinLocationCode";
            this.olvColumn2.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn2.Text = "Bin Location";
            this.olvColumn2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn2.Width = 110;
            // 
            // olvCartonQuantityRemains
            // 
            this.olvCartonQuantityRemains.AspectName = "QuantityRemains";
            this.olvCartonQuantityRemains.AspectToStringFormat = "{0:#,#}";
            this.olvCartonQuantityRemains.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvCartonQuantityRemains.Text = "Quantity";
            this.olvCartonQuantityRemains.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvCartonQuantityRemains.Width = 80;
            // 
            // olvCartonLineVolumeRemains
            // 
            this.olvCartonLineVolumeRemains.AspectName = "LineVolumeRemains";
            this.olvCartonLineVolumeRemains.AspectToStringFormat = "{0:#,##0.00}";
            this.olvCartonLineVolumeRemains.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvCartonLineVolumeRemains.Text = "Volume";
            this.olvCartonLineVolumeRemains.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvCartonLineVolumeRemains.Width = 90;
            // 
            // olvCartonCode
            // 
            this.olvCartonCode.AspectName = "CartonCode";
            this.olvCartonCode.FillsFreeSpace = true;
            this.olvCartonCode.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvCartonCode.Text = "Carton Code";
            this.olvCartonCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvCartonCode.Width = 150;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.panelBottomRight);
            this.panelBottom.Controls.Add(this.panelBottomLeft);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 548);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1386, 55);
            this.panelBottom.TabIndex = 72;
            // 
            // panelBottomRight
            // 
            this.panelBottomRight.Controls.Add(this.toolStrip1);
            this.panelBottomRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBottomRight.Location = new System.Drawing.Point(616, 0);
            this.panelBottomRight.Name = "panelBottomRight";
            this.panelBottomRight.Size = new System.Drawing.Size(770, 55);
            this.panelBottomRight.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonESC,
            this.buttonAddExit,
            this.buttonAdd});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(770, 55);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
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
            this.buttonAddExit.Image = global::TotalSmartCoding.Properties.Resources.Add_close;
            this.buttonAddExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonAddExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAddExit.Name = "buttonAddExit";
            this.buttonAddExit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonAddExit.Size = new System.Drawing.Size(158, 52);
            this.buttonAddExit.Text = "Add and Close";
            this.buttonAddExit.Click += new System.EventHandler(this.buttonAddESC_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Image = global::TotalSmartCoding.Properties.Resources.Add_continue;
            this.buttonAdd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonAdd.Size = new System.Drawing.Size(89, 52);
            this.buttonAdd.Text = "Add";
            this.buttonAdd.Click += new System.EventHandler(this.buttonAddESC_Click);
            // 
            // panelBottomLeft
            // 
            this.panelBottomLeft.Controls.Add(this.toolStrip2);
            this.panelBottomLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelBottomLeft.Location = new System.Drawing.Point(0, 0);
            this.panelBottomLeft.Name = "panelBottomLeft";
            this.panelBottomLeft.Size = new System.Drawing.Size(616, 55);
            this.panelBottomLeft.TabIndex = 1;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.textexFilters,
            this.buttonClearFilters});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStrip2.Size = new System.Drawing.Size(616, 55);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::TotalSmartCoding.Properties.Resources.Zoom_seach;
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(52, 52);
            this.toolStripButton1.Text = "Filters";
            // 
            // textexFilters
            // 
            this.textexFilters.AutoSize = false;
            this.textexFilters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textexFilters.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textexFilters.Name = "textexFilters";
            this.textexFilters.Size = new System.Drawing.Size(360, 28);
            this.textexFilters.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textexFilters.TextChanged += new System.EventHandler(this.textexFilters_TextChanged);
            // 
            // buttonClearFilters
            // 
            this.buttonClearFilters.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonClearFilters.Image = global::TotalSmartCoding.Properties.Resources.Edit_clear;
            this.buttonClearFilters.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonClearFilters.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonClearFilters.Name = "buttonClearFilters";
            this.buttonClearFilters.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.buttonClearFilters.Size = new System.Drawing.Size(52, 52);
            this.buttonClearFilters.Text = "Clear current filters";
            this.buttonClearFilters.Click += new System.EventHandler(this.buttonClearFilters_Click);
            // 
            // WizardDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1386, 603);
            this.Controls.Add(this.panelMaster);
            this.Controls.Add(this.panelBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WizardDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select and add items";
            this.Load += new System.EventHandler(this.WizardDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fastPendingPallets)).EndInit();
            this.contextMenuDetails.ResumeLayout(false);
            this.panelMaster.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fastPendingCartons)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.panelBottomRight.ResumeLayout(false);
            this.panelBottomRight.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelBottomLeft.ResumeLayout(false);
            this.panelBottomLeft.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.FastObjectListView fastPendingPallets;
        private System.Windows.Forms.Panel panelMaster;
        private BrightIdeasSoftware.OLVColumn olvCommodityCode;
        private BrightIdeasSoftware.OLVColumn olvPalletCode;
        private BrightIdeasSoftware.OLVColumn olvPalletSelected;
        private BrightIdeasSoftware.OLVColumn olvCommodityName;
        private BrightIdeasSoftware.FastObjectListView fastPendingCartons;
        private BrightIdeasSoftware.OLVColumn olvCartonSelected;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvCartonCode;
        private BrightIdeasSoftware.OLVColumn olvColumn6;
        private BrightIdeasSoftware.OLVColumn olvPalletBinLocationCode;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Panel panelBottomRight;
        private System.Windows.Forms.Panel panelBottomLeft;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton buttonESC;
        private System.Windows.Forms.ToolStripButton buttonAddExit;
        private System.Windows.Forms.ToolStripButton buttonAdd;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripTextBox textexFilters;
        private System.Windows.Forms.ToolStripButton buttonClearFilters;
        private System.Windows.Forms.ContextMenuStrip contextMenuDetails;
        private System.Windows.Forms.ToolStripMenuItem menuOptionBinLocations;
        private BrightIdeasSoftware.OLVColumn olvPalletQuantityRemains;
        private BrightIdeasSoftware.OLVColumn olvPalletLineVolumeRemains;
        private BrightIdeasSoftware.OLVColumn olvCartonQuantityRemains;
        private BrightIdeasSoftware.OLVColumn olvCartonLineVolumeRemains;
    }
}