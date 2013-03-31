using System.Collections.Generic;
using System.Windows.Forms;
using ImgProc.Shared;

namespace ImgProc
{
    public partial class OptionForm : Form
    {
        HashSet<IPlugin> plugins = new HashSet<IPlugin>();
        Dictionary<ListViewItem, IPlugin> listViewItemToPlugin = new Dictionary<ListViewItem, IPlugin>();

        public OptionForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            plugins.UnionWith(Utilities.CreateInstanceSet<IInputPlugin>(PluginManager.InputPluginTypes));
            plugins.UnionWith(Utilities.CreateInstanceSet<IProcessingPlugin>(PluginManager.ProcessingPluginTypes));
            plugins.UnionWith(Utilities.CreateInstanceSet<IOutputPlugin>(PluginManager.OutputPluginTypes));
            foreach (var plugin in plugins)
            {
                ListViewItem lvi = new ListViewItem();
                if (plugin.GetType().IsTypeOf(typeof(IInputPlugin)))
                {
                    lvi.Text = "输入插件";
                }
                else if (plugin.GetType().IsTypeOf(typeof(IProcessingPlugin)))
                {
                    lvi.Text = "处理插件";
                }
                else if (plugin.GetType().IsTypeOf(typeof(IOutputPlugin)))
                {
                    lvi.Text = "输出插件";
                }
                else
                {
                    lvi.Text = "未知插件";
                }
                lvi.SubItems.Add(plugin.Name);
                lvi.SubItems.Add(plugin.GetType().Module.Name);
                listViewPlugins.Items.Add(lvi);
                listViewItemToPlugin.Add(lvi, plugin);
            }
            listViewPlugins.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void listViewPlugins_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (listViewPlugins.SelectedItems.Count > 0)
            {
                var plugin = listViewItemToPlugin[listViewPlugins.SelectedItems[0]];
                if (plugin.GetType().IsTypeOf(typeof(IInputPlugin)) && plugin.ConfigForm != null)
                {
                    buttonConfig.Enabled = true;
                }
                else
                {
                    buttonConfig.Enabled = false;
                }
                if (!string.IsNullOrEmpty(plugin.AboutInfo))
                {
                    buttonAbout.Enabled = true;
                }
                else
                {
                    buttonAbout.Enabled = false;
                }
            }
            else
            {
                buttonConfig.Enabled = false;
                buttonAbout.Enabled = false;
            }
        }

        private void buttonConfig_Click(object sender, System.EventArgs e)
        {
            var plugin = listViewItemToPlugin[listViewPlugins.SelectedItems[0]];
            if (plugin.ConfigForm != null)
            {
                plugin.ConfigForm.ShowDialog();
            }
        }

        private void buttonAbout_Click(object sender, System.EventArgs e)
        {
            var plugin = listViewItemToPlugin[listViewPlugins.SelectedItems[0]];
            if (!string.IsNullOrEmpty(plugin.AboutInfo))
            {
                MessageBox.Show(plugin.AboutInfo, string.Format("关于 {0}", plugin.Name), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
