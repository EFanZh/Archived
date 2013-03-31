using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using ImgProc.Shared;

namespace ImgProc
{
    internal static class PluginManager
    {
        const string pluginPath = @"\Plugins\";
        static string fullPluginPath = Utilities.CombinePath(Application.StartupPath, pluginPath);

        static PluginManager()
        {
            InputPluginTypes = new HashSet<Type>();
            ProcessingPluginTypes = new HashSet<Type>();
            OutputPluginTypes = new HashSet<Type>();

            Initialize();
        }

        public static ISet<Type> InputPluginTypes
        {
            get;
            private set;
        }

        public static ISet<Type> ProcessingPluginTypes
        {
            get;
            private set;
        }

        public static ISet<Type> OutputPluginTypes
        {
            get;
            private set;
        }

        private static void Initialize()
        {
            if (!Directory.Exists(fullPluginPath))
            {
                MessageBox.Show("未检测到插件目录，系统可能将无法正常运作。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Directory.CreateDirectory(fullPluginPath);
            }
            LoadPlugins();
        }

        private static void LoadPlugins()
        {
            foreach (var file in Directory.GetFiles(fullPluginPath, "*.dll"))
            {
                try
                {
                    var types = Assembly.LoadFrom(file).GetTypes();
                    foreach (var type in types)
                    {
                        if (type.IsTypeOf(typeof(IPlugin)))
                        {
                            if (type.IsTypeOf(typeof(IInputPlugin)))
                            {
                                InputPluginTypes.Add(type);
                            }
                            else if (type.IsTypeOf(typeof(IProcessingPlugin)))
                            {
                                ProcessingPluginTypes.Add(type);
                            }
                            else if (type.IsTypeOf(typeof(IOutputPlugin)))
                            {
                                OutputPluginTypes.Add(type);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }
    }
}
