namespace Materal.DotNetty.Client.Core
{
    public interface IClientChannelHandler
    {
        /// <summary>
        /// 添加处理器
        /// </summary>
        /// <param name="handler"></param>
        void AddLastHandler(ClientHandlerContext handler);
    }
}
