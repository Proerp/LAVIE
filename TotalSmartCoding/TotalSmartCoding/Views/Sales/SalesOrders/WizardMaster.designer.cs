namespace TotalSmartCoding.Views.Sales.SalesOrders
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
            this.combexCustomerID = new CustomControls.CombexBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textexShippingAddress = new CustomControls.TextexBox();
            this.textexContactInfo = new CustomControls.TextexBox();
            this.combexSalespersonID = new CustomControls.CombexBox();
            this.textexRemarks = new CustomControls.TextexBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimexDeliveryDate = new CustomControls.DateTimexPicker();
            this.label6 = new System.Windows.Forms.Label();
            this.textexVoucherCode = new CustomControls.TextexBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dateTimexEntryDate = new CustomControls.DateTimexPicker();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.combexReceiverID = new CustomControls.CombexBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textexCustomerName = new CustomControls.TextexBox();
            this.textexReceiverName = new CustomControls.TextexBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
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
            this.toolStrip1.Location = new System.Drawing.Point(0, 381);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(955, 55);
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
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.layoutTop.Controls.Add(this.combexCustomerID, 3, 2);
            this.layoutTop.Controls.Add(this.label4, 2, 6);
            this.layoutTop.Controls.Add(this.label5, 2, 2);
            this.layoutTop.Controls.Add(this.textexShippingAddress, 3, 7);
            this.layoutTop.Controls.Add(this.textexContactInfo, 3, 6);
            this.layoutTop.Controls.Add(this.combexSalespersonID, 3, 10);
            this.layoutTop.Controls.Add(this.textexRemarks, 3, 11);
            this.layoutTop.Controls.Add(this.label1, 2, 7);
            this.layoutTop.Controls.Add(this.label2, 2, 10);
            this.layoutTop.Controls.Add(this.label3, 2, 11);
            this.layoutTop.Controls.Add(this.dateTimexDeliveryDate, 3, 9);
            this.layoutTop.Controls.Add(this.label6, 2, 9);
            this.layoutTop.Controls.Add(this.textexVoucherCode, 3, 8);
            this.layoutTop.Controls.Add(this.label7, 2, 8);
            this.layoutTop.Controls.Add(this.label8, 2, 1);
            this.layoutTop.Controls.Add(this.dateTimexEntryDate, 3, 1);
            this.layoutTop.Controls.Add(this.pictureBox2, 1, 1);
            this.layoutTop.Controls.Add(this.combexReceiverID, 3, 4);
            this.layoutTop.Controls.Add(this.label9, 2, 4);
            this.layoutTop.Controls.Add(this.textexCustomerName, 3, 3);
            this.layoutTop.Controls.Add(this.textexReceiverName, 3, 5);
            this.layoutTop.Controls.Add(this.label10, 2, 3);
            this.layoutTop.Controls.Add(this.label11, 2, 5);
            this.layoutTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutTop.Location = new System.Drawing.Point(0, 0);
            this.layoutTop.Margin = new System.Windows.Forms.Padding(0);
            this.layoutTop.Name = "layoutTop";
            this.layoutTop.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.layoutTop.RowCount = 13;
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
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.layoutTop.Size = new System.Drawing.Size(955, 381);
            this.layoutTop.TabIndex = 98;
            // 
            // combexCustomerID
            // 
            this.combexCustomerID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.combexCustomerID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combexCustomerID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.combexCustomerID.Editable = true;
            this.combexCustomerID.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combexCustomerID.FormattingEnabled = true;
            this.combexCustomerID.Location = new System.Drawing.Point(297, 58);
            this.combexCustomerID.Margin = new System.Windows.Forms.Padding(1);
            this.combexCustomerID.Name = "combexCustomerID";
            this.combexCustomerID.ReadOnly = false;
            this.combexCustomerID.Size = new System.Drawing.Size(634, 29);
            this.combexCustomerID.TabIndex = 74;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(87, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(206, 30);
            this.label4.TabIndex = 77;
            this.label4.Text = "Contact Info";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(87, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(206, 31);
            this.label5.TabIndex = 78;
            this.label5.Text = "Customer";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textexShippingAddress
            // 
            this.textexShippingAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexShippingAddress.Editable = true;
            this.textexShippingAddress.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textexShippingAddress.Location = new System.Drawing.Point(297, 210);
            this.textexShippingAddress.Margin = new System.Windows.Forms.Padding(1);
            this.textexShippingAddress.Name = "textexShippingAddress";
            this.textexShippingAddress.Size = new System.Drawing.Size(634, 28);
            this.textexShippingAddress.TabIndex = 76;
            // 
            // textexContactInfo
            // 
            this.textexContactInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexContactInfo.Editable = true;
            this.textexContactInfo.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textexContactInfo.Location = new System.Drawing.Point(297, 180);
            this.textexContactInfo.Margin = new System.Windows.Forms.Padding(1);
            this.textexContactInfo.Name = "textexContactInfo";
            this.textexContactInfo.Size = new System.Drawing.Size(634, 28);
            this.textexContactInfo.TabIndex = 79;
            // 
            // combexSalespersonID
            // 
            this.combexSalespersonID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.combexSalespersonID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combexSalespersonID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.combexSalespersonID.Editable = true;
            this.combexSalespersonID.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combexSalespersonID.FormattingEnabled = true;
            this.combexSalespersonID.Location = new System.Drawing.Point(297, 300);
            this.combexSalespersonID.Margin = new System.Windows.Forms.Padding(1);
            this.combexSalespersonID.Name = "combexSalespersonID";
            this.combexSalespersonID.ReadOnly = false;
            this.combexSalespersonID.Size = new System.Drawing.Size(634, 29);
            this.combexSalespersonID.TabIndex = 80;
            // 
            // textexRemarks
            // 
            this.textexRemarks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexRemarks.Editable = true;
            this.textexRemarks.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textexRemarks.Location = new System.Drawing.Point(297, 331);
            this.textexRemarks.Margin = new System.Windows.Forms.Padding(1);
            this.textexRemarks.Name = "textexRemarks";
            this.textexRemarks.Size = new System.Drawing.Size(634, 28);
            this.textexRemarks.TabIndex = 81;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(87, 209);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 30);
            this.label1.TabIndex = 82;
            this.label1.Text = "Shipping Address";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(87, 299);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(206, 31);
            this.label2.TabIndex = 83;
            this.label2.Text = "Salesperson";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(87, 330);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(206, 30);
            this.label3.TabIndex = 84;
            this.label3.Text = "Remarks";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dateTimexDeliveryDate
            // 
            this.dateTimexDeliveryDate.CustomFormat = "dd/MMM/yyyy HH:mm:ss";
            this.dateTimexDeliveryDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateTimexDeliveryDate.Editable = true;
            this.dateTimexDeliveryDate.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimexDeliveryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimexDeliveryDate.Location = new System.Drawing.Point(297, 270);
            this.dateTimexDeliveryDate.Margin = new System.Windows.Forms.Padding(1);
            this.dateTimexDeliveryDate.Name = "dateTimexDeliveryDate";
            this.dateTimexDeliveryDate.ReadOnly = false;
            this.dateTimexDeliveryDate.Size = new System.Drawing.Size(634, 28);
            this.dateTimexDeliveryDate.TabIndex = 85;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(87, 269);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(206, 30);
            this.label6.TabIndex = 86;
            this.label6.Text = "Delivery Lead Time";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textexVoucherCode
            // 
            this.textexVoucherCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexVoucherCode.Editable = true;
            this.textexVoucherCode.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textexVoucherCode.Location = new System.Drawing.Point(297, 240);
            this.textexVoucherCode.Margin = new System.Windows.Forms.Padding(1);
            this.textexVoucherCode.Name = "textexVoucherCode";
            this.textexVoucherCode.Size = new System.Drawing.Size(634, 28);
            this.textexVoucherCode.TabIndex = 87;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(87, 239);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(206, 30);
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
            this.label8.Size = new System.Drawing.Size(206, 30);
            this.label8.TabIndex = 89;
            this.label8.Text = "Sales Order Date";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dateTimexEntryDate
            // 
            this.dateTimexEntryDate.CustomFormat = "dd/MMM/yyyy HH:mm:ss";
            this.dateTimexEntryDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateTimexEntryDate.Editable = true;
            this.dateTimexEntryDate.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimexEntryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimexEntryDate.Location = new System.Drawing.Point(297, 28);
            this.dateTimexEntryDate.Margin = new System.Windows.Forms.Padding(1);
            this.dateTimexEntryDate.Name = "dateTimexEntryDate";
            this.dateTimexEntryDate.ReadOnly = false;
            this.dateTimexEntryDate.Size = new System.Drawing.Size(634, 28);
            this.dateTimexEntryDate.TabIndex = 90;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::TotalSmartCoding.Properties.Resources.Sign_Order_48;
            this.pictureBox2.Location = new System.Drawing.Point(33, 30);
            this.pictureBox2.Name = "pictureBox2";
            this.layoutTop.SetRowSpan(this.pictureBox2, 6);
            this.pictureBox2.Size = new System.Drawing.Size(48, 48);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 75;
            this.pictureBox2.TabStop = false;
            // 
            // combexReceiverID
            // 
            this.combexReceiverID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.combexReceiverID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combexReceiverID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.combexReceiverID.Editable = true;
            this.combexReceiverID.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combexReceiverID.FormattingEnabled = true;
            this.combexReceiverID.Location = new System.Drawing.Point(297, 119);
            this.combexReceiverID.Margin = new System.Windows.Forms.Padding(1);
            this.combexReceiverID.Name = "combexReceiverID";
            this.combexReceiverID.ReadOnly = false;
            this.combexReceiverID.Size = new System.Drawing.Size(634, 29);
            this.combexReceiverID.TabIndex = 91;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(87, 118);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(206, 31);
            this.label9.TabIndex = 92;
            this.label9.Text = "Receiver";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textexCustomerName
            // 
            this.textexCustomerName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexCustomerName.Editable = true;
            this.textexCustomerName.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textexCustomerName.Location = new System.Drawing.Point(297, 89);
            this.textexCustomerName.Margin = new System.Windows.Forms.Padding(1);
            this.textexCustomerName.Name = "textexCustomerName";
            this.textexCustomerName.ReadOnly = true;
            this.textexCustomerName.Size = new System.Drawing.Size(634, 28);
            this.textexCustomerName.TabIndex = 93;
            // 
            // textexReceiverName
            // 
            this.textexReceiverName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexReceiverName.Editable = true;
            this.textexReceiverName.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textexReceiverName.Location = new System.Drawing.Point(297, 150);
            this.textexReceiverName.Margin = new System.Windows.Forms.Padding(1);
            this.textexReceiverName.Name = "textexReceiverName";
            this.textexReceiverName.ReadOnly = true;
            this.textexReceiverName.Size = new System.Drawing.Size(634, 28);
            this.textexReceiverName.TabIndex = 94;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(87, 88);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(206, 30);
            this.label10.TabIndex = 95;
            this.label10.Text = "Customer Name";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(87, 149);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(206, 30);
            this.label11.TabIndex = 96;
            this.label11.Text = "Receiver Name";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // WizardMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 436);
            this.Controls.Add(this.layoutTop);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WizardMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Wizard [New Sales Order]";
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
        private CustomControls.CombexBox combexCustomerID;
        private CustomControls.TextexBox textexShippingAddress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label5;
        private CustomControls.TextexBox textexContactInfo;
        private CustomControls.CombexBox combexSalespersonID;
        private CustomControls.TextexBox textexRemarks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private CustomControls.DateTimexPicker dateTimexDeliveryDate;
        private System.Windows.Forms.Label label6;
        private CustomControls.TextexBox textexVoucherCode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private CustomControls.DateTimexPicker dateTimexEntryDate;
        private CustomControls.CombexBox combexReceiverID;
        private System.Windows.Forms.Label label9;
        private CustomControls.TextexBox textexCustomerName;
        private CustomControls.TextexBox textexReceiverName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
    }
}