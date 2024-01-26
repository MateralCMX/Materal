using Materal.MergeBlock.Abstractions;

[assembly: MergeBlockAssembly("HelloWorld模块", "HelloWorld")]
namespace Materal.MergeBlock.ConsulTest
{
    /// <summary>
    /// HelloWorld模块
    /// </summary>
    public class HelloWorldModule : MergeBlockModule, IMergeBlockModule
    {
        /// <summary>
        /// 配置服务之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceBeforeAsync(IConfigServiceContext context)
        {
            Console.WriteLine("HelloWorld模块配置服务之前");
            await base.OnConfigServiceAsync(context);
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            Console.WriteLine("HelloWorld模块配置服务");
            await base.OnConfigServiceAsync(context);
        }
        /// <summary>
        /// 配置服务之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAfterAsync(IConfigServiceContext context)
        {
            Console.WriteLine("HelloWorld模块配置服务之后");
            await base.OnConfigServiceAfterAsync(context);
        }
        /// <summary>
        /// 配置应用之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitBeforeAsync(IApplicationContext context)
        {
            Console.WriteLine("HelloWorld模块应用初始化之前");
            await base.OnApplicationInitBeforeAsync(context);
        }
        /// <summary>
        /// 应用初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAsync(IApplicationContext context)
        {
            Console.WriteLine("HelloWorld模块应用初始化");
            await base.OnApplicationInitAsync(context);
        }
        /// <summary>
        /// 应用初始化之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAfterAsync(IApplicationContext context)
        {
            Console.WriteLine("HelloWorld模块应用初始化之后");
            await base.OnApplicationInitAfterAsync(context);
        }
    }
}
