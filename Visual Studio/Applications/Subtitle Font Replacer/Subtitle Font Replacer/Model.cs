using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;

namespace SubtitleFontReplacer
{
    public class Model
    {
        private const string ConfigFile = "config.txt";
        private bool loaded = false;

        public Model()
        {
            ExistingFonts = new ObservableCollection<string>();
            FontMappings = new ObservableCollection<FontMapping>();
        }

        public void Load()
        {
            try
            {
                foreach (var line in File.ReadAllLines(ConfigFile).Select(l => l.Split(',')))
                {
                    if (line.Length >= 3)
                    {
                        var newFont = line[1].Trim();
                        var newVerticalFont = line[2].Trim();

                        if (string.Equals(newFont, newVerticalFont, StringComparison.InvariantCulture))
                        {
                            newVerticalFont = null;
                        }

                        FontMappings.Add(new FontMapping(line[0].Trim(), newFont, newVerticalFont));
                    }
                    else if (line.Length == 2)
                    {
                        FontMappings.Add(new FontMapping(line[0].Trim(), line[1].Trim(), null));
                    }
                }

                loaded = true;
            }
            catch (Exception)
            {
                // Ignored.
            }
        }

        public void Save()
        {
            if (loaded || FontMappings.Count > 0)
            {
                try
                {
                    File.WriteAllLines(ConfigFile, FontMappings.OrderBy(m => m.Original).Select(m =>
                    {
                        if (string.IsNullOrEmpty(m.VerticalTarget) || string.Equals(m.Target, m.VerticalTarget))
                        {
                            return $"{m.Original.Trim()}, {m.Target.Trim()}";
                        }
                        else
                        {
                            return $"{m.Original.Trim()}, {m.Target.Trim()}, {m.VerticalTarget.Trim()}";
                        }
                    }));
                }
                catch (Exception)
                {
                    // Ignored.
                }
            }
        }

        public ObservableCollection<string> ExistingFonts
        {
            get;
        }

        public ObservableCollection<FontMapping> FontMappings
        {
            get;
        }

        public void AddFontMapping(string original, string target, string verticalTarget)
        {
            if (FontMappings.All(m => !string.Equals(m.Original, original, StringComparison.InvariantCultureIgnoreCase)))
            {
                FontMappings.Add(new FontMapping(original, target, verticalTarget));
            }
        }

        public IDictionary<string, string> CreateReplaceDictionary()
        {
            var dict = new Dictionary<string, string>();

            foreach (var fontMapping in FontMappings)
            {
                if (string.IsNullOrWhiteSpace(fontMapping.Original) || string.IsNullOrWhiteSpace(fontMapping.Target))
                {
                    throw new Exception("Original font or target font cannot be empty.");
                }

                string key = fontMapping.Original.ToUpper(CultureInfo.InvariantCulture).Trim();
                string target = fontMapping.Target.Trim();

                dict.Add(key, target);

                if (string.IsNullOrWhiteSpace(fontMapping.VerticalTarget))
                {
                    dict.Add('@' + key, '@' + target);
                }
                else
                {
                    dict.Add('@' + key, '@' + fontMapping.VerticalTarget.Trim());
                }
            }

            return dict;
        }
    }
}
