using Materal.MergeBlock.Abstractions.NormalModule;

namespace Materal.MergeBlock.NormalModule
{
    /// <summary>
    /// 模块信息
    /// </summary>
    public class NormalModuleInfo(IModuleDirectoryInfo moduleDirectoryInfo, Type moduleType) : ModuleInfo<IMergeBlockNormalModule>(moduleDirectoryInfo, moduleType), IModuleInfo<IMergeBlockNormalModule>
    {
        /// <summary>
        /// 配置服务之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ConfigServiceBeforeAsync(IConfigServiceContext context)
        {
            await Instance.OnConfigServiceBeforeAsync(context);
            if (context is INormalConfigServiceContext normalConfigServiceContext)
            {
                await Instance.OnConfigServiceBeforeAsync(normalConfigServiceContext);
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
            if (context is INormalConfigServiceContext normalConfigServiceContext)
            {
                await Instance.OnConfigServiceAsync(normalConfigServiceContext);
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
            if (context is INormalConfigServiceContext normalConfigServiceContext)
            {
                await Instance.OnConfigServiceAfterAsync(normalConfigServiceContext);
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
            if (context is INormalApplicationContext normalApplicationContext)
            {
                await Instance.OnApplicationInitBeforeAsync(normalApplicationContext);
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
            if (context is INormalApplicationContext normalApplicationContext)
            {
                await Instance.OnApplicationInitAsync(normalApplicationContext);
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
            if (context is INormalApplicationContext normalApplicationContext)
            {
                await Instance.OnApplicationInitAfterAsync(normalApplicationContext);
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
            if (context is INormalApplicationContext normalApplicationContext)
            {
                await Instance.OnApplicationCloseBeforeAsync(normalApplicationContext);
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
            if (context is INormalApplicationContext normalApplicationContext)
            {
                await Instance.OnApplicationCloseAsync(normalApplicationContext);
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
            if (context is INormalApplicationContext normalApplicationContext)
            {
                await Instance.OnApplicationCloseAfterAsync(normalApplicationContext);
            }
        }
    }
}
