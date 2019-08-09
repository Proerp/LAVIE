namespace TotalSmartCoding.Views.Mains
{
    partial class SsrsViewer
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
            this.ssrsMainViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // ssrsMainViewer
            // 
            this.ssrsMainViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ssrsMainViewer.Location = new System.Drawing.Point(0, 0);
            this.ssrsMainViewer.Name = "ssrsMainViewer";
            this.ssrsMainViewer.PromptAreaCollapsed = true;
            this.ssrsMainViewer.Size = new System.Drawing.Size(1463, 868);
            this.ssrsMainViewer.TabIndex = 0;
            // 
            // SsrsViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1463, 868);
            this.Controls.Add(this.ssrsMainViewer);
            this.Name = "SsrsViewer";
            this.Text = "Print Preview";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SsrsViewer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer ssrsMainViewer;
    }
}