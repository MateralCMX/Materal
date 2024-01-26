using Materal.MergeBlock.GeneratorCode.Attributers;

namespace Materal.MergeBlock.GeneratorCode.Models
{
    /// <summary>
    /// 领域模型
    /// </summary>
    /// <param name="codes"></param>
    public class DomainModel(string[] codes) : ClassModel(codes)
    {
        /// <summary>
        /// 是树形领域
        /// </summary>
        public bool IsTreeDomain => Interfaces.Contains("ITreeDomain");
        /// <summary>
        /// 获取树形领域的分组属性
        /// </summary>
        /// <returns></returns>
        public PropertyModel? GetTreeGroupProperty() => Properties.FirstOrDefault(m => m.HasAttribute<TreeGroupAttribute>());
        /// <summary>
        /// 是位序领域
        /// </summary>
        public bool IsIndexDomain => Interfaces.Contains("IIndexDomain");
        /// <summary>
        /// 获取树形领域的分组属性
        /// </summary>
        /// <returns></returns>
        public PropertyModel? GetIndexGroupProperty() => Properties.FirstOrDefault(m => m.HasAttribute<IndexGroupAttribute>());

    }
}
