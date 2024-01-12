#nullable enable
using Materal.MergeBlock.GeneratorCode.Models;
using System.Collections.Generic;

namespace MateralMergeBlockVSIX.Extensions
{
    public static class DomainModelExtensions
    {
        /// <summary>
        /// 获得所有领域
        /// </summary>
        /// <param name="solutionItem"></param>
        /// <returns></returns>
        public static List<DomainModel> GetAllDomains(this SolutionItem solutionItem)
        {
            List<DomainModel> models = solutionItem.GetAllInterfaceModels<DomainModel>(directoryInfo => directoryInfo.Name == "Domain",
                fileInfo => fileInfo.Name.EndsWith(".cs"));
            return models;
        }
    }
}
