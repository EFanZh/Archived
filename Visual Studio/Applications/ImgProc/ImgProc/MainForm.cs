using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ImgProc.Shared;

namespace ImgProc
{
    public partial class MainForm : Form
    {
        GetBitmapManager getBitmapManager = new GetBitmapManager();
        Dictionary<object, string> getBitmapManagerUserStateToInput = new Dictionary<object, string>();
        ImageProcessingManager imageProcessingManager = new ImageProcessingManager();
        Dictionary<string, Bitmap> bitmapPreviewCache = new Dictionary<string, Bitmap>();
        Dictionary<ToolStripDropDownItem, Type> menuItemToProcessingPluginType = new Dictionary<ToolStripDropDownItem, Type>();
        Dictionary<ListViewItem, IProcessingPlugin> listViewItemToProcessingPlugin = new Dictionary<ListViewItem, IProcessingPlugin>();
        Dictionary<object, Type> comboBoxItemToOutputPluginType = new Dictionary<object, Type>();
        string currentOutputType;
        IOutputPlugin currentOutputPlugin;
        OptionForm optionForm = new OptionForm();
        AboutForm aboutForm = new AboutForm();

        public MainForm()
        {
            InitializeComponent();

            Initialize();
        }

        private void Initialize()
        {
            // Open file dialog filters.
            {
                var inputPlugins = Utilities.CreateInstanceSet<IInputPlugin>(PluginManager.InputPluginTypes);
                var allExtensionSet = new SortedSet<string>();
                var formatExtensionSet = new SortedSet<string>();
                foreach (var inputPlugin in inputPlugins)
                {
                    foreach (var kvp in inputPlugin.SupportedExtensions)
                    {
                        var tempExtensionSet = new SortedSet<string>();
                        foreach (var extension in kvp.Value)
                        {
                            tempExtensionSet.Add(extension);
                            allExtensionSet.Add(extension);
                        }
                        formatExtensionSet.Add(string.Format("{0} ({1})|{1}", kvp.Key, "*." + string.Join("; *.", tempExtensionSet)));
                    }
                }
                var filterSet = new HashSet<string>();
                if (allExtensionSet.Count > 0)
                {
                    filterSet.Add(string.Format("所有支持的文件类型 ({0})|{0}", "*." + string.Join("; *.", allExtensionSet)));
                }
                if (formatExtensionSet.Count > 0)
                {
                    filterSet.Add(string.Join("|", formatExtensionSet));
                }
                filterSet.Add("所有文件 (*.*)|*.*");
                openFileDialogMain.Filter = string.Join("|", filterSet);
            }

            // getBitmapManager
            {
                getBitmapManager.GetBitmapCompleted += new EventHandler<GetBitmapCompletedEventArgs>(getBitmapManager_GetBitmapCompleted);
            }

            // ToolStrip
            {
                var tspr = new ToolStripProfessionalRenderer() { RoundedEdges = false };
                toolStripMain.Renderer = tspr;
                toolStripImageList.Renderer = tspr;
                toolStripProcessing.Renderer = tspr;
            }

            // Processing Plugins
            {
                var processingPlugins = Utilities.CreateInstanceSet<IProcessingPlugin>(PluginManager.ProcessingPluginTypes);
                foreach (var processingPlugin in processingPlugins)
                {
                    ToolStripDropDownItem currentItem = toolStripDropDownButtonAddProcessing;
                    foreach (var folderName in processingPlugin.SetupPath)
                    {
                        var s = from ToolStripDropDownItem item in currentItem.DropDownItems where string.Equals(item.Text, folderName) select item;
                        if (s.Count() > 0)
                        {
                            currentItem = s.First();
                        }
                        else
                        {
                            var newItem = new ToolStripMenuItem(folderName);
                            currentItem.DropDownItems.Add(newItem);
                            currentItem = newItem;
                        }
                    }
                    currentItem.Click += new EventHandler(currentItem_Click);
                    menuItemToProcessingPluginType[currentItem] = processingPlugin.GetType();
                }
            }

            // Output type ComboBox
            {
                var outputPlugins = Utilities.CreateInstanceSet<IOutputPlugin>(PluginManager.OutputPluginTypes).OrderBy(outputPlugin => outputPlugin.TypeName);
                foreach (var outputPlugin in outputPlugins)
                {
                    comboBoxItemToOutputPluginType[outputPlugin.TypeName] = outputPlugin.GetType();
                    comboBoxOutputType.Items.Add(outputPlugin.TypeName);
                }
                if (comboBoxOutputType.Items.Count > 0)
                {
                    comboBoxOutputType.SelectedIndex = 0;
                }
            }
        }

