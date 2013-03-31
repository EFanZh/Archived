namespace ImgProc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonOptions = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAbout = new System.Windows.Forms.ToolStripButton();
            this.listViewImageList = new System.Windows.Forms.ListView();
            this.columnHeaderImagePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pictureBoxImagePreview = new System.Windows.Forms.PictureBox();
            this.openFileDialogMain = new System.Windows.Forms.OpenFileDialog();
            this.panelView = new System.Windows.Forms.Panel();
            this.panelImageList = new System.Windows.Forms.Panel();
            this.myGrayLine3 = new ImgProc.MyControls.MyGrayLine();
            this.toolStripImageList = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAddImages = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDeleteImages = new System.Windows.Forms.ToolStripButton();
            this.myGrayLine2 = new ImgProc.MyControls.MyGrayLine();
            this.myGradientTitleBarImageList = new ImgProc.MyControls.MyGradientTitleBar();
            this.mySplitterLeftRight = new ImgProc.MyControls.MySplitter();
            this.panelRight = new System.Windows.Forms.Panel();
            this.panelProcessing = new System.Windows.Forms.Panel();
            this.listViewProcessingList = new System.Windows.Forms.ListView();
            this.columnHeaderProcessing = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.myGrayLine6 = new ImgProc.MyControls.MyGrayLine();
            this.panelProcessingOption = new System.Windows.Forms.Panel();
            this.buttonStartProcessing = new System.Windows.Forms.Button();
            this.comboBoxOutputType = new System.Windows.Forms.ComboBox();
            this.buttonOutputOption = new System.Windows.Forms.Button();
            this.buttonOutputPath = new System.Windows.Forms.Button();
            this.textBoxOutputPath = new System.Windows.Forms.TextBox();
            this.labelOutputType = new System.Windows.Forms.Label();
            this.labelOutputPath = new System.Windows.Forms.Label();
            this.myGrayLine5 = new ImgProc.MyControls.MyGrayLine();
            this.toolStripProcessing = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButtonAddProcessing = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripButtonDeleteProcessing = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonProcessingOption = new System.Windows.Forms.ToolStripButton();
            this.myGrayLine4 = new ImgProc.MyControls.MyGrayLine();
            this.myGradientTitleBarProcessing = new ImgProc.MyControls.MyGradientTitleBar();
            this.mySplitterTopBottom = new ImgProc.MyControls.MySplitter();
            this.panelImagePreview = new System.Windows.Forms.Panel();
            this.panelImagePreviewContainer = new System.Windows.Forms.Panel();
            this.myGrayLine7 = new ImgProc.MyControls.MyGrayLine();
            this.myGradientTitleBarImagePreview = new ImgProc.MyControls.MyGradientTitleBar();
            this.folderBrowserDialogMain = new System.Windows.Forms.FolderBrowserDialog();
            this.myGrayLine1 = new ImgProc.MyControls.MyGrayLine();
            this.toolStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImagePreview)).BeginInit();
            this.panelView.SuspendLayout();
            this.panelImageList.SuspendLayout();
            this.toolStripImageList.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panelProcessing.SuspendLayout();
            this.panelProcessingOption.SuspendLayout();
            this.toolStripProcessing.SuspendLayout();
            this.panelImagePreview.SuspendLayout();
            this.panelImagePreviewContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMain
            // 
            this.toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonOptions,
            this.toolStripButtonAbout});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(704, 25);
            this.toolStripMain.TabIndex = 0;
            // 
            // toolStripButtonOptions
            // 
            this.toolStripButtonOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonOptions.Name = "toolStripButtonOptions";
            this.toolStripButtonOptions.Size = new System.Drawing.Size(44, 22);
            this.toolStripButtonOptions.Text = "选项...";
            this.toolStripButtonOptions.Click += new System.EventHandler(this.toolStripButtonOptions_Click);
            // 
            // toolStripButtonAbout
            // 
            this.toolStripButtonAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonAbout.Name = "toolStripButtonAbout";
            this.toolStripButtonAbout.Size = new System.Drawing.Size(44, 22);
            this.toolStripButtonAbout.Text = "关于...";
            this.toolStripButtonAbout.Click += new System.EventHandler(this.toolStripButtonAbout_Click);
            // 
            // listViewImageList
            // 
            this.listViewImageList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewImageList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderImagePath});
            this.listViewImageList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewImageList.Location = new System.Drawing.Point(0, 47);
            this.listViewImageList.Name = "listViewImageList";
            this.listViewImageList.Size = new System.Drawing.Size(432, 419);
            this.listViewImageList.TabIndex = 1;
            this.listViewImageList.UseCompatibleStateImageBehavior = false;
            this.listViewImageList.View = System.Windows.Forms.View.Details;
            this.listViewImageList.SelectedIndexChanged += new System.EventHandler(this.listViewImageList_SelectedIndexChanged);
            // 
            // columnHeaderImagePath
            // 
            this.columnHeaderImagePath.Text = "图像路径";
            this.columnHeaderImagePath.Width = 117;
            // 
            // pictureBoxImagePreview
            // 
            this.pictureBoxImagePreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxImagePreview.Location = new System.Drawing.Point(4, 4);
            this.pictureBoxImagePreview.Name = "pictureBoxImagePreview";
            this.pictureBoxImagePreview.Size = new System.Drawing.Size(248, 148);
            this.pictureBoxImagePreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxImagePreview.TabIndex = 0;
            this.pictureBoxImagePreview.TabStop = false;
            // 
            // openFileDialogMain
            // 
            this.openFileDialogMain.Multiselect = true;
            // 
            // panelView
            // 
            this.panelView.Controls.Add(this.panelImageList);
            this.panelView.Controls.Add(this.mySplitterLeftRight);
            this.panelView.Controls.Add(this.panelRight);
            this.panelView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelView.Location = new System.Drawing.Point(0, 26);
            this.panelView.Name = "panelView";
            this.panelView.Padding = new System.Windows.Forms.Padding(4);
            this.panelView.Size = new System.Drawing.Size(704, 476);
            this.panelView.TabIndex = 1;
            this.panelView.Resize += new System.EventHandler(this.panelView_Resize);
            // 
            // panelImageList
            // 
            this.panelImageList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelImageList.Controls.Add(this.listViewImageList);
            this.panelImageList.Controls.Add(this.myGrayLine3);
            this.panelImageList.Controls.Add(this.toolStripImageList);
            this.panelImageList.Controls.Add(this.myGrayLine2);
            this.panelImageList.Controls.Add(this.myGradientTitleBarImageList);
            this.panelImageList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelImageList.Location = new System.Drawing.Point(4, 4);
            this.panelImageList.Name = "panelImageList";
            this.panelImageList.Size = new System.Drawing.Size(434, 468);
            this.panelImageList.TabIndex = 0;
            // 
            // myGrayLine3
            // 
            this.myGrayLine3.Dock = System.Windows.Forms.DockStyle.Top;
            this.myGrayLine3.Location = new System.Drawing.Point(0, 46);
            this.myGrayLine3.Name = "myGrayLine3";
            this.myGrayLine3.Size = new System.Drawing.Size(432, 1);
            this.myGrayLine3.TabIndex = 1;
            this.myGrayLine3.TabStop = false;
            this.myGrayLine3.Text = "grayLine4";
            // 
            // toolStripImageList
            // 
            this.toolStripImageList.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripImageList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAddImages,
            this.toolStripButtonDeleteImages});
            this.toolStripImageList.Location = new System.Drawing.Point(0, 21);
            this.toolStripImageList.Name = "toolStripImageList";
            this.toolStripImageList.Size = new System.Drawing.Size(432, 25);
            this.toolStripImageList.TabIndex = 0;
            // 
            // toolStripButtonAddImages
            // 
            this.toolStripButtonAddImages.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonAddImages.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddImages.Image")));
            this.toolStripButtonAddImages.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddImages.Name = "toolStripButtonAddImages";
            this.toolStripButtonAddImages.Size = new System.Drawing.Size(59, 22);
            this.toolStripButtonAddImages.Text = "添加图像";
            this.toolStripButtonAddImages.Click += new System.EventHandler(this.toolStripButtonAddImages_Click);
            // 
            // toolStripButtonDeleteImages
            // 
            this.toolStripButtonDeleteImages.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonDeleteImages.Enabled = false;
            this.toolStripButtonDeleteImages.Name = "toolStripButtonDeleteImages";
            this.toolStripButtonDeleteImages.Size = new System.Drawing.Size(59, 22);
            this.toolStripButtonDeleteImages.Text = "删除图像";
            this.toolStripButtonDeleteImages.Click += new System.EventHandler(this.toolStripButtonDeleteImages_Click);
            // 
            // myGrayLine2
            // 
            this.myGrayLine2.Dock = System.Windows.Forms.DockStyle.Top;
            this.myGrayLine2.Location = new System.Drawing.Point(0, 20);
            this.myGrayLine2.Name = "myGrayLine2";
            this.myGrayLine2.Size = new System.Drawing.Size(432, 1);
            this.myGrayLine2.TabIndex = 3;
            this.myGrayLine2.TabStop = false;
            this.myGrayLine2.Text = "grayLine1";
            // 
            // myGradientTitleBarImageList
            // 
            this.myGradientTitleBarImageList.Location = new System.Drawing.Point(0, 0);
            this.myGradientTitleBarImageList.Name = "myGradientTitleBarImageList";
            this.myGradientTitleBarImageList.Size = new System.Drawing.Size(432, 20);
            this.myGradientTitleBarImageList.TabIndex = 4;
            this.myGradientTitleBarImageList.Text = "待处理的图像";
            // 
            // mySplitterLeftRight
            // 
            this.mySplitterLeftRight.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.mySplitterLeftRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.mySplitterLeftRight.Location = new System.Drawing.Point(438, 4);
            this.mySplitterLeftRight.MinExtra = 200;
            this.mySplitterLeftRight.MinSize = 200;
            this.mySplitterLeftRight.Name = "mySplitterLeftRight";
            this.mySplitterLeftRight.Size = new System.Drawing.Size(4, 468);
            this.mySplitterLeftRight.TabIndex = 1;
            this.mySplitterLeftRight.TabStop = false;
            this.mySplitterLeftRight.Text = "mySplitter1";
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.panelProcessing);
            this.panelRight.Controls.Add(this.mySplitterTopBottom);
            this.panelRight.Controls.Add(this.panelImagePreview);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRight.Location = new System.Drawing.Point(442, 4);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(258, 468);
            this.panelRight.TabIndex = 1;
            this.panelRight.Resize += new System.EventHandler(this.panelRight_Resize);
            // 
            // panelProcessing
            // 
            this.panelProcessing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelProcessing.Controls.Add(this.listViewProcessingList);
            this.panelProcessing.Controls.Add(this.myGrayLine6);
            this.panelProcessing.Controls.Add(this.panelProcessingOption);
            this.panelProcessing.Controls.Add(this.myGrayLine5);
            this.panelProcessing.Controls.Add(this.toolStripProcessing);
            this.panelProcessing.Controls.Add(this.myGrayLine4);
            this.panelProcessing.Controls.Add(this.myGradientTitleBarProcessing);
            this.panelProcessing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProcessing.Location = new System.Drawing.Point(0, 0);
            this.panelProcessing.Name = "panelProcessing";
            this.panelProcessing.Size = new System.Drawing.Size(258, 285);
            this.panelProcessing.TabIndex = 0;
            // 
            // listViewProcessingList
            // 
            this.listViewProcessingList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewProcessingList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderProcessing});
            this.listViewProcessingList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewProcessingList.Location = new System.Drawing.Point(0, 47);
            this.listViewProcessingList.Name = "listViewProcessingList";
            this.listViewProcessingList.Size = new System.Drawing.Size(256, 143);
            this.listViewProcessingList.TabIndex = 1;
            this.listViewProcessingList.UseCompatibleStateImageBehavior = false;
            this.listViewProcessingList.View = System.Windows.Forms.View.Details;
            this.listViewProcessingList.SelectedIndexChanged += new System.EventHandler(this.listViewProcessingList_SelectedIndexChanged);
            // 
            // columnHeaderProcessing
            // 
            this.columnHeaderProcessing.Text = "处理名称";
            // 
            // myGrayLine6
            // 
            this.myGrayLine6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.myGrayLine6.Location = new System.Drawing.Point(0, 190);
            this.myGrayLine6.Name = "myGrayLine6";
            this.myGrayLine6.Size = new System.Drawing.Size(256, 1);
            this.myGrayLine6.TabIndex = 1;
            this.myGrayLine6.TabStop = false;
            this.myGrayLine6.Text = "grayLine3";
            // 
            // panelProcessingOption
            // 
            this.panelProcessingOption.Controls.Add(this.buttonStartProcessing);
            this.panelProcessingOption.Controls.Add(this.comboBoxOutputType);
            this.panelProcessingOption.Controls.Add(this.buttonOutputOption);
            this.panelProcessingOption.Controls.Add(this.buttonOutputPath);
            this.panelProcessingOption.Controls.Add(this.textBoxOutputPath);
            this.panelProcessingOption.Controls.Add(this.labelOutputType);
            this.panelProcessingOption.Controls.Add(this.labelOutputPath);
            this.panelProcessingOption.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelProcessingOption.Location = new System.Drawing.Point(0, 191);
            this.panelProcessingOption.Name = "panelProcessingOption";
            this.panelProcessingOption.Size = new System.Drawing.Size(256, 92);
            this.panelProcessingOption.TabIndex = 2;
            // 
            // buttonStartProcessing
            // 
            this.buttonStartProcessing.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonStartProcessing.Location = new System.Drawing.Point(91, 64);
            this.buttonStartProcessing.Name = "buttonStartProcessing";
            this.buttonStartProcessing.Size = new System.Drawing.Size(75, 23);
            this.buttonStartProcessing.TabIndex = 6;
            this.buttonStartProcessing.Text = "开始处理(&G)";
            this.buttonStartProcessing.UseVisualStyleBackColor = true;
            this.buttonStartProcessing.Click += new System.EventHandler(this.buttonStartProcessing_Click);
            // 
            // comboBoxOutputType
            // 
            this.comboBoxOutputType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxOutputType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOutputType.FormattingEnabled = true;
            this.comboBoxOutputType.Location = new System.Drawing.Point(69, 35);
            this.comboBoxOutputType.Name = "comboBoxOutputType";
            this.comboBoxOutputType.Size = new System.Drawing.Size(154, 23);
            this.comboBoxOutputType.TabIndex = 4;
            this.comboBoxOutputType.SelectedIndexChanged += new System.EventHandler(this.comboBoxOutputType_SelectedIndexChanged);
            // 
            // buttonOutputOption
            // 
            this.buttonOutputOption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOutputOption.Enabled = false;
            this.buttonOutputOption.Location = new System.Drawing.Point(229, 35);
            this.buttonOutputOption.Name = "buttonOutputOption";
            this.buttonOutputOption.Size = new System.Drawing.Size(24, 23);
            this.buttonOutputOption.TabIndex = 5;
            this.buttonOutputOption.Text = "...";
            this.buttonOutputOption.UseVisualStyleBackColor = true;
            this.buttonOutputOption.Click += new System.EventHandler(this.buttonOutputOption_Click);
            // 
            // buttonOutputPath
            // 
            this.buttonOutputPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOutputPath.Location = new System.Drawing.Point(229, 6);
            this.buttonOutputPath.Name = "buttonOutputPath";
            this.buttonOutputPath.Size = new System.Drawing.Size(24, 23);
            this.buttonOutputPath.TabIndex = 2;
            this.buttonOutputPath.Text = "...";
            this.buttonOutputPath.UseVisualStyleBackColor = true;
            this.buttonOutputPath.Click += new System.EventHandler(this.buttonOutputPath_Click);
            // 
            // textBoxOutputPath
            // 
            this.textBoxOutputPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOutputPath.Location = new System.Drawing.Point(69, 6);
            this.textBoxOutputPath.Name = "textBoxOutputPath";
            this.textBoxOutputPath.ReadOnly = true;
            this.textBoxOutputPath.Size = new System.Drawing.Size(154, 23);
            this.textBoxOutputPath.TabIndex = 1;
            // 
            // labelOutputType
            // 
            this.labelOutputType.AutoSize = true;
            this.labelOutputType.Location = new System.Drawing.Point(5, 38);
            this.labelOutputType.Name = "labelOutputType";
            this.labelOutputType.Size = new System.Drawing.Size(58, 15);
            this.labelOutputType.TabIndex = 3;
            this.labelOutputType.Text = "输出类型:";
            // 
            // labelOutputPath
            // 
            this.labelOutputPath.AutoSize = true;
            this.labelOutputPath.Location = new System.Drawing.Point(5, 9);
            this.labelOutputPath.Name = "labelOutputPath";
            this.labelOutputPath.Size = new System.Drawing.Size(58, 15);
            this.labelOutputPath.TabIndex = 0;
            this.labelOutputPath.Text = "输出路径:";
            // 
            // myGrayLine5
            // 
            this.myGrayLine5.Dock = System.Windows.Forms.DockStyle.Top;
            this.myGrayLine5.Location = new System.Drawing.Point(0, 46);
            this.myGrayLine5.Name = "myGrayLine5";
            this.myGrayLine5.Size = new System.Drawing.Size(256, 1);
            this.myGrayLine5.TabIndex = 3;
            this.myGrayLine5.TabStop = false;
            this.myGrayLine5.Text = "greyLine4";
            // 
            // toolStripProcessing
            // 
            this.toolStripProcessing.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripProcessing.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButtonAddProcessing,
            this.toolStripButtonDeleteProcessing,
            this.toolStripButtonProcessingOption});
            this.toolStripProcessing.Location = new System.Drawing.Point(0, 21);
            this.toolStripProcessing.Name = "toolStripProcessing";
            this.toolStripProcessing.Size = new System.Drawing.Size(256, 25);
            this.toolStripProcessing.TabIndex = 0;
            // 
            // toolStripDropDownButtonAddProcessing
            // 
            this.toolStripDropDownButtonAddProcessing.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButtonAddProcessing.Name = "toolStripDropDownButtonAddProcessing";
            this.toolStripDropDownButtonAddProcessing.Size = new System.Drawing.Size(68, 22);
            this.toolStripDropDownButtonAddProcessing.Text = "添加处理";
            // 
            // toolStripButtonDeleteProcessing
            // 
            this.toolStripButtonDeleteProcessing.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonDeleteProcessing.Enabled = false;
            this.toolStripButtonDeleteProcessing.Name = "toolStripButtonDeleteProcessing";
            this.toolStripButtonDeleteProcessing.Size = new System.Drawing.Size(59, 22);
            this.toolStripButtonDeleteProcessing.Text = "删除处理";
            this.toolStripButtonDeleteProcessing.Click += new System.EventHandler(this.toolStripButtonDeleteProcessing_Click);
            // 
            // toolStripButtonProcessingOption
            // 
            this.toolStripButtonProcessingOption.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonProcessingOption.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonProcessingOption.Enabled = false;
            this.toolStripButtonProcessingOption.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonProcessingOption.Image")));
            this.toolStripButtonProcessingOption.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonProcessingOption.Name = "toolStripButtonProcessingOption";
            this.toolStripButtonProcessingOption.Size = new System.Drawing.Size(59, 22);
            this.toolStripButtonProcessingOption.Text = "处理选项";
            this.toolStripButtonProcessingOption.Click += new System.EventHandler(this.toolStripButtonProcessingOption_Click);
            // 
            // myGrayLine4
            // 
            this.myGrayLine4.Dock = System.Windows.Forms.DockStyle.Top;
            this.myGrayLine4.Location = new System.Drawing.Point(0, 20);
            this.myGrayLine4.Name = "myGrayLine4";
            this.myGrayLine4.Size = new System.Drawing.Size(256, 1);
            this.myGrayLine4.TabIndex = 5;
            this.myGrayLine4.TabStop = false;
            this.myGrayLine4.Text = "grayLine6";
            // 
            // myGradientTitleBarProcessing
            // 
            this.myGradientTitleBarProcessing.Location = new System.Drawing.Point(0, 0);
            this.myGradientTitleBarProcessing.Name = "myGradientTitleBarProcessing";
            this.myGradientTitleBarProcessing.Size = new System.Drawing.Size(256, 20);
            this.myGradientTitleBarProcessing.TabIndex = 6;
            this.myGradientTitleBarProcessing.Text = "处理";
            // 
            // mySplitterTopBottom
            // 
            this.mySplitterTopBottom.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.mySplitterTopBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mySplitterTopBottom.Location = new System.Drawing.Point(0, 285);
            this.mySplitterTopBottom.MinExtra = 250;
            this.mySplitterTopBottom.MinSize = 150;
            this.mySplitterTopBottom.Name = "mySplitterTopBottom";
            this.mySplitterTopBottom.Size = new System.Drawing.Size(258, 4);
            this.mySplitterTopBottom.TabIndex = 1;
            this.mySplitterTopBottom.TabStop = false;
            this.mySplitterTopBottom.Text = "mySplitter3";
            // 
            // panelImagePreview
            // 
            this.panelImagePreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelImagePreview.Controls.Add(this.panelImagePreviewContainer);
            this.panelImagePreview.Controls.Add(this.myGrayLine7);
            this.panelImagePreview.Controls.Add(this.myGradientTitleBarImagePreview);
            this.panelImagePreview.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelImagePreview.Location = new System.Drawing.Point(0, 289);
            this.panelImagePreview.Name = "panelImagePreview";
            this.panelImagePreview.Size = new System.Drawing.Size(258, 179);
            this.panelImagePreview.TabIndex = 1;
            // 
            // panelImagePreviewContainer
            // 
            this.panelImagePreviewContainer.Controls.Add(this.pictureBoxImagePreview);
            this.panelImagePreviewContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelImagePreviewContainer.Location = new System.Drawing.Point(0, 21);
            this.panelImagePreviewContainer.Name = "panelImagePreviewContainer";
            this.panelImagePreviewContainer.Padding = new System.Windows.Forms.Padding(4);
            this.panelImagePreviewContainer.Size = new System.Drawing.Size(256, 156);
            this.panelImagePreviewContainer.TabIndex = 3;
            // 
            // myGrayLine7
            // 
            this.myGrayLine7.Dock = System.Windows.Forms.DockStyle.Top;
            this.myGrayLine7.Location = new System.Drawing.Point(0, 20);
            this.myGrayLine7.Name = "myGrayLine7";
            this.myGrayLine7.Size = new System.Drawing.Size(256, 1);
            this.myGrayLine7.TabIndex = 1;
            this.myGrayLine7.TabStop = false;
            this.myGrayLine7.Text = "grayLine7";
            // 
            // myGradientTitleBarImagePreview
            // 
            this.myGradientTitleBarImagePreview.Location = new System.Drawing.Point(0, 0);
            this.myGradientTitleBarImagePreview.Name = "myGradientTitleBarImagePreview";
            this.myGradientTitleBarImagePreview.Size = new System.Drawing.Size(256, 20);
            this.myGradientTitleBarImagePreview.TabIndex = 2;
            this.myGradientTitleBarImagePreview.Text = "图像预览";
            // 
            // folderBrowserDialogMain
            // 
            this.folderBrowserDialogMain.Description = "请选择一个输出路径。";
            // 
            // myGrayLine1
            // 
            this.myGrayLine1.Dock = System.Windows.Forms.DockStyle.Top;
            this.myGrayLine1.Location = new System.Drawing.Point(0, 25);
            this.myGrayLine1.Name = "myGrayLine1";
            this.myGrayLine1.Size = new System.Drawing.Size(704, 1);
            this.myGrayLine1.TabIndex = 1;
            this.myGrayLine1.TabStop = false;
            this.myGrayLine1.Text = "grayLine5";
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(704, 502);
            this.Controls.Add(this.panelView);
            this.Controls.Add(this.myGrayLine1);
            this.Controls.Add(this.toolStripMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(720, 540);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ImgProc";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImagePreview)).EndInit();
            this.panelView.ResumeLayout(false);
            this.panelImageList.ResumeLayout(false);
            this.panelImageList.PerformLayout();
            this.toolStripImageList.ResumeLayout(false);
            this.toolStripImageList.PerformLayout();
            this.panelRight.ResumeLayout(false);
            this.panelProcessing.ResumeLayout(false);
            this.panelProcessing.PerformLayout();
            this.panelProcessingOption.ResumeLayout(false);
            this.panelProcessingOption.PerformLayout();
            this.toolStripProcessing.ResumeLayout(false);
            this.toolStripProcessing.PerformLayout();
            this.panelImagePreview.ResumeLayout(false);
            this.panelImagePreviewContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.OpenFileDialog openFileDialogMain;
        private System.Windows.Forms.ToolStripButton toolStripButtonOptions;
        private System.Windows.Forms.ToolStripButton toolStripButtonAbout;
        private System.Windows.Forms.ListView listViewImageList;
        private System.Windows.Forms.ColumnHeader columnHeaderImagePath;
        private System.Windows.Forms.PictureBox pictureBoxImagePreview;
        private MyControls.MySplitter mySplitterLeftRight;
        private System.Windows.Forms.Panel panelView;
        private System.Windows.Forms.Panel panelImageList;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panelImagePreview;
        private MyControls.MyGrayLine myGrayLine2;
        private System.Windows.Forms.Panel panelProcessing;
        private System.Windows.Forms.ListView listViewProcessingList;
        private System.Windows.Forms.ColumnHeader columnHeaderProcessing;
        private MyControls.MyGrayLine myGrayLine5;
        private System.Windows.Forms.ToolStrip toolStripProcessing;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonAddProcessing;
        private System.Windows.Forms.ToolStripButton toolStripButtonDeleteProcessing;
        private MyControls.MySplitter mySplitterTopBottom;
        private MyControls.MyGrayLine myGrayLine6;
        private System.Windows.Forms.Panel panelProcessingOption;
        private System.Windows.Forms.ComboBox comboBoxOutputType;
        private System.Windows.Forms.Button buttonOutputPath;
        private System.Windows.Forms.TextBox textBoxOutputPath;
        private System.Windows.Forms.Label labelOutputType;
        private System.Windows.Forms.Label labelOutputPath;
        private MyControls.MyGrayLine myGrayLine3;
        private System.Windows.Forms.ToolStrip toolStripImageList;
        private System.Windows.Forms.ToolStripButton toolStripButtonDeleteImages;
        private MyControls.MyGrayLine myGrayLine1;
        private MyControls.MyGrayLine myGrayLine4;
        private System.Windows.Forms.Button buttonStartProcessing;
        private System.Windows.Forms.ToolStripButton toolStripButtonProcessingOption;
        private MyControls.MyGrayLine myGrayLine7;
        private System.Windows.Forms.Button buttonOutputOption;
        private MyControls.MyGradientTitleBar myGradientTitleBarImageList;
        private MyControls.MyGradientTitleBar myGradientTitleBarProcessing;
        private MyControls.MyGradientTitleBar myGradientTitleBarImagePreview;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddImages;
        private System.Windows.Forms.Panel panelImagePreviewContainer;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogMain;
    }
}

