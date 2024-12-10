using Materal.MergeBlock.GeneratorCode.Attributers;
using Materal.MergeBlock.GeneratorCode.Models;

namespace Materal.MergeBlock.GeneratorCode.Extensions
{
    /// <summary>
    /// 领域模型扩展
    /// </summary>
    public static class DomainModelExtensions
    {
        /// <summary>
        /// 获得查询领域
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="domains"></param>
        /// <returns></returns>
        public static DomainModel GetQueryDomain(this DomainModel domain, List<DomainModel> domains)
        {
            DomainModel targetDomain = domain;
            AttributeModel? queryViewAttribute = targetDomain.GetAttribute<QueryViewAttribute>();
            if (queryViewAttribute is not null)
            {
                string? targetDomainName = queryViewAttribute.GetAttributeArgument()?.Value;
                if (targetDomainName is not null && !string.IsNullOrWhiteSpace(targetDomainName) && targetDomainName.Length > 2)
                {
                    targetDomainName = targetDomainName.RemovePackag();
                    targetDomain = domains.FirstOrDefault(m => m.Name == targetDomainName) ?? throw new Exception($"未找到[{targetDomainName}]");
                }
            }
            return targetDomain;
        }
    }
}
