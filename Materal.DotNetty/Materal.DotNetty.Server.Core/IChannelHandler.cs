namespace Materal.DotNetty.Server.Core
{
    public interface IMateralChannelHandler
    {
        /// <summary>
        /// 添加处理器
        /// </summary>
        /// <param name="handler"></param>
        void AddLastHandler(HandlerContext handler);
    }
}
