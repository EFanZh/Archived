namespace Sorting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panelDisplay = new System.Windows.Forms.Panel();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripLabelCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxCount = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButtonGenerate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonRandomize = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonReverse = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButtonSort = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItemInsertionSort = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSelectionSort = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMergeSort = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemStandardMergeSort = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMergeSort2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMergeSort3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemBubbleSort = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemStandardBubbleSort = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemBubbleSort2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHeapSort = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemQuickSort = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemStandardQuickSort = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRandomizedQuickSort = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemXSort = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonCancelOperation = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonInstantUpdate = new System.Windows.Forms.ToolStripButton();
            this.toolStripTextBoxRestTime = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabelRestTime = new System.Windows.Forms.ToolStripLabel();
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelDisplay
            // 
            this.panelDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDisplay.Location = new System.Drawing.Point(0, 25);
            this.panelDisplay.Name = "panelDisplay";
            this.panelDisplay.Size = new System.Drawing.Size(720, 375);
            this.panelDisplay.TabIndex = 1;
            this.panelDisplay.ClientSizeChanged += new System.EventHandler(this.panelDisplay_ClientSizeChanged);
            this.panelDisplay.Paint += new System.Windows.Forms.PaintEventHandler(this.panelDisplay_Paint);
            // 
            // toolStripMain
            // 
            this.toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelCount,
            this.toolStripTextBoxCount,
            this.toolStripButtonGenerate,
            this.toolStripSeparator1,
            this.toolStripButtonRandomize,
            this.toolStripButtonReverse,
            this.toolStripSeparator2,
            this.toolStripSplitButtonSort,
            this.toolStripSeparator3,
            this.toolStripButtonCancelOperation,
            this.toolStripButtonInstantUpdate,
            this.toolStripTextBoxRestTime,
            this.toolStripLabelRestTime});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.ShowItemToolTips = false;
            this.toolStripMain.Size = new System.Drawing.Size(720, 25);
            this.toolStripMain.TabIndex = 0;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // toolStripLabelCount
            // 
            this.toolStripLabelCount.Name = "toolStripLabelCount";
            this.toolStripLabelCount.Size = new System.Drawing.Size(43, 22);
            this.toolStripLabelCount.Text = "Count:";
            // 
            // toolStripTextBoxCount
            // 
            this.toolStripTextBoxCount.Name = "toolStripTextBoxCount";
            this.toolStripTextBoxCount.Size = new System.Drawing.Size(64, 25);
            this.toolStripTextBoxCount.Text = "100";
            this.toolStripTextBoxCount.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // toolStripButtonGenerate
            // 
            this.toolStripButtonGenerate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonGenerate.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonGenerate.Image")));
            this.toolStripButtonGenerate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonGenerate.Name = "toolStripButtonGenerate";
            this.toolStripButtonGenerate.Size = new System.Drawing.Size(58, 22);
            this.toolStripButtonGenerate.Text = "Generate";
            this.toolStripButtonGenerate.Click += new System.EventHandler(this.toolStripButtonGenerate_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonRandomize
            // 
            this.toolStripButtonRandomize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonRandomize.Enabled = false;
            this.toolStripButtonRandomize.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRandomize.Image")));
            this.toolStripButtonRandomize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRandomize.Name = "toolStripButtonRandomize";
            this.toolStripButtonRandomize.Size = new System.Drawing.Size(70, 22);
            this.toolStripButtonRandomize.Text = "Randomize";
            this.toolStripButtonRandomize.Click += new System.EventHandler(this.toolStripButtonRandomize_Click);
            // 
            // toolStripButtonReverse
            // 
            this.toolStripButtonReverse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonReverse.Enabled = false;
            this.toolStripButtonReverse.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonReverse.Image")));
            this.toolStripButtonReverse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonReverse.Name = "toolStripButtonReverse";
            this.toolStripButtonReverse.Size = new System.Drawing.Size(51, 22);
            this.toolStripButtonReverse.Text = "Reverse";
            this.toolStripButtonReverse.Click += new System.EventHandler(this.toolStripButtonReverse_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSplitButtonSort
            // 
            this.toolStripSplitButtonSort.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitButtonSort.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemInsertionSort,
            this.toolStripMenuItemSelectionSort,
            this.toolStripMenuItemMergeSort,
            this.toolStripMenuItemBubbleSort,
            this.toolStripMenuItemHeapSort,
            this.toolStripMenuItemQuickSort,
            this.toolStripMenuItemXSort});
            this.toolStripSplitButtonSort.Enabled = false;
            this.toolStripSplitButtonSort.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButtonSort.Image")));
            this.toolStripSplitButtonSort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButtonSort.Name = "toolStripSplitButtonSort";
            this.toolStripSplitButtonSort.Size = new System.Drawing.Size(93, 22);
            this.toolStripSplitButtonSort.Text = "Insertion Sort";
            this.toolStripSplitButtonSort.ButtonClick += new System.EventHandler(this.toolStripSplitButtonSort_ButtonClick);
            // 
            // toolStripMenuItemInsertionSort
            // 
            this.toolStripMenuItemInsertionSort.Name = "toolStripMenuItemInsertionSort";
            this.toolStripMenuItemInsertionSort.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemInsertionSort.Text = "Insertion Sort";
            // 
            // toolStripMenuItemSelectionSort
            // 
            this.toolStripMenuItemSelectionSort.Name = "toolStripMenuItemSelectionSort";
            this.toolStripMenuItemSelectionSort.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemSelectionSort.Text = "Selection Sort";
            // 
            // toolStripMenuItemMergeSort
            // 
            this.toolStripMenuItemMergeSort.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemStandardMergeSort,
            this.toolStripMenuItemMergeSort2,
            this.toolStripMenuItemMergeSort3});
            this.toolStripMenuItemMergeSort.Name = "toolStripMenuItemMergeSort";
            this.toolStripMenuItemMergeSort.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemMergeSort.Text = "Merge Sort";
            // 
            // toolStripMenuItemStandardMergeSort
            // 
            this.toolStripMenuItemStandardMergeSort.Name = "toolStripMenuItemStandardMergeSort";
            this.toolStripMenuItemStandardMergeSort.Size = new System.Drawing.Size(182, 22);
            this.toolStripMenuItemStandardMergeSort.Text = "Standard Merge Sort";
            // 
            // toolStripMenuItemMergeSort2
            // 
            this.toolStripMenuItemMergeSort2.Name = "toolStripMenuItemMergeSort2";
            this.toolStripMenuItemMergeSort2.Size = new System.Drawing.Size(182, 22);
            this.toolStripMenuItemMergeSort2.Text = "Merge Sort 2";
            // 
            // toolStripMenuItemMergeSort3
            // 
            this.toolStripMenuItemMergeSort3.Name = "toolStripMenuItemMergeSort3";
            this.toolStripMenuItemMergeSort3.Size = new System.Drawing.Size(182, 22);
            this.toolStripMenuItemMergeSort3.Text = "Merge Sort 3";
            // 
            // toolStripMenuItemBubbleSort
            // 
            this.toolStripMenuItemBubbleSort.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemStandardBubbleSort,
            this.toolStripMenuItemBubbleSort2});
            this.toolStripMenuItemBubbleSort.Name = "toolStripMenuItemBubbleSort";
            this.toolStripMenuItemBubbleSort.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemBubbleSort.Text = "Bubble Sort";
            // 
            // toolStripMenuItemStandardBubbleSort
            // 
            this.toolStripMenuItemStandardBubbleSort.Name = "toolStripMenuItemStandardBubbleSort";
            this.toolStripMenuItemStandardBubbleSort.Size = new System.Drawing.Size(185, 22);
            this.toolStripMenuItemStandardBubbleSort.Text = "Standard Bubble Sort";
            // 
            // toolStripMenuItemBubbleSort2
            // 
            this.toolStripMenuItemBubbleSort2.Name = "toolStripMenuItemBubbleSort2";
            this.toolStripMenuItemBubbleSort2.Size = new System.Drawing.Size(185, 22);
            this.toolStripMenuItemBubbleSort2.Text = "Bubble Sort 2";
            // 
            // toolStripMenuItemHeapSort
            // 
            this.toolStripMenuItemHeapSort.Name = "toolStripMenuItemHeapSort";
            this.toolStripMenuItemHeapSort.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemHeapSort.Text = "Heap Sort";
            // 
            // toolStripMenuItemQuickSort
            // 
            this.toolStripMenuItemQuickSort.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemStandardQuickSort,
            this.toolStripMenuItemRandomizedQuickSort});
            this.toolStripMenuItemQuickSort.Name = "toolStripMenuItemQuickSort";
            this.toolStripMenuItemQuickSort.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemQuickSort.Text = "Quick Sort";
            // 
            // toolStripMenuItemStandardQuickSort
            // 
            this.toolStripMenuItemStandardQuickSort.Name = "toolStripMenuItemStandardQuickSort";
            this.toolStripMenuItemStandardQuickSort.Size = new System.Drawing.Size(198, 22);
            this.toolStripMenuItemStandardQuickSort.Text = "Standard Quick Sort";
            // 
            // toolStripMenuItemRandomizedQuickSort
            // 
            this.toolStripMenuItemRandomizedQuickSort.Name = "toolStripMenuItemRandomizedQuickSort";
            this.toolStripMenuItemRandomizedQuickSort.Size = new System.Drawing.Size(198, 22);
            this.toolStripMenuItemRandomizedQuickSort.Text = "Randomized Quick Sort";
            // 
            // toolStripMenuItemXSort
            // 
            this.toolStripMenuItemXSort.Name = "toolStripMenuItemXSort";
            this.toolStripMenuItemXSort.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemXSort.Text = "X Sort";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonCancelOperation
            // 
            this.toolStripButtonCancelOperation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonCancelOperation.Enabled = false;
            this.toolStripButtonCancelOperation.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCancelOperation.Image")));
            this.toolStripButtonCancelOperation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCancelOperation.Name = "toolStripButtonCancelOperation";
            this.toolStripButtonCancelOperation.Size = new System.Drawing.Size(47, 22);
            this.toolStripButtonCancelOperation.Text = "Cancel";
            this.toolStripButtonCancelOperation.Click += new System.EventHandler(this.toolStripButtonCancelOperation_Click);
            // 
            // toolStripButtonInstantUpdate
            // 
            this.toolStripButtonInstantUpdate.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonInstantUpdate.CheckOnClick = true;
            this.toolStripButtonInstantUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonInstantUpdate.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonInstantUpdate.Image")));
            this.toolStripButtonInstantUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonInstantUpdate.Name = "toolStripButtonInstantUpdate";
            this.toolStripButtonInstantUpdate.Size = new System.Drawing.Size(88, 22);
            this.toolStripButtonInstantUpdate.Text = "Instant Update";
            // 
            // toolStripTextBoxRestTime
            // 
            this.toolStripTextBoxRestTime.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripTextBoxRestTime.Name = "toolStripTextBoxRestTime";
            this.toolStripTextBoxRestTime.Size = new System.Drawing.Size(64, 25);
            this.toolStripTextBoxRestTime.Text = "10";
            this.toolStripTextBoxRestTime.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolStripTextBoxRestTime.TextChanged += new System.EventHandler(this.toolStripTextBoxRestTime_TextChanged);
            // 
            // toolStripLabelRestTime
            // 
            this.toolStripLabelRestTime.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabelRestTime.Name = "toolStripLabelRestTime";
            this.toolStripLabelRestTime.Size = new System.Drawing.Size(62, 22);
            this.toolStripLabelRestTime.Text = "Rest Time:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 400);
            this.Controls.Add(this.panelDisplay);
            this.Controls.Add(this.toolStripMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sorting";
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelDisplay;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxCount;
        private System.Windows.Forms.ToolStripButton toolStripButtonGenerate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonRandomize;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonSort;
        private System.Windows.Forms.ToolStripButton toolStripButtonCancelOperation;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMergeSort;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemInsertionSort;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemBubbleSort;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHeapSort;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemQuickSort;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSelectionSort;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemStandardMergeSort;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMergeSort2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMergeSort3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemStandardBubbleSort;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemBubbleSort2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemStandardQuickSort;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRandomizedQuickSort;
        private System.Windows.Forms.ToolStripButton toolStripButtonReverse;
        private System.Windows.Forms.ToolStripLabel toolStripLabelCount;
        private System.Windows.Forms.ToolStripButton toolStripButtonInstantUpdate;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxRestTime;
        private System.Windows.Forms.ToolStripLabel toolStripLabelRestTime;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemXSort;
    }
}

