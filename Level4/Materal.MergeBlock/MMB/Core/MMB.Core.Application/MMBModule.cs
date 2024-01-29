using Materal.Extensions;
using Materal.MergeBlock.Abstractions;
using Materal.MergeBlock.Abstractions.WebModule;
using Microsoft.Extensions.Configuration;

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
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            Type moduleType = GetType();
            string configFilePath = moduleType.Assembly.GetDirectoryPath();
            configFilePath = Path.Combine(configFilePath, $"{moduleType.Namespace}.json");
            context.Configuration.AddJsonFile(configFilePath, optional: true, reloadOnChange: true);
            await base.OnConfigServiceAsync(context);
        }
    }
}
