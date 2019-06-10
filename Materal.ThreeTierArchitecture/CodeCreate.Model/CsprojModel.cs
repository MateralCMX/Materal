using CodeCreate.Common;
using System;
using System.IO;
using Materal.FileHelper;

namespace CodeCreate.Model
{
    public sealed class CsprojModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// SDK
        /// </summary>
        public string Sdk { get; set; } = "Microsoft.NET.Sdk";
        /// <summary>
        /// TargetFramework
        /// </summary>
        public string TargetFramework { get; set; } = "netstandard2.0";
        /// <summary>
        /// 有Xml文件
        /// </summary>
        public bool HasXmlFile { get; set; } = false;
        /// <summary>
        /// ItemGroup
        /// </summary>
        public ItemGroupModel[][] ItemGroups { get; set; }

        public void CreateFile(string targetPath, string subSystemName)
        {
            TextFileManager.WriteText($"{targetPath}/{Name}.csproj", GetFileContent(targetPath, subSystemName), ApplicationConfig.ApplicationEncoding);
        }
        /// <summary>
        /// 获得文件内容
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="subSystemName"></param>
        /// <returns></returns>
        private string GetFileContent(string targetPath, string subSystemName)
        {
            string result = $"<Project Sdk=\"{Sdk}\">\r\n";
            result += "  <PropertyGroup>\r\n";
            result += $"    <TargetFramework>{TargetFramework}</TargetFramework>\r\n";
            if (TargetFramework.IndexOf("netcoreapp", StringComparison.Ordinal) > -1)
            {
                result += "    <AspNetCoreHostingModel>InProcess</AspNetCoreHostingModel>\r\n";
            }
            result += "  </PropertyGroup>\r\n";
            if (HasXmlFile)
            {
                result += "  <PropertyGroup  Condition=\"'$(Configuration)|$(Platform)' == 'Release|AnyCPU'\">\r\n";
                result += $"    <DocumentationFile>{Name}.xml</DocumentationFile>\r\n";
                result += "  </PropertyGroup>\r\n";
                result += "  <PropertyGroup  Condition=\"'$(Configuration)|$(Platform)' == 'Debug|AnyCPU'\">\r\n";
                result += $"    <DocumentationFile>{Name}.xml</DocumentationFile>\r\n";
                result += "  </PropertyGroup>\r\n";
            }
            if (ItemGroups != null)
            {
                foreach (ItemGroupModel[] groups in ItemGroups)
                {
                    result += "  <ItemGroup>\r\n";
                    foreach (ItemGroupModel item in groups)
                    {
                        switch (item.Type)
                        {
                            case ItemGroupType.ProjectReference:
                                result += $"    <{item.Type.ToString()} Include=\"{item.Value}\" />\r\n";
                                break;
                            case ItemGroupType.Folder:
                                result += $"    <{item.Type.ToString()} Include=\"{item.Value}\" />\r\n";
                                Directory.CreateDirectory($"{targetPath}/{item.Value}");
                                break;
                            case ItemGroupType.PackageReference:
                                result += $"    <{item.Type.ToString()} Include=\"{item.Value}\" Version=\"{item.Version}\" />\r\n";
                                break;
                        }
                    }
                    result += "  </ItemGroup>\r\n";
                }
            }
            result += "</Project>";
            return result;
        }
    }
}
