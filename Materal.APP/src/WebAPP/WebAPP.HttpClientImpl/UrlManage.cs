using Materal.APP.Core.Models;
using Materal.APP.DataTransmitModel;
using Materal.APP.Enums;
using System.Collections.Generic;
using System.Linq;
using ConfigCenter.DataTransmitModel.ConfigCenter;
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
            DeployUrls = serverList.Where(m => m.ServerCategory == ServerCategoryEnum.Deploy).Select(m=>new KeyValueModel(m.Url, $"{m.Name}[{m.Url}]")).OrderBy(m=>m.Value).ToArray();
            if (DeployUrls.Count > 0)
            {
                DeployUrl = DeployUrls.First();
            }
        }
        /// <summary>
        /// 初始化环境列表
        /// </summary>
        /// <param name="environmentList"></param>
        public static void InitEnvironmentUrl(List<EnvironmentListDTO> environmentList)
        {
            EnvironmentUrls = environmentList.Select(m => new KeyValueModel(m.Url, $"{m.Name}[{m.Url}]")).OrderBy(m => m.Value).ToArray();
            if (EnvironmentUrls.Count > 0)
            {
                EnvironmentUrl = EnvironmentUrls.First();
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
        public static KeyValueModel DeployUrl { get; set; }
        /// <summary>
        /// 环境Url
        /// </summary>
        public static KeyValueModel EnvironmentUrl { get; set; }
        /// <summary>
        /// 部署服务Url组
        /// </summary>
        public static ICollection<KeyValueModel> DeployUrls { get; private set; }
        /// <summary>
        /// 环境Url组
        /// </summary>
        public static ICollection<KeyValueModel> EnvironmentUrls { get; private set; }
    }
}
