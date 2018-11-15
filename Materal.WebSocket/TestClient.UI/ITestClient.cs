namespace TestClient.UI
{
    public interface ITestClient
    {
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
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        void SendMessage(string message);
    }
}
