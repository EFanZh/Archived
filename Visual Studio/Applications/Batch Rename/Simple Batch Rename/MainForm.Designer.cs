namespace SimpleBatchRename
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.buttonImport = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.listViewFileList = new System.Windows.Forms.ListView();
            this.columnHeaderFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderFilePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTargetFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStripFileList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemFileListImport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemFileListRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemFileListSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemFileListClear = new System.Windows.Forms.ToolStripMenuItem();
            this.labelFileNameFormat = new System.Windows.Forms.Label();
            this.labelStartId = new System.Windows.Forms.Label();
            this.comboBoxExtensionFormat = new System.Windows.Forms.ComboBox();
            this.labelExtensionFormat = new System.Windows.Forms.Label();
            this.buttonRename = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.labelSplitter2 = new System.Windows.Forms.Label();
            this.numericUpDownStartId = new System.Windows.Forms.NumericUpDown();
            this.comboBoxFileNameFormat = new System.Windows.Forms.ComboBox();
            this.labelSplitter1 = new System.Windows.Forms.Label();
            this.openFileDialogImport = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStripFileList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartId)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonImport
            // 
            this.buttonImport.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonImport.Location = new System.Drawing.Point(12, 12);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(75, 23);
            this.buttonImport.TabIndex = 0;
            this.buttonImport.Text = "导入";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Enabled = false;
            this.buttonRemove.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonRemove.Location = new System.Drawing.Point(93, 12);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove.TabIndex = 1;
            this.buttonRemove.Text = "移除";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Enabled = false;
            this.buttonClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonClear.Location = new System.Drawing.Point(174, 12);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 2;
            this.buttonClear.Text = "清空";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // listViewFileList
            // 
            this.listViewFileList.AllowDrop = true;
            this.listViewFileList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewFileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderFileName,
            this.columnHeaderFilePath,
            this.columnHeaderTargetFileName});
            this.listViewFileList.ContextMenuStrip = this.contextMenuStripFileList;
            this.listViewFileList.FullRowSelect = true;
            this.listViewFileList.HideSelection = false;
            this.listViewFileList.Location = new System.Drawing.Point(12, 41);
            this.listViewFileList.Name = "listViewFileList";
            this.listViewFileList.Size = new System.Drawing.Size(920, 417);
            this.listViewFileList.TabIndex = 3;
            this.listViewFileList.UseCompatibleStateImageBehavior = false;
            this.listViewFileList.View = System.Windows.Forms.View.Details;
            this.listViewFileList.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listViewFileList_ItemDrag);
            this.listViewFileList.SelectedIndexChanged += new System.EventHandler(this.listViewFileList_SelectedIndexChanged);
            this.listViewFileList.DragDrop += new System.Windows.Forms.DragEventHandler(this.listViewFileList_DragDrop);
            this.listViewFileList.DragEnter += new System.Windows.Forms.DragEventHandler(this.listViewFileList_DragEnter);
            this.listViewFileList.DragOver += new System.Windows.Forms.DragEventHandler(this.listViewFileList_DragOver);
            this.listViewFileList.DragLeave += new System.EventHandler(this.listViewFileList_DragLeave);
            this.listViewFileList.DoubleClick += new System.EventHandler(this.listViewFileList_DoubleClick);
            // 
            // columnHeaderFileName
            // 
            this.columnHeaderFileName.Text = "文件名";
            // 
            // columnHeaderFilePath
            // 
            this.columnHeaderFilePath.Text = "文件路径";
            // 
            // columnHeaderTargetFileName
            // 
            this.columnHeaderTargetFileName.Text = "目标文件名";
            // 
            // contextMenuStripFileList
            // 
            this.contextMenuStripFileList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFileListImport,
            this.toolStripMenuItemFileListRemove,
            this.toolStripMenuItemFileListSelectAll,
            this.toolStripMenuItemFileListClear});
            this.contextMenuStripFileList.Name = "contextMenuStripFileList";
            this.contextMenuStripFileList.Size = new System.Drawing.Size(143, 92);
            // 
            // toolStripMenuItemFileListImport
            // 
            this.toolStripMenuItemFileListImport.Name = "toolStripMenuItemFileListImport";
            this.toolStripMenuItemFileListImport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.toolStripMenuItemFileListImport.Size = new System.Drawing.Size(142, 22);
            this.toolStripMenuItemFileListImport.Text = "导入";
            this.toolStripMenuItemFileListImport.Click += new System.EventHandler(this.toolStripMenuItemFileListImport_Click);
            // 
            // toolStripMenuItemFileListRemove
            // 
            this.toolStripMenuItemFileListRemove.Enabled = false;
            this.toolStripMenuItemFileListRemove.Name = "toolStripMenuItemFileListRemove";
            this.toolStripMenuItemFileListRemove.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.toolStripMenuItemFileListRemove.Size = new System.Drawing.Size(142, 22);
            this.toolStripMenuItemFileListRemove.Text = "移除";
            this.toolStripMenuItemFileListRemove.Click += new System.EventHandler(this.toolStripMenuItemFileListRemove_Click);
            // 
            // toolStripMenuItemFileListSelectAll
            // 
            this.toolStripMenuItemFileListSelectAll.Name = "toolStripMenuItemFileListSelectAll";
            this.toolStripMenuItemFileListSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.toolStripMenuItemFileListSelectAll.Size = new System.Drawing.Size(142, 22);
            this.toolStripMenuItemFileListSelectAll.Text = "全选";
            this.toolStripMenuItemFileListSelectAll.Click += new System.EventHandler(this.toolStripMenuItemFileListSelectAll_Click);
            // 
            // toolStripMenuItemFileListClear
            // 
            this.toolStripMenuItemFileListClear.Enabled = false;
            this.toolStripMenuItemFileListClear.Name = "toolStripMenuItemFileListClear";
            this.toolStripMenuItemFileListClear.Size = new System.Drawing.Size(142, 22);
            this.toolStripMenuItemFileListClear.Text = "清空";
            this.toolStripMenuItemFileListClear.Click += new System.EventHandler(this.toolStripMenuItemFileListClear_Click);
            // 
            // labelFileNameFormat
            // 
            this.labelFileNameFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelFileNameFormat.AutoSize = true;
            this.labelFileNameFormat.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelFileNameFormat.Location = new System.Drawing.Point(12, 475);
            this.labelFileNameFormat.Name = "labelFileNameFormat";
            this.labelFileNameFormat.Size = new System.Drawing.Size(75, 15);
            this.labelFileNameFormat.TabIndex = 5;
            this.labelFileNameFormat.Text = "文件名格式:";
            // 
            // labelStartId
            // 
            this.labelStartId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelStartId.AutoSize = true;
            this.labelStartId.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelStartId.Location = new System.Drawing.Point(12, 503);
            this.labelStartId.Name = "labelStartId";
            this.labelStartId.Size = new System.Drawing.Size(62, 15);
            this.labelStartId.TabIndex = 7;
            this.labelStartId.Text = "起始编号:";
            // 
            // comboBoxExtensionFormat
            // 
            this.comboBoxExtensionFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxExtensionFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxExtensionFormat.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxExtensionFormat.FormattingEnabled = true;
            this.comboBoxExtensionFormat.Items.AddRange(new object[] {
            "保持不变",
            "转换为小写",
            "转换为大写"});
            this.comboBoxExtensionFormat.Location = new System.Drawing.Point(88, 530);
            this.comboBoxExtensionFormat.Name = "comboBoxExtensionFormat";
            this.comboBoxExtensionFormat.Size = new System.Drawing.Size(844, 23);
            this.comboBoxExtensionFormat.TabIndex = 10;
            this.comboBoxExtensionFormat.SelectedIndexChanged += new System.EventHandler(this.comboBoxExtensionFormat_SelectedIndexChanged);
            // 
            // labelExtensionFormat
            // 
            this.labelExtensionFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelExtensionFormat.AutoSize = true;
            this.labelExtensionFormat.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelExtensionFormat.Location = new System.Drawing.Point(12, 533);
            this.labelExtensionFormat.Name = "labelExtensionFormat";
            this.labelExtensionFormat.Size = new System.Drawing.Size(75, 15);
            this.labelExtensionFormat.TabIndex = 9;
            this.labelExtensionFormat.Text = "扩展名格式:";
            // 
            // buttonRename
            // 
            this.buttonRename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRename.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonRename.Location = new System.Drawing.Point(776, 567);
            this.buttonRename.Name = "buttonRename";
            this.buttonRename.Size = new System.Drawing.Size(75, 23);
            this.buttonRename.TabIndex = 12;
            this.buttonRename.Text = "重命名";
            this.buttonRename.UseVisualStyleBackColor = true;
            this.buttonRename.Click += new System.EventHandler(this.buttonRename_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonExit.Location = new System.Drawing.Point(857, 567);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(75, 23);
            this.buttonExit.TabIndex = 13;
            this.buttonExit.Text = "退出";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // labelSplitter2
            // 
            this.labelSplitter2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSplitter2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSplitter2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelSplitter2.Location = new System.Drawing.Point(12, 559);
            this.labelSplitter2.Margin = new System.Windows.Forms.Padding(3);
            this.labelSplitter2.Name = "labelSplitter2";
            this.labelSplitter2.Size = new System.Drawing.Size(920, 2);
            this.labelSplitter2.TabIndex = 11;
            // 
            // numericUpDownStartId
            // 
            this.numericUpDownStartId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownStartId.Location = new System.Drawing.Point(88, 501);
            this.numericUpDownStartId.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownStartId.Name = "numericUpDownStartId";
            this.numericUpDownStartId.Size = new System.Drawing.Size(844, 23);
            this.numericUpDownStartId.TabIndex = 8;
            this.numericUpDownStartId.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownStartId.ValueChanged += new System.EventHandler(this.numericUpDownStartId_ValueChanged);
            // 
            // comboBoxFileNameFormat
            // 
            this.comboBoxFileNameFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxFileNameFormat.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxFileNameFormat.FormattingEnabled = true;
            this.comboBoxFileNameFormat.Location = new System.Drawing.Point(88, 472);
            this.comboBoxFileNameFormat.Name = "comboBoxFileNameFormat";
            this.comboBoxFileNameFormat.Size = new System.Drawing.Size(844, 23);
            this.comboBoxFileNameFormat.TabIndex = 6;
            this.comboBoxFileNameFormat.Text = "#";
            this.comboBoxFileNameFormat.TextChanged += new System.EventHandler(this.comboBoxFileNameFormat_TextChanged);
            // 
            // labelSplitter1
            // 
            this.labelSplitter1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSplitter1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSplitter1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelSplitter1.Location = new System.Drawing.Point(12, 464);
            this.labelSplitter1.Margin = new System.Windows.Forms.Padding(3);
            this.labelSplitter1.Name = "labelSplitter1";
            this.labelSplitter1.Size = new System.Drawing.Size(920, 2);
            this.labelSplitter1.TabIndex = 4;
            // 
            // openFileDialogImport
            // 
            this.openFileDialogImport.Filter = "所有文件 (*.*)|*.*";
            this.openFileDialogImport.Multiselect = true;
            // 
            // MainForm
            // 
            this.AcceptButton = this.buttonRename;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonExit;
            this.ClientSize = new System.Drawing.Size(944, 602);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonRename);
            this.Controls.Add(this.labelSplitter2);
            this.Controls.Add(this.comboBoxExtensionFormat);
            this.Controls.Add(this.labelExtensionFormat);
            this.Controls.Add(this.numericUpDownStartId);
            this.Controls.Add(this.labelStartId);
            this.Controls.Add(this.comboBoxFileNameFormat);
            this.Controls.Add(this.labelFileNameFormat);
            this.Controls.Add(this.labelSplitter1);
            this.Controls.Add(this.listViewFileList);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonImport);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simple Batch Rename";
            this.contextMenuStripFileList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartId)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.ListView listViewFileList;
        private System.Windows.Forms.Label labelFileNameFormat;
        private System.Windows.Forms.Label labelStartId;
        private System.Windows.Forms.ComboBox comboBoxExtensionFormat;
        private System.Windows.Forms.Label labelExtensionFormat;
        private System.Windows.Forms.Button buttonRename;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Label labelSplitter2;
        private System.Windows.Forms.NumericUpDown numericUpDownStartId;
        private System.Windows.Forms.ComboBox comboBoxFileNameFormat;
        private System.Windows.Forms.Label labelSplitter1;
        private System.Windows.Forms.ColumnHeader columnHeaderFileName;
        private System.Windows.Forms.ColumnHeader columnHeaderFilePath;
        private System.Windows.Forms.OpenFileDialog openFileDialogImport;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripFileList;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFileListImport;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFileListRemove;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFileListClear;
        private System.Windows.Forms.ColumnHeader columnHeaderTargetFileName;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFileListSelectAll;
    }
}

