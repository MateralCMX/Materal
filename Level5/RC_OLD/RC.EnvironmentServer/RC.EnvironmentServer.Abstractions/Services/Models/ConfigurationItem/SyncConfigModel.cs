namespace RC.EnvironmentServer.Abstractions.Services.Models.ConfigurationItem
{
    /// <summary>
    /// 同步配置模型
    /// </summary>
    public class SyncConfigModel : FilterModel
    {
        /// <summary>
        /// 项目唯一标识
        /// </summary>
        [Equal]
        public Guid ProjectID { get; set; }
        /// <summary>
        /// 命名空间唯一标识
        /// </summary>
        [Contains("NamespaceID")]
        public List<Guid>? NamespaceIDs { get; set; }
        /// <summary>
        /// 目标源环境
        /// </summary>
        public string[] TargetEnvironments { get; set; } = [];
        /// <summary>
        /// 同步模式
        /// </summary>
        public SyncModeEnum Mode { get; set; }
    }
}
