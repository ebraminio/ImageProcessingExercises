namespace ImageProcessing3
{
    partial class MainForm
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
            this.vertextShaderTextBox = new ScintillaNET.Scintilla();
            this.fragmentShaderTextBox = new ScintillaNET.Scintilla();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.glControl = new OpenTK.GLControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // vertextShaderTextBox
            // 
            this.vertextShaderTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vertextShaderTextBox.IndentationGuides = ScintillaNET.IndentView.LookBoth;
            this.vertextShaderTextBox.IndentWidth = 2;
            this.vertextShaderTextBox.Lexer = ScintillaNET.Lexer.Cpp;
            this.vertextShaderTextBox.Location = new System.Drawing.Point(3, 3);
            this.vertextShaderTextBox.Name = "vertextShaderTextBox";
            this.vertextShaderTextBox.Size = new System.Drawing.Size(866, 358);
            this.vertextShaderTextBox.TabIndex = 1;
            this.vertextShaderTextBox.TabWidth = 2;
            this.vertextShaderTextBox.TextChanged += new System.EventHandler(this.vertextShaderTextBox_TextChanged);
            // 
            // fragmentShaderTextBox
            // 
            this.fragmentShaderTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fragmentShaderTextBox.IndentationGuides = ScintillaNET.IndentView.LookBoth;
            this.fragmentShaderTextBox.IndentWidth = 2;
            this.fragmentShaderTextBox.Lexer = ScintillaNET.Lexer.Cpp;
            this.fragmentShaderTextBox.Location = new System.Drawing.Point(3, 367);
            this.fragmentShaderTextBox.Name = "fragmentShaderTextBox";
            this.fragmentShaderTextBox.Size = new System.Drawing.Size(866, 359);
            this.fragmentShaderTextBox.TabIndex = 1;
            this.fragmentShaderTextBox.TabWidth = 2;
            this.fragmentShaderTextBox.TextChanged += new System.EventHandler(this.fragmentShaderTextBox_TextChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.vertextShaderTextBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.fragmentShaderTextBox, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(872, 729);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.glControl, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1757, 735);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // glControl
            // 
            this.glControl.BackColor = System.Drawing.Color.Black;
            this.glControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControl.Location = new System.Drawing.Point(884, 6);
            this.glControl.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.glControl.Name = "glControl";
            this.glControl.Size = new System.Drawing.Size(867, 723);
            this.glControl.TabIndex = 6;
            this.glControl.VSync = false;
            this.glControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControl_MouseDown);
            this.glControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControl_MouseMove);
            this.glControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glControl_MouseUp);
            this.glControl.Resize += new System.EventHandler(this.glControl_Resize);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1757, 735);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private ScintillaNET.Scintilla vertextShaderTextBox;
        private ScintillaNET.Scintilla fragmentShaderTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private OpenTK.GLControl glControl;
    }
}