namespace ImageProcessing1
{
    partial class TrackDialog
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
            this.threshouldTrackBar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.threshouldTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // threshouldTrackBar
            // 
            this.threshouldTrackBar.AutoSize = false;
            this.threshouldTrackBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.threshouldTrackBar.LargeChange = 40;
            this.threshouldTrackBar.Location = new System.Drawing.Point(0, 0);
            this.threshouldTrackBar.Maximum = 255;
            this.threshouldTrackBar.Name = "threshouldTrackBar";
            this.threshouldTrackBar.Size = new System.Drawing.Size(583, 90);
            this.threshouldTrackBar.TabIndex = 6;
            this.threshouldTrackBar.Scroll += new System.EventHandler(this.threshouldTrackBar_Scroll);
            // 
            // TrackDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 90);
            this.Controls.Add(this.threshouldTrackBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TrackDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "-";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.threshouldTrackBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TrackBar threshouldTrackBar;
    }
}