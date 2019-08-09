namespace TotalSmartCoding.Views.Inventories.Pickups
{
    partial class Pickups
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pickups));
            this.tableLayoutMaster = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textexReference = new CustomControls.TextexBox();
            this.dateTimexEntryDate = new CustomControls.DateTimexPicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textexTotalLineVolume = new CustomControls.TextexBox();
            this.textexTotalQuantity = new CustomControls.TextexBox();
            this.textexTotalPalletCounts = new CustomControls.TextexBox();
            this.textexWarehouseCode = new CustomControls.TextexBox();
            this.combexForkliftDriverID = new CustomControls.CombexBox();
            this.combexStorekeeperID = new CustomControls.CombexBox();
            this.textexRemarks = new CustomControls.TextexBox();
            this.textexDescription = new CustomControls.TextexBox();
            this.toolStripChildForm = new System.Windows.Forms.ToolStrip();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.naviDetails = new Guifreaks.Navisuite.NaviGroup(this.components);
            this.toolStripNaviGroup = new System.Windows.Forms.ToolStrip();
            this.buttonRemoveDetailItem = new System.Windows.Forms.ToolStripButton();
            this.labelFillingLineName = new System.Windows.Forms.Label();
            this.gridexPalletDetails = new CustomControls.DataGridexView();
            this.CommodityCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PalletCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BinLocationCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LineVolume = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fastPickupIndex = new BrightIdeasSoftware.FastObjectListView();
            this.olvID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvEntryDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvFillingLineNickName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvPickupIndexReference = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvApproved = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageList32 = new System.Windows.Forms.ImageList(this.components);
            this.naviPendingItems = new Guifreaks.Navisuite.NaviBar(this.components);
            this.naviPendingPallets = new Guifreaks.Navisuite.NaviBand(this.components);
            this.fastPendingPallets = new BrightIdeasSoftware.FastObjectListView();
            this.olvIsSelected = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvPendingPalletCode = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.checkTimerEnable = new System.Windows.Forms.CheckBox();
            this.naviPickupIndex = new Guifreaks.Navisuite.NaviBand(this.components);
            this.naviIndex = new Guifreaks.Navisuite.NaviBar(this.components);
            this.panelMaster = new System.Windows.Forms.Panel();
            this.tableLayoutMaster.SuspendLayout();
            this.toolStripChildForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.naviDetails)).BeginInit();
            this.naviDetails.SuspendLayout();
            this.toolStripNaviGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridexPalletDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastPickupIndex)).BeginInit();
            this.naviPendingItems.SuspendLayout();
            this.naviPendingPallets.ClientArea.SuspendLayout();
            this.naviPendingPallets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastPendingPallets)).BeginInit();
            this.naviPickupIndex.ClientArea.SuspendLayout();
            this.naviPickupIndex.SuspendLayout();
            this.naviIndex.SuspendLayout();
            this.panelMaster.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutMaster
            // 
            this.tableLayoutMaster.AutoSize = true;
            this.tableLayoutMaster.BackColor = System.Drawing.Color.Ivory;
            this.tableLayoutMaster.ColumnCount = 6;
            this.tableLayoutMaster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutMaster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutMaster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 11F));
            this.tableLayoutMaster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutMaster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutMaster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 97F));
            this.tableLayoutMaster.Controls.Add(this.label8, 3, 4);
            this.tableLayoutMaster.Controls.Add(this.label5, 3, 3);
            this.tableLayoutMaster.Controls.Add(this.label2, 3, 2);
            this.tableLayoutMaster.Controls.Add(this.label4, 3, 1);
            this.tableLayoutMaster.Controls.Add(this.textexReference, 1, 2);
            this.tableLayoutMaster.Controls.Add(this.dateTimexEntryDate, 1, 1);
            this.tableLayoutMaster.Controls.Add(this.label3, 0, 4);
            this.tableLayoutMaster.Controls.Add(this.label10, 0, 3);
            this.tableLayoutMaster.Controls.Add(this.label7, 0, 2);
            this.tableLayoutMaster.Controls.Add(this.label6, 0, 1);
            this.tableLayoutMaster.Controls.Add(this.textexTotalLineVolume, 4, 4);
            this.tableLayoutMaster.Controls.Add(this.textexTotalQuantity, 4, 3);
            this.tableLayoutMaster.Controls.Add(this.textexTotalPalletCounts, 4, 2);
            this.tableLayoutMaster.Controls.Add(this.textexWarehouseCode, 4, 1);
            this.tableLayoutMaster.Controls.Add(this.combexForkliftDriverID, 1, 3);
            this.tableLayoutMaster.Controls.Add(this.combexStorekeeperID, 1, 4);
            this.tableLayoutMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutMaster.Location = new System.Drawing.Point(0, 45);
            this.tableLayoutMaster.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutMaster.Name = "tableLayoutMaster";
            this.tableLayoutMaster.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.tableLayoutMaster.RowCount = 6;
            this.tableLayoutMaster.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 7F));
            this.tableLayoutMaster.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutMaster.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutMaster.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutMaster.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutMaster.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutMaster.Size = new System.Drawing.Size(828, 197);
            this.tableLayoutMaster.TabIndex = 62;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(372, 101);
            this.label8.Margin = new System.Windows.Forms.Padding(1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(142, 28);
            this.label8.TabIndex = 78;
            this.label8.Text = "Total Volume";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(372, 71);
            this.label5.Margin = new System.Windows.Forms.Padding(1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(142, 28);
            this.label5.TabIndex = 77;
            this.label5.Text = "Total Quantity";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(374, 44);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 22);
            this.label2.TabIndex = 56;
            this.label2.Text = "Pallet Count";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(372, 11);
            this.label4.Margin = new System.Windows.Forms.Padding(1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 28);
            this.label4.TabIndex = 28;
            this.label4.Text = "Warehouse";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textexReference
            // 
            this.textexReference.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexReference.Editable = false;
            this.textexReference.Location = new System.Drawing.Point(147, 41);
            this.textexReference.Margin = new System.Windows.Forms.Padding(3, 1, 1, 1);
            this.textexReference.Name = "textexReference";
            this.textexReference.Size = new System.Drawing.Size(212, 28);
            this.textexReference.TabIndex = 71;
            // 
            // dateTimexEntryDate
            // 
            this.dateTimexEntryDate.CustomFormat = "dd/MMM/yyyy HH:mm:ss";
            this.dateTimexEntryDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateTimexEntryDate.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.dateTimexEntryDate.Editable = false;
            this.dateTimexEntryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimexEntryDate.Location = new System.Drawing.Point(147, 11);
            this.dateTimexEntryDate.Margin = new System.Windows.Forms.Padding(3, 1, 1, 1);
            this.dateTimexEntryDate.Name = "dateTimexEntryDate";
            this.dateTimexEntryDate.ReadOnly = false;
            this.dateTimexEntryDate.Size = new System.Drawing.Size(212, 28);
            this.dateTimexEntryDate.TabIndex = 70;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 104);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 22);
            this.label3.TabIndex = 76;
            this.label3.Text = "Store Keeper";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(3, 74);
            this.label10.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(138, 22);
            this.label10.TabIndex = 35;
            this.label10.Text = "Forklift Driver";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(3, 44);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 22);
            this.label7.TabIndex = 31;
            this.label7.Text = "Reference";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(1, 11);
            this.label6.Margin = new System.Windows.Forms.Padding(1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(142, 28);
            this.label6.TabIndex = 30;
            this.label6.Text = "Date";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textexTotalLineVolume
            // 
            this.textexTotalLineVolume.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexTotalLineVolume.Editable = false;
            this.textexTotalLineVolume.Location = new System.Drawing.Point(518, 101);
            this.textexTotalLineVolume.Margin = new System.Windows.Forms.Padding(3, 1, 1, 1);
            this.textexTotalLineVolume.Name = "textexTotalLineVolume";
            this.textexTotalLineVolume.Size = new System.Drawing.Size(212, 28);
            this.textexTotalLineVolume.TabIndex = 82;
            this.textexTotalLineVolume.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textexTotalQuantity
            // 
            this.textexTotalQuantity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexTotalQuantity.Editable = false;
            this.textexTotalQuantity.Location = new System.Drawing.Point(518, 71);
            this.textexTotalQuantity.Margin = new System.Windows.Forms.Padding(3, 1, 1, 1);
            this.textexTotalQuantity.Name = "textexTotalQuantity";
            this.textexTotalQuantity.Size = new System.Drawing.Size(212, 28);
            this.textexTotalQuantity.TabIndex = 81;
            this.textexTotalQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textexTotalPalletCounts
            // 
            this.textexTotalPalletCounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexTotalPalletCounts.Editable = false;
            this.textexTotalPalletCounts.Location = new System.Drawing.Point(518, 41);
            this.textexTotalPalletCounts.Margin = new System.Windows.Forms.Padding(3, 1, 1, 1);
            this.textexTotalPalletCounts.Name = "textexTotalPalletCounts";
            this.textexTotalPalletCounts.Size = new System.Drawing.Size(212, 28);
            this.textexTotalPalletCounts.TabIndex = 80;
            this.textexTotalPalletCounts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textexWarehouseCode
            // 
            this.textexWarehouseCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexWarehouseCode.Editable = false;
            this.textexWarehouseCode.Location = new System.Drawing.Point(518, 11);
            this.textexWarehouseCode.Margin = new System.Windows.Forms.Padding(3, 1, 1, 1);
            this.textexWarehouseCode.Name = "textexWarehouseCode";
            this.textexWarehouseCode.Size = new System.Drawing.Size(212, 28);
            this.textexWarehouseCode.TabIndex = 85;
            // 
            // combexForkliftDriverID
            // 
            this.combexForkliftDriverID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.combexForkliftDriverID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combexForkliftDriverID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.combexForkliftDriverID.Editable = true;
            this.combexForkliftDriverID.FormattingEnabled = true;
            this.combexForkliftDriverID.Location = new System.Drawing.Point(147, 71);
            this.combexForkliftDriverID.Margin = new System.Windows.Forms.Padding(3, 1, 1, 1);
            this.combexForkliftDriverID.Name = "combexForkliftDriverID";
            this.combexForkliftDriverID.ReadOnly = false;
            this.combexForkliftDriverID.Size = new System.Drawing.Size(212, 29);
            this.combexForkliftDriverID.TabIndex = 73;
            // 
            // combexStorekeeperID
            // 
            this.combexStorekeeperID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.combexStorekeeperID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combexStorekeeperID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.combexStorekeeperID.Editable = true;
            this.combexStorekeeperID.FormattingEnabled = true;
            this.combexStorekeeperID.Location = new System.Drawing.Point(147, 101);
            this.combexStorekeeperID.Margin = new System.Windows.Forms.Padding(3, 1, 1, 1);
            this.combexStorekeeperID.Name = "combexStorekeeperID";
            this.combexStorekeeperID.ReadOnly = false;
            this.combexStorekeeperID.Size = new System.Drawing.Size(212, 29);
            this.combexStorekeeperID.TabIndex = 74;
            // 
            // textexRemarks
            // 
            this.textexRemarks.BackColor = System.Drawing.SystemColors.Control;
            this.textexRemarks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textexRemarks.Dock = System.Windows.Forms.DockStyle.Right;
            this.textexRemarks.Editable = true;
            this.textexRemarks.Location = new System.Drawing.Point(675, 4);
            this.textexRemarks.Margin = new System.Windows.Forms.Padding(3, 1, 1, 1);
            this.textexRemarks.Multiline = true;
            this.textexRemarks.Name = "textexRemarks";
            this.textexRemarks.Size = new System.Drawing.Size(153, 612);
            this.textexRemarks.TabIndex = 84;
            // 
            // textexDescription
            // 
            this.textexDescription.BackColor = System.Drawing.SystemColors.Control;
            this.textexDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textexDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexDescription.Editable = true;
            this.textexDescription.Location = new System.Drawing.Point(631, 4);
            this.textexDescription.Margin = new System.Windows.Forms.Padding(3, 1, 1, 1);
            this.textexDescription.Multiline = true;
            this.textexDescription.Name = "textexDescription";
            this.textexDescription.Size = new System.Drawing.Size(44, 612);
            this.textexDescription.TabIndex = 83;
            // 
            // toolStripChildForm
            // 
            this.toolStripChildForm.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStripChildForm.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripChildForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton2});
            this.toolStripChildForm.Location = new System.Drawing.Point(0, 0);
            this.toolStripChildForm.Name = "toolStripChildForm";
            this.toolStripChildForm.Size = new System.Drawing.Size(1851, 51);
            this.toolStripChildForm.TabIndex = 29;
            this.toolStripChildForm.Text = "toolStrip1";
            this.toolStripChildForm.Visible = false;
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = global::TotalSmartCoding.Properties.Resources._20130106011449193_easyicon_cn_32;
            this.toolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(118, 48);
            this.toolStripButton2.Text = "Disconnect";
            this.toolStripButton2.Visible = false;
            // 
            // naviDetails
            // 
            this.naviDetails.Caption = "   Pickup for";
            this.naviDetails.Controls.Add(this.toolStripNaviGroup);
            this.naviDetails.Controls.Add(this.labelFillingLineName);
            this.naviDetails.Controls.Add(this.tableLayoutMaster);
            this.naviDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.naviDetails.ExpandedHeight = 242;
            this.naviDetails.HeaderContextMenuStrip = null;
            this.naviDetails.HeaderHeight = 43;
            this.naviDetails.Location = new System.Drawing.Point(158, 0);
            this.naviDetails.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.naviDetails.Name = "naviDetails";
            this.naviDetails.Padding = new System.Windows.Forms.Padding(0, 45, 0, 0);
            this.naviDetails.Size = new System.Drawing.Size(828, 242);
            this.naviDetails.TabIndex = 30;
            // 
            // toolStripNaviGroup
            // 
            this.toolStripNaviGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStripNaviGroup.BackColor = System.Drawing.Color.Transparent;
            this.toolStripNaviGroup.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripNaviGroup.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripNaviGroup.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStripNaviGroup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonRemoveDetailItem});
            this.toolStripNaviGroup.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStripNaviGroup.Location = new System.Drawing.Point(768, 0);
            this.toolStripNaviGroup.Name = "toolStripNaviGroup";
            this.toolStripNaviGroup.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStripNaviGroup.Size = new System.Drawing.Size(39, 39);
            this.toolStripNaviGroup.TabIndex = 75;
            this.toolStripNaviGroup.Visible = false;
            // 
            // buttonRemoveDetailItem
            // 
            this.buttonRemoveDetailItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonRemoveDetailItem.Image = global::TotalSmartCoding.Properties.Resources.Red_cross;
            this.buttonRemoveDetailItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonRemoveDetailItem.Name = "buttonRemoveDetailItem";
            this.buttonRemoveDetailItem.Size = new System.Drawing.Size(36, 36);
            this.buttonRemoveDetailItem.ToolTipText = "Xóa pallet đang chọn";
            this.buttonRemoveDetailItem.Click += new System.EventHandler(this.buttonRemoveDetailItem_Click);
            // 
            // labelFillingLineName
            // 
            this.labelFillingLineName.AutoSize = true;
            this.labelFillingLineName.BackColor = System.Drawing.Color.Transparent;
            this.labelFillingLineName.ForeColor = System.Drawing.Color.Firebrick;
            this.labelFillingLineName.Location = new System.Drawing.Point(173, 0);
            this.labelFillingLineName.Name = "labelFillingLineName";
            this.labelFillingLineName.Size = new System.Drawing.Size(88, 21);
            this.labelFillingLineName.TabIndex = 63;
            this.labelFillingLineName.Text = "DRUM LINE";
            // 
            // gridexPalletDetails
            // 
            this.gridexPalletDetails.AllowAddRow = false;
            this.gridexPalletDetails.AllowDeleteRow = true;
            this.gridexPalletDetails.BackgroundColor = System.Drawing.Color.Ivory;
            this.gridexPalletDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridexPalletDetails.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridexPalletDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridexPalletDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridexPalletDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CommodityCode,
            this.PalletCode,
            this.BinLocationCode,
            this.Quantity,
            this.LineVolume});
            this.gridexPalletDetails.Dock = System.Windows.Forms.DockStyle.Left;
            this.gridexPalletDetails.Editable = false;
            this.gridexPalletDetails.GridColor = System.Drawing.SystemColors.ControlLight;
            this.gridexPalletDetails.Location = new System.Drawing.Point(0, 4);
            this.gridexPalletDetails.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridexPalletDetails.Name = "gridexPalletDetails";
            this.gridexPalletDetails.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gridexPalletDetails.RowTemplate.Height = 39;
            this.gridexPalletDetails.Size = new System.Drawing.Size(631, 612);
            this.gridexPalletDetails.TabIndex = 65;
            this.gridexPalletDetails.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.gridexPalletDetails_UserDeletedRow);
            // 
            // CommodityCode
            // 
            this.CommodityCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CommodityCode.DataPropertyName = "CommodityCode";
            this.CommodityCode.FillWeight = 17F;
            this.CommodityCode.HeaderText = "Item";
            this.CommodityCode.MinimumWidth = 9;
            this.CommodityCode.Name = "CommodityCode";
            // 
            // PalletCode
            // 
            this.PalletCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PalletCode.DataPropertyName = "PalletCode";
            this.PalletCode.FillWeight = 50F;
            this.PalletCode.HeaderText = "Pallet Code";
            this.PalletCode.MinimumWidth = 9;
            this.PalletCode.Name = "PalletCode";
            // 
            // BinLocationCode
            // 
            this.BinLocationCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BinLocationCode.DataPropertyName = "BinLocationCode";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.BinLocationCode.DefaultCellStyle = dataGridViewCellStyle2;
            this.BinLocationCode.FillWeight = 15F;
            this.BinLocationCode.HeaderText = "Location";
            this.BinLocationCode.MinimumWidth = 9;
            this.BinLocationCode.Name = "BinLocationCode";
            // 
            // Quantity
            // 
            this.Quantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Quantity.DataPropertyName = "Quantity";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Quantity.DefaultCellStyle = dataGridViewCellStyle3;
            this.Quantity.FillWeight = 7F;
            this.Quantity.HeaderText = "Qty";
            this.Quantity.MinimumWidth = 9;
            this.Quantity.Name = "Quantity";
            // 
            // LineVolume
            // 
            this.LineVolume.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LineVolume.DataPropertyName = "LineVolume";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.LineVolume.DefaultCellStyle = dataGridViewCellStyle4;
            this.LineVolume.FillWeight = 15F;
            this.LineVolume.HeaderText = "Volume";
            this.LineVolume.MinimumWidth = 9;
            this.LineVolume.Name = "LineVolume";
            // 
            // fastPickupIndex
            // 
            this.fastPickupIndex.AllColumns.Add(this.olvID);
            this.fastPickupIndex.AllColumns.Add(this.olvEntryDate);
            this.fastPickupIndex.AllColumns.Add(this.olvFillingLineNickName);
            this.fastPickupIndex.AllColumns.Add(this.olvPickupIndexReference);
            this.fastPickupIndex.AllColumns.Add(this.olvApproved);
            this.fastPickupIndex.BackColor = System.Drawing.Color.Ivory;
            this.fastPickupIndex.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvID,
            this.olvFillingLineNickName,
            this.olvPickupIndexReference,
            this.olvApproved});
            this.fastPickupIndex.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastPickupIndex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastPickupIndex.FullRowSelect = true;
            this.fastPickupIndex.GroupImageList = this.imageList32;
            this.fastPickupIndex.HideSelection = false;
            this.fastPickupIndex.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastPickupIndex.Location = new System.Drawing.Point(0, 0);
            this.fastPickupIndex.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fastPickupIndex.Name = "fastPickupIndex";
            this.fastPickupIndex.OwnerDraw = true;
            this.fastPickupIndex.RowHeight = 39;
            this.fastPickupIndex.ShowGroups = false;
            this.fastPickupIndex.Size = new System.Drawing.Size(156, 776);
            this.fastPickupIndex.TabIndex = 68;
            this.fastPickupIndex.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastPickupIndex.UseCompatibleStateImageBehavior = false;
            this.fastPickupIndex.UseFiltering = true;
            this.fastPickupIndex.View = System.Windows.Forms.View.Details;
            this.fastPickupIndex.VirtualMode = true;
            // 
            // olvID
            // 
            this.olvID.Groupable = false;
            this.olvID.Text = "";
            this.olvID.Width = 0;
            // 
            // olvEntryDate
            // 
            this.olvEntryDate.AspectName = "EntryDate";
            this.olvEntryDate.AspectToStringFormat = "{0:d}";
            this.olvEntryDate.DisplayIndex = 1;
            this.olvEntryDate.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvEntryDate.IsVisible = false;
            this.olvEntryDate.Text = "Date";
            this.olvEntryDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvEntryDate.Width = 10;
            // 
            // olvFillingLineNickName
            // 
            this.olvFillingLineNickName.AspectName = "FillingLineNickName";
            this.olvFillingLineNickName.Groupable = false;
            this.olvFillingLineNickName.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvFillingLineNickName.Text = "";
            this.olvFillingLineNickName.Width = 35;
            // 
            // olvPickupIndexReference
            // 
            this.olvPickupIndexReference.AspectName = "Reference";
            this.olvPickupIndexReference.Groupable = false;
            this.olvPickupIndexReference.HeaderForeColor = System.Drawing.Color.Firebrick;
            this.olvPickupIndexReference.Text = "Pickup";
            // 
            // olvApproved
            // 
            this.olvApproved.AspectName = "Approved";
            this.olvApproved.FillsFreeSpace = true;
            this.olvApproved.Groupable = false;
            this.olvApproved.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvApproved.Text = "";
            this.olvApproved.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvApproved.Width = 24;
            // 
            // imageList32
            // 
            this.imageList32.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList32.ImageStream")));
            this.imageList32.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList32.Images.SetKeyName(0, "Forklift");
            this.imageList32.Images.SetKeyName(1, "ForkliftYellow");
            this.imageList32.Images.SetKeyName(2, "ForkliftOrange");
            this.imageList32.Images.SetKeyName(3, "ForkliftJapan");
            this.imageList32.Images.SetKeyName(4, "Placeholder32");
            // 
            // naviPendingItems
            // 
            this.naviPendingItems.ActiveBand = this.naviPendingPallets;
            this.naviPendingItems.Controls.Add(this.checkTimerEnable);
            this.naviPendingItems.Controls.Add(this.naviPendingPallets);
            this.naviPendingItems.Dock = System.Windows.Forms.DockStyle.Right;
            this.naviPendingItems.HeaderHeight = 42;
            this.naviPendingItems.Location = new System.Drawing.Point(986, 0);
            this.naviPendingItems.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.naviPendingItems.Name = "naviPendingItems";
            this.naviPendingItems.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.naviPendingItems.Size = new System.Drawing.Size(288, 858);
            this.naviPendingItems.TabIndex = 69;
            this.naviPendingItems.Text = "naviBar1";
            // 
            // naviPendingPallets
            // 
            // 
            // naviPendingPallets.ClientArea
            // 
            this.naviPendingPallets.ClientArea.Controls.Add(this.fastPendingPallets);
            this.naviPendingPallets.ClientArea.LayoutStyle = Guifreaks.Navisuite.NaviLayoutStyle.StyleFromOwner;
            this.naviPendingPallets.ClientArea.Location = new System.Drawing.Point(0, 0);
            this.naviPendingPallets.ClientArea.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.naviPendingPallets.ClientArea.Name = "ClientArea";
            this.naviPendingPallets.ClientArea.Size = new System.Drawing.Size(286, 776);
            this.naviPendingPallets.ClientArea.TabIndex = 0;
            this.naviPendingPallets.LargeImageIndex = 0;
            this.naviPendingPallets.LayoutStyle = Guifreaks.Navisuite.NaviLayoutStyle.StyleFromOwner;
            this.naviPendingPallets.Location = new System.Drawing.Point(1, 42);
            this.naviPendingPallets.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.naviPendingPallets.Name = "naviPendingPallets";
            this.naviPendingPallets.Size = new System.Drawing.Size(286, 776);
            this.naviPendingPallets.SmallImageIndex = 0;
            this.naviPendingPallets.TabIndex = 3;
            // 
            // fastPendingPallets
            // 
            this.fastPendingPallets.AllColumns.Add(this.olvIsSelected);
            this.fastPendingPallets.AllColumns.Add(this.olvPendingPalletCode);
            this.fastPendingPallets.BackColor = System.Drawing.Color.Ivory;
            this.fastPendingPallets.CheckBoxes = true;
            this.fastPendingPallets.CheckedAspectName = "IsSelected";
            this.fastPendingPallets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvIsSelected,
            this.olvPendingPalletCode});
            this.fastPendingPallets.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastPendingPallets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastPendingPallets.FullRowSelect = true;
            this.fastPendingPallets.HideSelection = false;
            this.fastPendingPallets.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastPendingPallets.Location = new System.Drawing.Point(0, 0);
            this.fastPendingPallets.Name = "fastPendingPallets";
            this.fastPendingPallets.OwnerDraw = true;
            this.fastPendingPallets.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fastPendingPallets.RowHeight = 45;
            this.fastPendingPallets.ShowGroups = false;
            this.fastPendingPallets.ShowImagesOnSubItems = true;
            this.fastPendingPallets.Size = new System.Drawing.Size(286, 776);
            this.fastPendingPallets.TabIndex = 70;
            this.fastPendingPallets.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastPendingPallets.UseCompatibleStateImageBehavior = false;
            this.fastPendingPallets.UseFiltering = true;
            this.fastPendingPallets.View = System.Windows.Forms.View.Details;
            this.fastPendingPallets.VirtualMode = true;
            this.fastPendingPallets.MouseClick += new System.Windows.Forms.MouseEventHandler(this.fastPendingPallets_MouseClick);
            // 
            // olvIsSelected
            // 
            this.olvIsSelected.AspectName = "";
            this.olvIsSelected.HeaderCheckBox = true;
            this.olvIsSelected.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvIsSelected.Sortable = false;
            this.olvIsSelected.Text = "";
            this.olvIsSelected.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvIsSelected.Width = 20;
            // 
            // olvPendingPalletCode
            // 
            this.olvPendingPalletCode.AspectName = "Code";
            this.olvPendingPalletCode.FillsFreeSpace = true;
            this.olvPendingPalletCode.HeaderForeColor = System.Drawing.Color.Firebrick;
            this.olvPendingPalletCode.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvPendingPalletCode.Sortable = false;
            this.olvPendingPalletCode.Text = "                         Pending Pallet";
            this.olvPendingPalletCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvPendingPalletCode.Width = 400;
            // 
            // checkTimerEnable
            // 
            this.checkTimerEnable.AutoSize = true;
            this.checkTimerEnable.BackColor = System.Drawing.Color.Transparent;
            this.checkTimerEnable.Location = new System.Drawing.Point(48, 12);
            this.checkTimerEnable.Name = "checkTimerEnable";
            this.checkTimerEnable.Size = new System.Drawing.Size(228, 25);
            this.checkTimerEnable.TabIndex = 8;
            this.checkTimerEnable.Text = "Stop checking for new pallet";
            this.checkTimerEnable.UseVisualStyleBackColor = false;
            // 
            // naviPickupIndex
            // 
            // 
            // naviPickupIndex.ClientArea
            // 
            this.naviPickupIndex.ClientArea.Controls.Add(this.fastPickupIndex);
            this.naviPickupIndex.ClientArea.LayoutStyle = Guifreaks.Navisuite.NaviLayoutStyle.StyleFromOwner;
            this.naviPickupIndex.ClientArea.Location = new System.Drawing.Point(0, 0);
            this.naviPickupIndex.ClientArea.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.naviPickupIndex.ClientArea.Name = "ClientArea";
            this.naviPickupIndex.ClientArea.Size = new System.Drawing.Size(156, 776);
            this.naviPickupIndex.ClientArea.TabIndex = 0;
            this.naviPickupIndex.LargeImageIndex = 0;
            this.naviPickupIndex.LayoutStyle = Guifreaks.Navisuite.NaviLayoutStyle.StyleFromOwner;
            this.naviPickupIndex.Location = new System.Drawing.Point(1, 42);
            this.naviPickupIndex.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.naviPickupIndex.Name = "naviPickupIndex";
            this.naviPickupIndex.Size = new System.Drawing.Size(156, 776);
            this.naviPickupIndex.SmallImageIndex = 0;
            this.naviPickupIndex.TabIndex = 72;
            // 
            // naviIndex
            // 
            this.naviIndex.ActiveBand = this.naviPickupIndex;
            this.naviIndex.Controls.Add(this.naviPickupIndex);
            this.naviIndex.Dock = System.Windows.Forms.DockStyle.Left;
            this.naviIndex.HeaderHeight = 42;
            this.naviIndex.Location = new System.Drawing.Point(0, 0);
            this.naviIndex.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.naviIndex.Name = "naviIndex";
            this.naviIndex.Size = new System.Drawing.Size(158, 858);
            this.naviIndex.TabIndex = 71;
            this.naviIndex.Text = "naviBar2";
            // 
            // panelMaster
            // 
            this.panelMaster.BackColor = System.Drawing.Color.Ivory;
            this.panelMaster.Controls.Add(this.textexDescription);
            this.panelMaster.Controls.Add(this.textexRemarks);
            this.panelMaster.Controls.Add(this.gridexPalletDetails);
            this.panelMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMaster.Location = new System.Drawing.Point(158, 242);
            this.panelMaster.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelMaster.Name = "panelMaster";
            this.panelMaster.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.panelMaster.Size = new System.Drawing.Size(828, 616);
            this.panelMaster.TabIndex = 72;
            // 
            // Pickups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1274, 858);
            this.Controls.Add(this.panelMaster);
            this.Controls.Add(this.naviDetails);
            this.Controls.Add(this.naviIndex);
            this.Controls.Add(this.naviPendingItems);
            this.Controls.Add(this.toolStripChildForm);
            this.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Pickups";
            this.Text = "Pickups";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Pickups_FormClosing);
            this.Controls.SetChildIndex(this.toolStripChildForm, 0);
            this.Controls.SetChildIndex(this.naviPendingItems, 0);
            this.Controls.SetChildIndex(this.naviIndex, 0);
            this.Controls.SetChildIndex(this.naviDetails, 0);
            this.Controls.SetChildIndex(this.panelMaster, 0);
            this.tableLayoutMaster.ResumeLayout(false);
            this.tableLayoutMaster.PerformLayout();
            this.toolStripChildForm.ResumeLayout(false);
            this.toolStripChildForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.naviDetails)).EndInit();
            this.naviDetails.ResumeLayout(false);
            this.naviDetails.PerformLayout();
            this.toolStripNaviGroup.ResumeLayout(false);
            this.toolStripNaviGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridexPalletDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastPickupIndex)).EndInit();
            this.naviPendingItems.ResumeLayout(false);
            this.naviPendingItems.PerformLayout();
            this.naviPendingPallets.ClientArea.ResumeLayout(false);
            this.naviPendingPallets.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fastPendingPallets)).EndInit();
            this.naviPickupIndex.ClientArea.ResumeLayout(false);
            this.naviPickupIndex.ResumeLayout(false);
            this.naviIndex.ResumeLayout(false);
            this.panelMaster.ResumeLayout(false);
            this.panelMaster.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutMaster;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private Guifreaks.Navisuite.NaviGroup naviDetails;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolStrip toolStripChildForm;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private CustomControls.DateTimexPicker dateTimexEntryDate;
        private BrightIdeasSoftware.FastObjectListView fastPickupIndex;
        private BrightIdeasSoftware.OLVColumn olvID;
        private BrightIdeasSoftware.OLVColumn olvEntryDate;
        private CustomControls.DataGridexView gridexPalletDetails;
        private Guifreaks.Navisuite.NaviBar naviPendingItems;
        private Guifreaks.Navisuite.NaviBar naviIndex;
        private System.Windows.Forms.Panel panelMaster;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private CustomControls.TextexBox textexReference;
        private CustomControls.CombexBox combexForkliftDriverID;
        private CustomControls.CombexBox combexStorekeeperID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private CustomControls.TextexBox textexTotalPalletCounts;
        private CustomControls.TextexBox textexTotalQuantity;
        private CustomControls.TextexBox textexTotalLineVolume;
        private CustomControls.TextexBox textexDescription;
        private CustomControls.TextexBox textexRemarks;
        private Guifreaks.Navisuite.NaviBand naviPickupIndex;
        private Guifreaks.Navisuite.NaviBand naviPendingPallets;
        private CustomControls.TextexBox textexWarehouseCode;
        private System.Windows.Forms.Label labelFillingLineName;
        private BrightIdeasSoftware.OLVColumn olvPickupIndexReference;
        private System.Windows.Forms.ImageList imageList32;
        private BrightIdeasSoftware.OLVColumn olvApproved;
        private BrightIdeasSoftware.OLVColumn olvFillingLineNickName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CommodityCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn PalletCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn BinLocationCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn LineVolume;
        private BrightIdeasSoftware.FastObjectListView fastPendingPallets;
        private BrightIdeasSoftware.OLVColumn olvPendingPalletCode;
        private System.Windows.Forms.ToolStrip toolStripNaviGroup;
        private System.Windows.Forms.ToolStripButton buttonRemoveDetailItem;
        private BrightIdeasSoftware.OLVColumn olvIsSelected;
        private System.Windows.Forms.CheckBox checkTimerEnable;

    }
}