#nullable enable
using Materal.MergeBlock.GeneratorCode.Models;
using System.Collections.Generic;

namespace MateralMergeBlockVSIX.Extensions
{
    public static class IServiceModelExtensions
    {
        /// <summary>
        /// 获得所有服务接口
        /// </summary>
        /// <param name="solutionItem"></param>
        /// <returns></returns>
        public static List<IServiceModel> GetAllIServices(this SolutionItem solutionItem)
        {
            List<IServiceModel> models = solutionItem.GetAllInterfaceModels<IServiceModel>(directoryInfo => directoryInfo.Name == "Services",
                fileInfo => fileInfo.Name.StartsWith("I") && fileInfo.Name.Contains("Service.") && fileInfo.Name.EndsWith(".cs"));
            return models;
        }
    }
}
