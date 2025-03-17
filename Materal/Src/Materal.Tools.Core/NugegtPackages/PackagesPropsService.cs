namespace Materal.Tools.Core.NugegtPackages
{
    using System.Xml.Linq;

    /// <summary>
    /// 包Props文件服务
    /// </summary>
    public class PackagesPropsService : IPackagesPropsService
    {
        /// <inheritdoc/>
        public void SortAndRemoveDuplicates(string filePath)
        {
            FileInfo fileInfo = new(filePath);
            SortAndRemoveDuplicates(fileInfo);
        }
        /// <inheritdoc/>
        public void SortAndRemoveDuplicates(FileInfo fileInfo)
        {
            if (!fileInfo.Exists) throw new ToolsException("文件不存在");
            XDocument doc = XDocument.Load(fileInfo.FullName);
            XElement? rootElement = doc.Root ?? throw new ToolsException("无效的XML文件");
            List<XElement> itemGroups = [.. rootElement.Elements("ItemGroup")];
            foreach (XElement itemGroup in itemGroups)
            {
                List<XElement> packageVersions = [.. itemGroup.Elements("PackageVersion")
                    .Select(e => new
                    {
                        Element = e,
                        Include = e.Attribute("Include")?.Value ?? string.Empty
                    })
                    .OrderBy(x => x.Include)
                    .Select(x => x.Element)
                    .Distinct(new PackageVersionComparer())];
                itemGroup.RemoveNodes();
                packageVersions.ForEach(itemGroup.Add);
            }
            doc.Save(fileInfo.FullName);
        }
        /// <summary>
        /// PackageVersion比较器
        /// </summary>
        private class PackageVersionComparer : IEqualityComparer<XElement>
        {
            public bool Equals(XElement? x, XElement? y)
            {
                if (x == null || y == null) return false;
                string? xInclude = x.Attribute("Include")?.Value;
                string? yInclude = y.Attribute("Include")?.Value;
                return xInclude == yInclude;
            }
            public int GetHashCode(XElement obj)
            {
                string? include = obj.Attribute("Include")?.Value;
                return include?.GetHashCode() ?? 0;
            }
        }
    }
}
