namespace Materal.Gateway.ConfigModel
{
    /// <summary>
    /// 负载均衡配置模型
    /// </summary>
    public class LoadBalancerOptionsModel
    {
        /// <summary>
        /// 类型
        /// NoLoadBalancer:不使用负载均衡 LeastConnection:最小连接 RoundRobin:循环 CookieStickySessions:会话黏贴
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public string Type { get; set; } = "NoLoadBalancer";
    }
}
