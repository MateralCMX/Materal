#nullable enable
using Materal.MergeBlock.GeneratorCode;
using Materal.MergeBlock.GeneratorCode.Extensions;
using Materal.MergeBlock.GeneratorCode.Models;
using System.Collections.Generic;
using System.IO;

namespace MateralMergeBlockVSIX.Extensions
{
    public static class SolutionItemExtensions
    {
        /// <summary>
        /// 获得生成代码插件路径
        /// </summary>
        /// <param name="solutionItem"></param>
        /// <returns></returns>
        public static List<string> GetGeneratorCodePlugPaths(this SolutionItem solutionItem)
        {
            string directoryPath = Path.GetDirectoryName(solutionItem.FullPath);
            DirectoryInfo directoryInfo = new(directoryPath);
            List<string> result = directoryInfo.GetGeneratorCodePlugPaths();
            return result;
        }
        /// <summary>
        /// 获得生成代码插件路径
        /// </summary>
        /// <param name="solutionItem"></param>
        /// <returns></returns>
        public static List<string> GetEditGeneratorCodePlugPaths(this SolutionItem solutionItem)
        {
            string directoryPath = Path.GetDirectoryName(solutionItem.FullPath);
            DirectoryInfo directoryInfo = new(directoryPath);
            List<string> result = directoryInfo.GetEditGeneratorCodePlugPaths();
            return result;
        }
        /// <summary>
        /// 获得生成代码插件路径
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <returns></returns>
        public static List<string> GetGeneratorCodePlugPaths(this DirectoryInfo directoryInfo)
        {
            List<string> result = [];
            foreach (DirectoryInfo subDirectoryInfo in directoryInfo.GetDirectories())
            {
                result.AddRange(subDirectoryInfo.GetGeneratorCodePlugPaths());
            }
            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                if (!fileInfo.IsGeneratorCodePlugFile()) continue;
                result.Add(fileInfo.FullName);
            }
            return result;
        }
        /// <summary>
        /// 获得生成代码插件路径
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <returns></returns>
        public static List<string> GetEditGeneratorCodePlugPaths(this DirectoryInfo directoryInfo)
        {
            List<string> result = [];
            foreach (DirectoryInfo subDirectoryInfo in directoryInfo.GetDirectories())
            {
                result.AddRange(subDirectoryInfo.GetEditGeneratorCodePlugPaths());
            }
            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                if (!fileInfo.IsEditGeneratorCodePlugFile()) continue;
                result.Add(fileInfo.FullName);
            }
            return result;
        }
        /// <summary>
        /// 获得生成代码插件路径
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <returns></returns>
        public static bool IsGeneratorCodePlugFile(this FileInfo fileInfo)
        {
            if (!fileInfo.Name.EndsWith(".cs")) return false;
            string[] fileContent = File.ReadAllLines(fileInfo.FullName);
            foreach (string line in fileContent)
            {
                string lineTrim = line.Trim();
                if (lineTrim.StartsWith("public class ") && lineTrim.EndsWith($": {nameof(IMergeBlockGeneratorCodePlug)}")) return true;
            }
            return false;
        }
        /// <summary>
        /// 获得生成代码插件路径
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <returns></returns>
        public static bool IsEditGeneratorCodePlugFile(this FileInfo fileInfo)
        {
            if (!fileInfo.Name.EndsWith(".cs")) return false;
            string[] fileContent = File.ReadAllLines(fileInfo.FullName);
            foreach (string line in fileContent)
            {
                string lineTrim = line.Trim();
                if (lineTrim.StartsWith("public class ") && lineTrim.EndsWith($": {nameof(IMergeBlockEditGeneratorCodePlug)}")) return true;
            }
            return false;
        }
        /// <summary>
        /// 获得所有领域
        /// </summary>
        /// <param name="solutionItem"></param>
        /// <returns></returns>
        public static List<DomainModel> GetAllDomains(this SolutionItem solutionItem)
        {
            List<DomainModel> models = solutionItem.GetAllCSharpCodeFileModels<DomainModel>(directoryInfo => directoryInfo.Name == "Domain",
                fileInfo => fileInfo.Name.EndsWith(".cs"));
            return models;
        }
        /// <summary>
        /// 获得所有控制器接口
        /// </summary>
        /// <param name="solutionItem"></param>
        /// <returns></returns>
        public static List<IControllerModel> GetAllIControllers(this SolutionItem solutionItem)
        {
            List<IControllerModel> models = solutionItem.GetAllCSharpCodeFileModels<IControllerModel>(directoryInfo => directoryInfo.Name == "Controllers",
                fileInfo => fileInfo.Name.StartsWith("I") && fileInfo.Name.Contains("Controller.") && fileInfo.Name.EndsWith(".cs"));
            return models;
        }
        /// <summary>
        /// 获得所有服务接口
        /// </summary>
        /// <param name="solutionItem"></param>
        /// <returns></returns>
        public static List<IServiceModel> GetAllIServices(this SolutionItem solutionItem)
        {
            List<IServiceModel> models = solutionItem.GetAllCSharpCodeFileModels<IServiceModel>(directoryInfo => directoryInfo.Name == "Services",
                fileInfo => fileInfo.Name.StartsWith("I") && fileInfo.Name.Contains("Service.") && fileInfo.Name.EndsWith(".cs"));
            return models;
        }
        /// <summary>
        /// 获得所有枚举
        /// </summary>
        /// <param name="solutionItem"></param>
        /// <returns></returns>
        public static List<EnumModel> GetAllEnums(this SolutionItem solutionItem)
        {
            List<EnumModel> models = solutionItem.GetAllCSharpCodeFileModels<EnumModel>(directoryInfo => directoryInfo.Name == "Enums",
                fileInfo => fileInfo.Name.EndsWith(".cs"));
            return models;
        }
        /// <summary>
        /// 获得所有C#代码文件模型
        /// </summary>
        /// <param name="solutionItem"></param>
        /// <returns></returns>
        public static List<T> GetAllCSharpCodeFileModels<T>(this SolutionItem solutionItem, Func<DirectoryInfo, bool> isTargetDirectory, Func<FileInfo, bool> isTargetFile)
            where T : CSharpCodeFileModel
        {
            string directoryPath = Path.GetDirectoryName(solutionItem.FullPath);
            DirectoryInfo directoryInfo = new(directoryPath);
            List<T> models = directoryInfo.GetAllInterfaceModels<T>(isTargetDirectory, isTargetFile);
            return models;
        }
    }
}
