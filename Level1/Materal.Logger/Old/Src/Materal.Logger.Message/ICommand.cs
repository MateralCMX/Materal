namespace Materal.Logger.Message
{
    /// <summary>
    /// 命令
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// 命令名称
        /// </summary>
        public string CommandName { get; set; }
    }
}
