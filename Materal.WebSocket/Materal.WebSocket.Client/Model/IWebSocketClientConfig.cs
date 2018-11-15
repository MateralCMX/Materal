using System.Collections.Generic;

namespace Materal.WebSocket.Client.Model
{
    public interface IWebSocketClientConfig
    {
        /// <summary>
        /// 验证合法性
        /// </summary>
        /// <param name="messages">验证消息</param>
        /// <returns>验证结果</returns>
        bool Verification(out List<string> messages);
    }
}
