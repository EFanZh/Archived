using System;
using System.IO;

namespace AutoWakeUp
{
    internal static class ConfigManager
    {
        private const string config_file = "config.txt";
        private const string wake_up_time_key = "Wake Up Time";
        private const string run_program_key = "Run Program";
        private const string run_program_parameter_key = "Run Program Parameter";
        private const string wait_until_time_key = "Wait Until Time";
        private const string do_stuff_key = "Do Stuff";

        public static DateTime WakeUpTime
        {
            get;
            set;
        }

        public static string RunProgram
        {
            get;
            set;
        }

        public static string RunProgramParameter
        {
            get;
            set;
        }

        public static DateTime WaitUntilTime
        {
            get;
            set;
        }

        public static string DoStuff
        {
            get;
            set;
        }

        public static bool LoadConfig()
        {
            bool result = false;

            try
            {
                using (StreamReader sr = new StreamReader(config_file))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine().Trim();
                        Func<string, string> get_line_value = (key) =>
                        {
                            if (line.StartsWith(key))
                            {
                                int i = key.Length;
                                while (char.IsWhiteSpace(line[i]))
                                {
                                    i++;
                                }
                                if (line[i] != '=')
                                {
                                    return null;
                                }
                                return line.Substring(i + 1).Trim();
                            }
                            return null;
                        };
                        string value = get_line_value(wake_up_time_key);
                        if (value != null)
                        {
                            WakeUpTime = DateTime.Parse(value);
                            continue;
                        }
                        value = get_line_value(run_program_key);
                        if (value != null)
                        {
                            RunProgram = value;
                            continue;
                        }
                        value = get_line_value(run_program_parameter_key);
                        if (value != null)
                        {
                            RunProgramParameter = value;
                            continue;
                        }
                        value = get_line_value(wait_until_time_key);
                        if (value != null)
                        {
                            WaitUntilTime = DateTime.Parse(value);
                            continue;
                        }
                        value = get_line_value(do_stuff_key);
                        if (value != null)
                        {
                            DoStuff = value;
                            continue;
                        }
                    }
                    result = true;
                }
            }
            catch (Exception)
            {
            }
            return result;
        }

        public static void SaveConfig()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(config_file))
                {
                    Action<string, string> write_config = (key, value) =>
                    {
                        sw.WriteLine(string.Format("{0} = {1}", key, value).Trim());
                    };
                    write_config(wake_up_time_key, string.Format("{0:00}:{1:00}:{2:00}", WakeUpTime.Hour, WakeUpTime.Minute, WakeUpTime.Second));
                    write_config(run_program_key, RunProgram);
                    write_config(run_program_parameter_key, RunProgramParameter);
                    write_config(wait_until_time_key, string.Format("{0:00}:{1:00}:{2:00}", WaitUntilTime.Hour, WaitUntilTime.Minute, WaitUntilTime.Second));
                    write_config(do_stuff_key, DoStuff);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
