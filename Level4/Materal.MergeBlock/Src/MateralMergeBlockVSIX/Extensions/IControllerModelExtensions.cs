#nullable enable
using Materal.MergeBlock.GeneratorCode.Models;
using System.Collections.Generic;

namespace MateralMergeBlockVSIX.Extensions
{
    public static class IControllerModelExtensions
    {
        /// <summary>
        /// 获得所有控制器接口
        /// </summary>
        /// <param name="solutionItem"></param>
        /// <returns></returns>
        public static List<IControllerModel> GetAllIControllers(this SolutionItem solutionItem)
        {
            List<IControllerModel> models = solutionItem.GetAllInterfaceModels<IControllerModel>(directoryInfo => directoryInfo.Name == "Controllers", 
                fileInfo => fileInfo.Name.StartsWith("I") && fileInfo.Name.Contains("Controller.") && fileInfo.Name.EndsWith(".cs"));
            return models;
        }
    }
}
