using Materal.MergeBlock.Abstractions;

[assembly: MergeBlockAssembly]
namespace Materal.MergeBlock.ConsulTest2
{
    /// <summary>
    /// Consul模块
    /// </summary>
    public class ConsulTestModule : MergeBlockModule, IMergeBlockModule
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public ConsulTestModule() : base("Consul测试模块2", "ConsulTest2", ["Consul"])
        {

        }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.Services.AddConsulConfig("ConsulTest2", ["ConsulTest2"]);
            await base.OnConfigServiceAsync(context);
        }
    }
}
