namespace TotalSmartCoding.Views.Mains
{
    partial class SearchBarcode
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
            this.fastPacks = new BrightIdeasSoftware.FastObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvPackCode = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.textFilterPack = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelReprintCarton = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.fastCartons = new BrightIdeasSoftware.FastObjectListView();
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvCartonCode = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.fastPallets = new BrightIdeasSoftware.FastObjectListView();
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvPalletCode = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.panelCenter = new System.Windows.Forms.Panel();
            this.textFilterPallet = new System.Windows.Forms.TextBox();
            this.textFilterCarton = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.fastPacks)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastCartons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastPallets)).BeginInit();
            this.panelCenter.SuspendLayout();
            this.SuspendLayout();
            // 
            // fastPacks
            // 
            this.fastPacks.AllColumns.Add(this.olvColumn1);
            this.fastPacks.AllColumns.Add(this.olvPackCode);
            this.fastPacks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvPackCode});
            this.fastPacks.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastPacks.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fastPacks.FullRowSelect = true;
            this.fastPacks.HideSelection = false;
            this.fastPacks.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastPacks.Location = new System.Drawing.Point(3, 3);
            this.fastPacks.Name = "fastPacks";
            this.fastPacks.OwnerDraw = true;
            this.fastPacks.ShowGroups = false;
            this.fastPacks.Size = new System.Drawing.Size(289, 220);
            this.fastPacks.TabIndex = 71;
            this.fastPacks.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastPacks.UseCompatibleStateImageBehavior = false;
            this.fastPacks.UseFiltering = true;
            this.fastPacks.View = System.Windows.Forms.View.Details;
            this.fastPacks.VirtualMode = true;
            this.fastPacks.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.fastBarcodes_FormatRow);
            this.fastPacks.DoubleClick += new System.EventHandler(this.fastBarcodes_DoubleClick);
            // 
            // olvColumn1
            // 
            this.olvColumn1.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn1.Text = "";
            this.olvColumn1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn1.Width = 35;
            // 
            // olvPackCode
            // 
            this.olvPackCode.AspectName = "Code";
            this.olvPackCode.FillsFreeSpace = true;
            this.olvPackCode.HeaderForeColor = System.Drawing.SystemColors.HotTrack;
            this.olvPackCode.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvPackCode.Text = "Double click to print journal.";
            this.olvPackCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvPackCode.Width = 263;
            // 
            // textFilterPack
            // 
            this.textFilterPack.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textFilterPack.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.textFilterPack.Location = new System.Drawing.Point(325, 12);
            this.textFilterPack.Name = "textFilterPack";
            this.textFilterPack.Size = new System.Drawing.Size(215, 27);
            this.textFilterPack.TabIndex = 72;
            this.textFilterPack.Text = "Enter any text here to search ...";
            this.textFilterPack.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textFilterPack.TextChanged += new System.EventHandler(this.textFilter_TextChanged);
            this.textFilterPack.Enter += new System.EventHandler(this.textFilter_Enter);
            this.textFilterPack.Leave += new System.EventHandler(this.textFilter_Leave);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelReprintCarton);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(182, 823);
            this.panel1.TabIndex = 73;
            // 
            // labelReprintCarton
            // 
            this.labelReprintCarton.AutoSize = true;
            this.labelReprintCarton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelReprintCarton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelReprintCarton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReprintCarton.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelReprintCarton.Location = new System.Drawing.Point(0, 786);
            this.labelReprintCarton.Name = "labelReprintCarton";
            this.labelReprintCarton.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.labelReprintCarton.Size = new System.Drawing.Size(175, 37);
            this.labelReprintCarton.TabIndex = 8;
            this.labelReprintCarton.Text = "Click here to reprint carton";
            this.labelReprintCarton.Visible = false;
            this.labelReprintCarton.Click += new System.EventHandler(this.labelReprintCarton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TotalSmartCoding.Properties.Resources.Search_barcode_32;
            this.pictureBox1.Location = new System.Drawing.Point(31, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // fastCartons
            // 
            this.fastCartons.AllColumns.Add(this.olvColumn2);
            this.fastCartons.AllColumns.Add(this.olvCartonCode);
            this.fastCartons.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn2,
            this.olvCartonCode});
            this.fastCartons.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastCartons.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fastCartons.FullRowSelect = true;
            this.fastCartons.HideSelection = false;
            this.fastCartons.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastCartons.Location = new System.Drawing.Point(0, 239);
            this.fastCartons.Name = "fastCartons";
            this.fastCartons.OwnerDraw = true;
            this.fastCartons.ShowGroups = false;
            this.fastCartons.Size = new System.Drawing.Size(292, 173);
            this.fastCartons.TabIndex = 74;
            this.fastCartons.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastCartons.UseCompatibleStateImageBehavior = false;
            this.fastCartons.UseFiltering = true;
            this.fastCartons.View = System.Windows.Forms.View.Details;
            this.fastCartons.VirtualMode = true;
            this.fastCartons.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.fastBarcodes_FormatRow);
            this.fastCartons.DoubleClick += new System.EventHandler(this.fastBarcodes_DoubleClick);
            this.fastCartons.MouseDown += new System.Windows.Forms.MouseEventHandler(this.fastBarcodes_MouseDown);
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "";
            this.olvColumn2.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn2.Text = "";
            this.olvColumn2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn2.Width = 35;
            // 
            // olvCartonCode
            // 
            this.olvCartonCode.AspectName = "Code";
            this.olvCartonCode.FillsFreeSpace = true;
            this.olvCartonCode.HeaderForeColor = System.Drawing.SystemColors.HotTrack;
            this.olvCartonCode.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvCartonCode.Text = "Right click to view carton details. Double click to print journal.";
            this.olvCartonCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvCartonCode.Width = 263;
            // 
            // fastPallets
            // 
            this.fastPallets.AllColumns.Add(this.olvColumn4);
            this.fastPallets.AllColumns.Add(this.olvPalletCode);
            this.fastPallets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn4,
            this.olvPalletCode});
            this.fastPallets.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastPallets.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fastPallets.FullRowSelect = true;
            this.fastPallets.HideSelection = false;
            this.fastPallets.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastPallets.Location = new System.Drawing.Point(0, 429);
            this.fastPallets.Name = "fastPallets";
            this.fastPallets.OwnerDraw = true;
            this.fastPallets.ShowGroups = false;
            this.fastPallets.Size = new System.Drawing.Size(289, 151);
            this.fastPallets.TabIndex = 75;
            this.fastPallets.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastPallets.UseCompatibleStateImageBehavior = false;
            this.fastPallets.UseFiltering = true;
            this.fastPallets.View = System.Windows.Forms.View.Details;
            this.fastPallets.VirtualMode = true;
            this.fastPallets.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.fastBarcodes_FormatRow);
            this.fastPallets.DoubleClick += new System.EventHandler(this.fastBarcodes_DoubleClick);
            this.fastPallets.MouseDown += new System.Windows.Forms.MouseEventHandler(this.fastBarcodes_MouseDown);
            // 
            // olvColumn4
            // 
            this.olvColumn4.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn4.Text = "";
            this.olvColumn4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn4.Width = 35;
            // 
            // olvPalletCode
            // 
            this.olvPalletCode.AspectName = "Code";
            this.olvPalletCode.FillsFreeSpace = true;
            this.olvPalletCode.HeaderForeColor = System.Drawing.SystemColors.HotTrack;
            this.olvPalletCode.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvPalletCode.Text = "Right click to view pallet details. Double click to print journal.";
            this.olvPalletCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvPalletCode.Width = 263;
            // 
            // panelCenter
            // 
            this.panelCenter.Controls.Add(this.textFilterPallet);
            this.panelCenter.Controls.Add(this.textFilterCarton);
            this.panelCenter.Controls.Add(this.fastPacks);
            this.panelCenter.Controls.Add(this.textFilterPack);
            this.panelCenter.Controls.Add(this.fastPallets);
            this.panelCenter.Controls.Add(this.fastCartons);
            this.panelCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCenter.Location = new System.Drawing.Point(182, 0);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Size = new System.Drawing.Size(592, 823);
            this.panelCenter.TabIndex = 76;
            // 
            // textFilterPallet
            // 
            this.textFilterPallet.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textFilterPallet.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.textFilterPallet.Location = new System.Drawing.Point(325, 110);
            this.textFilterPallet.Name = "textFilterPallet";
            this.textFilterPallet.Size = new System.Drawing.Size(215, 27);
            this.textFilterPallet.TabIndex = 77;
            this.textFilterPallet.Text = "Enter any text here to search ...";
            this.textFilterPallet.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textFilterPallet.TextChanged += new System.EventHandler(this.textFilter_TextChanged);
            this.textFilterPallet.Enter += new System.EventHandler(this.textFilter_Enter);
            this.textFilterPallet.Leave += new System.EventHandler(this.textFilter_Leave);
            // 
            // textFilterCarton
            // 
            this.textFilterCarton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textFilterCarton.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.textFilterCarton.Location = new System.Drawing.Point(325, 60);
            this.textFilterCarton.Name = "textFilterCarton";
            this.textFilterCarton.Size = new System.Drawing.Size(215, 27);
            this.textFilterCarton.TabIndex = 76;
            this.textFilterCarton.Text = "Enter any text here to search ...";
            this.textFilterCarton.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textFilterCarton.TextChanged += new System.EventHandler(this.textFilter_TextChanged);
            this.textFilterCarton.Enter += new System.EventHandler(this.textFilter_Enter);
            this.textFilterCarton.Leave += new System.EventHandler(this.textFilter_Leave);
            // 
            // SearchBarcode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 823);
            this.Controls.Add(this.panelCenter);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchBarcode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search Results";
            ((System.ComponentModel.ISupportInitialize)(this.fastPacks)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastCartons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastPallets)).EndInit();
            this.panelCenter.ResumeLayout(false);
            this.panelCenter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.FastObjectListView fastPacks;
        private BrightIdeasSoftware.OLVColumn olvPackCode;
        private System.Windows.Forms.TextBox textFilterPack;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.FastObjectListView fastCartons;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvCartonCode;
        private BrightIdeasSoftware.FastObjectListView fastPallets;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvPalletCode;
        private System.Windows.Forms.Panel panelCenter;
        private System.Windows.Forms.TextBox textFilterPallet;
        private System.Windows.Forms.TextBox textFilterCarton;
        private System.Windows.Forms.Label labelReprintCarton;
    }
}