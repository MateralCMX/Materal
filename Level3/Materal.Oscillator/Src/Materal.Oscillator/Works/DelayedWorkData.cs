using Materal.Oscillator.Abstractions.Works;

namespace Materal.Oscillator.Works
{
    /// <summary>
    /// 延时任务数据
    /// </summary>
    public class DelayedWorkData : BaseWorkData, IWorkData
    {
        /// <summary>
        /// 延时毫秒数
        /// </summary>
        public int MillisecondsDelay { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public DelayedWorkData() { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="millisecondsDelay"></param>
        public DelayedWorkData(int millisecondsDelay = 100)
        {
            MillisecondsDelay = millisecondsDelay;
        }
    }
}
