namespace Materal.TTA.SqlServerEFRepository
{
    /// <summary>
    /// SqlServer数据库配置模型
    /// </summary>
    public class SqlServerConfigModel : SqlServerSubordinateConfigModel
    {
        /// <summary>
        /// 参数前缀
        /// </summary>
        public const string ParamsPrefix = "@";
        /// <summary>
        /// 字段前缀
        /// </summary>
        public const string FieldPrefix = "[";
        /// <summary>
        /// 字段后缀
        /// </summary>
        public const string FieldSuffix = "]";
        /// <summary>
        /// 从属数据库配置组
        /// </summary>
        public IEnumerable<SqlServerSubordinateConfigModel>? SubordinateConfigs { get; set; }
    }
}
