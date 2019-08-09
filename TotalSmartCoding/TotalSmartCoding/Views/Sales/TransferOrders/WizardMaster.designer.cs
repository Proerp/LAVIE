namespace TotalSmartCoding.Views.Sales.TransferOrders
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
            this.components = new System.ComponentModel.Container();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonESC = new System.Windows.Forms.ToolStripButton();
            this.buttonOK = new System.Windows.Forms.ToolStripButton();
            this.errorProviderMaster = new System.Windows.Forms.ErrorProvider(this.components);
            this.layoutTop = new System.Windows.Forms.TableLayoutPanel();
            this.combexWarehouseID = new CustomControls.CombexBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textexTransferJobs = new CustomControls.TextexBox();
            this.textexRemarks = new CustomControls.TextexBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textexVoucherCode = new CustomControls.TextexBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dateTimexEntryDate = new CustomControls.DateTimexPicker();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.combexWarehouseReceiptID = new CustomControls.CombexBox();
            this.textexVehicle = new CustomControls.TextexBox();
            this.textexVehicleDriver = new CustomControls.TextexBox();
            this.combexTransferOrderTypeID = new CustomControls.CombexBox();
            this.combexTransferPackageTypeID = new CustomControls.CombexBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.combexForkliftDriverID = new CustomControls.CombexBox();
            this.combexStorekeeperID = new CustomControls.CombexBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderMaster)).BeginInit();
            this.layoutTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
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
            this.toolStrip1.Location = new System.Drawing.Point(0, 432);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(1029, 55);
            this.toolStrip1.TabIndex = 3;
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
            this.buttonOK.Enabled = false;
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
            // errorProviderMaster
            // 
            this.errorProviderMaster.ContainerControl = this;
            // 
            // layoutTop
            // 
            this.layoutTop.AutoSize = true;
            this.layoutTop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.layoutTop.ColumnCount = 5;
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.layoutTop.Controls.Add(this.combexWarehouseID, 3, 2);
            this.layoutTop.Controls.Add(this.label4, 2, 3);
            this.layoutTop.Controls.Add(this.label5, 2, 2);
            this.layoutTop.Controls.Add(this.textexTransferJobs, 3, 6);
            this.layoutTop.Controls.Add(this.textexRemarks, 3, 12);
            this.layoutTop.Controls.Add(this.label1, 2, 6);
            this.layoutTop.Controls.Add(this.label2, 2, 11);
            this.layoutTop.Controls.Add(this.label3, 2, 12);
            this.layoutTop.Controls.Add(this.label6, 2, 8);
            this.layoutTop.Controls.Add(this.textexVoucherCode, 3, 7);
            this.layoutTop.Controls.Add(this.label7, 2, 7);
            this.layoutTop.Controls.Add(this.label8, 2, 1);
            this.layoutTop.Controls.Add(this.dateTimexEntryDate, 3, 1);
            this.layoutTop.Controls.Add(this.pictureBox2, 1, 1);
            this.layoutTop.Controls.Add(this.combexWarehouseReceiptID, 3, 3);
            this.layoutTop.Controls.Add(this.textexVehicle, 3, 8);
            this.layoutTop.Controls.Add(this.textexVehicleDriver, 3, 9);
            this.layoutTop.Controls.Add(this.combexTransferOrderTypeID, 3, 4);
            this.layoutTop.Controls.Add(this.combexTransferPackageTypeID, 3, 5);
            this.layoutTop.Controls.Add(this.label9, 2, 4);
            this.layoutTop.Controls.Add(this.label10, 2, 5);
            this.layoutTop.Controls.Add(this.label11, 2, 9);
            this.layoutTop.Controls.Add(this.label12, 2, 10);
            this.layoutTop.Controls.Add(this.combexForkliftDriverID, 3, 10);
            this.layoutTop.Controls.Add(this.combexStorekeeperID, 3, 11);
            this.layoutTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutTop.Location = new System.Drawing.Point(0, 0);
            this.layoutTop.Margin = new System.Windows.Forms.Padding(0);
            this.layoutTop.Name = "layoutTop";
            this.layoutTop.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.layoutTop.RowCount = 14;
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.layoutTop.Size = new System.Drawing.Size(1029, 432);
            this.layoutTop.TabIndex = 98;
            // 
            // combexWarehouseID
            // 
            this.combexWarehouseID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.combexWarehouseID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combexWarehouseID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.combexWarehouseID.Editable = true;
            this.combexWarehouseID.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combexWarehouseID.FormattingEnabled = true;
            this.combexWarehouseID.Location = new System.Drawing.Point(315, 58);
            this.combexWarehouseID.Margin = new System.Windows.Forms.Padding(1);
            this.combexWarehouseID.Name = "combexWarehouseID";
            this.combexWarehouseID.ReadOnly = false;
            this.combexWarehouseID.Size = new System.Drawing.Size(688, 29);
            this.combexWarehouseID.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(87, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(224, 31);
            this.label4.TabIndex = 77;
            this.label4.Text = "Destination";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(87, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(224, 31);
            this.label5.TabIndex = 78;
            this.label5.Text = "Source Warehouse";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textexTransferJobs
            // 
            this.textexTransferJobs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexTransferJobs.Editable = true;
            this.textexTransferJobs.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textexTransferJobs.Location = new System.Drawing.Point(315, 182);
            this.textexTransferJobs.Margin = new System.Windows.Forms.Padding(1);
            this.textexTransferJobs.Name = "textexTransferJobs";
            this.textexTransferJobs.Size = new System.Drawing.Size(688, 28);
            this.textexTransferJobs.TabIndex = 6;
            // 
            // textexRemarks
            // 
            this.textexRemarks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexRemarks.Editable = true;
            this.textexRemarks.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textexRemarks.Location = new System.Drawing.Point(315, 364);
            this.textexRemarks.Margin = new System.Windows.Forms.Padding(1);
            this.textexRemarks.Name = "textexRemarks";
            this.textexRemarks.Size = new System.Drawing.Size(688, 28);
            this.textexRemarks.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(87, 181);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 30);
            this.label1.TabIndex = 82;
            this.label1.Text = "Jobs";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(87, 332);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(224, 31);
            this.label2.TabIndex = 83;
            this.label2.Text = "Prepared by";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(87, 363);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(224, 30);
            this.label3.TabIndex = 84;
            this.label3.Text = "Remarks";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(87, 241);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(224, 30);
            this.label6.TabIndex = 86;
            this.label6.Text = "Truck #";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textexVoucherCode
            // 
            this.textexVoucherCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexVoucherCode.Editable = true;
            this.textexVoucherCode.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textexVoucherCode.Location = new System.Drawing.Point(315, 212);
            this.textexVoucherCode.Margin = new System.Windows.Forms.Padding(1);
            this.textexVoucherCode.Name = "textexVoucherCode";
            this.textexVoucherCode.Size = new System.Drawing.Size(688, 28);
            this.textexVoucherCode.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(87, 211);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(224, 30);
            this.label7.TabIndex = 88;
            this.label7.Text = "Voucher #";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(87, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(224, 30);
            this.label8.TabIndex = 89;
            this.label8.Text = "Transfer Order Date";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dateTimexEntryDate
            // 
            this.dateTimexEntryDate.CustomFormat = "dd/MMM/yyyy HH:mm:ss";
            this.dateTimexEntryDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateTimexEntryDate.Editable = true;
            this.dateTimexEntryDate.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimexEntryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimexEntryDate.Location = new System.Drawing.Point(315, 28);
            this.dateTimexEntryDate.Margin = new System.Windows.Forms.Padding(1);
            this.dateTimexEntryDate.Name = "dateTimexEntryDate";
            this.dateTimexEntryDate.ReadOnly = false;
            this.dateTimexEntryDate.Size = new System.Drawing.Size(688, 28);
            this.dateTimexEntryDate.TabIndex = 1;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::TotalSmartCoding.Properties.Resources.Sign_Order_48;
            this.pictureBox2.Location = new System.Drawing.Point(33, 30);
            this.pictureBox2.Name = "pictureBox2";
            this.layoutTop.SetRowSpan(this.pictureBox2, 3);
            this.pictureBox2.Size = new System.Drawing.Size(48, 48);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 75;
            this.pictureBox2.TabStop = false;
            // 
            // combexWarehouseReceiptID
            // 
            this.combexWarehouseReceiptID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.combexWarehouseReceiptID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combexWarehouseReceiptID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.combexWarehouseReceiptID.Editable = true;
            this.combexWarehouseReceiptID.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combexWarehouseReceiptID.FormattingEnabled = true;
            this.combexWarehouseReceiptID.Location = new System.Drawing.Point(315, 89);
            this.combexWarehouseReceiptID.Margin = new System.Windows.Forms.Padding(1);
            this.combexWarehouseReceiptID.Name = "combexWarehouseReceiptID";
            this.combexWarehouseReceiptID.ReadOnly = false;
            this.combexWarehouseReceiptID.Size = new System.Drawing.Size(688, 29);
            this.combexWarehouseReceiptID.TabIndex = 3;
            // 
            // textexVehicle
            // 
            this.textexVehicle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexVehicle.Editable = true;
            this.textexVehicle.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textexVehicle.Location = new System.Drawing.Point(315, 242);
            this.textexVehicle.Margin = new System.Windows.Forms.Padding(1);
            this.textexVehicle.Name = "textexVehicle";
            this.textexVehicle.Size = new System.Drawing.Size(688, 28);
            this.textexVehicle.TabIndex = 8;
            // 
            // textexVehicleDriver
            // 
            this.textexVehicleDriver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexVehicleDriver.Editable = true;
            this.textexVehicleDriver.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textexVehicleDriver.Location = new System.Drawing.Point(315, 272);
            this.textexVehicleDriver.Margin = new System.Windows.Forms.Padding(1);
            this.textexVehicleDriver.Name = "textexVehicleDriver";
            this.textexVehicleDriver.Size = new System.Drawing.Size(688, 28);
            this.textexVehicleDriver.TabIndex = 9;
            // 
            // combexTransferOrderTypeID
            // 
            this.combexTransferOrderTypeID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.combexTransferOrderTypeID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combexTransferOrderTypeID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.combexTransferOrderTypeID.Editable = true;
            this.combexTransferOrderTypeID.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combexTransferOrderTypeID.FormattingEnabled = true;
            this.combexTransferOrderTypeID.Location = new System.Drawing.Point(315, 120);
            this.combexTransferOrderTypeID.Margin = new System.Windows.Forms.Padding(1);
            this.combexTransferOrderTypeID.Name = "combexTransferOrderTypeID";
            this.combexTransferOrderTypeID.ReadOnly = false;
            this.combexTransferOrderTypeID.Size = new System.Drawing.Size(688, 29);
            this.combexTransferOrderTypeID.TabIndex = 4;
            // 
            // combexTransferPackageTypeID
            // 
            this.combexTransferPackageTypeID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.combexTransferPackageTypeID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combexTransferPackageTypeID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.combexTransferPackageTypeID.Editable = true;
            this.combexTransferPackageTypeID.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combexTransferPackageTypeID.FormattingEnabled = true;
            this.combexTransferPackageTypeID.Location = new System.Drawing.Point(315, 151);
            this.combexTransferPackageTypeID.Margin = new System.Windows.Forms.Padding(1);
            this.combexTransferPackageTypeID.Name = "combexTransferPackageTypeID";
            this.combexTransferPackageTypeID.ReadOnly = false;
            this.combexTransferPackageTypeID.Size = new System.Drawing.Size(688, 29);
            this.combexTransferPackageTypeID.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(87, 119);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(224, 31);
            this.label9.TabIndex = 98;
            this.label9.Text = "Transfer Type";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(87, 150);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(224, 31);
            this.label10.TabIndex = 99;
            this.label10.Text = "Pallet Shipping";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(87, 271);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(224, 30);
            this.label11.TabIndex = 100;
            this.label11.Text = "Customer Attention";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(87, 301);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(224, 31);
            this.label12.TabIndex = 101;
            this.label12.Text = "Storekeeper";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // combexForkliftDriverID
            // 
            this.combexForkliftDriverID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.combexForkliftDriverID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combexForkliftDriverID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.combexForkliftDriverID.Editable = true;
            this.combexForkliftDriverID.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combexForkliftDriverID.FormattingEnabled = true;
            this.combexForkliftDriverID.Location = new System.Drawing.Point(315, 302);
            this.combexForkliftDriverID.Margin = new System.Windows.Forms.Padding(1);
            this.combexForkliftDriverID.Name = "combexForkliftDriverID";
            this.combexForkliftDriverID.ReadOnly = false;
            this.combexForkliftDriverID.Size = new System.Drawing.Size(688, 29);
            this.combexForkliftDriverID.TabIndex = 10;
            // 
            // combexStorekeeperID
            // 
            this.combexStorekeeperID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.combexStorekeeperID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combexStorekeeperID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.combexStorekeeperID.Editable = true;
            this.combexStorekeeperID.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combexStorekeeperID.FormattingEnabled = true;
            this.combexStorekeeperID.Location = new System.Drawing.Point(315, 333);
            this.combexStorekeeperID.Margin = new System.Windows.Forms.Padding(1);
            this.combexStorekeeperID.Name = "combexStorekeeperID";
            this.combexStorekeeperID.ReadOnly = false;
            this.combexStorekeeperID.Size = new System.Drawing.Size(688, 29);
            this.combexStorekeeperID.TabIndex = 11;
            // 
            // WizardMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 487);
            this.Controls.Add(this.layoutTop);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WizardMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Wizard [New Transfer Order]";
            this.Load += new System.EventHandler(this.WizardMaster_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderMaster)).EndInit();
            this.layoutTop.ResumeLayout(false);
            this.layoutTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton buttonESC;
        private System.Windows.Forms.ToolStripButton buttonOK;
        private System.Windows.Forms.ErrorProvider errorProviderMaster;
        private System.Windows.Forms.TableLayoutPanel layoutTop;
        private CustomControls.CombexBox combexWarehouseID;
        private CustomControls.TextexBox textexTransferJobs;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label5;
        private CustomControls.CombexBox combexStorekeeperID;
        private CustomControls.TextexBox textexRemarks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private CustomControls.TextexBox textexVoucherCode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private CustomControls.DateTimexPicker dateTimexEntryDate;
        private CustomControls.CombexBox combexWarehouseReceiptID;
        private CustomControls.CombexBox combexForkliftDriverID;
        private CustomControls.TextexBox textexVehicle;
        private CustomControls.TextexBox textexVehicleDriver;
        private CustomControls.CombexBox combexTransferOrderTypeID;
        private CustomControls.CombexBox combexTransferPackageTypeID;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
    }
}