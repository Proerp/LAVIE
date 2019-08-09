namespace TotalSmartCoding.Views.Productions
{
    partial class QuickView
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
            this.fastBarcodes = new BrightIdeasSoftware.FastObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvCode = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvLocked = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.textFilter = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelLock = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.fastBarcodes)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // fastBarcodes
            // 
            this.fastBarcodes.AllColumns.Add(this.olvColumn1);
            this.fastBarcodes.AllColumns.Add(this.olvCode);
            this.fastBarcodes.AllColumns.Add(this.olvLocked);
            this.fastBarcodes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvCode,
            this.olvLocked});
            this.fastBarcodes.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastBarcodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastBarcodes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fastBarcodes.FullRowSelect = true;
            this.fastBarcodes.HideSelection = false;
            this.fastBarcodes.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastBarcodes.Location = new System.Drawing.Point(136, 23);
            this.fastBarcodes.Margin = new System.Windows.Forms.Padding(2);
            this.fastBarcodes.Name = "fastBarcodes";
            this.fastBarcodes.OwnerDraw = true;
            this.fastBarcodes.ShowGroups = false;
            this.fastBarcodes.Size = new System.Drawing.Size(444, 658);
            this.fastBarcodes.TabIndex = 71;
            this.fastBarcodes.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastBarcodes.UseCompatibleStateImageBehavior = false;
            this.fastBarcodes.UseFiltering = true;
            this.fastBarcodes.View = System.Windows.Forms.View.Details;
            this.fastBarcodes.VirtualMode = true;
            this.fastBarcodes.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.fastBarcodes_FormatRow);
            this.fastBarcodes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.fastBarcodes_MouseDown);
            // 
            // olvColumn1
            // 
            this.olvColumn1.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn1.Text = "";
            this.olvColumn1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn1.Width = 35;
            // 
            // olvCode
            // 
            this.olvCode.AspectName = "Code";
            this.olvCode.FillsFreeSpace = true;
            this.olvCode.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvCode.Text = "Barcode";
            this.olvCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvCode.Width = 327;
            // 
            // olvLocked
            // 
            this.olvLocked.AspectName = "Locked";
            this.olvLocked.CheckBoxes = true;
            this.olvLocked.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvLocked.IsEditable = false;
            this.olvLocked.Text = "Locked";
            this.olvLocked.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textFilter
            // 
            this.textFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.textFilter.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textFilter.Location = new System.Drawing.Point(136, 0);
            this.textFilter.Margin = new System.Windows.Forms.Padding(2);
            this.textFilter.Name = "textFilter";
            this.textFilter.Size = new System.Drawing.Size(444, 23);
            this.textFilter.TabIndex = 72;
            this.textFilter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textFilter.TextChanged += new System.EventHandler(this.textFilter_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelLock);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(136, 681);
            this.panel1.TabIndex = 73;
            // 
            // labelLock
            // 
            this.labelLock.AutoSize = true;
            this.labelLock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelLock.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelLock.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLock.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelLock.Location = new System.Drawing.Point(0, 652);
            this.labelLock.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelLock.Name = "labelLock";
            this.labelLock.Padding = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.labelLock.Size = new System.Drawing.Size(136, 29);
            this.labelLock.TabIndex = 9;
            this.labelLock.Text = "Click here to lock or unlock";
            this.labelLock.Visible = false;
            this.labelLock.Click += new System.EventHandler(this.labelLock_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TotalSmartCoding.Properties.Resources.Barcode2D;
            this.pictureBox1.Location = new System.Drawing.Point(16, 22);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(36, 39);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // QuickView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 681);
            this.Controls.Add(this.fastBarcodes);
            this.Controls.Add(this.textFilter);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuickView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quick View";
            ((System.ComponentModel.ISupportInitialize)(this.fastBarcodes)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BrightIdeasSoftware.FastObjectListView fastBarcodes;
        private BrightIdeasSoftware.OLVColumn olvCode;
        private System.Windows.Forms.TextBox textFilter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private System.Windows.Forms.Label labelLock;
        private BrightIdeasSoftware.OLVColumn olvLocked;
    }
}