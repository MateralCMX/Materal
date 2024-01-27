using Materal.MergeBlock.Abstractions.ConsoleModule;

namespace Materal.MergeBlock.HelloWorldTest
{
    /// <summary>
    /// HelloWorld模块
    /// </summary>
    public class HelloWorldConsoleModule : MergeBlockConsoleModule, IMergeBlockConsoleModule
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public HelloWorldConsoleModule() : base("HelloWorld控制台模块")
        {
        }
        /// <summary>
        /// 配置服务之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceBeforeAsync(IConfigServiceContext context)
        {
            Console.WriteLine($"[{nameof(OnConfigServiceBeforeAsync)}](Console)Hello World!");
            await base.OnConfigServiceBeforeAsync(context);
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            Console.WriteLine($"[{nameof(OnConfigServiceAsync)}](Console)Hello World!");
            await base.OnConfigServiceAsync(context);
        }
        /// <summary>
        /// 配置服务之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAfterAsync(IConfigServiceContext context)
        {
            Console.WriteLine($"[{nameof(OnConfigServiceAfterAsync)}](Console)Hello World!");
            await base.OnConfigServiceAfterAsync(context);
        }
        /// <summary>
        /// 配置应用程序之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitBeforeAsync(IApplicationContext context)
        {
            Console.WriteLine($"[{nameof(OnApplicationInitBeforeAsync)}](Console)Hello World!");
            await base.OnApplicationInitBeforeAsync(context);
        }
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAsync(IApplicationContext context)
        {
            Console.WriteLine($"[{nameof(OnApplicationInitAsync)}](Console)Hello World!");
            await base.OnApplicationInitAsync(context);
        }
        /// <summary>
        /// 应用程序初始化之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAfterAsync(IApplicationContext context)
        {
            Console.WriteLine($"[{nameof(OnApplicationInitAfterAsync)}](Console)Hello World!");
            await base.OnApplicationInitAfterAsync(context);
        }
        /// <summary>
        /// 配置服务之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceBeforeAsync(IConsoleConfigServiceContext context)
        {
            Console.WriteLine($"[{nameof(OnConfigServiceBeforeAsync)}](Console)Hello World! --> Console");
            await base.OnConfigServiceBeforeAsync(context);
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConsoleConfigServiceContext context)
        {
            Console.WriteLine($"[{nameof(OnConfigServiceAsync)}](Console)Hello World! --> Console");
            await base.OnConfigServiceAsync(context);
        }
        /// <summary>
        /// 配置服务之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAfterAsync(IConsoleConfigServiceContext context)
        {
            Console.WriteLine($"[{nameof(OnConfigServiceAfterAsync)}](Console)Hello World! --> Console");
            await base.OnConfigServiceAfterAsync(context);
        }
        /// <summary>
        /// 应用程序初始化之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitBeforeAsync(IConsoleApplicationContext context)
        {
            Console.WriteLine($"[{nameof(OnApplicationInitBeforeAsync)}](Console)Hello World! --> Console");
            await base.OnApplicationInitBeforeAsync(context);
        }
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAsync(IConsoleApplicationContext context)
        {
            Console.WriteLine($"[{nameof(OnApplicationInitAsync)}](Console)Hello World! --> Console");
            await base.OnApplicationInitAsync(context);
        }
        /// <summary>
        /// 应用程序初始化之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAfterAsync(IConsoleApplicationContext context)
        {
            Console.WriteLine($"[{nameof(OnApplicationInitAfterAsync)}](Console)Hello World! --> Console");
            await base.OnApplicationInitAfterAsync(context);
        }
    }
}
