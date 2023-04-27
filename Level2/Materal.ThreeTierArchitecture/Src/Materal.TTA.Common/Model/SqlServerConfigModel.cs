namespace Materal.TTA.Common.Model
{
    /// <summary>
    /// SqlServer数据库配置模型
    /// </summary>
    public class SqlServerConfigModel : SqlServerSubordinateConfigModel
    {
        /// <summary>
        /// 从属数据库配置组
        /// </summary>
        public IEnumerable<SqlServerSubordinateConfigModel>? SubordinateConfigs { get; set; }
    }
}
