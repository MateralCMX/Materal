namespace Materal.MergeBlock.HelloWorldTest
{
    /// <summary>
    /// HelloWorld模块
    /// </summary>
    public class HelloWorldModule() : MergeBlockModule("HelloWorld模块")
    {
        /// <summary>
        /// 配置服务之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceBeforeAsync(IConfigServiceContext context)
        {
            Console.WriteLine($"[{nameof(OnConfigServiceBeforeAsync)}](Module)Hello World!");
            await base.OnConfigServiceBeforeAsync(context);
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            Console.WriteLine($"[{nameof(OnConfigServiceAsync)}](Module)Hello World!");
            await base.OnConfigServiceAsync(context);
        }
        /// <summary>
        /// 配置服务之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAfterAsync(IConfigServiceContext context)
        {
            Console.WriteLine($"[{nameof(OnConfigServiceAfterAsync)}](Module)Hello World!");
            await base.OnConfigServiceAfterAsync(context);
        }
        /// <summary>
        /// 配置应用程序之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitBeforeAsync(IApplicationContext context)
        {
            Console.WriteLine($"[{nameof(OnApplicationInitBeforeAsync)}](Module)Hello World!");
            await base.OnApplicationInitBeforeAsync(context);
        }
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAsync(IApplicationContext context)
        {
            Console.WriteLine($"[{nameof(OnApplicationInitAsync)}](Module)Hello World!");
            await base.OnApplicationInitAsync(context);
        }
        /// <summary>
        /// 应用程序初始化之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAfterAsync(IApplicationContext context)
        {
            Console.WriteLine($"[{nameof(OnApplicationInitAfterAsync)}](Module)Hello World!");
            await base.OnApplicationInitAfterAsync(context);
        }
    }
}
