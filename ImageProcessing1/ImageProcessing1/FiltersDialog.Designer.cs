namespace ImageProcessing1
{
    partial class FiltersDialog
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
            this.label4 = new System.Windows.Forms.Label();
            this.biasTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.embossButton = new System.Windows.Forms.Button();
            this.sharpenButton = new System.Windows.Forms.Button();
            this.findEdgesButton = new System.Windows.Forms.Button();
            this.motionBlurButton = new System.Windows.Forms.Button();
            this.blurButton = new System.Windows.Forms.Button();
            this.factorTextBox = new System.Windows.Forms.TextBox();
            this.transformMatrixTextBox = new System.Windows.Forms.TextBox();
            this.applyButton = new System.Windows.Forms.Button();
            this.laplacianButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 334);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 25);
            this.label4.TabIndex = 20;
            this.label4.Text = "Bias:";
            // 
            // biasTextBox
            // 
            this.biasTextBox.Location = new System.Drawing.Point(97, 334);
            this.biasTextBox.Name = "biasTextBox";
            this.biasTextBox.Size = new System.Drawing.Size(156, 31);
            this.biasTextBox.TabIndex = 19;
            this.biasTextBox.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 297);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 25);
            this.label3.TabIndex = 17;
            this.label3.Text = "Factor:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 25);
            this.label2.TabIndex = 18;
            this.label2.Text = "Matrix:";
            // 
            // embossButton
            // 
            this.embossButton.Location = new System.Drawing.Point(339, 225);
            this.embossButton.Name = "embossButton";
            this.embossButton.Size = new System.Drawing.Size(145, 47);
            this.embossButton.TabIndex = 12;
            this.embossButton.Text = "Emboss";
            this.embossButton.UseVisualStyleBackColor = true;
            this.embossButton.Click += new System.EventHandler(this.embossButton_Click);
            // 
            // sharpenButton
            // 
            this.sharpenButton.Location = new System.Drawing.Point(339, 172);
            this.sharpenButton.Name = "sharpenButton";
            this.sharpenButton.Size = new System.Drawing.Size(145, 47);
            this.sharpenButton.TabIndex = 13;
            this.sharpenButton.Text = "Sharpen";
            this.sharpenButton.UseVisualStyleBackColor = true;
            this.sharpenButton.Click += new System.EventHandler(this.sharpenButton_Click);
            // 
            // findEdgesButton
            // 
            this.findEdgesButton.Location = new System.Drawing.Point(339, 119);
            this.findEdgesButton.Name = "findEdgesButton";
            this.findEdgesButton.Size = new System.Drawing.Size(145, 47);
            this.findEdgesButton.TabIndex = 14;
            this.findEdgesButton.Text = "Find Edges";
            this.findEdgesButton.UseVisualStyleBackColor = true;
            this.findEdgesButton.Click += new System.EventHandler(this.findEdgesButton_Click);
            // 
            // motionBlurButton
            // 
            this.motionBlurButton.Location = new System.Drawing.Point(339, 66);
            this.motionBlurButton.Name = "motionBlurButton";
            this.motionBlurButton.Size = new System.Drawing.Size(145, 47);
            this.motionBlurButton.TabIndex = 15;
            this.motionBlurButton.Text = "Motion Blur";
            this.motionBlurButton.UseVisualStyleBackColor = true;
            this.motionBlurButton.Click += new System.EventHandler(this.motionBlurButton_Click);
            // 
            // blurButton
            // 
            this.blurButton.Location = new System.Drawing.Point(340, 13);
            this.blurButton.Name = "blurButton";
            this.blurButton.Size = new System.Drawing.Size(144, 47);
            this.blurButton.TabIndex = 16;
            this.blurButton.Text = "Blur";
            this.blurButton.UseVisualStyleBackColor = true;
            this.blurButton.Click += new System.EventHandler(this.blurButton_Click);
            // 
            // factorTextBox
            // 
            this.factorTextBox.Location = new System.Drawing.Point(97, 294);
            this.factorTextBox.Name = "factorTextBox";
            this.factorTextBox.Size = new System.Drawing.Size(156, 31);
            this.factorTextBox.TabIndex = 10;
            this.factorTextBox.Text = "1";
            // 
            // transformMatrixTextBox
            // 
            this.transformMatrixTextBox.Location = new System.Drawing.Point(12, 44);
            this.transformMatrixTextBox.Multiline = true;
            this.transformMatrixTextBox.Name = "transformMatrixTextBox";
            this.transformMatrixTextBox.Size = new System.Drawing.Size(322, 225);
            this.transformMatrixTextBox.TabIndex = 11;
            this.transformMatrixTextBox.Text = "0, 0, 0,\r\n0, 1, 0,\r\n0, 0, 0";
            this.transformMatrixTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(339, 362);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(145, 47);
            this.applyButton.TabIndex = 12;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // laplacianButton
            // 
            this.laplacianButton.Location = new System.Drawing.Point(339, 275);
            this.laplacianButton.Name = "laplacianButton";
            this.laplacianButton.Size = new System.Drawing.Size(145, 47);
            this.laplacianButton.TabIndex = 12;
            this.laplacianButton.Text = "Laplacian";
            this.laplacianButton.UseVisualStyleBackColor = true;
            this.laplacianButton.Click += new System.EventHandler(this.laplacianButton_Click);
            // 
            // FiltersDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 422);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.biasTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.laplacianButton);
            this.Controls.Add(this.embossButton);
            this.Controls.Add(this.sharpenButton);
            this.Controls.Add(this.findEdgesButton);
            this.Controls.Add(this.motionBlurButton);
            this.Controls.Add(this.blurButton);
            this.Controls.Add(this.factorTextBox);
            this.Controls.Add(this.transformMatrixTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FiltersDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Filters";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FiltersDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox biasTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button embossButton;
        private System.Windows.Forms.Button sharpenButton;
        private System.Windows.Forms.Button findEdgesButton;
        private System.Windows.Forms.Button motionBlurButton;
        private System.Windows.Forms.Button blurButton;
        private System.Windows.Forms.TextBox factorTextBox;
        private System.Windows.Forms.TextBox transformMatrixTextBox;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button laplacianButton;
    }
}