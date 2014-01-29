using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SimpleBatchRename
{
    public partial class MainForm : Form, IMessageFilter
    {
        public MainForm()
        {
            InitializeComponent();

            // For processing ListView DoubleClick Event.
            Application.AddMessageFilter(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Set ListView style
            listViewFileList.SetDoubleBuffered(true);
            listViewFileList.SetWindowTheme("Explorer", null);
            listViewFileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            comboBoxExtensionFormat.SelectedIndex = 1;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case NativeMethods.WM_NOTIFY:
                    var nmhdr = (NativeMethods.NMHDR)m.GetLParam(typeof(NativeMethods.NMHDR));
                    switch (nmhdr.code)
                    {
                        case NativeMethods.LVN_GETEMPTYMARKUP:
                            if (Control.FromHandle(nmhdr.hwndFrom) == listViewFileList)
                            {
                                var markup = (NativeMethods.NMLVEMPTYMARKUP)m.GetLParam(typeof(NativeMethods.NMLVEMPTYMARKUP));
                                markup.szMarkup = "要添加文件，请单击“导入”按钮；或将文件拖曳到此处；或双击空白处。";
                                Marshal.StructureToPtr(markup, m.LParam, false);
                                m.Result = new IntPtr(1);
                                return;
                            }
                            break;
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        #region IMessageFilter Members

        public bool PreFilterMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case NativeMethods.WM_LBUTTONDBLCLK:
                    if (Control.FromHandle(m.HWnd) == listViewFileList)
                    {
                        Point target_point = listViewFileList.PointToClient(Control.MousePosition);

                        if (listViewFileList.GetItemAt(target_point.X, target_point.Y) == null)
                        {
                            ImportCommand();
                            return true;
                        }
                    }
                    break;
            }
            return false;
        }

        #endregion IMessageFilter Members

        private void buttonImport_Click(object sender, EventArgs e)
        {
            ImportCommand();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            RemoveCommand();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearCommand();
        }

        private void listViewFileList_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                listViewFileList.Enabled = false;

                Process.Start(listViewFileList.SelectedItems[0].SubItems[1].Text);
            }

            catch (Win32Exception)
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("在打开文件的过程中发生了错误，错误提示信息如下:\r\n“{0}”", ex.Message), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                listViewFileList.Enabled = true;
            }
        }

        private void listViewFileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewFileList.SelectedItems.Count > 0)
            {
                buttonRemove.Enabled = true;
                toolStripMenuItemFileListRemove.Enabled = true;
            }
            else
            {
                buttonRemove.Enabled = false;
                toolStripMenuItemFileListRemove.Enabled = false;
            }
        }

        private void listViewFileList_ItemDrag(object sender, ItemDragEventArgs e)
        {
            listViewFileList.DoDragDrop(listViewFileList.SelectedItems, DragDropEffects.Move | DragDropEffects.Scroll);
        }

        private void listViewFileList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link | DragDropEffects.Scroll;
            }
            else if (e.Data.GetDataPresent(typeof(ListView.SelectedListViewItemCollection)))
            {
                e.Effect = e.AllowedEffect;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void listViewFileList_DragOver(object sender, DragEventArgs e)
        {
            if (e.Effect == DragDropEffects.None)
            {
                return;
            }

            if (listViewFileList.Items.Count == 0)
            {
                listViewFileList.InsertionMark.Index = -1;
                listViewFileList.InsertionMark.AppearsAfterItem = true;
                return;
            }

            Point target_point = listViewFileList.PointToClient(new Point(e.X, e.Y));
            Rectangle rect = new Rectangle();

            for (int i = 0; i < listViewFileList.Items.Count; i++)
            {
                rect = listViewFileList.GetItemRect(i);
                int bottom;
                if (i == listViewFileList.Items.Count - 1)
                {
                    bottom = int.MaxValue;
                }
                else
                {
                    bottom = (rect.Bottom + listViewFileList.GetItemRect(i + 1).Top) / 2;
                }
                if (target_point.Y < bottom)
                {
                    listViewFileList.InsertionMark.Index = i;
                    break;
                }
            }
            listViewFileList.InsertionMark.AppearsAfterItem = target_point.Y > rect.Top + rect.Height / 2;
        }

        private void listViewFileList_DragLeave(object sender, EventArgs e)
        {
            listViewFileList.InsertionMark.Index = -1;
        }

        private void listViewFileList_DragDrop(object sender, DragEventArgs e)
        {
            int target_index = listViewFileList.InsertionMark.AppearsAfterItem ? listViewFileList.InsertionMark.Index + 1 : listViewFileList.InsertionMark.Index;

            listViewFileList.BeginUpdate();
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                listViewFileList.SelectedItems.Clear();
                ImportFiles(target_index, e.Data.GetData(DataFormats.FileDrop) as string[]);
                AutoResizeListViewColumns();
            }
            else if (e.Data.GetDataPresent(typeof(ListView.SelectedListViewItemCollection)))
            {
                var move_items = e.Data.GetData(typeof(ListView.SelectedListViewItemCollection)) as ListView.SelectedListViewItemCollection;
                Debug.Assert(move_items != null);

                var new_move_items = new List<ListViewItem>();
                for (int i = 0; i < move_items.Count; i++)
                {
                    ListViewItem new_item = move_items[i].Clone() as ListViewItem;
                    Debug.Assert(new_item != null);

                    new_move_items.Add(listViewFileList.Items.Insert(target_index + i, new_item));
                    if (move_items[i].Focused)
                    {
                        new_item.Focused = true;
                    }
                }
                while (move_items.Count > 0)
                {
                    move_items[0].Remove();
                }
                foreach (var lvi in new_move_items)
                {
                    lvi.Selected = true;
                }
                UpdateTargetFileNames();
            }
            if (listViewFileList.Items.Count > 0)
            {
                buttonClear.Enabled = true;
                toolStripMenuItemFileListClear.Enabled = true;
                listViewFileList.Focus();
            }

            listViewFileList.InsertionMark.Index = -1; // Maybe this is really necessary.
            listViewFileList.EndUpdate();
        }

        private void toolStripMenuItemFileListImport_Click(object sender, EventArgs e)
        {
            ImportCommand();
        }

        private void toolStripMenuItemFileListRemove_Click(object sender, EventArgs e)
        {
            RemoveCommand();
        }

        private void toolStripMenuItemFileListSelectAll_Click(object sender, EventArgs e)
        {
            toolStripMenuItemFileListSelectAll.Enabled = false;
            listViewFileList.BeginUpdate();
            for (int i = 0; i < listViewFileList.Items.Count; i++)
            {
                listViewFileList.Items[i].Selected = true;
            }
            listViewFileList.EndUpdate();
            toolStripMenuItemFileListSelectAll.Enabled = true;
        }

        private void toolStripMenuItemFileListClear_Click(object sender, EventArgs e)
        {
            ClearCommand();
        }

        private void comboBoxFileNameFormat_TextChanged(object sender, EventArgs e)
        {
            UpdateCommand();
        }

        private void numericUpDownStartId_ValueChanged(object sender, EventArgs e)
        {
            UpdateCommand();
        }

        private void comboBoxExtensionFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCommand();
        }

        private void buttonRename_Click(object sender, EventArgs e)
        {
            buttonRename.Enabled = false;
            var dict_tasks = new Dictionary<string, string>();
            for (int i = 0; i < listViewFileList.Items.Count; i++)
            {
                var subitems = listViewFileList.Items[i].SubItems;
                string file = subitems[1].Text;
                string target_file_name = subitems[2].Text;
                if (!File.Exists(file))
                {
                    MessageBox.Show(string.Format("文件不存在:\r\n“{0}”", file), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (target_file_name.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
                {
                    MessageBox.Show(string.Format("文件名称不合法:\r\n“{0}”", target_file_name), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                dict_tasks[file] = Path.Combine(Path.GetDirectoryName(file), target_file_name);
            }
            if (!Renamer.RenameTest(dict_tasks))
            {
                MessageBox.Show("重命名条件未满足。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                buttonRename.Enabled = true;
                return;
            }
            try
            {
                Renamer.Rename(dict_tasks);

                listViewFileList.BeginUpdate();
                for (int i = 0; i < listViewFileList.Items.Count; i++)
                {
                    var subitems = listViewFileList.Items[i].SubItems;
                    subitems[1].Text = dict_tasks[subitems[1].Text];
                    subitems[0].Text = Path.GetFileName(subitems[1].Text);
                }
                AutoResizeListViewColumns();
                listViewFileList.Focus();
                listViewFileList.EndUpdate();
                MessageBox.Show("重命名完成。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("在重命名的过程中发生了错误，错误提示信息如下:\r\n“{0}”", ex.Message), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            buttonRename.Enabled = true;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ImportCommand()
        {
            buttonImport.Enabled = false;
            listViewFileList.Enabled = false;
            toolStripMenuItemFileListImport.Enabled = false;

            DialogResult result = openFileDialogImport.ShowDialog();
            if (result == DialogResult.OK)
            {
                listViewFileList.BeginUpdate();
                listViewFileList.SelectedItems.Clear();
                ImportFiles(listViewFileList.Items.Count, openFileDialogImport.FileNames);
                AutoResizeListViewColumns();
                listViewFileList.EndUpdate();

                toolStripMenuItemFileListImport.Enabled = true;
                listViewFileList.Enabled = true;
                buttonImport.Enabled = true;

                if (listViewFileList.Items.Count > 0)
                {
                    buttonClear.Enabled = true;
                    toolStripMenuItemFileListClear.Enabled = true;
                    listViewFileList.Focus();
                }
            }
            else
            {
                toolStripMenuItemFileListImport.Enabled = true;
                listViewFileList.Enabled = true;
                buttonImport.Enabled = true;
            }
        }

        private void RemoveCommand()
        {
            buttonRemove.Enabled = false;
            toolStripMenuItemFileListRemove.Enabled = false;

            listViewFileList.BeginUpdate();
            while (listViewFileList.SelectedItems.Count > 0)
            {
                listViewFileList.SelectedItems[0].Remove();
            }
            UpdateTargetFileNames();
            AutoResizeListViewColumns();
            listViewFileList.Focus();
            listViewFileList.EndUpdate();

            if (listViewFileList.Items.Count == 0)
            {
                buttonClear.Enabled = false;
                toolStripMenuItemFileListRemove.Enabled = false;
            }
        }

        private void ClearCommand()
        {
            buttonClear.Enabled = false;
            toolStripMenuItemFileListClear.Enabled = false;
            buttonRemove.Enabled = false;
            toolStripMenuItemFileListRemove.Enabled = false;

            listViewFileList.Items.Clear();
            listViewFileList.Focus();
        }

        private void UpdateCommand()
        {
            listViewFileList.BeginUpdate();
            UpdateTargetFileNames();
            AutoResizeListViewColumns();
            listViewFileList.EndUpdate();
        }

        private void ImportFiles(int index, IEnumerable<string> files)
        {
            foreach (var file in files)
            {
                bool exists = false;
                foreach (ListViewItem item in listViewFileList.Items)
                {
                    if (item.SubItems[1].Text.Equals(file, StringComparison.OrdinalIgnoreCase))
                    {
                        item.Selected = true;
                        exists = true;
                        break;
                    }
                }
                if (!exists && File.Exists(file))
                {
                    ListViewItem new_item = new ListViewItem(Path.GetFileName(file));
                    new_item.SubItems.Add(file);
                    listViewFileList.Items.Insert(index, new_item);
                    new_item.Selected = true;
                    index++;
                }
            }
            UpdateTargetFileNames();
        }

        private void UpdateTargetFileNames()
        {
            for (int i = 0; i < listViewFileList.Items.Count; i++)
            {
                var subitems = listViewFileList.Items[i].SubItems;
                if (subitems.Count <= 2)
                {
                    subitems.Add(Renamer.GetNewFileName(comboBoxFileNameFormat.Text, (int)numericUpDownStartId.Value + i) + GetFormattedExtension(subitems[1].Text));
                }
                else
                {
                    subitems[2].Text = Renamer.GetNewFileName(comboBoxFileNameFormat.Text, (int)numericUpDownStartId.Value + i) + GetFormattedExtension(subitems[1].Text);
                }
            }
        }

        private string GetFormattedExtension(string file)
        {
            switch (comboBoxExtensionFormat.SelectedIndex)
            {
                case 1:
                    {
                        string extension = Path.GetExtension(file);
                        return extension == null ? string.Empty : extension.ToLower();
                    }
                case 2:
                    {
                        string extension = Path.GetExtension(file);
                        return extension == null ? string.Empty : extension.ToUpper();
                    }
                default:
                    {
                        string extension = Path.GetExtension(file);
                        return extension ?? string.Empty;
                    }
            }
        }

        private void AutoResizeListViewColumns()
        {
            if (listViewFileList.Items.Count > 0)
            {
                listViewFileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
        }
    }
}
