namespace Materal.TimeServer
{
    public interface ITimerSubject
    {
        /// <summary>
        /// 开始执行
        /// </summary>
        void Start();
        /// <summary>
        /// 结束执行
        /// </summary>
        void Stop();
    }
}
