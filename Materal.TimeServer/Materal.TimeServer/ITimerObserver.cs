using System;

namespace Materal.TimeServer
{
    public interface ITimerObserver
    {
        /// <summary>
        /// 类型
        /// </summary>
        TimerObserverCategory Category { get; }
        /// <summary>
        /// 下次执行时间
        /// </summary>
        DateTime NextExecuteTime { get; }
        /// <summary>
        /// 更新下次执行时间
        /// </summary>
        void UpdateNextExecuteTime();
    }
}
