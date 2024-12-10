#nullable enable
using Materal.MergeBlock.GeneratorCode;
using Materal.MergeBlock.GeneratorCode.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MateralMergeBlockVSIX.Extensions
{
    public static class GeneratorCodeContextExtensions
    {
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="context"></param>
        /// <param name="solutionItem"></param>
        public static void Refresh(this GeneratorCodeContext context, SolutionItem? solutionItem)
        {
            if (solutionItem is null) return;
            string directoryPath = Path.GetDirectoryName(solutionItem.FullPath);
            DirectoryInfo directoryInfo = new(directoryPath);
            List<CSharpCodeFileModel> models = GetCSharpCodeFileModels(directoryInfo);
            context.Domains = models.OfType<DomainModel>().ToList();
            context.Domains = CSharpFileParser.Merge(context.Domains);
            context.Services = models.OfType<IServiceModel>().ToList();
            context.Services = CSharpFileParser.Merge(context.Services);
            context.Controllers = models.OfType<IControllerModel>().ToList();
            context.Controllers = CSharpFileParser.Merge(context.Controllers);
            context.Enums = models.OfType<EnumModel>().ToList();
            context.Enums = CSharpFileParser.Merge(context.Enums);
        }
        private static List<CSharpCodeFileModel> GetCSharpCodeFileModels(DirectoryInfo directoryInfo)
        {
            List<CSharpCodeFileModel> models = [];
            foreach (DirectoryInfo? item in directoryInfo.GetDirectories())
            {
                if (item is null) continue;
                if (item.Name == "Domain")
                {
                    models.AddRange(GetCSharpCodeFileModels(item, fileInfo => fileInfo.Name.EndsWith(".cs")));
                }
                else if (item.Name == "Controllers")
                {
                    models.AddRange(GetCSharpCodeFileModels(item, fileInfo => fileInfo.Name.StartsWith("I") && fileInfo.Name.Contains("Controller.") && fileInfo.Name.EndsWith(".cs")));
                }
                else if (item.Name == "Services")
                {
                    models.AddRange(GetCSharpCodeFileModels(item, fileInfo => fileInfo.Name.StartsWith("I") && fileInfo.Name.Contains("Service.") && fileInfo.Name.EndsWith(".cs")));
                }
                else if (item.Name == "Enums")
                {
                    models.AddRange(GetCSharpCodeFileModels(item, fileInfo => fileInfo.Name.EndsWith(".cs")));
                }
                else if (item.Name == "MGC")
                {
                    models.AddRange(GetCSharpCodeFileModels(item));
                }
            }
            return models;
        }
        private static List<CSharpCodeFileModel> GetCSharpCodeFileModels(DirectoryInfo directoryInfo, Func<FileInfo, bool> isTargetFile)
        {
            List<CSharpCodeFileModel> models = [];
            foreach (DirectoryInfo? item in directoryInfo.GetDirectories())
            {
                if (item is null) continue;
                models.AddRange(GetCSharpCodeFileModels(item, isTargetFile));
            }
            foreach (FileInfo? item in directoryInfo.GetFiles())
            {
                if (item is null || !isTargetFile(item)) continue;
                models.AddRange(CSharpFileParser.ParseByFilePath(item.FullName));
            }
            return models;
        }
    }
}
