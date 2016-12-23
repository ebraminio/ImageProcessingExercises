namespace ImageProcessing1
{
    partial class MorphologyDialog
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.discSizeTextBox = new System.Windows.Forms.TextBox();
            this.transformMatrixTextBox = new System.Windows.Forms.TextBox();
            this.erosionButton = new System.Windows.Forms.Button();
            this.dilationButton = new System.Windows.Forms.Button();
            this.closingButton = new System.Windows.Forms.Button();
            this.openingButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 297);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 25);
            this.label3.TabIndex = 17;
            this.label3.Text = "Disc size:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(206, 25);
            this.label2.TabIndex = 18;
            this.label2.Text = "Structering Element:";
            // 
            // discSizeTextBox
            // 
            this.discSizeTextBox.Location = new System.Drawing.Point(123, 294);
            this.discSizeTextBox.Name = "discSizeTextBox";
            this.discSizeTextBox.Size = new System.Drawing.Size(156, 31);
            this.discSizeTextBox.TabIndex = 10;
            this.discSizeTextBox.Text = "3";
            this.discSizeTextBox.TextChanged += new System.EventHandler(this.discSizeTextBox_TextChanged);
            // 
            // transformMatrixTextBox
            // 
            this.transformMatrixTextBox.Location = new System.Drawing.Point(12, 44);
            this.transformMatrixTextBox.Multiline = true;
            this.transformMatrixTextBox.Name = "transformMatrixTextBox";
            this.transformMatrixTextBox.Size = new System.Drawing.Size(322, 225);
            this.transformMatrixTextBox.TabIndex = 11;
            this.transformMatrixTextBox.Text = "0, 1, 0,\r\n1, 1, 1,\r\n0, 1, 0";
            this.transformMatrixTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // erosionButton
            // 
            this.erosionButton.Location = new System.Drawing.Point(339, 66);
            this.erosionButton.Name = "erosionButton";
            this.erosionButton.Size = new System.Drawing.Size(178, 47);
            this.erosionButton.TabIndex = 12;
            this.erosionButton.Text = "Erosion (A ⊖ B)";
            this.erosionButton.UseVisualStyleBackColor = true;
            this.erosionButton.Click += new System.EventHandler(this.erosionButton_Click);
            // 
            // dilationButton
            // 
            this.dilationButton.Location = new System.Drawing.Point(339, 13);
            this.dilationButton.Name = "dilationButton";
            this.dilationButton.Size = new System.Drawing.Size(178, 47);
            this.dilationButton.TabIndex = 21;
            this.dilationButton.Text = "Dilation (A ⊕ B)";
            this.dilationButton.UseVisualStyleBackColor = true;
            this.dilationButton.Click += new System.EventHandler(this.dilationButton_Click);
            // 
            // closingButton
            // 
            this.closingButton.Location = new System.Drawing.Point(339, 200);
            this.closingButton.Name = "closingButton";
            this.closingButton.Size = new System.Drawing.Size(178, 69);
            this.closingButton.TabIndex = 12;
            this.closingButton.Text = "Closing\r\n(A ⊕ B) ⊖ B";
            this.closingButton.UseVisualStyleBackColor = true;
            this.closingButton.Click += new System.EventHandler(this.closingButton_Click);
            // 
            // openingButton
            // 
            this.openingButton.Location = new System.Drawing.Point(339, 119);
            this.openingButton.Name = "openingButton";
            this.openingButton.Size = new System.Drawing.Size(178, 75);
            this.openingButton.TabIndex = 21;
            this.openingButton.Text = "Opening\r\n(A ⊖ B) ⊕ B";
            this.openingButton.UseVisualStyleBackColor = true;
            this.openingButton.Click += new System.EventHandler(this.openingButton_Click);
            // 
            // MorphologyDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 344);
            this.Controls.Add(this.openingButton);
            this.Controls.Add(this.dilationButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.closingButton);
            this.Controls.Add(this.erosionButton);
            this.Controls.Add(this.discSizeTextBox);
            this.Controls.Add(this.transformMatrixTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MorphologyDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Filters";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox discSizeTextBox;
        private System.Windows.Forms.TextBox transformMatrixTextBox;
        private System.Windows.Forms.Button erosionButton;
        private System.Windows.Forms.Button dilationButton;
        private System.Windows.Forms.Button closingButton;
        private System.Windows.Forms.Button openingButton;
    }
}