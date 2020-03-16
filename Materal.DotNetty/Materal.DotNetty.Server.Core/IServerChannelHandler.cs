namespace Materal.DotNetty.Server.Core
{
    public interface IServerChannelHandler
    {
        /// <summary>
        /// 添加处理器
        /// </summary>
        /// <param name="handler"></param>
        void AddLastHandler(ServerHandlerContext handler);
    }
}
