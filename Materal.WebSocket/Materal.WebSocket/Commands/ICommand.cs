namespace Materal.WebSocket.Commands
{
    public interface ICommand
    {
        /// <summary>
        /// 处理器名称
        /// </summary>
        string HandlerName { get; }
    }
}
