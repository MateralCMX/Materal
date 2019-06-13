namespace Common.Model
{
    public class ReRouteModel
    {
        /// <summary>
        /// 下游路径模板
        /// </summary>
        public string DownstreamPathTemplate { get; set; }
        /// <summary>
        /// 下游
        /// </summary>
        public string DownstreamScheme { get; set; }
        /// <summary>
        /// 下游主机和端口
        /// </summary>
        public DownstreamHostAndPortModel[] DownstreamHostAndPorts { get; set; }
        /// <summary>
        /// 上游路径模板
        /// </summary>
        public string UpstreamPathTemplate { get; set; }
        /// <summary>
        /// 上游HttpMethod
        /// </summary>
        public string[] UpstreamHttpMethod { get; set; }
        /// <summary>
        /// 忽略SSL错误
        /// </summary>
        public bool DangerousAcceptAnyServerCertificateValidator { get; set; }
        /// <summary>
        /// 大小写敏感
        /// </summary>
        public bool ReRouteIsCaseSensitive { get; set; }
    }
}
