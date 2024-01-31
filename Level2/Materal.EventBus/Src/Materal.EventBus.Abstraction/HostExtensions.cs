using Microsoft.Extensions.Hosting;

namespace Materal.EventBus.Abstraction
{
    /// <summary>
    /// 主机扩展
    /// </summary>
    public static class HostExtensions
    {
        /// <summary>
        /// 使用事件总线
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static IHost UseEventBus(this IHost host)
        {
            host.Services.UseEventBus();
            return host;
        }
    }
}
