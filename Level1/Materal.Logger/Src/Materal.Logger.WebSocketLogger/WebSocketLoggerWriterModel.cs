namespace Materal.Logger.WebSocketLogger
{
    /// <summary>
    /// WebSocket日志写入器模型
    /// </summary>
    public class WebSocketLoggerWriterModel(LoggerWriterModel model, WebSocketLoggerTargetConfig target) : LoggerWriterModel(model)
    {
    }
}
