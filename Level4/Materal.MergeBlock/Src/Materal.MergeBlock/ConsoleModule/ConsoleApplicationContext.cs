using Materal.MergeBlock.Abstractions.ConsoleModule;

namespace Materal.MergeBlock.ConsoleModule
{
    /// <summary>
    /// 控制台应用程序上下文
    /// </summary>
    /// <param name="serviceProvider"></param>
    public class ConsoleApplicationContext(IServiceProvider serviceProvider) : ApplicationContext(serviceProvider), IConsoleApplicationContext
    {
    }
}
