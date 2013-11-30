namespace SocketNS
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.spltrMain = new System.Windows.Forms.Splitter();
            this.tlStrpRequest = new System.Windows.Forms.ToolStrip();
            this.tlStrpLblServer = new System.Windows.Forms.ToolStripLabel();
            this.tlStrpTxtBxServer = new System.Windows.Forms.ToolStripTextBox();
            this.tlStrpLblPort = new System.Windows.Forms.ToolStripLabel();
            this.tlStrpTxtBxPort = new System.Windows.Forms.ToolStripTextBox();
            this.tlStrpSprtr1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlStrpLblRequestEncoding = new System.Windows.Forms.ToolStripLabel();
            this.tlStrpCmbBxRequestEncoding = new System.Windows.Forms.ToolStripComboBox();
            this.tlStrpBtnSendRequest = new System.Windows.Forms.ToolStripButton();
            this.txtBxRequest = new System.Windows.Forms.TextBox();
            this.tlStrpRespond = new System.Windows.Forms.ToolStrip();
            this.tlStrpLblRespondEncoding = new System.Windows.Forms.ToolStripLabel();
            this.tlStrpCmbBxRespondEncoding = new System.Windows.Forms.ToolStripComboBox();
            this.tlStrpBtnClearRespond = new System.Windows.Forms.ToolStripButton();
            this.txtBxRespond = new System.Windows.Forms.TextBox();
            this.tlStrpRequest.SuspendLayout();
            this.tlStrpRespond.SuspendLayout();
            this.SuspendLayout();
            // 
            // spltrMain
            // 
            this.spltrMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.spltrMain.Location = new System.Drawing.Point(0, 255);
            this.spltrMain.Name = "spltrMain";
            this.spltrMain.Size = new System.Drawing.Size(744, 3);
            this.spltrMain.TabIndex = 1;
            this.spltrMain.TabStop = false;
            // 
            // tlStrpRequest
            // 
            this.tlStrpRequest.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlStrpRequest.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlStrpLblServer,
            this.tlStrpTxtBxServer,
            this.tlStrpLblPort,
            this.tlStrpTxtBxPort,
            this.tlStrpSprtr1,
            this.tlStrpLblRequestEncoding,
            this.tlStrpCmbBxRequestEncoding,
            this.tlStrpBtnSendRequest});
            this.tlStrpRequest.Location = new System.Drawing.Point(0, 0);
            this.tlStrpRequest.Name = "tlStrpRequest";
            this.tlStrpRequest.Size = new System.Drawing.Size(744, 25);
            this.tlStrpRequest.TabIndex = 0;
            this.tlStrpRequest.Text = "toolStrip1";
            // 
            // tlStrpLblServer
            // 
            this.tlStrpLblServer.Name = "tlStrpLblServer";
            this.tlStrpLblServer.Size = new System.Drawing.Size(56, 22);
            this.tlStrpLblServer.Text = "Server(&S):";
            // 
            // tlStrpTxtBxServer
            // 
            this.tlStrpTxtBxServer.Name = "tlStrpTxtBxServer";
            this.tlStrpTxtBxServer.Size = new System.Drawing.Size(116, 25);
            // 
            // tlStrpLblPort
            // 
            this.tlStrpLblPort.Name = "tlStrpLblPort";
            this.tlStrpLblPort.Size = new System.Drawing.Size(47, 22);
            this.tlStrpLblPort.Text = "Port(&P):";
            // 
            // tlStrpTxtBxPort
            // 
            this.tlStrpTxtBxPort.Name = "tlStrpTxtBxPort";
            this.tlStrpTxtBxPort.Size = new System.Drawing.Size(116, 25);
            // 
            // tlStrpSprtr1
            // 
            this.tlStrpSprtr1.Name = "tlStrpSprtr1";
            this.tlStrpSprtr1.Size = new System.Drawing.Size(6, 25);
            // 
            // tlStrpLblRequestEncoding
            // 
            this.tlStrpLblRequestEncoding.Name = "tlStrpLblRequestEncoding";
            this.tlStrpLblRequestEncoding.Size = new System.Drawing.Size(74, 22);
            this.tlStrpLblRequestEncoding.Text = "Encoding(&E):";
            // 
            // tlStrpCmbBxRequestEncoding
            // 
            this.tlStrpCmbBxRequestEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tlStrpCmbBxRequestEncoding.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.tlStrpCmbBxRequestEncoding.Name = "tlStrpCmbBxRequestEncoding";
            this.tlStrpCmbBxRequestEncoding.Size = new System.Drawing.Size(139, 25);
            // 
            // tlStrpBtnSendRequest
            // 
            this.tlStrpBtnSendRequest.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tlStrpBtnSendRequest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlStrpBtnSendRequest.Name = "tlStrpBtnSendRequest";
            this.tlStrpBtnSendRequest.Size = new System.Drawing.Size(54, 22);
            this.tlStrpBtnSendRequest.Text = "Send(&N)";
            this.tlStrpBtnSendRequest.Click += new System.EventHandler(this.tlStrpBtnSendRequest_Click);
            // 
            // txtBxRequest
            // 
            this.txtBxRequest.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtBxRequest.Location = new System.Drawing.Point(0, 25);
            this.txtBxRequest.Multiline = true;
            this.txtBxRequest.Name = "txtBxRequest";
            this.txtBxRequest.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBxRequest.Size = new System.Drawing.Size(744, 230);
            this.txtBxRequest.TabIndex = 1;
            this.txtBxRequest.WordWrap = false;
            // 
            // tlStrpRespond
            // 
            this.tlStrpRespond.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlStrpRespond.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlStrpLblRespondEncoding,
            this.tlStrpCmbBxRespondEncoding,
            this.tlStrpBtnClearRespond});
            this.tlStrpRespond.Location = new System.Drawing.Point(0, 258);
            this.tlStrpRespond.Name = "tlStrpRespond";
            this.tlStrpRespond.Size = new System.Drawing.Size(744, 25);
            this.tlStrpRespond.TabIndex = 0;
            this.tlStrpRespond.Text = "toolStrip2";
            // 
            // tlStrpLblRespondEncoding
            // 
            this.tlStrpLblRespondEncoding.Name = "tlStrpLblRespondEncoding";
            this.tlStrpLblRespondEncoding.Size = new System.Drawing.Size(76, 22);
            this.tlStrpLblRespondEncoding.Text = "Encoding(&C):";
            // 
            // tlStrpCmbBxRespondEncoding
            // 
            this.tlStrpCmbBxRespondEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tlStrpCmbBxRespondEncoding.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.tlStrpCmbBxRespondEncoding.Name = "tlStrpCmbBxRespondEncoding";
            this.tlStrpCmbBxRespondEncoding.Size = new System.Drawing.Size(139, 25);
            // 
            // tlStrpBtnClearRespond
            // 
            this.tlStrpBtnClearRespond.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tlStrpBtnClearRespond.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlStrpBtnClearRespond.Name = "tlStrpBtnClearRespond";
            this.tlStrpBtnClearRespond.Size = new System.Drawing.Size(52, 22);
            this.tlStrpBtnClearRespond.Text = "Clear(&L)";
            this.tlStrpBtnClearRespond.Click += new System.EventHandler(this.tlStrpBtnClearRespond_Click);
            // 
            // txtBxRespond
            // 
            this.txtBxRespond.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBxRespond.Location = new System.Drawing.Point(0, 283);
            this.txtBxRespond.Multiline = true;
            this.txtBxRespond.Name = "txtBxRespond";
            this.txtBxRespond.ReadOnly = true;
            this.txtBxRespond.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBxRespond.Size = new System.Drawing.Size(744, 194);
            this.txtBxRespond.TabIndex = 1;
            this.txtBxRespond.WordWrap = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 477);
            this.Controls.Add(this.txtBxRespond);
            this.Controls.Add(this.tlStrpRespond);
            this.Controls.Add(this.spltrMain);
            this.Controls.Add(this.txtBxRequest);
            this.Controls.Add(this.tlStrpRequest);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "MainForm";
            this.Text = "Socket";
            this.tlStrpRequest.ResumeLayout(false);
            this.tlStrpRequest.PerformLayout();
            this.tlStrpRespond.ResumeLayout(false);
            this.tlStrpRespond.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Splitter spltrMain;
        private System.Windows.Forms.TextBox txtBxRequest;
        private System.Windows.Forms.ToolStrip tlStrpRequest;
        private System.Windows.Forms.ToolStripLabel tlStrpLblServer;
        private System.Windows.Forms.ToolStripTextBox tlStrpTxtBxServer;
        private System.Windows.Forms.ToolStripLabel tlStrpLblPort;
        private System.Windows.Forms.ToolStripTextBox tlStrpTxtBxPort;
        private System.Windows.Forms.ToolStripLabel tlStrpLblRequestEncoding;
        private System.Windows.Forms.ToolStripComboBox tlStrpCmbBxRequestEncoding;
        private System.Windows.Forms.TextBox txtBxRespond;
        private System.Windows.Forms.ToolStrip tlStrpRespond;
        private System.Windows.Forms.ToolStripButton tlStrpBtnSendRequest;
        private System.Windows.Forms.ToolStripLabel tlStrpLblRespondEncoding;
        private System.Windows.Forms.ToolStripComboBox tlStrpCmbBxRespondEncoding;
        private System.Windows.Forms.ToolStripSeparator tlStrpSprtr1;
        private System.Windows.Forms.ToolStripButton tlStrpBtnClearRespond;

    }
}

