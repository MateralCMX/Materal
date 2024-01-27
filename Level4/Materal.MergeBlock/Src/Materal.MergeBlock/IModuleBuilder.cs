namespace Materal.MergeBlock
{
    /// <summary>
    /// 模块构造器
    /// </summary>
    public interface IModuleBuilder
    {
        /// <summary>
        /// 获取模块信息
        /// </summary>
        /// <param name="moduleDirectoryInfo"></param>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        IModuleInfo? GetModuleInfo(ModuleDirectoryInfo moduleDirectoryInfo, Type moduleType);
    }
}
