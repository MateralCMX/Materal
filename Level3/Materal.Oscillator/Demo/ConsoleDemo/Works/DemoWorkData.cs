using Materal.Oscillator.Abstractions.Works;

namespace ConsoleDemo.Works
{
    public class DemoWorkData : BaseWorkData, IWorkData
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = string.Empty;
        public DemoWorkData()
        {
        }
        public DemoWorkData(string message)
        {
            Message = message;
        }
    }
}
