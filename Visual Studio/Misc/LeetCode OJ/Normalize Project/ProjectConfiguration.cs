using System.Xml;

namespace NormalizeProject
{
    internal class ProjectConfiguration
    {
        public XmlElement ProjectGuid
        {
            get;
            set;
        }

        public XmlElement RootNamespace
        {
            get;
            set;
        }

        public XmlElement ClCompileGlobal
        {
            get;
            set;
        }

        public XmlElement ClCompileSourceFiles
        {
            get;
            set;
        }

        public XmlElement ClIncludes
        {
            get;
            set;
        }

        public void AcceptContent(ProjectConfiguration other)
        {
            ProjectGuid.InnerXml = other.ProjectGuid.InnerXml;
            RootNamespace.InnerXml = other.RootNamespace.InnerXml;
            ClCompileSourceFiles.InnerXml = other.ClCompileSourceFiles.InnerXml;
            ClIncludes.InnerXml = other.ClIncludes.InnerXml;
        }

        public void AddGlobalClCompileOption(string name, string value)
        {
            var document = ClCompileGlobal.OwnerDocument;
            var nsUri = document.DocumentElement.NamespaceURI;
            var node = ClCompileGlobal.AppendChild(document.CreateElement(name, nsUri));

            node.AppendChild(document.CreateTextNode(value));
        }
    }
}
