namespace Materal.Gateway.Application
{
    /// <summary>
    /// WebAPIConfig
    /// </summary>
    [Options("Gateway")]
    public class ApplicationConfig : IOptions
    {
        /// <summary>
        /// 授权配置
        /// </summary>
        public List<UserEntity> Users { get; set; } = [];
    }
}
