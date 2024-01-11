#nullable enable
using Materal.MergeBlock.GeneratorCode.Models;
using System.Collections.Generic;
using System.IO;

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
            List<IServiceModel> domains = [];
            foreach (SolutionItem? item in solutionItem.Children)
            {
                if (item is null) continue;
                if (item.Type != SolutionItemType.PhysicalFolder || item.Text != "Services") continue;
                domains.AddRange(item.GetIServices());
            }
            return domains;
        }
        /// <summary>
        /// 获得服务接口
        /// </summary>
        /// <param name="solutionItem"></param>
        /// <returns></returns>
        private static List<IServiceModel>? GetIServices(this SolutionItem solutionItem)
        {
            List<IServiceModel> domains = [];
            if (solutionItem.Type == SolutionItemType.PhysicalFolder)
            {
                foreach (SolutionItem? item in solutionItem.Children)
                {
                    if (item is null) continue;
                    domains.AddRange(item.GetIServices());
                }
            }
            else if (solutionItem.Type == SolutionItemType.PhysicalFile && solutionItem.Text.StartsWith("I") && solutionItem.Text.EndsWith("Service.cs"))
            {
                string[] codes = File.ReadAllLines(solutionItem.FullPath);
                domains.Add(new IServiceModel(codes));
            }
            return domains;
        }
    }
}
