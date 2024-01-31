using Materal.MergeBlock.Abstractions.WebModule;

namespace Materal.MergeBlock.WebModule
{
    /// <summary>
    /// 模块信息
    /// </summary>
    public class WebModuleInfo(IModuleDirectoryInfo moduleDirectoryInfo, Type moduleType) : ModuleInfo<MergeBlockWebModule>(moduleDirectoryInfo, moduleType), IModuleInfo<MergeBlockWebModule>
    {
        /// <summary>
        /// 配置服务之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ConfigServiceBeforeAsync(IConfigServiceContext context)
        {
            await Instance.OnConfigServiceBeforeAsync(context);
            if (context is WebConfigServiceContext webConfigServiceContext)
            {
                await Instance.OnConfigServiceBeforeAsync(webConfigServiceContext);
            }
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ConfigServiceAsync(IConfigServiceContext context)
        {
            await Instance.OnConfigServiceAsync(context);
            if (context is WebConfigServiceContext webConfigServiceContext)
            {
                await Instance.OnConfigServiceAsync(webConfigServiceContext);
            }
        }
        /// <summary>
        /// 配置服务之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ConfigServiceAfterAsync(IConfigServiceContext context)
        {
            await Instance.OnConfigServiceAfterAsync(context);
            if (context is WebConfigServiceContext webConfigServiceContext)
            {
                await Instance.OnConfigServiceAfterAsync(webConfigServiceContext);
            }
        }
        /// <summary>
        /// 应用程序初始化之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ApplicationInitBeforeAsync(IApplicationContext context)
        {
            await Instance.OnApplicationInitBeforeAsync(context);
            if (context is WebApplicationContext webApplicationContext)
            {
                await Instance.OnApplicationInitBeforeAsync(webApplicationContext);
            }

        }
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ApplicationInitAsync(IApplicationContext context)
        {
            await Instance.OnApplicationInitAsync(context);
            if (context is WebApplicationContext webApplicationContext)
            {
                await Instance.OnApplicationInitAsync(webApplicationContext);
            }
        }
        /// <summary>
        /// 应用程序初始化之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ApplicationInitAfterAsync(IApplicationContext context)
        {
            await Instance.OnApplicationInitAfterAsync(context);
            if (context is WebApplicationContext webApplicationContext)
            {
                await Instance.OnApplicationInitAfterAsync(webApplicationContext);
            }
        }
        /// <summary>
        /// 应用程序关闭之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ApplicationCloseBeforeAsync(IApplicationContext context)
        {
            await Instance.OnApplicationCloseBeforeAsync(context);
            if (context is WebApplicationContext webApplicationContext)
            {
                await Instance.OnApplicationCloseBeforeAsync(webApplicationContext);
            }

        }
        /// <summary>
        /// 应用程序关闭
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ApplicationCloseAsync(IApplicationContext context)
        {
            await Instance.OnApplicationCloseAsync(context);
            if (context is WebApplicationContext webApplicationContext)
            {
                await Instance.OnApplicationCloseAsync(webApplicationContext);
            }
        }
        /// <summary>
        /// 应用程序关闭之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ApplicationCloseAfterAsync(IApplicationContext context)
        {
            await Instance.OnApplicationCloseAfterAsync(context);
            if (context is WebApplicationContext webApplicationContext)
            {
                await Instance.OnApplicationCloseAfterAsync(webApplicationContext);
            }
        }
    }
}
