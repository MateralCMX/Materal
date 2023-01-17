namespace RC.ServerCenter.DataTransmitModel.Server
{
    /// <summary>
    /// 配置环境数据传输模型
    /// </summary>
    public class EnvironmentServerListDTO
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 主机
        /// </summary>
        public string Host { get; set; } = string.Empty;
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 服务
        /// </summary>
        public string Service { get; set; } = string.Empty;
    }
}
