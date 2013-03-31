using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowsDataTypes
{
    public partial class MainForm : Form
    {
        private Dictionary<string, string> type_data;
        private Dictionary<string, List<KeyValuePair<string, string>>> resolved_type_data;
        private Dictionary<string, Tuple<string, string>> resolved_type_data_final;

        public MainForm()
        {
            InitializeComponent();

            type_data = WindowsDataTypes.GetTypeData();
            resolved_type_data = WindowsDataTypes.GetResovledTypeData(type_data);
            resolved_type_data_final = new Dictionary<string, Tuple<string, string>>();

            PropertyInfo pi = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(listViewResult, true);
            NativeMethods.SetWindowTheme(listViewResult.Handle, "Explorer", null);

            ResolveTypeDataFinal();
            UpdateListView();
        }

        private void ResolveTypeDataFinal()
        {
            Func<string, string> resolve_type_final = null, resolve_type = type =>
            {
                var item_list = resolved_type_data[type];

                foreach (var item in item_list)
                {
                    if (string.IsNullOrEmpty(item.Key))
                    {
                        return item.Value;
                    }
                    switch (item.Key)
                    {
                        case "UNICODE":
                            if (checkBoxUnicode.Checked)
                            {
                                return item.Value;
                            }
                            break;

                        case "WINVER >= 0x0500":
                            if (checkBoxWinVer.Checked)
                            {
                                return item.Value;
                            }
                            break;

                        case "_MSC_VER >= 1300":
                            if (checkBoxMSCVer.Checked)
                            {
                                return item.Value;
                            }
                            break;

                        case "_M_IX86":
                            if (checkBoxMIX86.Checked)
                            {
                                return item.Value;
                            }
                            break;

                        case "_WIN64":
                            if (checkBoxWin64.Checked)
                            {
                                return item.Value;
                            }
                            break;

                        default:
                            if (string.IsNullOrEmpty(item.Key))
                            {
                                return item.Value;
                            }
                            break;
                    }
                }
                return string.Empty;
            };

            resolve_type_final = type =>
            {
                var parts = type.Split('\t', ' ');

                for (int i = 0; i < parts.Length; i++)
                {
                    if (resolved_type_data.ContainsKey(parts[i]))
                    {
                        string type_string = resolve_type(parts[i]);
                        if (string.IsNullOrEmpty(type_string))
                        {
                            return string.Empty;
                        }
                        parts[i] = resolve_type_final(type_string);
                    }
                }
                return string.Join(" ", parts);
            };

            foreach (var kvp in resolved_type_data)
            {
                resolved_type_data_final[kvp.Key] = new Tuple<string, string>(resolve_type(kvp.Key), resolve_type_final(kvp.Key));
            }
        }

        private void UpdateListView()
        {
            Regex regex = new Regex(Regex.Escape(textBoxFilter.Text.Trim().ToLower()).Replace("\\*", ".*").Replace("\\?", "."));

            listViewResult.BeginUpdate();
            listViewResult.Items.Clear();
            foreach (var kvp in resolved_type_data_final)
            {
                if (regex.IsMatch(kvp.Key.ToLower()))
                {
                    var lvi_subitems = listViewResult.Items.Add(kvp.Key).SubItems;
                    lvi_subitems.Add(kvp.Value.Item1);
                    string s = kvp.Value.Item2;
                    while (s.Contains("* *"))
                    {
                        s = s.Replace("* *", "**");
                    }
                    lvi_subitems.Add(s);
                    lvi_subitems.Add(type_data[kvp.Key]);
                }
            }
            if (listViewResult.Items.Count > 0)
            {
                listViewResult.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            else
            {
                listViewResult.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            listViewResult.EndUpdate();
        }

        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            UpdateListView();
        }

        private void checkBoxes_CheckedChanged(object sender, EventArgs e)
        {
            ResolveTypeDataFinal();
            UpdateListView();
        }
    }
}
