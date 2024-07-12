#nullable enable
using MateralToolsVSIX.Extensions;
using Microsoft.VisualStudio.PlatformUI;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace MateralToolsVSIX.ToolWindows.ViewModels
{
    /// <summary>
    /// 更改包版本视图模型
    /// </summary>
    public class ChangePackVersionViewModel : ObservableObject
    {
        private string _filter = "^(Materal|RC|Dy)\\..+$";
        public string Filter
        {
            get => _filter;
            set
            {
                _filter = value; NotifyPropertyChanged(nameof(Filter));
            }
        }
        private string _version = "*";
        public string Version
        {
            get => _version;
            set
            {
                _version = value;
                NotifyPropertyChanged(nameof(Version));
            }
        }
        public async Task ChangePackVersionAsync()
        {
            Solution? solution = await VS.Solutions.GetCurrentSolutionAsync();
            if (solution is null) return;
            ChangePackVersion(solution);
        }
        private void ChangePackVersion(SolutionItem solutionItem)
        {
            if (solutionItem.Type == SolutionItemType.Project)
            {
                XmlDocument xmlDocument = new();
                xmlDocument.Load(solutionItem.FullPath);
                XmlNode? projectNode = xmlDocument.SelectSingleNode("//Project");
                if (projectNode is null) return;
                foreach (XmlNode node in projectNode.ChildNodes)
                {
                    if (node.Name != "ItemGroup") continue;
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        if (childNode.Name != "PackageReference" || childNode.Attributes is null || childNode.Attributes.Count <= 0) continue;
                        XmlAttribute? nameAttribute = childNode.Attributes["Include"];
                        if (nameAttribute is null || !Regex.IsMatch(nameAttribute.Value, Filter)) continue;
                        XmlAttribute? versionAttribute = childNode.Attributes["Version"];
                        if (versionAttribute is null) continue;
                        if (versionAttribute.Value == Version) continue;
                        versionAttribute.Value = Version;
                    }
                }
                string xmlContent = xmlDocument.GetFormatXmlContent();
                xmlContent = xmlContent.StartsWith("<?xml") switch
                {
                    true => xmlContent[(xmlContent.IndexOf("?>", StringComparison.Ordinal) + 2)..],
                    false => xmlContent
                };
                xmlContent = xmlContent.Trim();
                File.WriteAllText(solutionItem.FullPath, xmlContent, Encoding.UTF8);
            }
            else
            {
                foreach (SolutionItem? item in solutionItem.Children)
                {
                    if (item is null) continue;
                    ChangePackVersion(item);
                }
            }
        }
    }
}
