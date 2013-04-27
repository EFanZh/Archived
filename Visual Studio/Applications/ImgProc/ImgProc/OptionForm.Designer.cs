namespace ImgProc
{
    partial class OptionForm
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
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPagePlugin = new System.Windows.Forms.TabPage();
            this.buttonAbout = new System.Windows.Forms.Button();
            this.buttonConfig = new System.Windows.Forms.Button();
            this.listViewPlugins = new System.Windows.Forms.ListView();
            this.columnHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderModule = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonClose = new System.Windows.Forms.Button();
            this.tabControlMain.SuspendLayout();
            this.tabPagePlugin.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabPagePlugin);
            this.tabControlMain.Location = new System.Drawing.Point(12, 12);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(520, 309);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabPagePlugin
            // 
            this.tabPagePlugin.Controls.Add(this.buttonAbout);
            this.tabPagePlugin.Controls.Add(this.buttonConfig);
            this.tabPagePlugin.Controls.Add(this.listViewPlugins);
            this.tabPagePlugin.Location = new System.Drawing.Point(4, 24);
            this.tabPagePlugin.Name = "tabPagePlugin";
            this.tabPagePlugin.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePlugin.Size = new System.Drawing.Size(512, 281);
            this.tabPagePlugin.TabIndex = 0;
            this.tabPagePlugin.Text = "插件管理";
            this.tabPagePlugin.UseVisualStyleBackColor = true;
            // 
            // buttonAbout
            // 
            this.buttonAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAbout.Enabled = false;
            this.buttonAbout.Location = new System.Drawing.Point(431, 252);
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(75, 23);
            this.buttonAbout.TabIndex = 2;
            this.buttonAbout.Text = "关于";
            this.buttonAbout.UseVisualStyleBackColor = true;
            this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
            // 
            // buttonConfig
            // 
            this.buttonConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonConfig.Enabled = false;
            this.buttonConfig.Location = new System.Drawing.Point(350, 252);
            this.buttonConfig.Name = "buttonConfig";
            this.buttonConfig.Size = new System.Drawing.Size(75, 23);
            this.buttonConfig.TabIndex = 1;
            this.buttonConfig.Text = "配置";
            this.buttonConfig.UseVisualStyleBackColor = true;
            this.buttonConfig.Click += new System.EventHandler(this.buttonConfig_Click);
            // 
            // listViewPlugins
            // 
            this.listViewPlugins.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewPlugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderType,
            this.columnHeaderName,
            this.columnHeaderModule});
            this.listViewPlugins.FullRowSelect = true;
            this.listViewPlugins.Location = new System.Drawing.Point(6, 6);
            this.listViewPlugins.MultiSelect = false;
            this.listViewPlugins.Name = "listViewPlugins";
            this.listViewPlugins.Size = new System.Drawing.Size(500, 240);
            this.listViewPlugins.TabIndex = 0;
            this.listViewPlugins.UseCompatibleStateImageBehavior = false;
            this.listViewPlugins.View = System.Windows.Forms.View.Details;
            this.listViewPlugins.SelectedIndexChanged += new System.EventHandler(this.listViewPlugins_SelectedIndexChanged);
            // 
            // columnHeaderType
            // 
            this.columnHeaderType.Text = "类型";
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "名称";
            // 
            // columnHeaderModule
            // 
            this.columnHeaderModule.Text = "提供模块";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(457, 327);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "关闭";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // OptionForm
            // 
            this.AcceptButton = this.buttonClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(544, 362);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.tabControlMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "OptionForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选项";
            this.tabControlMain.ResumeLayout(false);
            this.tabPagePlugin.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPagePlugin;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonConfig;
        private System.Windows.Forms.ListView listViewPlugins;
        private System.Windows.Forms.ColumnHeader columnHeaderType;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderModule;
        private System.Windows.Forms.Button buttonAbout;
    }
}
