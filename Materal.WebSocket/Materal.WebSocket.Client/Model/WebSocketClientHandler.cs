using System;

namespace Materal.WebSocket.Client.Model
{
    public abstract class WebSocketClientHandler : IWebSocketClientHandler
    {
        public abstract void ChannelActive(WebSocketClientImpl channel);
        public abstract void ChannelInactive(WebSocketClientImpl channel);
        public abstract void ChannelRead0(WebSocketClientImpl channel, byte[] msg);
        public abstract void ExceptionCaught(WebSocketClientImpl channel, Exception ex);
        public abstract void ChannelStart(WebSocketClientImpl channel);
        public abstract void OnSendMessage(object message);
    }
}
