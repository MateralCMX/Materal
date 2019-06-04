using System;
using System.Threading.Tasks;

namespace Materal.TimeServer
{
    public interface IAsyncTimerObserver : ITimerObserver
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <returns></returns>
        Task ExecuteAsync();
    }
}
