#nullable enable
using Materal.MergeBlock.GeneratorCode;
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
    }
}
