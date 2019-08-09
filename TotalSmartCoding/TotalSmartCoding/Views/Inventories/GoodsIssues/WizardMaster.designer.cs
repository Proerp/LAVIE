namespace TotalSmartCoding.Views.Inventories.GoodsIssues
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
            this.fastPendingDeliveryAdvices = new BrightIdeasSoftware.FastObjectListView();
            this.olvDeliveryAdviceEntryDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvDeliveryAdviceReference = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvSalesOrderReferences = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvVoucherCode = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvCustomerName1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.fastPendingDeliveryAdviceCustomers = new BrightIdeasSoftware.FastObjectListView();
            this.olvCustomerCode = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvCustomerName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.panelMaster = new System.Windows.Forms.Panel();
            this.fastPendingTransferOrders = new BrightIdeasSoftware.FastObjectListView();
            this.olvTransferOrderEntryDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvTransferOrderReference = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn7 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvTransferJobs = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.fastPendingTransferOrderWarehouses = new BrightIdeasSoftware.FastObjectListView();
            this.olvWarehouseCode = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvWarehouseName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvWarehouseReceiptCode = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvWarehouseReceiptName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastPendingDeliveryAdvices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastPendingDeliveryAdviceCustomers)).BeginInit();
            this.panelMaster.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastPendingTransferOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastPendingTransferOrderWarehouses)).BeginInit();
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
            this.buttonESC.Image = global::TotalSmartCoding.Properties.Resources.signout_icon_24;
            this.buttonESC.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonESC.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonESC.Name = "buttonESC";
            this.buttonESC.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonESC.Size = new System.Drawing.Size(81, 52);
            this.buttonESC.Text = "Cancel";
            this.buttonESC.Click += new System.EventHandler(this.buttonOKESC_Click);
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
            this.buttonOK.Click += new System.EventHandler(this.buttonOKESC_Click);
            // 
            // fastPendingDeliveryAdvices
            // 
            this.fastPendingDeliveryAdvices.AllColumns.Add(this.olvDeliveryAdviceEntryDate);
            this.fastPendingDeliveryAdvices.AllColumns.Add(this.olvDeliveryAdviceReference);
            this.fastPendingDeliveryAdvices.AllColumns.Add(this.olvSalesOrderReferences);
            this.fastPendingDeliveryAdvices.AllColumns.Add(this.olvVoucherCode);
            this.fastPendingDeliveryAdvices.AllColumns.Add(this.olvCustomerName1);
            this.fastPendingDeliveryAdvices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvDeliveryAdviceEntryDate,
            this.olvDeliveryAdviceReference,
            this.olvSalesOrderReferences,
            this.olvVoucherCode,
            this.olvCustomerName1});
            this.fastPendingDeliveryAdvices.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastPendingDeliveryAdvices.Dock = System.Windows.Forms.DockStyle.Top;
            this.fastPendingDeliveryAdvices.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fastPendingDeliveryAdvices.FullRowSelect = true;
            this.fastPendingDeliveryAdvices.HideSelection = false;
            this.fastPendingDeliveryAdvices.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastPendingDeliveryAdvices.Location = new System.Drawing.Point(0, 87);
            this.fastPendingDeliveryAdvices.Name = "fastPendingDeliveryAdvices";
            this.fastPendingDeliveryAdvices.OwnerDraw = true;
            this.fastPendingDeliveryAdvices.ShowGroups = false;
            this.fastPendingDeliveryAdvices.Size = new System.Drawing.Size(1147, 112);
            this.fastPendingDeliveryAdvices.TabIndex = 69;
            this.fastPendingDeliveryAdvices.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastPendingDeliveryAdvices.UseCompatibleStateImageBehavior = false;
            this.fastPendingDeliveryAdvices.UseFiltering = true;
            this.fastPendingDeliveryAdvices.View = System.Windows.Forms.View.Details;
            this.fastPendingDeliveryAdvices.VirtualMode = true;
            // 
            // olvDeliveryAdviceEntryDate
            // 
            this.olvDeliveryAdviceEntryDate.AspectName = "DeliveryAdviceEntryDate";
            this.olvDeliveryAdviceEntryDate.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvDeliveryAdviceEntryDate.Text = "D.A. Date";
            this.olvDeliveryAdviceEntryDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvDeliveryAdviceEntryDate.Width = 120;
            // 
            // olvDeliveryAdviceReference
            // 
            this.olvDeliveryAdviceReference.AspectName = "DeliveryAdviceReference";
            this.olvDeliveryAdviceReference.Text = "Reference";
            this.olvDeliveryAdviceReference.Width = 80;
            // 
            // olvSalesOrderReferences
            // 
            this.olvSalesOrderReferences.AspectName = "SalesOrderReferences";
            this.olvSalesOrderReferences.Text = "Sales Order";
            this.olvSalesOrderReferences.Width = 120;
            // 
            // olvVoucherCode
            // 
            this.olvVoucherCode.AspectName = "VoucherCode";
            this.olvVoucherCode.Text = "Voucher";
            this.olvVoucherCode.Width = 180;
            // 
            // olvCustomerName1
            // 
            this.olvCustomerName1.AspectName = "CustomerName";
            this.olvCustomerName1.FillsFreeSpace = true;
            this.olvCustomerName1.Text = "Customer";
            this.olvCustomerName1.Width = 192;
            // 
            // fastPendingDeliveryAdviceCustomers
            // 
            this.fastPendingDeliveryAdviceCustomers.AllColumns.Add(this.olvCustomerCode);
            this.fastPendingDeliveryAdviceCustomers.AllColumns.Add(this.olvCustomerName);
            this.fastPendingDeliveryAdviceCustomers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvCustomerCode,
            this.olvCustomerName});
            this.fastPendingDeliveryAdviceCustomers.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastPendingDeliveryAdviceCustomers.Dock = System.Windows.Forms.DockStyle.Top;
            this.fastPendingDeliveryAdviceCustomers.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fastPendingDeliveryAdviceCustomers.FullRowSelect = true;
            this.fastPendingDeliveryAdviceCustomers.HideSelection = false;
            this.fastPendingDeliveryAdviceCustomers.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastPendingDeliveryAdviceCustomers.Location = new System.Drawing.Point(0, 9);
            this.fastPendingDeliveryAdviceCustomers.Name = "fastPendingDeliveryAdviceCustomers";
            this.fastPendingDeliveryAdviceCustomers.OwnerDraw = true;
            this.fastPendingDeliveryAdviceCustomers.ShowGroups = false;
            this.fastPendingDeliveryAdviceCustomers.Size = new System.Drawing.Size(1147, 78);
            this.fastPendingDeliveryAdviceCustomers.TabIndex = 70;
            this.fastPendingDeliveryAdviceCustomers.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastPendingDeliveryAdviceCustomers.UseCompatibleStateImageBehavior = false;
            this.fastPendingDeliveryAdviceCustomers.UseFiltering = true;
            this.fastPendingDeliveryAdviceCustomers.View = System.Windows.Forms.View.Details;
            this.fastPendingDeliveryAdviceCustomers.VirtualMode = true;
            // 
            // olvCustomerCode
            // 
            this.olvCustomerCode.AspectName = "CustomerCode";
            this.olvCustomerCode.Text = "Customer Code";
            this.olvCustomerCode.Width = 145;
            // 
            // olvCustomerName
            // 
            this.olvCustomerName.AspectName = "CustomerName";
            this.olvCustomerName.FillsFreeSpace = true;
            this.olvCustomerName.Text = "Name";
            this.olvCustomerName.Width = 263;
            // 
            // panelMaster
            // 
            this.panelMaster.Controls.Add(this.fastPendingTransferOrders);
            this.panelMaster.Controls.Add(this.fastPendingTransferOrderWarehouses);
            this.panelMaster.Controls.Add(this.fastPendingDeliveryAdvices);
            this.panelMaster.Controls.Add(this.fastPendingDeliveryAdviceCustomers);
            this.panelMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMaster.Location = new System.Drawing.Point(0, 0);
            this.panelMaster.Name = "panelMaster";
            this.panelMaster.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
            this.panelMaster.Size = new System.Drawing.Size(1147, 548);
            this.panelMaster.TabIndex = 71;
            // 
            // fastPendingTransferOrders
            // 
            this.fastPendingTransferOrders.AllColumns.Add(this.olvTransferOrderEntryDate);
            this.fastPendingTransferOrders.AllColumns.Add(this.olvTransferOrderReference);
            this.fastPendingTransferOrders.AllColumns.Add(this.olvColumn7);
            this.fastPendingTransferOrders.AllColumns.Add(this.olvColumn6);
            this.fastPendingTransferOrders.AllColumns.Add(this.olvTransferJobs);
            this.fastPendingTransferOrders.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvTransferOrderEntryDate,
            this.olvTransferOrderReference,
            this.olvColumn7,
            this.olvColumn6,
            this.olvTransferJobs});
            this.fastPendingTransferOrders.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastPendingTransferOrders.Dock = System.Windows.Forms.DockStyle.Top;
            this.fastPendingTransferOrders.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fastPendingTransferOrders.FullRowSelect = true;
            this.fastPendingTransferOrders.HideSelection = false;
            this.fastPendingTransferOrders.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastPendingTransferOrders.Location = new System.Drawing.Point(0, 316);
            this.fastPendingTransferOrders.Name = "fastPendingTransferOrders";
            this.fastPendingTransferOrders.OwnerDraw = true;
            this.fastPendingTransferOrders.ShowGroups = false;
            this.fastPendingTransferOrders.Size = new System.Drawing.Size(1147, 112);
            this.fastPendingTransferOrders.TabIndex = 72;
            this.fastPendingTransferOrders.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastPendingTransferOrders.UseCompatibleStateImageBehavior = false;
            this.fastPendingTransferOrders.UseFiltering = true;
            this.fastPendingTransferOrders.View = System.Windows.Forms.View.Details;
            this.fastPendingTransferOrders.VirtualMode = true;
            // 
            // olvTransferOrderEntryDate
            // 
            this.olvTransferOrderEntryDate.AspectName = "TransferOrderEntryDate";
            this.olvTransferOrderEntryDate.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvTransferOrderEntryDate.Text = "Order Date";
            this.olvTransferOrderEntryDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvTransferOrderEntryDate.Width = 120;
            // 
            // olvTransferOrderReference
            // 
            this.olvTransferOrderReference.AspectName = "TransferOrderReference";
            this.olvTransferOrderReference.Text = "Reference";
            this.olvTransferOrderReference.Width = 80;
            // 
            // olvColumn7
            // 
            this.olvColumn7.AspectName = "WarehouseReceiptName";
            this.olvColumn7.DisplayIndex = 4;
            this.olvColumn7.FillsFreeSpace = true;
            this.olvColumn7.Text = "Destination";
            this.olvColumn7.Width = 192;
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "VoucherCode";
            this.olvColumn6.Text = "Voucher";
            this.olvColumn6.Width = 180;
            // 
            // olvTransferJobs
            // 
            this.olvTransferJobs.AspectName = "TransferJobs";
            this.olvTransferJobs.DisplayIndex = 2;
            this.olvTransferJobs.Text = "Jobs Description";
            this.olvTransferJobs.Width = 120;
            // 
            // fastPendingTransferOrderWarehouses
            // 
            this.fastPendingTransferOrderWarehouses.AllColumns.Add(this.olvWarehouseCode);
            this.fastPendingTransferOrderWarehouses.AllColumns.Add(this.olvWarehouseName);
            this.fastPendingTransferOrderWarehouses.AllColumns.Add(this.olvWarehouseReceiptCode);
            this.fastPendingTransferOrderWarehouses.AllColumns.Add(this.olvWarehouseReceiptName);
            this.fastPendingTransferOrderWarehouses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvWarehouseCode,
            this.olvWarehouseName,
            this.olvWarehouseReceiptCode,
            this.olvWarehouseReceiptName});
            this.fastPendingTransferOrderWarehouses.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastPendingTransferOrderWarehouses.Dock = System.Windows.Forms.DockStyle.Top;
            this.fastPendingTransferOrderWarehouses.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fastPendingTransferOrderWarehouses.FullRowSelect = true;
            this.fastPendingTransferOrderWarehouses.HideSelection = false;
            this.fastPendingTransferOrderWarehouses.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastPendingTransferOrderWarehouses.Location = new System.Drawing.Point(0, 199);
            this.fastPendingTransferOrderWarehouses.Name = "fastPendingTransferOrderWarehouses";
            this.fastPendingTransferOrderWarehouses.OwnerDraw = true;
            this.fastPendingTransferOrderWarehouses.ShowGroups = false;
            this.fastPendingTransferOrderWarehouses.Size = new System.Drawing.Size(1147, 117);
            this.fastPendingTransferOrderWarehouses.TabIndex = 71;
            this.fastPendingTransferOrderWarehouses.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastPendingTransferOrderWarehouses.UseCompatibleStateImageBehavior = false;
            this.fastPendingTransferOrderWarehouses.UseFiltering = true;
            this.fastPendingTransferOrderWarehouses.View = System.Windows.Forms.View.Details;
            this.fastPendingTransferOrderWarehouses.VirtualMode = true;
            // 
            // olvWarehouseCode
            // 
            this.olvWarehouseCode.AspectName = "WarehouseCode";
            this.olvWarehouseCode.Text = "Source Warehouse";
            // 
            // olvWarehouseName
            // 
            this.olvWarehouseName.AspectName = "WarehouseName";
            this.olvWarehouseName.Text = "Source Warehouse";
            // 
            // olvWarehouseReceiptCode
            // 
            this.olvWarehouseReceiptCode.AspectName = "WarehouseReceiptCode";
            this.olvWarehouseReceiptCode.Text = "Destination Code";
            this.olvWarehouseReceiptCode.Width = 145;
            // 
            // olvWarehouseReceiptName
            // 
            this.olvWarehouseReceiptName.AspectName = "WarehouseReceiptName";
            this.olvWarehouseReceiptName.FillsFreeSpace = true;
            this.olvWarehouseReceiptName.Text = "Destination Name";
            this.olvWarehouseReceiptName.Width = 255;
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
            this.Text = "Create Wizard [Add New Goods Issue]";
            this.Load += new System.EventHandler(this.Wizard_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastPendingDeliveryAdvices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastPendingDeliveryAdviceCustomers)).EndInit();
            this.panelMaster.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fastPendingTransferOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastPendingTransferOrderWarehouses)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton buttonESC;
        private System.Windows.Forms.ToolStripButton buttonOK;
        private BrightIdeasSoftware.FastObjectListView fastPendingDeliveryAdvices;
        private BrightIdeasSoftware.FastObjectListView fastPendingDeliveryAdviceCustomers;
        private System.Windows.Forms.Panel panelMaster;
        private BrightIdeasSoftware.OLVColumn olvDeliveryAdviceEntryDate;
        private BrightIdeasSoftware.OLVColumn olvCustomerName1;
        private BrightIdeasSoftware.OLVColumn olvCustomerCode;
        private BrightIdeasSoftware.OLVColumn olvCustomerName;
        private BrightIdeasSoftware.OLVColumn olvDeliveryAdviceReference;
        private BrightIdeasSoftware.OLVColumn olvSalesOrderReferences;
        private BrightIdeasSoftware.OLVColumn olvVoucherCode;
        private BrightIdeasSoftware.FastObjectListView fastPendingTransferOrders;
        private BrightIdeasSoftware.OLVColumn olvTransferOrderEntryDate;
        private BrightIdeasSoftware.OLVColumn olvTransferOrderReference;
        private BrightIdeasSoftware.OLVColumn olvTransferJobs;
        private BrightIdeasSoftware.OLVColumn olvColumn6;
        private BrightIdeasSoftware.OLVColumn olvColumn7;
        private BrightIdeasSoftware.FastObjectListView fastPendingTransferOrderWarehouses;
        private BrightIdeasSoftware.OLVColumn olvWarehouseReceiptCode;
        private BrightIdeasSoftware.OLVColumn olvWarehouseReceiptName;
        private BrightIdeasSoftware.OLVColumn olvWarehouseCode;
        private BrightIdeasSoftware.OLVColumn olvWarehouseName;
    }
}