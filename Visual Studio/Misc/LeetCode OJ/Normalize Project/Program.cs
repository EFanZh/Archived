using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Linq;

namespace NormalizeProject
{
    internal class Program
    {
        private static (XmlDocument Document, Encoding Encoding) OpenProjectFile(string path)
        {
            using (var streamReader = File.OpenText(path))
            {
                var document = new XmlDocument();

                document.Load(streamReader);

                return (document, streamReader.CurrentEncoding);
            }
        }

        private static void SaveProjectFile(XmlDocument document, string path, Encoding encoding)
        {
            using (var streamWriter = new StreamWriter(File.Open(path, FileMode.Create, FileAccess.Write), encoding))
            {
                document.Save(streamWriter);
            }
        }

        private static ProjectConfiguration ReadProjectConfiguration(XmlDocument document)
        {
            var result = new ProjectConfiguration();
            var documentElement = document.DocumentElement;

            foreach (var group in documentElement.OfType<XmlElement>().Where(x => !x.HasAttribute("Condition")))
            {
                switch (group.Name)
                {
                    case "ItemDefinitionGroup":
                        result.ClCompileGlobal = (XmlElement)group.GetElementsByTagName("ClCompile")[0];

                        break;

                    case "ItemGroup":
                        if (group.OfType<XmlElement>().All(x => x.Name == "ClCompile"))
                        {
                            result.ClCompileSourceFiles = group;
                        }
                        else if (group.OfType<XmlElement>().All(x => x.Name == "ClInclude"))
                        {
                            result.ClIncludes = group;
                        }

                        break;

                    case "PropertyGroup":
                        foreach (var property in group.OfType<XmlElement>())
                        {
                            switch (property.Name)
                            {
                                case "ProjectGuid":
                                    result.ProjectGuid = property;

                                    break;

                                case "RootNamespace":
                                    result.RootNamespace = property;

                                    break;
                            }
                        }

                        break;
                }
            }

            if (result.ClIncludes == null)
            {
                result.ClIncludes = (XmlElement)documentElement.InsertAfter(document.CreateElement("ItemGroup", documentElement.NamespaceURI), result.ClCompileSourceFiles);
            }

            return result;
        }

        private static void ApplyContent(XmlDocument template, XmlDocument document)
        {
            var templateConfiguration = ReadProjectConfiguration(template);

            templateConfiguration.AcceptContent(ReadProjectConfiguration(document));
            templateConfiguration.AddGlobalClCompileOption("ForcedIncludeFiles", @"..\Common.h");
        }

        private static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Parameters: template-file source-file output-file");
            }
            else
            {
                var templateFile = args[0];
                var sourceFile = args[1];
                var outputFile = args[2];
                var (template, encoding) = OpenProjectFile(templateFile);

                ApplyContent(template, OpenProjectFile(sourceFile).Document);
                SaveProjectFile(template, outputFile, encoding);
            }
        }
    }
}
