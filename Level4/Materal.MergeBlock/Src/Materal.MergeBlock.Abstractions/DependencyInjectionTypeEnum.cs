namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// 依赖注入类型枚举
    /// </summary>
    public enum DependencyInjectionTypeEnum
    {
        /// <summary>
        /// 存在则不添加
        /// </summary>
        TryAdd = 0,
        /// <summary>
        /// 添加
        /// </summary>
        Add = 1,
        /// <summary>
        /// 替换
        /// </summary>
        Replace = 2
    }
}
