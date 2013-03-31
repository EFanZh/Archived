using System.Collections.Generic;
using System.IO;

namespace WindowsDataTypes
{
    internal static class WindowsDataTypes
    {
        public static Dictionary<string, string> GetTypeData()
        {
            StreamReader sr = new StreamReader("Data.txt");
            var dict = new Dictionary<string, string>();

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine().Trim();
                if (line.StartsWith("#"))
                {
                    continue;
                }
                else
                {
                    int split = line.IndexOf('=');
                    dict[line.Substring(0, split).Trim()] = line.Substring(split + 1).Trim();
                }
            }

            return dict;
        }

        public static Dictionary<string, List<KeyValuePair<string, string>>> GetResovledTypeData(Dictionary<string, string> type_data)
        {
            var dict = new Dictionary<string, List<KeyValuePair<string, string>>>();

            foreach (var kvp in type_data)
            {
                string[] items = kvp.Value.Split(',');
                var item_list = new List<KeyValuePair<string, string>>();

                foreach (var item in kvp.Value.Split(','))
                {
                    int split = item.IndexOf(':');
                    if (split >= 0)
                    {
                        item_list.Add(new KeyValuePair<string, string>(item.Substring(0, split), item.Substring(split + 1)));
                    }
                    else
                    {
                        item_list.Add(new KeyValuePair<string, string>(string.Empty, item));
                    }
                }

                dict[kvp.Key] = item_list;
            }

            return dict;
        }
    }
}
