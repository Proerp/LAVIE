namespace TotalSmartCoding.Views.Commons
{
    partial class CustomMessageBox
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
            this.labelText = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureIcon = new System.Windows.Forms.PictureBox();
            this.labelPromptText = new System.Windows.Forms.Label();
            this.layoutTop = new System.Windows.Forms.TableLayoutPanel();
            this.comboVoidTypeNames = new System.Windows.Forms.ComboBox();
            this.textRemarks = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIcon)).BeginInit();
            this.layoutTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelText
            // 
            this.labelText.AutoSize = true;
            this.labelText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelText.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelText.Location = new System.Drawing.Point(96, 22);
            this.labelText.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(514, 36);
            this.labelText.TabIndex = 38;
            this.labelText.Text = "Message";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 237);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(626, 50);
            this.panel1.TabIndex = 40;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Image = global::TotalSmartCoding.Properties.Resources.Check_Snowish_Ok;
            this.button3.Location = new System.Drawing.Point(331, 0);
            this.button3.Margin = new System.Windows.Forms.Padding(0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(91, 43);
            this.button3.TabIndex = 41;
            this.button3.Text = "Yes";
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Image = global::TotalSmartCoding.Properties.Resources.Check_Snowish_Ok;
            this.button2.Location = new System.Drawing.Point(425, 0);
            this.button2.Margin = new System.Windows.Forms.Padding(0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(91, 43);
            this.button2.TabIndex = 40;
            this.button2.Text = "OK";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::TotalSmartCoding.Properties.Resources.Yellow_cross;
            this.button1.Location = new System.Drawing.Point(520, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 43);
            this.button1.TabIndex = 39;
            this.button1.Text = "Cancel";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // pictureIcon
            // 
            this.pictureIcon.Image = global::TotalSmartCoding.Properties.Resources.Kyo_Tux_Phuzion_Sign_Info;
            this.pictureIcon.Location = new System.Drawing.Point(37, 24);
            this.pictureIcon.Margin = new System.Windows.Forms.Padding(2);
            this.pictureIcon.Name = "pictureIcon";
            this.pictureIcon.Size = new System.Drawing.Size(32, 32);
            this.pictureIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureIcon.TabIndex = 6;
            this.pictureIcon.TabStop = false;
            // 
            // labelPromptText
            // 
            this.labelPromptText.AutoSize = true;
            this.labelPromptText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPromptText.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.labelPromptText.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.labelPromptText.Location = new System.Drawing.Point(96, 58);
            this.labelPromptText.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.labelPromptText.Name = "labelPromptText";
            this.labelPromptText.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.labelPromptText.Size = new System.Drawing.Size(514, 41);
            this.labelPromptText.TabIndex = 42;
            this.labelPromptText.Text = "Input";
            // 
            // layoutTop
            // 
            this.layoutTop.AutoSize = true;
            this.layoutTop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.layoutTop.ColumnCount = 5;
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.layoutTop.Controls.Add(this.labelText, 3, 1);
            this.layoutTop.Controls.Add(this.labelPromptText, 3, 2);
            this.layoutTop.Controls.Add(this.pictureIcon, 1, 1);
            this.layoutTop.Controls.Add(this.comboVoidTypeNames, 3, 3);
            this.layoutTop.Controls.Add(this.textRemarks, 3, 4);
            this.layoutTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutTop.Location = new System.Drawing.Point(0, 0);
            this.layoutTop.Margin = new System.Windows.Forms.Padding(0);
            this.layoutTop.Name = "layoutTop";
            this.layoutTop.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.layoutTop.RowCount = 6;
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.layoutTop.Size = new System.Drawing.Size(626, 237);
            this.layoutTop.TabIndex = 102;
            // 
            // comboVoidTypeNames
            // 
            this.comboVoidTypeNames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboVoidTypeNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboVoidTypeNames.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboVoidTypeNames.FormattingEnabled = true;
            this.comboVoidTypeNames.Location = new System.Drawing.Point(99, 102);
            this.comboVoidTypeNames.Name = "comboVoidTypeNames";
            this.comboVoidTypeNames.Size = new System.Drawing.Size(511, 29);
            this.comboVoidTypeNames.TabIndex = 43;
            this.comboVoidTypeNames.TextChanged += new System.EventHandler(this.comboVoidTypeNames_TextChanged);
            // 
            // textRemarks
            // 
            this.textRemarks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textRemarks.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textRemarks.Location = new System.Drawing.Point(99, 137);
            this.textRemarks.Name = "textRemarks";
            this.textRemarks.Size = new System.Drawing.Size(511, 29);
            this.textRemarks.TabIndex = 44;
            // 
            // CustomMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(626, 287);
            this.Controls.Add(this.layoutTop);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomMessageBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Warning";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureIcon)).EndInit();
            this.layoutTop.ResumeLayout(false);
            this.layoutTop.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureIcon;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label labelPromptText;
        private System.Windows.Forms.TableLayoutPanel layoutTop;
        public System.Windows.Forms.ComboBox comboVoidTypeNames;
        public System.Windows.Forms.TextBox textRemarks;
    }
}