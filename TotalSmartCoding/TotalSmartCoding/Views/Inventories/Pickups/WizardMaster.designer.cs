namespace TotalSmartCoding.Views.Inventories.Pickups
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.combexWarehouseID = new CustomControls.CombexBox();
            this.combexForkliftDriverID = new CustomControls.CombexBox();
            this.combexStorekeeperID = new CustomControls.CombexBox();
            this.textexRemarks = new CustomControls.TextexBox();
            this.combexFillingLineID = new CustomControls.CombexBox();
            this.label4 = new System.Windows.Forms.Label();
            this.layoutTop = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.layoutTop.SuspendLayout();
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
            this.toolStrip1.Location = new System.Drawing.Point(0, 194);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(612, 55);
            this.toolStrip1.TabIndex = 3;
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
            this.buttonOK.Enabled = false;
            this.buttonOK.Image = global::TotalSmartCoding.Properties.Resources.Oxygen_Icons_org_Oxygen_Actions_go_next_view;
            this.buttonOK.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonOK.Size = new System.Drawing.Size(92, 52);
            this.buttonOK.Text = "Next";
            this.buttonOK.Click += new System.EventHandler(this.buttonOKESC_Click);
            // 
            // errorProviderMaster
            // 
            this.errorProviderMaster.ContainerControl = this;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TotalSmartCoding.Properties.Resources.Forklift48;
            this.pictureBox1.Location = new System.Drawing.Point(28, 27);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.layoutTop.SetRowSpan(this.pictureBox1, 4);
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 95;
            this.pictureBox1.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(97, 121);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(120, 21);
            this.label12.TabIndex = 88;
            this.label12.Text = "Store Keeper";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(97, 152);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(120, 21);
            this.label11.TabIndex = 86;
            this.label11.Text = "Remarks";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(97, 90);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 21);
            this.label6.TabIndex = 82;
            this.label6.Text = "Forklift Driver";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(97, 28);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 21);
            this.label5.TabIndex = 78;
            this.label5.Text = "Production Line";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // combexWarehouseID
            // 
            this.combexWarehouseID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.combexWarehouseID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combexWarehouseID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.combexWarehouseID.Editable = true;
            this.combexWarehouseID.FormattingEnabled = true;
            this.combexWarehouseID.Location = new System.Drawing.Point(221, 55);
            this.combexWarehouseID.Margin = new System.Windows.Forms.Padding(2, 1, 1, 1);
            this.combexWarehouseID.Name = "combexWarehouseID";
            this.combexWarehouseID.ReadOnly = false;
            this.combexWarehouseID.Size = new System.Drawing.Size(369, 29);
            this.combexWarehouseID.TabIndex = 86;
            // 
            // combexForkliftDriverID
            // 
            this.combexForkliftDriverID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.combexForkliftDriverID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combexForkliftDriverID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.combexForkliftDriverID.Editable = true;
            this.combexForkliftDriverID.FormattingEnabled = true;
            this.combexForkliftDriverID.Location = new System.Drawing.Point(221, 86);
            this.combexForkliftDriverID.Margin = new System.Windows.Forms.Padding(2, 1, 1, 1);
            this.combexForkliftDriverID.Name = "combexForkliftDriverID";
            this.combexForkliftDriverID.ReadOnly = false;
            this.combexForkliftDriverID.Size = new System.Drawing.Size(369, 29);
            this.combexForkliftDriverID.TabIndex = 87;
            // 
            // combexStorekeeperID
            // 
            this.combexStorekeeperID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.combexStorekeeperID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combexStorekeeperID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.combexStorekeeperID.Editable = true;
            this.combexStorekeeperID.FormattingEnabled = true;
            this.combexStorekeeperID.Location = new System.Drawing.Point(221, 117);
            this.combexStorekeeperID.Margin = new System.Windows.Forms.Padding(2, 1, 1, 1);
            this.combexStorekeeperID.Name = "combexStorekeeperID";
            this.combexStorekeeperID.ReadOnly = false;
            this.combexStorekeeperID.Size = new System.Drawing.Size(369, 29);
            this.combexStorekeeperID.TabIndex = 88;
            // 
            // textexRemarks
            // 
            this.textexRemarks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexRemarks.Editable = true;
            this.textexRemarks.Location = new System.Drawing.Point(221, 148);
            this.textexRemarks.Margin = new System.Windows.Forms.Padding(2, 1, 1, 1);
            this.textexRemarks.Name = "textexRemarks";
            this.textexRemarks.Size = new System.Drawing.Size(369, 28);
            this.textexRemarks.TabIndex = 92;
            // 
            // combexFillingLineID
            // 
            this.combexFillingLineID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.combexFillingLineID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combexFillingLineID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.combexFillingLineID.Editable = true;
            this.combexFillingLineID.FormattingEnabled = true;
            this.combexFillingLineID.Location = new System.Drawing.Point(221, 24);
            this.combexFillingLineID.Margin = new System.Windows.Forms.Padding(2, 1, 1, 1);
            this.combexFillingLineID.Name = "combexFillingLineID";
            this.combexFillingLineID.ReadOnly = false;
            this.combexFillingLineID.Size = new System.Drawing.Size(369, 29);
            this.combexFillingLineID.TabIndex = 94;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(97, 59);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 21);
            this.label4.TabIndex = 77;
            this.label4.Text = "Warehouse";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // layoutTop
            // 
            this.layoutTop.AutoSize = true;
            this.layoutTop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.layoutTop.ColumnCount = 5;
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutTop.Controls.Add(this.label4, 2, 2);
            this.layoutTop.Controls.Add(this.combexFillingLineID, 3, 1);
            this.layoutTop.Controls.Add(this.pictureBox1, 1, 1);
            this.layoutTop.Controls.Add(this.textexRemarks, 3, 5);
            this.layoutTop.Controls.Add(this.combexStorekeeperID, 3, 4);
            this.layoutTop.Controls.Add(this.combexForkliftDriverID, 3, 3);
            this.layoutTop.Controls.Add(this.combexWarehouseID, 3, 2);
            this.layoutTop.Controls.Add(this.label5, 2, 1);
            this.layoutTop.Controls.Add(this.label6, 2, 3);
            this.layoutTop.Controls.Add(this.label11, 2, 5);
            this.layoutTop.Controls.Add(this.label12, 2, 4);
            this.layoutTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutTop.Location = new System.Drawing.Point(0, 0);
            this.layoutTop.Margin = new System.Windows.Forms.Padding(0);
            this.layoutTop.Name = "layoutTop";
            this.layoutTop.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.layoutTop.RowCount = 7;
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutTop.Size = new System.Drawing.Size(612, 194);
            this.layoutTop.TabIndex = 99;
            // 
            // WizardMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 249);
            this.Controls.Add(this.layoutTop);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WizardMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Wizard [New Pickup]";
            this.Load += new System.EventHandler(this.WizardMaster_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.layoutTop.ResumeLayout(false);
            this.layoutTop.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton buttonESC;
        private System.Windows.Forms.ToolStripButton buttonOK;
        private System.Windows.Forms.ErrorProvider errorProviderMaster;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TableLayoutPanel layoutTop;
        private System.Windows.Forms.Label label4;
        private CustomControls.CombexBox combexFillingLineID;
        private CustomControls.TextexBox textexRemarks;
        private CustomControls.CombexBox combexStorekeeperID;
        private CustomControls.CombexBox combexForkliftDriverID;
        private CustomControls.CombexBox combexWarehouseID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
    }
}