using System;

namespace TestClient.UI
{
    public interface ITestClient : IDisposable
    {
        /// <summary>
        /// 自动重连标识
        /// </summary>
        bool IsAutoReload { get; set; }
        /// <summary>
        /// 初始化
        /// </summary>
        void Init();
        /// <summary>
        /// 启动服务
        /// </summary>
        void Start();
        /// <summary>
        /// 停止服务
        /// </summary>
        void Stop();
    }
}
