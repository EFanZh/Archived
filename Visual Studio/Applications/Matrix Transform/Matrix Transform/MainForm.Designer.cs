namespace MatrixTransform
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
            this.textBoxMatrixA11 = new System.Windows.Forms.TextBox();
            this.textBoxMatrixA12 = new System.Windows.Forms.TextBox();
            this.textBoxMatrixA21 = new System.Windows.Forms.TextBox();
            this.textBoxMatrixA22 = new System.Windows.Forms.TextBox();
            this.panelMatrix = new System.Windows.Forms.Panel();
            this.panelCanvas = new System.Windows.Forms.Panel();
            this.panelMatrix.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxMatrixA11
            // 
            this.textBoxMatrixA11.Location = new System.Drawing.Point(3, 3);
            this.textBoxMatrixA11.Name = "textBoxMatrixA11";
            this.textBoxMatrixA11.Size = new System.Drawing.Size(46, 23);
            this.textBoxMatrixA11.TabIndex = 0;
            this.textBoxMatrixA11.Text = "1";
            this.textBoxMatrixA11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxMatrixA11.TextChanged += new System.EventHandler(this.textBoxMatrix_TextChanged);
            // 
            // textBoxMatrixA12
            // 
            this.textBoxMatrixA12.Location = new System.Drawing.Point(55, 3);
            this.textBoxMatrixA12.Name = "textBoxMatrixA12";
            this.textBoxMatrixA12.Size = new System.Drawing.Size(46, 23);
            this.textBoxMatrixA12.TabIndex = 1;
            this.textBoxMatrixA12.Text = "0";
            this.textBoxMatrixA12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxMatrixA12.TextChanged += new System.EventHandler(this.textBoxMatrix_TextChanged);
            // 
            // textBoxMatrixA21
            // 
            this.textBoxMatrixA21.Location = new System.Drawing.Point(3, 32);
            this.textBoxMatrixA21.Name = "textBoxMatrixA21";
            this.textBoxMatrixA21.Size = new System.Drawing.Size(46, 23);
            this.textBoxMatrixA21.TabIndex = 2;
            this.textBoxMatrixA21.Text = "0";
            this.textBoxMatrixA21.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxMatrixA21.TextChanged += new System.EventHandler(this.textBoxMatrix_TextChanged);
            // 
            // textBoxMatrixA22
            // 
            this.textBoxMatrixA22.Location = new System.Drawing.Point(55, 32);
            this.textBoxMatrixA22.Name = "textBoxMatrixA22";
            this.textBoxMatrixA22.Size = new System.Drawing.Size(46, 23);
            this.textBoxMatrixA22.TabIndex = 3;
            this.textBoxMatrixA22.Text = "1";
            this.textBoxMatrixA22.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxMatrixA22.TextChanged += new System.EventHandler(this.textBoxMatrix_TextChanged);
            // 
            // panelMatrix
            // 
            this.panelMatrix.Controls.Add(this.textBoxMatrixA22);
            this.panelMatrix.Controls.Add(this.textBoxMatrixA21);
            this.panelMatrix.Controls.Add(this.textBoxMatrixA12);
            this.panelMatrix.Controls.Add(this.textBoxMatrixA11);
            this.panelMatrix.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelMatrix.Location = new System.Drawing.Point(691, 0);
            this.panelMatrix.Name = "panelMatrix";
            this.panelMatrix.Size = new System.Drawing.Size(104, 453);
            this.panelMatrix.TabIndex = 1;
            // 
            // panelCanvas
            // 
            this.panelCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCanvas.Location = new System.Drawing.Point(0, 0);
            this.panelCanvas.Name = "panelCanvas";
            this.panelCanvas.Size = new System.Drawing.Size(691, 453);
            this.panelCanvas.TabIndex = 0;
            this.panelCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.panelCanvas_Paint);
            this.panelCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelCanvas_MouseDown);
            this.panelCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelCanvas_MouseMove);
            this.panelCanvas.Resize += new System.EventHandler(this.panelCanvas_Resize);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 453);
            this.Controls.Add(this.panelCanvas);
            this.Controls.Add(this.panelMatrix);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "MainForm";
            this.Text = "Matrix Transform";
            this.panelMatrix.ResumeLayout(false);
            this.panelMatrix.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxMatrixA11;
        private System.Windows.Forms.TextBox textBoxMatrixA12;
        private System.Windows.Forms.TextBox textBoxMatrixA21;
        private System.Windows.Forms.TextBox textBoxMatrixA22;
        private System.Windows.Forms.Panel panelMatrix;
        private System.Windows.Forms.Panel panelCanvas;
    }
}

