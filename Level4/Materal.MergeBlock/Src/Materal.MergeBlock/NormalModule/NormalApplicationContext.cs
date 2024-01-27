using Materal.MergeBlock.Abstractions.NormalModule;

namespace Materal.MergeBlock.NormalModule
{
    /// <summary>
    /// 应用程序上下文
    /// </summary>
    public class NormalApplicationContext(IServiceProvider serviceProvider) : ApplicationContext(serviceProvider), INormalApplicationContext
    {
    }
}
