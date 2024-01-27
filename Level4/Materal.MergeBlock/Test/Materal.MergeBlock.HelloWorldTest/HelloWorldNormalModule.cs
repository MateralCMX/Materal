using Materal.MergeBlock.Abstractions.NormalModule;

namespace Materal.MergeBlock.HelloWorldTest
{
    /// <summary>
    /// HelloWorld模块
    /// </summary>
    public class HelloWorldNormalModule : MergeBlockNormalModule, IMergeBlockNormalModule
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public HelloWorldNormalModule() : base("HelloWorld普通模块")
        {
        }
        /// <summary>
        /// 配置服务之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceBeforeAsync(IConfigServiceContext context)
        {
            Console.WriteLine($"[{nameof(OnConfigServiceBeforeAsync)}](Normal)Hello World!");
            await base.OnConfigServiceBeforeAsync(context);
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            Console.WriteLine($"[{nameof(OnConfigServiceAsync)}](Normal)Hello World!");
            await base.OnConfigServiceAsync(context);
        }
        /// <summary>
        /// 配置服务之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAfterAsync(IConfigServiceContext context)
        {
            Console.WriteLine($"[{nameof(OnConfigServiceAfterAsync)}](Normal)Hello World!");
            await base.OnConfigServiceAfterAsync(context);
        }
        /// <summary>
        /// 配置应用程序之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitBeforeAsync(IApplicationContext context)
        {
            Console.WriteLine($"[{nameof(OnApplicationInitBeforeAsync)}](Normal)Hello World!");
            await base.OnApplicationInitBeforeAsync(context);
        }
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAsync(IApplicationContext context)
        {
            Console.WriteLine($"[{nameof(OnApplicationInitAsync)}](Normal)Hello World!");
            await base.OnApplicationInitAsync(context);
        }
        /// <summary>
        /// 应用程序初始化之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAfterAsync(IApplicationContext context)
        {
            Console.WriteLine($"[{nameof(OnApplicationInitAfterAsync)}](Normal)Hello World!");
            await base.OnApplicationInitAfterAsync(context);
        }
        /// <summary>
        /// 配置服务之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceBeforeAsync(INormalConfigServiceContext context)
        {
            Console.WriteLine($"[{nameof(OnConfigServiceBeforeAsync)}](Normal)Hello World! --> Normal");
            await base.OnConfigServiceBeforeAsync(context);
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(INormalConfigServiceContext context)
        {
            Console.WriteLine($"[{nameof(OnConfigServiceAsync)}](Normal)Hello World! --> Normal");
            await base.OnConfigServiceAsync(context);
        }
        /// <summary>
        /// 配置服务之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAfterAsync(INormalConfigServiceContext context)
        {
            Console.WriteLine($"[{nameof(OnConfigServiceAfterAsync)}](Normal)Hello World! --> Normal");
            await base.OnConfigServiceAfterAsync(context);
        }
        /// <summary>
        /// 应用程序初始化之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitBeforeAsync(INormalApplicationContext context)
        {
            Console.WriteLine($"[{nameof(OnApplicationInitBeforeAsync)}](Normal)Hello World! --> Normal");
            await base.OnApplicationInitBeforeAsync(context);
        }
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAsync(INormalApplicationContext context)
        {
            Console.WriteLine($"[{nameof(OnApplicationInitAsync)}](Normal)Hello World! --> Normal");
            await base.OnApplicationInitAsync(context);
        }
        /// <summary>
        /// 应用程序初始化之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAfterAsync(INormalApplicationContext context)
        {
            Console.WriteLine($"[{nameof(OnApplicationInitAfterAsync)}](Normal)Hello World! --> Normal");
            await base.OnApplicationInitAfterAsync(context);
        }
    }
}
