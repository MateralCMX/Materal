using Materal.Logger.Message;
using Materal.Logger.Models;
using Materal.LoggerClientHandler;
using Fleck;

namespace Materal.Logger
{
    public class MateralLoggerLocalServer
    {
        private readonly System.Collections.Concurrent.ConcurrentDictionary<Guid, WebSocketConnectionModel> webSockets = new();
        private readonly WebSocketServer webSocketServer;
        private static readonly CommandBus commandBus = new();
        public MateralLoggerLocalServer()
        {
            //commandBus.Subscribe<>
            webSocketServer = new WebSocketServer($"ws://0.0.0.0:{MateralLoggerConfig.ServerConfig.Port}");
            FleckLog.LogAction = WriteWebsocketMessage;
            webSocketServer.Start(WebscoketConfig);
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="messageEvent"></param>
        public void SendMessage(MateralLoggerTargetConfigModel target, string message, Microsoft.Extensions.Logging.LogLevel logLevel)
        {
            if (!MateralLoggerConfig.ServerConfig.Enable || !MateralLoggerConfig.ServerConfig.Targets.Contains(target.Name)) return;
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
        /// <param name="messageEvent"></param>
        public void SendMessage(MateralLoggerTargetConfigModel target, LogMessageEvent messageEvent)
        {
            if (!MateralLoggerConfig.ServerConfig.Enable || !MateralLoggerConfig.ServerConfig.Targets.Contains(target.Name)) return;
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
            while (!webSockets.TryRemove(tempModel)) { }
        }
        private void WriteWebsocketMessage(LogLevel logLevel, string message, Exception ex)
        {
            if (logLevel < MateralLoggerConfig.ServerConfig.FleckMinLevel || logLevel > MateralLoggerConfig.ServerConfig.FleckMaxLevel) return;
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}|{logLevel}|MateralLoggerLocalServer]{message}");
        }
    }
}
