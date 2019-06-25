namespace NCWM.Model
{
    /// <summary>
    /// 配置
    /// </summary>
    public class ConfigModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 目标名称
        /// </summary>
        public string TargetName { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public string Arguments { get; set; }
        /// <summary>
        /// .NetCore版本
        /// </summary>
        public float DotNetCoreVersion { get; set; }
        /// <summary>
        /// 目录
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 主模块名称
        /// </summary>
        public string MainModuleName => DotNetCoreVersion < 3 ? $"{TargetName}.dll" : $"{TargetName}.exe";
        /// <summary>
        /// 命令行命令
        /// </summary>
        public string CmdCommand => string.IsNullOrEmpty(Arguments)? $@"dotnet {Path}\{MainModuleName}": $@"dotnet {Path}\{MainModuleName} {Arguments}";
    }
}
