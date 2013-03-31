using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SimpleBatchRename
{
    internal static class Renamer
    {
        public static string GetNewFileName(string file_name_pattern, int id)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            string id_string = id.ToString();

            while (i < file_name_pattern.Length)
            {
                int count = 0;
                while (i < file_name_pattern.Length && file_name_pattern[i] == '#')
                {
                    count++;
                    i++;
                }
                if (count > 0)
                {
                    if (id_string.Length < count)
                    {
                        sb.Append(new string('0', count - id_string.Length));
                    }
                    sb.Append(id_string);
                }

                while (i < file_name_pattern.Length && file_name_pattern[i] != '#')
                {
                    sb.Append(file_name_pattern[i]);
                    i++;
                }
            }

            return sb.ToString();
        }

        public static void Rename(Dictionary<string, string> file_to_new_file)
        {
            var file_to_new_file_copy = new Dictionary<string, string>(file_to_new_file);
            while (file_to_new_file_copy.Count > 0)
            {
                var kvp = file_to_new_file_copy.ElementAt(0);
                Action<string> recursive_rename_delegate = null;
                recursive_rename_delegate = (string file) =>
                {
                    if (File.Exists(file_to_new_file_copy[file]))
                    {
                        if (file_to_new_file_copy[file] == kvp.Key)
                        {
                            string temp_file = Path.Combine(Path.GetDirectoryName(file), Path.GetRandomFileName());
                            File.Move(file, temp_file);
                            file_to_new_file_copy[temp_file] = file_to_new_file_copy[file];
                            file_to_new_file_copy.Remove(file);
                            return;
                        }
                        else
                        {
                            recursive_rename_delegate(file_to_new_file_copy[file]);
                        }
                    }
                    File.Move(file, file_to_new_file_copy[file]);
                    file_to_new_file_copy.Remove(file);
                };
                recursive_rename_delegate(kvp.Key);
            }
        }

        public static bool RenameTest(Dictionary<string, string> file_to_new_file)
        {
            for (int i = 0; i < file_to_new_file.Count - 1; i++)
            {
                for (int j = i + 1; j < file_to_new_file.Count; j++)
                {
                    if (file_to_new_file.Values.ElementAt(i).Equals(file_to_new_file.Values.ElementAt(j)))
                    {
                        return false;
                    }
                }
            }
            foreach (var kvp in file_to_new_file)
            {
                if (File.Exists(kvp.Value) && !file_to_new_file.ContainsKey(kvp.Value))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
