using Materal.MergeBlock.Abstractions.WebModule;

namespace Materal.MergeBlock.HelloWorldTest
{
    /// <summary>
    /// HelloWorld模块
    /// </summary>
    public class HelloWorldWebModule : MergeBlockWebModule, IMergeBlockWebModule
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public HelloWorldWebModule() : base("HelloWorld普通模块")
        {
        }
        /// <summary>
        /// 配置服务之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceBeforeAsync(IConfigServiceContext context)
        {
            Console.WriteLine($"[{nameof(OnConfigServiceBeforeAsync)}](Web)Hello World!");
            await base.OnConfigServiceBeforeAsync(context);
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            Console.WriteLine($"[{nameof(OnConfigServiceAsync)}](Web)Hello World!");
            await base.OnConfigServiceAsync(context);
        }
        /// <summary>
        /// 配置服务之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAfterAsync(IConfigServiceContext context)
        {
            Console.WriteLine($"[{nameof(OnConfigServiceAfterAsync)}](Web)Hello World!");
            await base.OnConfigServiceAfterAsync(context);
        }
        /// <summary>
        /// 配置应用程序之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitBeforeAsync(IApplicationContext context)
        {
            Console.WriteLine($"[{nameof(OnApplicationInitBeforeAsync)}](Web)Hello World!");
            await base.OnApplicationInitBeforeAsync(context);
        }
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAsync(IApplicationContext context)
        {
            Console.WriteLine($"[{nameof(OnApplicationInitAsync)}](Web)Hello World!");
            await base.OnApplicationInitAsync(context);
        }
        /// <summary>
        /// 应用程序初始化之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAfterAsync(IApplicationContext context)
        {
            Console.WriteLine($"[{nameof(OnApplicationInitAfterAsync)}](Web)Hello World!");
            await base.OnApplicationInitAfterAsync(context);
        }
        /// <summary>
        /// 配置服务之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceBeforeAsync(IWebConfigServiceContext context)
        {
            Console.WriteLine($"[{nameof(OnConfigServiceBeforeAsync)}](Web)Hello World! --> Web");
            await base.OnConfigServiceBeforeAsync(context);
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IWebConfigServiceContext context)
        {
            Console.WriteLine($"[{nameof(OnConfigServiceAsync)}](Web)Hello World! --> Web");
            await base.OnConfigServiceAsync(context);
        }
        /// <summary>
        /// 配置服务之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAfterAsync(IWebConfigServiceContext context)
        {
            Console.WriteLine($"[{nameof(OnConfigServiceAfterAsync)}](Web)Hello World! --> Web");
            await base.OnConfigServiceAfterAsync(context);
        }
        /// <summary>
        /// 应用程序初始化之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitBeforeAsync(IWebApplicationContext context)
        {
            Console.WriteLine($"[{nameof(OnApplicationInitBeforeAsync)}](Web)Hello World! --> Web");
            await base.OnApplicationInitBeforeAsync(context);
        }
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAsync(IWebApplicationContext context)
        {
            Console.WriteLine($"[{nameof(OnApplicationInitAsync)}](Web)Hello World! --> Web");
            await base.OnApplicationInitAsync(context);
        }
        /// <summary>
        /// 应用程序初始化之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAfterAsync(IWebApplicationContext context)
        {
            Console.WriteLine($"[{nameof(OnApplicationInitAfterAsync)}](Web)Hello World! --> Web");
            await base.OnApplicationInitAfterAsync(context);
        }
    }
}
