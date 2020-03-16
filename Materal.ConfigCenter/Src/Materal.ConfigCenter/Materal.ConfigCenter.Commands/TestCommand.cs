using Materal.DotNetty.CommandBus;

namespace Materal.ConfigCenter.Commands
{
    /// <summary>
    /// 测试命令
    /// </summary>
    public class TestCommand : BaseCommand
    {
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
    }
}
