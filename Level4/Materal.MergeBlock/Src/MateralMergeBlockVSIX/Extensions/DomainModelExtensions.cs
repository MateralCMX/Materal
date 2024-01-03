#nullable enable
using Materal.MergeBlock.GeneratorCode.Models;
using System.Collections.Generic;
using System.IO;

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
            List<DomainModel> domains = [];
            foreach (SolutionItem? item in solutionItem.Children)
            {
                if (item is null) continue;
                if (item.Type != SolutionItemType.PhysicalFolder || item.Text != "Domain") continue;
                domains.AddRange(GetDomains(item));
            }
            return domains;
        }
        /// <summary>
        /// 获得领域
        /// </summary>
        /// <param name="solutionItem"></param>
        /// <returns></returns>
        public static List<DomainModel>? GetDomains(this SolutionItem solutionItem)
        {
            List<DomainModel> domains = [];
            if (solutionItem.Type == SolutionItemType.PhysicalFolder)
            {
                foreach (SolutionItem? item in solutionItem.Children)
                {
                    if (item is null) continue;
                    domains.AddRange(GetDomains(item));
                }
            }
            else if (solutionItem.Type == SolutionItemType.PhysicalFile && solutionItem.Text.EndsWith(".cs"))
            {
                string[] codes = File.ReadAllLines(solutionItem.FullPath);
                domains.Add(new DomainModel(codes));
            }
            return domains;
        }
    }
}
