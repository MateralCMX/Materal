using Materal.APP.DataTransmitModel;
using System.Collections.Generic;
using System.Linq;
using Materal.APP.Enums;
using WebAPP.Common;

namespace WebAPP.HttpClientImpl
{
    public static class UrlManage
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="serverList"></param>
        public static void Init(List<ServerListDTO> serverList)
        {
            ServerListDTO server = serverList.FirstOrDefault(m => m.ServerCategory == ServerCategoryEnum.Authority);
            if (server != null)
            {
                AuthorityUrl = server.Url;
            }
            server = serverList.FirstOrDefault(m => m.ServerCategory == ServerCategoryEnum.ConfigCenter);
            if (server != null)
            {
                ConfigCenterUrl = server.Url;
            }
            server = serverList.FirstOrDefault(m => m.ServerCategory == ServerCategoryEnum.Deploy);
            if (server != null)
            {
                DeployUrl = server.Url;
            }
        }
        /// <summary>
        /// 核心服务Url
        /// </summary>
        public static string MateralAppUrl => WebAPPConfig.MateralAppUrl;
        /// <summary>
        /// 权限服务Url
        /// </summary>
        public static string AuthorityUrl { get; private set; }
        /// <summary>
        /// 配置中心Url
        /// </summary>
        public static string ConfigCenterUrl { get; private set; }
        /// <summary>
        /// 部署服务Url
        /// </summary>
        public static string DeployUrl { get; private set; }
    }
}
