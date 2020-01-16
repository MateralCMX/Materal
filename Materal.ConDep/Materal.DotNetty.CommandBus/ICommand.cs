namespace Materal.DotNetty.CommandBus
{
    public interface ICommand
    {
        /// <summary>
        /// 命令
        /// </summary>
        string Command { get; }
        /// <summary>
        /// 命令处理器
        /// </summary>
        string CommandHandler { get; }
    }
}
