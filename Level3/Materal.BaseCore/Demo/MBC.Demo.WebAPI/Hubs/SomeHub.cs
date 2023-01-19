using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace MBC.Demo.WebAPI.Hubs
{
    /// <summary>
    /// 一个Hub
    /// </summary>
    [SignalRHub("/hubs/Some")]
    public class SomeHub : Hub
    {
        /// <summary>
        /// 一个方法
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <returns></returns>
        public async Task SomeMethod(int arg1, object arg2)
        {
            await Clients.All.SendAsync(nameof(SomeMethod), arg1, arg2);
        }
        /// <summary>
        /// 其他方法
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <returns></returns>
        public async Task SomeOtherMethod(int arg1, object arg2)
        {
            await Clients.All.SendAsync(nameof(SomeOtherMethod), arg1, arg2);
        }
    }
}
