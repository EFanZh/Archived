using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Sorting
{
    public partial class MainForm : Form
    {
        private SortVisualization sv = new SortVisualization();
        private SortManager current_worker, sort_worker;

        public MainForm()
        {
            InitializeComponent();

            toolStripMain.SizeChanged += (sender, e) =>
            {
                toolStripMain.Update();
            };

            AssociateToolStripMenuItemSortHandlers<InsertionSortManager>(toolStripMenuItemInsertionSort);
            AssociateToolStripMenuItemSortHandlers<SelectionSortManager>(toolStripMenuItemSelectionSort);
            AssociateToolStripMenuItemSortHandlers<StandardMergeSortManager>(toolStripMenuItemStandardMergeSort);
            AssociateToolStripMenuItemSortHandlers<MergeSort2Manager>(toolStripMenuItemMergeSort2);
            AssociateToolStripMenuItemSortHandlers<MergeSort3Manager>(toolStripMenuItemMergeSort3);
            AssociateToolStripMenuItemSortHandlers<StandardBubbleSortManager>(toolStripMenuItemStandardBubbleSort);
            AssociateToolStripMenuItemSortHandlers<BubbleSort2Manager>(toolStripMenuItemBubbleSort2);
            AssociateToolStripMenuItemSortHandlers<HeapSortManager>(toolStripMenuItemHeapSort);
            AssociateToolStripMenuItemSortHandlers<StandardQuickSortManager>(toolStripMenuItemStandardQuickSort);
            AssociateToolStripMenuItemSortHandlers<RandomizedQuickSortManager>(toolStripMenuItemRandomizedQuickSort);

            PropertyInfo pi = panelDisplay.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(panelDisplay, true);

            sv.Interval = 2;
            sv.Size = panelDisplay.ClientSize;
            sort_worker = CreateSortManager<InsertionSortManager>();
        }

        private void toolStripButtonGenerate_Click(object sender, EventArgs e)
        {
            SetToolStripItemEnabled(false, toolStripButtonGenerate);

            int n;
            if (int.TryParse(toolStripTextBoxCount.Text, out n))
            {
                if (n > 0)
                {
                    sv.Data = new int[n];

                    for (int i = 0; i < sv.Data.Length; i++)
                    {
                        sv.Data[i] = i * i;
                    }
                    sv.Max = (int)(1.2 * sv.Data.Max());
                    UpdatePanelDisplay();
                    SetToolStripItemEnabled(true, toolStripButtonRandomize, toolStripButtonReverse, toolStripSplitButtonSort);
                }
            }

            SetToolStripItemEnabled(true, toolStripButtonGenerate);
        }

        private void toolStripButtonRandomize_Click(object sender, EventArgs e)
        {
            BeginWork<RandomizeManager>();
        }

        private void toolStripButtonReverse_Click(object sender, EventArgs e)
        {
            BeginWork<ReverseManager>();
        }

        private void toolStripSplitButtonSort_ButtonClick(object sender, EventArgs e)
        {
            BeginSort();
        }

        private void toolStripButtonCancelOperation_Click(object sender, EventArgs e)
        {
            current_worker.SortAsyncCancel();
        }

        private void toolStripTextBoxRestTime_TextChanged(object sender, EventArgs e)
        {
            if (current_worker != null)
            {
                SetSortManagerRestTime(current_worker);
            }
        }

        private void panelDisplay_ClientSizeChanged(object sender, EventArgs e)
        {
            sv.Size = panelDisplay.ClientSize;
            UpdatePanelDisplay();
        }

        private void panelDisplay_Paint(object sender, PaintEventArgs e)
        {
            sv.Render(e.Graphics);
        }

        private void AssociateToolStripMenuItemSortHandlers<T>(ToolStripItem item) where T : SortManager, new()
        {
            item.Click += (sender, e) =>
            {
                BeginSort<T>();
                toolStripSplitButtonSort.Text = item.Text;
                toolStripMain.Update();
            };
        }

        private T CreateSortManager<T>() where T : SortManager, new()
        {
            T sort_manager = new T();

            SetSortManagerRestTime(sort_manager);

            sort_manager.Compare += (sender, e) =>
            {
                sv.Compare(e.A, e.B);
                UpdatePanelDisplay();
            };

            sort_manager.SetValue += (sender, e) =>
            {
                sv.SetValue(e.A, e.B);
                UpdatePanelDisplay();
            };

            sort_manager.SetValueIndirect += (sender, e) =>
            {
                sv.SetValueIndirect(e.A, e.B);
                UpdatePanelDisplay();
            };

            sort_manager.SortCompleted += (sender, e) =>
            {
                SetToolStripItemEnabled(false, toolStripButtonCancelOperation);
                current_worker = null;
                sv.Reset();
                UpdatePanelDisplay();
                SetToolStripItemEnabled(true, toolStripButtonGenerate, toolStripButtonRandomize, toolStripButtonReverse, toolStripSplitButtonSort);
            };

            sort_manager.Swap += (sender, e) =>
            {
                sv.Swap(e.A, e.B);
                UpdatePanelDisplay();
            };

            return sort_manager;
        }

        private void SetToolStripItemEnabled(bool value, params ToolStripItem[] toolStripItems)
        {
            foreach (var toolStripItem in toolStripItems)
            {
                toolStripItem.Enabled = value;
            }
        }

        private void UpdatePanelDisplay()
        {
            panelDisplay.Invalidate();
            if (toolStripButtonInstantUpdate.Checked)
            {
                panelDisplay.Update();
            }
        }

        private void BeginWork(SortManager sort_manager)
        {
            current_worker = sort_manager;
            SetToolStripItemEnabled(false, toolStripButtonGenerate, toolStripButtonRandomize, toolStripButtonReverse, toolStripSplitButtonSort);
            current_worker.SortAsync(sv.Data.ToArray());
            SetToolStripItemEnabled(true, toolStripButtonCancelOperation);
        }

        private void BeginWork<T>() where T : SortManager, new()
        {
            BeginWork(CreateSortManager<T>());
        }

        private void BeginSort()
        {
            SetSortManagerRestTime(sort_worker);
            BeginWork(sort_worker);
        }

        private void BeginSort<T>() where T : SortManager, new()
        {
            sort_worker = CreateSortManager<T>();
            BeginSort();
        }

        private void SetSortManagerRestTime(SortManager sort_manager)
        {
            int value;
            if (int.TryParse(toolStripTextBoxRestTime.Text, out value) && value >= 0)
            {
                sort_manager.RestTime = value;
            }
        }
    }
}
