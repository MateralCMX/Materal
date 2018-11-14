using Materal.WebSocket;
using System.Threading.Tasks;
using TestClient.Events;

namespace TestClient.WebStockClient
{
    public interface ITestClientClient : IClient
    {
        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="eventM">消息</param>
        /// <returns></returns>
        Task HandleEventAsync(Event eventM);
    }
}
