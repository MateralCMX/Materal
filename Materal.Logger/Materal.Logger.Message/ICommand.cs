namespace Materal.Logger.Message
{
    public interface ICommand
    {
        /// <summary>
        /// 命令名称
        /// </summary>
        public string CommandName { get; set; }
    }
}
