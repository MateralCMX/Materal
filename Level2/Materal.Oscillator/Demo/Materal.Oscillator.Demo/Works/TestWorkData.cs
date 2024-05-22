using Dy.Oscillator.Demo.Works;
using Materal.Oscillator.Works;

namespace Materal.Oscillator.Demo.Works
{
    public class TestWorkData : WorkData<TestWork>
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = "Hello World!";
    }
}
