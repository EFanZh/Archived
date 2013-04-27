namespace ThreeDDrawing
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
            this.canvas3DMain = new ThreeDDrawing.Canvas3D();
            this.SuspendLayout();
            // 
            // canvas3DMain
            // 
            this.canvas3DMain.BackColor = System.Drawing.Color.White;
            this.canvas3DMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas3DMain.Location = new System.Drawing.Point(0, 0);
            this.canvas3DMain.Name = "canvas3DMain";
            this.canvas3DMain.Size = new System.Drawing.Size(703, 467);
            this.canvas3DMain.TabIndex = 1;
            this.canvas3DMain.ClientSizeChanged += new System.EventHandler(this.canvas3DMain_ClientSizeChanged);
            this.canvas3DMain.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas3DMain_Paint);
            this.canvas3DMain.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.canvas3DMain_PreviewKeyDown);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 467);
            this.Controls.Add(this.canvas3DMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "MainForm";
            this.Text = "3D Drawing";
            this.ResumeLayout(false);

        }

        #endregion

        private Canvas3D canvas3DMain;




    }
}

