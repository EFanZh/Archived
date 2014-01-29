namespace LCDNumbers
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBar3 = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.trackBar4 = new System.Windows.Forms.TrackBar();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar4)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.trackBar1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.trackBar2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.trackBar3, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.trackBar4, 1, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(466, 324);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(357, 206);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 51);
            this.label1.TabIndex = 0;
            this.label1.Text = "Width:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // trackBar1
            // 
            this.trackBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar1.Location = new System.Drawing.Point(75, 3);
            this.trackBar1.Maximum = 1280;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(279, 45);
            this.trackBar1.TabIndex = 1;
            this.trackBar1.Value = 180;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 51);
            this.label2.TabIndex = 2;
            this.label2.Text = "Height:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // trackBar2
            // 
            this.trackBar2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar2.Location = new System.Drawing.Point(75, 54);
            this.trackBar2.Maximum = 1280;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(279, 45);
            this.trackBar2.TabIndex = 3;
            this.trackBar2.Value = 360;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 51);
            this.label3.TabIndex = 4;
            this.label3.Text = "Part Width:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // trackBar3
            // 
            this.trackBar3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar3.Location = new System.Drawing.Point(75, 105);
            this.trackBar3.Maximum = 1280;
            this.trackBar3.Name = "trackBar3";
            this.trackBar3.Size = new System.Drawing.Size(279, 45);
            this.trackBar3.TabIndex = 5;
            this.trackBar3.Value = 32;
            this.trackBar3.Scroll += new System.EventHandler(this.trackBar_Scroll);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 53);
            this.label4.TabIndex = 6;
            this.label4.Text = "Part Sep:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // trackBar4
            // 
            this.trackBar4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar4.Location = new System.Drawing.Point(75, 156);
            this.trackBar4.Maximum = 1280;
            this.trackBar4.Name = "trackBar4";
            this.trackBar4.Size = new System.Drawing.Size(279, 47);
            this.trackBar4.TabIndex = 7;
            this.trackBar4.Value = 8;
            this.trackBar4.Scroll += new System.EventHandler(this.trackBar_Scroll);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 542);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "MainForm";
            this.Text = "LCD Numbers";
            this.ClientSizeChanged += new System.EventHandler(this.MainForm_ClientSizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion Windows Form Designer generated code

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBar3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar trackBar4;
    }
}