        private void currentItem_Click(object sender, EventArgs e)
        {
            Type processingType = menuItemToProcessingPluginType[sender as ToolStripDropDownItem];
            IProcessingPlugin newProcessing = Activator.CreateInstance(processingType) as IProcessingPlugin;
            if (newProcessing.ConfigForm != null)
            {
                if (newProcessing.ConfigForm.ShowDialog() != DialogResult.OK)
                {
                    newProcessing.Dispose();
                    return;
                }
            }
            ListViewItem lvi = new ListViewItem(newProcessing.Name);
            listViewItemToProcessingPlugin[lvi] = newProcessing;
            listViewProcessingList.Items.Add(lvi);
            listViewProcessingList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void getBitmapManager_GetBitmapCompleted(object sender, GetBitmapCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                bitmapPreviewCache[getBitmapManagerUserStateToInput[e.UserState]] = e.Bitmap;
                if (listViewImageList.SelectedItems.Count > 0)
                {
                    string key = listViewImageList.SelectedItems[0].Text;
                    if (bitmapPreviewCache.ContainsKey(key))
                    {
                        pictureBoxImagePreview.Image = bitmapPreviewCache[key];
                    }
                }
            }
        }

        #region Control events

        #region UI update

        private void MainForm_Activated(object sender, EventArgs e)
        {
            myGradientTitleBarImageList.Activate();
            myGradientTitleBarImagePreview.Activate();
            myGradientTitleBarProcessing.Activate();
        }

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            myGradientTitleBarImageList.Deactivate();
            myGradientTitleBarImagePreview.Deactivate();
            myGradientTitleBarProcessing.Deactivate();
        }

