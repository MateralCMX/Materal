#nullable enable
using Materal.MergeBlock.GeneratorCode;

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
            context.Domains = solutionItem?.GetAllDomains() ?? [];
            context.Services = solutionItem?.GetAllIServices() ?? [];
            context.Controllers = solutionItem?.GetAllIControllers() ?? [];
            context.Enums = solutionItem?.GetAllEnums() ?? [];
        }
    }
}
