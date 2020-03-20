namespace Materal.ConfigCenter.ProtalServer.PresentationModel.ConfigServer
{
    public class CopyConfigServerModel
    {
        /// <summary>
        /// 源服务名称
        /// </summary>
        public string SourceConfigServerName { get; set; }
        /// <summary>
        /// 目标配置服务名称
        /// </summary>
        public string[] TargetConfigServerNames { get; set; }
    }
}
