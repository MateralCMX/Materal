using Materal.MergeBlock.Abstractions.WebModule;

namespace Materal.MergeBlock.WebModule
{
    /// <summary>
    /// 模块构造器
    /// </summary>
    public class WebModuleBuilder : IModuleBuilder
    {
        /// <summary>
        /// 获取模块信息
        /// </summary>
        /// <param name="moduleDirectoryInfo"></param>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public IModuleInfo? GetModuleInfo(ModuleDirectoryInfo moduleDirectoryInfo, Type moduleType)
        {
            if (!moduleType.IsAssignableTo<IMergeBlockWebModule>()) return null;
            return new WebModuleInfo(moduleDirectoryInfo, moduleType);
        }
    }
}
