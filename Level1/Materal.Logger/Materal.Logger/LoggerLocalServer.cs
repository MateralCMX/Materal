using Fleck;
using Materal.Logger.CommandHandlers;
using Materal.Logger.Message;
using Materal.Logger.Models;
using System.Collections.Concurrent;

namespace Materal.Logger
{
    /// <summary>
    /// 日志本地服务
    /// </summary>
    public class LoggerLocalServer
    {
        private readonly ConcurrentDictionary<Guid, WebSocketConnectionModel> webSockets = new();
        private readonly WebSocketServer webSocketServer;
        private static readonly ICommandBus commandBus = new CommandBus();
        /// <summary>
        /// 构造方法
        /// </summary>
        public LoggerLocalServer()
        {
            //commandBus.Subscribe<>
            webSocketServer = new WebSocketServer($"ws://0.0.0.0:{LoggerConfig.ServerConfig.Port}");
            FleckLog.LogAction = WriteWebsocketMessage;
            webSocketServer.Start(WebscoketConfig);
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="target"></param>
        /// <param name="message"></param>
        /// <param name="logLevel"></param>
        public void SendMessage(LoggerTargetConfigModel target, string message, Microsoft.Extensions.Logging.LogLevel logLevel)
        {
            if (!LoggerConfig.ServerConfig.Enable || !LoggerConfig.ServerConfig.Targets.Contains(target.Name)) return;
            LogMessageEvent messageEvent = new()
            {
                Color = target.Colors.GetConsoleColor(logLevel),
                LogLevel = logLevel,
                Message = $"{target.Name}:\r\n{message}"
            };
            SendMessage(target, messageEvent);
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="target"></param>
        /// <param name="messageEvent"></param>
        public void SendMessage(LoggerTargetConfigModel target, LogMessageEvent messageEvent)
        {
            if (!LoggerConfig.ServerConfig.Enable || !LoggerConfig.ServerConfig.Targets.Contains(target.Name)) return;
            var nowWebSockets = webSockets.Select(m => m.Value).ToArray();
            List<WebSocketConnectionModel> removeList = new();
            foreach (WebSocketConnectionModel item in nowWebSockets)
            {
                if(item.WebSocket.IsAvailable)
                {
                    commandBus.SendEvent(item, messageEvent);
                }
                else
                {
                    removeList.Add(item);
                }
            }
            foreach (WebSocketConnectionModel item in removeList)
            {
                item.Close();
            }
        }
        /// <summary>
        /// Websocket配置
        /// </summary>
        /// <param name="webSocket"></param>
        private void WebscoketConfig(IWebSocketConnection webSocket)
        {
            var webSocketModel = new WebSocketConnectionModel(webSocket);
            webSocketModel.OnClose += WebSocketModel_OnClose;
            webSocketModel.OnMessage += WebSocketModel_OnMessage;
            while (!webSockets.TryAdd(webSocketModel.ID, webSocketModel));
        }
        private void WebSocketModel_OnMessage(WebSocketConnectionModel model, string message)
        {
            commandBus.Handler(this, model, message);
        }
        private void WebSocketModel_OnClose(WebSocketConnectionModel webSocketModel)
        {
            KeyValuePair<Guid, WebSocketConnectionModel> tempModel = webSockets.FirstOrDefault(m => m.Key == webSocketModel.ID);
            while (!webSockets.TryRemove(tempModel.Key, out _)) { }
        }
        private void WriteWebsocketMessage(LogLevel logLevel, string message, Exception ex)
        {
            if (logLevel < LoggerConfig.ServerConfig.FleckMinLevel || logLevel > LoggerConfig.ServerConfig.FleckMaxLevel) return;
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}|{logLevel}|MateralLoggerLocalServer]{message}");
        }
    }
}
