using Materal.Oscillator.Abstractions.Works;

namespace Materal.Oscillator.Works
{
    /// <summary>
    /// 控制台任务数据
    /// </summary>
    public class ConsoleWorkData : BaseWorkData, IWorkData
    {
        /// <summary>
        /// 显示的消息
        /// </summary>
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// 构造方法
        /// </summary>
        public ConsoleWorkData()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        public ConsoleWorkData(string message)
        {
            Message = message;
        }
    }
}
