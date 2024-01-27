using Materal.MergeBlock.Abstractions.NormalModule;

namespace Materal.MergeBlock.NormalModule
{
    /// <summary>
    /// 模块构造器
    /// </summary>
    public class NormalModuleBuilder : IModuleBuilder
    {
        /// <summary>
        /// 获取模块信息
        /// </summary>
        /// <param name="moduleDirectoryInfo"></param>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public IModuleInfo? GetModuleInfo(ModuleDirectoryInfo moduleDirectoryInfo, Type moduleType)
        {
            if (!moduleType.IsAssignableTo<IMergeBlockNormalModule>()) return null;
            return new NormalModuleInfo(moduleDirectoryInfo, moduleType);
        }
    }
}
