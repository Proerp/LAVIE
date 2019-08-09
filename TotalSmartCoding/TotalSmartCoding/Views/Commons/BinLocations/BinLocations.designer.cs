namespace TotalSmartCoding.Views.Commons.BinLocations
{
    partial class BinLocations
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BinLocations));
            this.textexCode = new CustomControls.TextexBox();
            this.combexWarehouseID = new CustomControls.CombexBox();
            this.toolStripChildForm = new System.Windows.Forms.ToolStrip();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.panelCenter = new System.Windows.Forms.Panel();
            this.layoutTop = new System.Windows.Forms.TableLayoutPanel();
            this.textexVATCode = new CustomControls.TextexBox();
            this.textexAttentionName = new CustomControls.TextexBox();
            this.textexContactInfo = new CustomControls.TextexBox();
            this.textexName = new CustomControls.TextexBox();
            this.textexRemarks = new CustomControls.TextexBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.fastBinLocationIndex = new BrightIdeasSoftware.FastObjectListView();
            this.olvID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvBlank = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvBinLocationCode = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvBinLocationName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvLocationName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvInActive = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvRemarks = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageList32 = new System.Windows.Forms.ImageList(this.components);
            this.toolStripChildForm.SuspendLayout();
            this.panelCenter.SuspendLayout();
            this.layoutTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastBinLocationIndex)).BeginInit();
            this.SuspendLayout();
            // 
            // textexCode
            // 
            this.textexCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexCode.Editable = true;
            this.textexCode.Location = new System.Drawing.Point(35, 49);
            this.textexCode.Margin = new System.Windows.Forms.Padding(5, 1, 1, 1);
            this.textexCode.Name = "textexCode";
            this.textexCode.Size = new System.Drawing.Size(488, 28);
            this.textexCode.TabIndex = 81;
            // 
            // combexWarehouseID
            // 
            this.combexWarehouseID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.combexWarehouseID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combexWarehouseID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.combexWarehouseID.Editable = true;
            this.combexWarehouseID.FormattingEnabled = true;
            this.combexWarehouseID.Location = new System.Drawing.Point(35, 175);
            this.combexWarehouseID.Margin = new System.Windows.Forms.Padding(5, 1, 1, 1);
            this.combexWarehouseID.Name = "combexWarehouseID";
            this.combexWarehouseID.ReadOnly = false;
            this.combexWarehouseID.Size = new System.Drawing.Size(488, 29);
            this.combexWarehouseID.TabIndex = 83;
            // 
            // toolStripChildForm
            // 
            this.toolStripChildForm.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStripChildForm.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripChildForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton2,
            this.toolStripSeparator1});
            this.toolStripChildForm.Location = new System.Drawing.Point(0, 0);
            this.toolStripChildForm.Name = "toolStripChildForm";
            this.toolStripChildForm.Size = new System.Drawing.Size(1531, 39);
            this.toolStripChildForm.TabIndex = 29;
            this.toolStripChildForm.Text = "toolStrip1";
            this.toolStripChildForm.Visible = false;
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(118, 36);
            this.toolStripButton2.Text = "Disconnect";
            this.toolStripButton2.Visible = false;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            this.toolStripSeparator1.Visible = false;
            // 
            // panelCenter
            // 
            this.panelCenter.BackColor = System.Drawing.Color.Ivory;
            this.panelCenter.BackgroundImage = global::TotalSmartCoding.Properties.Resources.Blue2010Large;
            this.panelCenter.Controls.Add(this.layoutTop);
            this.panelCenter.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelCenter.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelCenter.Location = new System.Drawing.Point(982, 0);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Size = new System.Drawing.Size(549, 654);
            this.panelCenter.TabIndex = 76;
            // 
            // layoutTop
            // 
            this.layoutTop.AutoSize = true;
            this.layoutTop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.layoutTop.BackColor = System.Drawing.Color.Ivory;
            this.layoutTop.ColumnCount = 3;
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.layoutTop.Controls.Add(this.textexVATCode, 1, 8);
            this.layoutTop.Controls.Add(this.textexAttentionName, 1, 12);
            this.layoutTop.Controls.Add(this.textexCode, 1, 2);
            this.layoutTop.Controls.Add(this.combexWarehouseID, 1, 6);
            this.layoutTop.Controls.Add(this.textexContactInfo, 1, 10);
            this.layoutTop.Controls.Add(this.textexName, 1, 4);
            this.layoutTop.Controls.Add(this.textexRemarks, 1, 14);
            this.layoutTop.Controls.Add(this.label13, 1, 1);
            this.layoutTop.Controls.Add(this.label14, 1, 3);
            this.layoutTop.Controls.Add(this.label3, 1, 5);
            this.layoutTop.Controls.Add(this.label5, 1, 7);
            this.layoutTop.Controls.Add(this.label8, 1, 9);
            this.layoutTop.Controls.Add(this.label9, 1, 11);
            this.layoutTop.Controls.Add(this.label12, 1, 13);
            this.layoutTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutTop.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutTop.Location = new System.Drawing.Point(0, 0);
            this.layoutTop.Margin = new System.Windows.Forms.Padding(0);
            this.layoutTop.Name = "layoutTop";
            this.layoutTop.RowCount = 16;
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
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
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.Size = new System.Drawing.Size(549, 654);
            this.layoutTop.TabIndex = 8;
            // 
            // textexVATCode
            // 
            this.textexVATCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexVATCode.Editable = false;
            this.textexVATCode.Location = new System.Drawing.Point(35, 239);
            this.textexVATCode.Margin = new System.Windows.Forms.Padding(5, 1, 1, 1);
            this.textexVATCode.Name = "textexVATCode";
            this.textexVATCode.Size = new System.Drawing.Size(488, 28);
            this.textexVATCode.TabIndex = 80;
            // 
            // textexAttentionName
            // 
            this.textexAttentionName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexAttentionName.Editable = false;
            this.textexAttentionName.Location = new System.Drawing.Point(35, 365);
            this.textexAttentionName.Margin = new System.Windows.Forms.Padding(5, 1, 1, 1);
            this.textexAttentionName.Name = "textexAttentionName";
            this.textexAttentionName.Size = new System.Drawing.Size(488, 28);
            this.textexAttentionName.TabIndex = 82;
            // 
            // textexContactInfo
            // 
            this.textexContactInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexContactInfo.Editable = false;
            this.textexContactInfo.Location = new System.Drawing.Point(35, 302);
            this.textexContactInfo.Margin = new System.Windows.Forms.Padding(5, 1, 1, 1);
            this.textexContactInfo.Name = "textexContactInfo";
            this.textexContactInfo.Size = new System.Drawing.Size(488, 28);
            this.textexContactInfo.TabIndex = 78;
            // 
            // textexName
            // 
            this.textexName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexName.Editable = true;
            this.textexName.Location = new System.Drawing.Point(35, 112);
            this.textexName.Margin = new System.Windows.Forms.Padding(5, 1, 1, 1);
            this.textexName.Name = "textexName";
            this.textexName.Size = new System.Drawing.Size(488, 28);
            this.textexName.TabIndex = 86;
            // 
            // textexRemarks
            // 
            this.textexRemarks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexRemarks.Editable = true;
            this.textexRemarks.Location = new System.Drawing.Point(35, 428);
            this.textexRemarks.Margin = new System.Windows.Forms.Padding(5, 1, 1, 1);
            this.textexRemarks.Name = "textexRemarks";
            this.textexRemarks.Size = new System.Drawing.Size(488, 28);
            this.textexRemarks.TabIndex = 88;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Location = new System.Drawing.Point(30, 16);
            this.label13.Margin = new System.Windows.Forms.Padding(0, 1, 1, 1);
            this.label13.Name = "label13";
            this.label13.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.label13.Size = new System.Drawing.Size(493, 31);
            this.label13.TabIndex = 96;
            this.label13.Text = "Code";
            this.label13.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Location = new System.Drawing.Point(30, 79);
            this.label14.Margin = new System.Windows.Forms.Padding(0, 1, 1, 1);
            this.label14.Name = "label14";
            this.label14.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.label14.Size = new System.Drawing.Size(493, 31);
            this.label14.TabIndex = 97;
            this.label14.Text = "Description";
            this.label14.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(30, 142);
            this.label3.Margin = new System.Windows.Forms.Padding(0, 1, 1, 1);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.label3.Size = new System.Drawing.Size(493, 31);
            this.label3.TabIndex = 77;
            this.label3.Text = "Warehouse";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(30, 206);
            this.label5.Margin = new System.Windows.Forms.Padding(0, 1, 1, 1);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.label5.Size = new System.Drawing.Size(493, 31);
            this.label5.TabIndex = 89;
            this.label5.Text = "Maximum Pallets";
            this.label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(30, 269);
            this.label8.Margin = new System.Windows.Forms.Padding(0, 1, 1, 1);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.label8.Size = new System.Drawing.Size(493, 31);
            this.label8.TabIndex = 90;
            this.label8.Text = "Total Pallets";
            this.label8.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(30, 332);
            this.label9.Margin = new System.Windows.Forms.Padding(0, 1, 1, 1);
            this.label9.Name = "label9";
            this.label9.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.label9.Size = new System.Drawing.Size(493, 31);
            this.label9.TabIndex = 91;
            this.label9.Text = "Recommended Value";
            this.label9.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(30, 395);
            this.label12.Margin = new System.Windows.Forms.Padding(0, 1, 1, 1);
            this.label12.Name = "label12";
            this.label12.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.label12.Size = new System.Drawing.Size(493, 31);
            this.label12.TabIndex = 94;
            this.label12.Text = "Remarks";
            this.label12.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // fastBinLocationIndex
            // 
            this.fastBinLocationIndex.AllColumns.Add(this.olvID);
            this.fastBinLocationIndex.AllColumns.Add(this.olvBlank);
            this.fastBinLocationIndex.AllColumns.Add(this.olvBinLocationCode);
            this.fastBinLocationIndex.AllColumns.Add(this.olvBinLocationName);
            this.fastBinLocationIndex.AllColumns.Add(this.olvLocationName);
            this.fastBinLocationIndex.AllColumns.Add(this.olvInActive);
            this.fastBinLocationIndex.AllColumns.Add(this.olvRemarks);
            this.fastBinLocationIndex.BackColor = System.Drawing.Color.Ivory;
            this.fastBinLocationIndex.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvID,
            this.olvBlank,
            this.olvBinLocationCode,
            this.olvBinLocationName,
            this.olvLocationName,
            this.olvInActive,
            this.olvRemarks});
            this.fastBinLocationIndex.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastBinLocationIndex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastBinLocationIndex.Font = new System.Drawing.Font("Calibri Light", 10.2F);
            this.fastBinLocationIndex.FullRowSelect = true;
            this.fastBinLocationIndex.GroupImageList = this.imageList32;
            this.fastBinLocationIndex.HideSelection = false;
            this.fastBinLocationIndex.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastBinLocationIndex.Location = new System.Drawing.Point(0, 0);
            this.fastBinLocationIndex.Name = "fastBinLocationIndex";
            this.fastBinLocationIndex.OwnerDraw = true;
            this.fastBinLocationIndex.ShowGroups = false;
            this.fastBinLocationIndex.Size = new System.Drawing.Size(982, 654);
            this.fastBinLocationIndex.TabIndex = 68;
            this.fastBinLocationIndex.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastBinLocationIndex.UseCompatibleStateImageBehavior = false;
            this.fastBinLocationIndex.UseFiltering = true;
            this.fastBinLocationIndex.View = System.Windows.Forms.View.Details;
            this.fastBinLocationIndex.VirtualMode = true;
            // 
            // olvID
            // 
            this.olvID.Text = "";
            this.olvID.Width = 0;
            // 
            // olvBlank
            // 
            this.olvBlank.Text = "";
            this.olvBlank.Width = 15;
            // 
            // olvBinLocationCode
            // 
            this.olvBinLocationCode.AspectName = "BinLocationCode";
            this.olvBinLocationCode.AspectToStringFormat = "";
            this.olvBinLocationCode.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvBinLocationCode.Text = "Code";
            this.olvBinLocationCode.Width = 296;
            // 
            // olvBinLocationName
            // 
            this.olvBinLocationName.AspectName = "BinLocationName";
            this.olvBinLocationName.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvBinLocationName.Text = "Name";
            this.olvBinLocationName.Width = 420;
            // 
            // olvLocationName
            // 
            this.olvLocationName.AspectName = "LocationName";
            this.olvLocationName.Text = "Location";
            this.olvLocationName.Width = 120;
            // 
            // olvInActive
            // 
            this.olvInActive.AspectName = "InActive";
            this.olvInActive.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvInActive.Text = "";
            this.olvInActive.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvInActive.Width = 30;
            // 
            // olvRemarks
            // 
            this.olvRemarks.AspectName = "Remarks";
            this.olvRemarks.FillsFreeSpace = true;
            this.olvRemarks.Text = "Remarks";
            this.olvRemarks.Width = 500;
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
            this.imageList32.Images.SetKeyName(5, "Storage32");
            this.imageList32.Images.SetKeyName(6, "Sales-Order-32");
            this.imageList32.Images.SetKeyName(7, "Sign_Order_32");
            this.imageList32.Images.SetKeyName(8, "CustomerRed");
            // 
            // BinLocations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1531, 654);
            this.Controls.Add(this.fastBinLocationIndex);
            this.Controls.Add(this.panelCenter);
            this.Controls.Add(this.toolStripChildForm);
            this.Name = "BinLocations";
            this.Text = "Bin Locations";
            this.Controls.SetChildIndex(this.toolStripChildForm, 0);
            this.Controls.SetChildIndex(this.panelCenter, 0);
            this.Controls.SetChildIndex(this.fastBinLocationIndex, 0);
            this.toolStripChildForm.ResumeLayout(false);
            this.toolStripChildForm.PerformLayout();
            this.panelCenter.ResumeLayout(false);
            this.panelCenter.PerformLayout();
            this.layoutTop.ResumeLayout(false);
            this.layoutTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastBinLocationIndex)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel layoutTop;
        private System.Windows.Forms.ToolStrip toolStripChildForm;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private BrightIdeasSoftware.FastObjectListView fastBinLocationIndex;
        private BrightIdeasSoftware.OLVColumn olvID;
        private BrightIdeasSoftware.OLVColumn olvBinLocationCode;
        private BrightIdeasSoftware.OLVColumn olvBinLocationName;
        private System.Windows.Forms.ImageList imageList32;
        private BrightIdeasSoftware.OLVColumn olvInActive;
        private BrightIdeasSoftware.OLVColumn olvLocationName;
        private BrightIdeasSoftware.OLVColumn olvRemarks;
        private System.Windows.Forms.Label label3;
        private CustomControls.TextexBox textexContactInfo;
        private CustomControls.TextexBox textexVATCode;
        private CustomControls.TextexBox textexAttentionName;
        private CustomControls.TextexBox textexName;
        private CustomControls.TextexBox textexRemarks;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private CustomControls.TextexBox textexCode;
        private CustomControls.CombexBox combexWarehouseID;
        private System.Windows.Forms.Panel panelCenter;
        private BrightIdeasSoftware.OLVColumn olvBlank;
        private System.Windows.Forms.Label label14;

    }
}