﻿using Fleck;
using Materal.StringHelper;
using System.Text;

namespace Materal.Logger.Models
{
    public class WebSocketConnectionModel
    {
        private static readonly Encoding _encoding = Encoding.UTF8;
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID => WebSocket.ConnectionInfo.Id;
        public event Action<WebSocketConnectionModel>? OnOpen;
        public event Action<WebSocketConnectionModel>? OnClose;
        public event Action<WebSocketConnectionModel, string>? OnMessage;
        /// <summary>
        /// 连接对象
        /// </summary>
        public IWebSocketConnection WebSocket { get; private set; }
        public WebSocketConnectionModel(IWebSocketConnection webSocket)
        {
            WebSocket = webSocket;
            WebSocket.OnOpen = WebSocketOnOpen;
            WebSocket.OnClose = WebSocketOnClose;
            WebSocket.OnMessage = WebSocketOnMessage;
            WebSocket.OnPong = WebSocketOnPong;
            WebSocket.OnPing = WebSocketOnPing;
        }
        public void SendMessage(string message)
        {
            WebSocket.Send(message);
        }
        private void WebSocketOnOpen()
        {
            MateralLoggerLog.LogInfomation($"新的监测程序已连接:{WebSocket.ConnectionInfo.ClientIpAddress}:{WebSocket.ConnectionInfo.ClientPort}");
            OnOpen?.Invoke(this);
        }
        private void WebSocketOnClose()
        {
            Close();
            OnClose?.Invoke(this);
        }
        private void WebSocketOnMessage(string message)
        {
            OnMessage?.Invoke(this, message);
        }
        /// <summary>
        /// 接受到心跳包
        /// </summary>
        /// <param name="bytes"></param>
        private void WebSocketOnPong(byte[] bytes)
        {
            string idString = _encoding.GetString(bytes, 0, bytes.Length);
            if (idString.IsGuid() && Guid.Parse(idString) == ID) return;
            WebSocketOnClose();
        }
        /// <summary>
        /// 接受到心跳包
        /// </summary>
        /// <param name="bytes"></param>
        private void WebSocketOnPing(byte[] bytes)
        {
            string idString = _encoding.GetString(bytes, 0, bytes.Length);
            if (idString.IsGuid() && Guid.Parse(idString) == ID) return;
            WebSocketOnClose();
        }
        /// <summary>
        /// 发送心跳包
        /// </summary>
        public void SendPing()
        {
            byte[] message = _encoding.GetBytes(ID.ToString());
            if(WebSocket.IsAvailable)
            {
                WebSocket.SendPing(message);
            }
            else
            {
                WebSocketOnClose();
            }
        }
        /// <summary>
        /// 发送心跳包
        /// </summary>
        public void SendPong()
        {
            byte[] message = _encoding.GetBytes(ID.ToString());
            WebSocket.SendPong(message);
        }
        /// <summary>
        /// 销毁
        /// </summary>
        public void Close()
        {
            WebSocket.Close();
            OnClose?.Invoke(this);
        }
    }
}
