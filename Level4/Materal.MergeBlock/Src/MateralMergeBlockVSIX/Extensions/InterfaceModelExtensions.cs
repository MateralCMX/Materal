#nullable enable
using Materal.MergeBlock.GeneratorCode.Extensions;
using Materal.MergeBlock.GeneratorCode.Models;
using System.Collections.Generic;
using System.IO;

namespace MateralMergeBlockVSIX.Extensions
{
    /// <summary>
    /// 接口模型扩展
    /// </summary>
    public static class InterfaceModelExtensions
    {
        /// <summary>
        /// 获得所有控制器接口
        /// </summary>
        /// <param name="solutionItem"></param>
        /// <returns></returns>
        public static List<T> GetAllInterfaceModels<T>(this SolutionItem solutionItem, Func<DirectoryInfo, bool> isTargetDirectory, Func<FileInfo, bool> isTargetFile)
            where T : InterfaceModel
        {
            string directoryPath = Path.GetDirectoryName(solutionItem.FullPath);
            DirectoryInfo directoryInfo = new(directoryPath);
            List<T> models = directoryInfo.GetAllInterfaceModels<T>(isTargetDirectory, isTargetFile);
            return models;
        }
    }
}
