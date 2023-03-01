namespace Materal.Logger.Message
{
    /// <summary>
    /// 基命令
    /// </summary>
    public class BaseCommand : ICommand
    {
        /// <summary>
        /// 命令名称
        /// </summary>
        public string CommandName { get; set; } = string.Empty;
        /// <summary>
        /// 构造方法
        /// </summary>
        public BaseCommand()
        {
            if (string.IsNullOrWhiteSpace(CommandName))
            {
                CommandName = GetType().Name;
            }
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="commandName"></param>
        public BaseCommand(string commandName)
        {
            CommandName = commandName;
        }
    }
}