        private void panelView_Resize(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                if (panelImageList.Width < mySplitterLeftRight.MinExtra)
                {
                    panelRight.Width = panelView.ClientSize.Width - mySplitterLeftRight.Width - mySplitterLeftRight.MinExtra;
                }
            }
        }

        private void panelRight_Resize(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                if (panelProcessing.Height < mySplitterTopBottom.MinExtra)
                {
                    panelImagePreview.Height = panelRight.ClientSize.Height - mySplitterTopBottom.Height - mySplitterTopBottom.MinExtra;
                }
            }
        }

        #endregion UI update

        private void toolStripButtonAddImages_Click(object sender, EventArgs e)
        {
            toolStripButtonAddImages.Enabled = false;
            if (openFileDialogMain.ShowDialog() == DialogResult.OK)
            {
                foreach (var fileName in openFileDialogMain.FileNames)
                {
                    if ((from ListViewItem item in listViewImageList.Items where string.Equals(item.Text, fileName) select item).Count() == 0)
                    {
                        listViewImageList.Items.Add(fileName);
                    }
                }
                listViewImageList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            toolStripButtonAddImages.Enabled = true;
        }

        private void toolStripButtonDeleteImages_Click(object sender, EventArgs e)
        {
            toolStripButtonDeleteImages.Enabled = false;
            foreach (ListViewItem item in listViewImageList.SelectedItems)
            {
                item.Remove();
            }
            if (listViewImageList.Items.Count > 0)
            {
                listViewImageList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            toolStripButtonDeleteImages.Enabled = true;
        }

        private void listViewImageList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewImageList.SelectedItems.Count > 0)
            {
                toolStripButtonDeleteImages.Enabled = true;
                string key = listViewImageList.SelectedItems[0].Text;
                if (bitmapPreviewCache.ContainsKey(key))
                {
                    pictureBoxImagePreview.Image = bitmapPreviewCache[key];
                }
                else
                {
                    object userState = Guid.NewGuid();
                    getBitmapManagerUserStateToInput[userState] = key;
                    getBitmapManager.GetBitmapAsync(key, userState);
                }
            }
            else
            {
                toolStripButtonDeleteImages.Enabled = false;
                pictureBoxImagePreview.Image = null;
            }
        }

        private void toolStripButtonOptions_Click(object sender, EventArgs e)
        {
            toolStripButtonOptions.Enabled = false;
            optionForm.ShowDialog();
            toolStripButtonOptions.Enabled = true;
        }

        private void toolStripButtonAbout_Click(object sender, EventArgs e)
        {
            toolStripButtonAbout.Enabled = false;
            aboutForm.ShowDialog();
            toolStripButtonAbout.Enabled = true;
        }

        private void toolStripButtonDeleteProcessing_Click(object sender, EventArgs e)
        {
            toolStripButtonDeleteProcessing.Enabled = false;
            foreach (ListViewItem lvi in listViewProcessingList.SelectedItems)
            {
                if (listViewItemToProcessingPlugin.Remove(lvi))
                {
                    lvi.Remove();
                }
            }
            if (listViewProcessingList.Items.Count > 0)
            {
                listViewProcessingList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            toolStripButtonDeleteProcessing.Enabled = true;
        }

        private void toolStripButtonProcessingOption_Click(object sender, EventArgs e)
        {
            toolStripButtonProcessingOption.Enabled = false;
            listViewItemToProcessingPlugin[listViewProcessingList.SelectedItems[0]].ConfigForm.ShowDialog();
            toolStripButtonProcessingOption.Enabled = true;
        }

        private void listViewProcessingList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewProcessingList.SelectedItems.Count > 0)
            {
                toolStripButtonDeleteProcessing.Enabled = true;
                if (listViewProcessingList.SelectedItems.Count == 1 && listViewItemToProcessingPlugin[listViewProcessingList.SelectedItems[0]].ConfigForm != null)
                {
                    toolStripButtonProcessingOption.Enabled = true;
                }
                else
                {
                    toolStripButtonProcessingOption.Enabled = false;
                }
            }
            else
            {
                toolStripButtonDeleteProcessing.Enabled = false;
            }
        }

        private void buttonOutputPath_Click(object sender, EventArgs e)
        {
            buttonOutputPath.Enabled = false;
            if (folderBrowserDialogMain.ShowDialog() == DialogResult.OK)
            {
                textBoxOutputPath.Text = folderBrowserDialogMain.SelectedPath;
            }
            buttonOutputPath.Enabled = true;
        }

        private void comboBoxOutputType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currentOutputPlugin != null)
            {
                currentOutputPlugin.Dispose();
            }
            currentOutputType = comboBoxOutputType.Text;
            currentOutputPlugin = Activator.CreateInstance(comboBoxItemToOutputPluginType[currentOutputType]) as IOutputPlugin;
            if (currentOutputPlugin.ConfigForm == null)
            {
                buttonOutputOption.Enabled = false;
            }
            else
            {
                buttonOutputOption.Enabled = true;
            }
        }

        private void buttonOutputOption_Click(object sender, EventArgs e)
        {
            if (currentOutputPlugin.ConfigForm != null)
            {
                currentOutputPlugin.ConfigForm.ShowDialog();
            }
        }

        private void buttonStartProcessing_Click(object sender, EventArgs e)
        {
            if (listViewImageList.Items.Count > 0 && !string.IsNullOrEmpty(textBoxOutputPath.Text) && !string.IsNullOrEmpty(comboBoxOutputType.Text))
            {
                var inputToOutput = new Dictionary<string, string>();
                foreach (ListViewItem lvi in listViewImageList.Items)
                {
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(lvi.Text);
                    string outputPath = Utilities.CombinePath(textBoxOutputPath.Text, string.Format("{0}.{1}", fileNameWithoutExt, currentOutputPlugin.SelectedExtension));
                    if (inputToOutput.ContainsValue(outputPath) || File.Exists(outputPath))
                    {
                        int i = 1;
                        do
                        {
                            outputPath = Utilities.CombinePath(textBoxOutputPath.Text, string.Format("{0} {1}.{2}", fileNameWithoutExt, i, currentOutputPlugin.SelectedExtension));
                            i++;
                        }
                        while (inputToOutput.ContainsValue(outputPath));
                    }
                    inputToOutput.Add(lvi.Text, outputPath);
                }
                var processingList = new List<IProcessingPlugin>();
                foreach (ListViewItem lvi in listViewProcessingList.Items)
                {
                    processingList.Add(listViewItemToProcessingPlugin[lvi]);
                }
                imageProcessingManager.ProcessAsync(inputToOutput, processingList, currentOutputPlugin, Guid.NewGuid());
            }
            else
            {
                if (listViewImageList.Items.Count == 0)
                {
                    MessageBox.Show("图像列表为空。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrEmpty(textBoxOutputPath.Text))
                {
                    MessageBox.Show("输出路径为空。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrEmpty(comboBoxOutputType.Text))
                {
                    MessageBox.Show("输出插件为空。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        #endregion Control events
    }
}
