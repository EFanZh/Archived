using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CleanupVisualStudioInstaller
{
    internal class Program
    {
        private class DictionaryComparer<TKey, TValue> : IComparer<IReadOnlyDictionary<TKey, TValue>>
            where TKey : IComparable<TKey>
            where TValue : IComparable<TValue>
        {
            public int Compare(IReadOnlyDictionary<TKey, TValue> x, IReadOnlyDictionary<TKey, TValue> y)
            {
                var e1 = x.OrderBy(kvp => kvp.Key).GetEnumerator();
                var e2 = y.OrderBy(kvp => kvp.Key).GetEnumerator();

                while (true)
                {
                    if (e1.MoveNext())
                    {
                        if (e2.MoveNext())
                        {
                            var result = e1.Current.Key.CompareTo(e2.Current.Key);

                            if (result != 0)
                            {
                                return result;
                            }

                            result = e1.Current.Value.CompareTo(e2.Current.Value);

                            if (result != 0)
                            {
                                return result;
                            }
                        }
                        else
                        {
                            return 1;
                        }
                    }
                    else
                    {
                        return e2.MoveNext() ? -1 : 0;
                    }
                }
            }
        }

        private static SortedDictionary<string, string> ParsePackage(string packageSpecification)
        {
            var result = new SortedDictionary<string, string>();

            foreach (var item in packageSpecification.Split(','))
            {
                var keyAndValue = item.Split('=');

                if (keyAndValue.Length == 1)
                {
                    result.Add("_name", keyAndValue[0]);
                }
                else if (keyAndValue.Length == 2)
                {
                    result.Add(keyAndValue[0], keyAndValue[1]);
                }
            }

            if (result.ContainsKey("version"))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        private static int Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Installer path is required.");

                return 1;
            }
            else
            {
                var installerPath = args[0];
                var packages = new SortedDictionary<SortedDictionary<string, string>, SortedDictionary<Version, string>>(new DictionaryComparer<string, string>());

                foreach (var packageFolder in Directory.GetDirectories(installerPath))
                {
                    var packageInfo = ParsePackage(Path.GetFileName(packageFolder));

                    if (packageInfo != null)
                    {
                        var version = packageInfo["version"];

                        packageInfo.Remove("version");

                        if (!packages.TryGetValue(packageInfo, out SortedDictionary<Version, string> packageVersions))
                        {
                            packageVersions = new SortedDictionary<Version, string>();

                            packages.Add(packageInfo, packageVersions);
                        }

                        packageVersions.Add(new Version(version), packageFolder);
                    }
                }

                foreach (var package in packages)
                {
                    if (package.Value.Count > 1)
                    {
                        foreach (var version in package.Value.Take(package.Value.Count - 1))
                        {
                            Console.WriteLine(version.Value);
                            Directory.Delete(version.Value, true);
                        }
                    }
                }

                return 0;
            }
        }
    }
}
