using Materal.MergeBlock.Abstractions.ConsoleModule;

namespace Materal.MergeBlock.ConsoleModule
{
    /// <summary>
    /// 模块构造器
    /// </summary>
    public class ConsoleModuleBuilder : IModuleBuilder
    {
        /// <summary>
        /// 获取模块信息
        /// </summary>
        /// <param name="moduleDirectoryInfo"></param>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public IModuleInfo? GetModuleInfo(ModuleDirectoryInfo moduleDirectoryInfo, Type moduleType)
        {
            if (!moduleType.IsAssignableTo<IMergeBlockConsoleModule>()) return null;
            return new ConsoleModuleInfo(moduleDirectoryInfo, moduleType);
        }
    }
}
