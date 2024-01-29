using Materal.MergeBlock.Abstractions.WebModule;

namespace MMB.Core.Application
{
    /// <summary>
    /// MMB模块
    /// </summary>
    public abstract class MMBModule : MergeBlockWebModule
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="description"></param>
        /// <param name="depends"></param>
        protected MMBModule(string description, string[]? depends) : base(description, depends)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="description"></param>
        /// <param name="moduleName"></param>
        /// <param name="depends"></param>
        protected MMBModule(string description, string? moduleName = null, string[]? depends = null) : base(description, moduleName, depends)
        {
        }
    }
}
