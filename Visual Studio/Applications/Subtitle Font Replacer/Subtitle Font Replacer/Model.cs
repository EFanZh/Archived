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
        private const string FontMappingsFile = "Font Mappings.txt";
        private const string VirtualFontsFile = "Virtual Fonts.txt";
        private bool fontMappingsLoaded = false;
        private bool virtualFontsLoaded = false;

        public void Load()
        {
            LoadConfig(FontMappingsFile, (x, y, z) => new FontMapping(x, y, z), FontMappings, ref fontMappingsLoaded);
            LoadConfig(VirtualFontsFile, (x, y, z) => new VirtualFont(x, y, z), VirtualFonts, ref virtualFontsLoaded);
        }

        public void Save()
        {
            SaveConfig(FontMappingsFile, fontMappingsLoaded, FontMappings, m => m.Original, m => m.Target, m => m.VerticalTarget);
            SaveConfig(VirtualFontsFile, virtualFontsLoaded, VirtualFonts, f => f.Name, f => f.HorizontalFont, f => f.VerticalFont);
        }

        public ObservableCollection<string> ExistingFonts
        {
            get;
        } = new ObservableCollection<string>();

        public ObservableCollection<FontMapping> FontMappings
        {
            get;
        } = new ObservableCollection<FontMapping>();

        public ObservableCollection<VirtualFont> VirtualFonts
        {
            get;
        } = new ObservableCollection<VirtualFont>();

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
            var horizontalVirtualFonts = CreateVirtualFontDictionary(f => f.HorizontalFont);
            var verticalVirtualFonts = CreateVirtualFontDictionary(f => f.VerticalFont);

            foreach (var fontMapping in FontMappings)
            {
                if (string.IsNullOrWhiteSpace(fontMapping.Original) || string.IsNullOrWhiteSpace(fontMapping.Target))
                {
                    throw new Exception("Original font or target font cannot be empty.");
                }

                var key = fontMapping.Original.ToUpper(CultureInfo.InvariantCulture).Trim();
                var target = fontMapping.Target.Trim();

                dict.Add(key, UseVirtualFont(target, horizontalVirtualFonts));

                if (string.IsNullOrWhiteSpace(fontMapping.VerticalTarget))
                {
                    dict.Add('@' + key, '@' + UseVirtualFont(target, verticalVirtualFonts));
                }
                else
                {
                    dict.Add('@' + key, '@' + UseVirtualFont(fontMapping.VerticalTarget.Trim(), verticalVirtualFonts));
                }
            }

            return dict;
        }

        private IDictionary<string, string> CreateVirtualFontDictionary(Func<VirtualFont, string> getValue)
        {
            var dict = new Dictionary<string, string>();

            foreach (var virtualFont in VirtualFonts)
            {
                if (string.IsNullOrEmpty(virtualFont.Name) || string.IsNullOrEmpty(virtualFont.HorizontalFont))
                {
                    throw new Exception("Virtual font name or horizontal font cannot be empty.");
                }

                var value = getValue(virtualFont);

                dict.Add(virtualFont.Name, string.IsNullOrWhiteSpace(value) ? virtualFont.HorizontalFont : value);
            }

            return dict;
        }

        private static string UseVirtualFont(string fontName, IDictionary<string, string> virtualFonts)
        {
            string result;

            return virtualFonts.TryGetValue(fontName, out result) ? result : fontName;
        }

        private static void LoadConfig<T>(string file, Func<string, string, string, T> makeItem, ICollection<T> result, ref bool loaded)
        {
            try
            {
                foreach (var line in File.ReadAllLines(file).Select(l => l.Split(',')))
                {
                    if (line.Length >= 3)
                    {
                        var newFont = line[1].Trim();
                        var newVerticalFont = line[2].Trim();

                        if (string.Equals(newFont, newVerticalFont, StringComparison.InvariantCulture))
                        {
                            newVerticalFont = null;
                        }

                        result.Add(makeItem(line[0].Trim(), newFont, newVerticalFont));
                    }
                    else if (line.Length == 2)
                    {
                        result.Add(makeItem(line[0].Trim(), line[1].Trim(), null));
                    }
                }

                loaded = true;
            }
            catch (Exception)
            {
                // Ignored.
            }
        }

        private static void SaveConfig<T>(string file, bool loaded, ICollection<T> result, Func<T, string> extractItem1, Func<T, string> extractItem2, Func<T, string> extractItem3)
        {
            if (loaded || result.Count > 0)
            {
                try
                {
                    File.WriteAllLines(file, result.OrderBy(extractItem1).Select(m =>
                    {
                        if (string.IsNullOrEmpty(extractItem3(m)) || string.Equals(extractItem2(m), extractItem3(m)))
                        {
                            return $"{ extractItem1(m).Trim()}, {extractItem2(m).Trim()}";
                        }
                        else
                        {
                            return $"{ extractItem1(m).Trim()}, {extractItem2(m).Trim()}, {extractItem3(m).Trim()}";
                        }
                    }));
                }
                catch (Exception)
                {
                    // Ignored.
                }
            }
        }
    }
}
