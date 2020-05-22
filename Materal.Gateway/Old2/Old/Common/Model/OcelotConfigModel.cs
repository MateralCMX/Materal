namespace Common.Model
{
    public class OcelotConfigModel
    {
        /// <summary>
        /// 重定向路由
        /// </summary>
        public ReRouteModel[] ReRoutes { get; set; }
        /// <summary>
        /// 全局配置模型
        /// </summary>
        public GlobalConfigurationModel GlobalConfiguration { get; set; }
    }
}
