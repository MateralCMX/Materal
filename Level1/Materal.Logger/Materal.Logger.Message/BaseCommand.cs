namespace Materal.Logger.Message
{
    public class BaseCommand : ICommand
    {
        /// <summary>
        /// 命令名称
        /// </summary>
        public string CommandName { get; set; }
        public BaseCommand()
        {
            if (string.IsNullOrWhiteSpace(CommandName))
            {
                CommandName = GetType().Name;
            }
        }
        public BaseCommand(string commandName)
        {
            CommandName = commandName;
        }
    }
}
