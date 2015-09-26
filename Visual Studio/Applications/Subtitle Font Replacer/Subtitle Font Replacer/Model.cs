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
                var fontMappings = from mapping in
                                       from line in File.ReadAllLines(ConfigFile)
                                       where line.Contains(",")
                                       select line.Split(',')
                                   select new FontMapping(mapping[0].Trim(), mapping[1].Trim(), mapping[2].Trim());

                foreach (var mapping in fontMappings)
                {
                    FontMappings.Add(mapping);
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
                    File.WriteAllLines(ConfigFile, FontMappings.OrderBy(m => m.Original).Select(m => $"{m.Original}, {m.Target}, {m.VerticalTarget}"));
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
                string key = fontMapping.Original.ToUpper(CultureInfo.InvariantCulture);

                dict.Add(key, fontMapping.Target);
                dict.Add('@' + key, '@' + fontMapping.VerticalTarget);
            }

            return dict;
        }
    }
}
