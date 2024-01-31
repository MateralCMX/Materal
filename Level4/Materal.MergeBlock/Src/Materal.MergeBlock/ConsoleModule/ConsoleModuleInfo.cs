using Materal.MergeBlock.Abstractions.ConsoleModule;

namespace Materal.MergeBlock.ConsoleModule
{
    /// <summary>
    /// 模块信息
    /// </summary>
    public class ConsoleModuleInfo(IModuleDirectoryInfo moduleDirectoryInfo, Type moduleType) : ModuleInfo<IMergeBlockConsoleModule>(moduleDirectoryInfo, moduleType), IModuleInfo<IMergeBlockConsoleModule>
    {
        /// <summary>
        /// 配置服务之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ConfigServiceBeforeAsync(IConfigServiceContext context)
        {
            await Instance.OnConfigServiceBeforeAsync(context);
            if (context is IConsoleConfigServiceContext consoleConfigServiceContext)
            {
                await Instance.OnConfigServiceBeforeAsync(consoleConfigServiceContext);
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
            if (context is IConsoleConfigServiceContext consoleConfigServiceContext)
            {
                await Instance.OnConfigServiceAsync(consoleConfigServiceContext);
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
            if (context is IConsoleConfigServiceContext consoleConfigServiceContext)
            {
                await Instance.OnConfigServiceAfterAsync(consoleConfigServiceContext);
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
            if (context is IConsoleApplicationContext consoleApplicationContext)
            {
                await Instance.OnApplicationInitBeforeAsync(consoleApplicationContext);
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
            if (context is IConsoleApplicationContext consoleApplicationContext)
            {
                await Instance.OnApplicationInitAsync(consoleApplicationContext);
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
            if (context is IConsoleApplicationContext consoleApplicationContext)
            {
                await Instance.OnApplicationInitAfterAsync(consoleApplicationContext);
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
            if (context is IConsoleApplicationContext consoleApplicationContext)
            {
                await Instance.OnApplicationCloseBeforeAsync(consoleApplicationContext);
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
            if (context is IConsoleApplicationContext consoleApplicationContext)
            {
                await Instance.OnApplicationCloseAsync(consoleApplicationContext);
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
            if (context is IConsoleApplicationContext consoleApplicationContext)
            {
                await Instance.OnApplicationCloseAfterAsync(consoleApplicationContext);
            }
        }
    }
}
