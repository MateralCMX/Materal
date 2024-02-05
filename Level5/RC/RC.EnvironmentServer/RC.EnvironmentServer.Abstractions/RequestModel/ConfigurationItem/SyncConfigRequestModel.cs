namespace RC.EnvironmentServer.Abstractions.RequestModel.ConfigurationItem
{
    /// <summary>
    /// 同步配置请求模型
    /// </summary>
    public class SyncConfigRequestModel
    {
        /// <summary>
        /// 项目唯一标识
        /// </summary>
        public Guid ProjectID { get; set; }
        /// <summary>
        /// 命名空间唯一标识
        /// </summary>
        public List<Guid>? NamespaceIDs { get; set; }
        /// <summary>
        /// 同步模式
        /// </summary>
        public SyncModeEnum Mode { get; set; }
        /// <summary>
        /// 目标源环境
        /// </summary>
        public string[] TargetEnvironments { get; set; } = [];
    }
}
