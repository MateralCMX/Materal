using Materal.CacheHelper;
using Materal.Gateway.Common.Models;
using Materal.Gateway.WebAPI.Services.Models;
using Materal.Gateway.WebAPI.Utility;
using Ocelot.Configuration.File;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Materal.Gateway.WebAPI.Services
{
    public class RouteServices : BaseServices<RouteServices>, IRouteServices
    {
        public RouteServices(ICacheManager cacheManager, ILogger<RouteServices> logger) : base(cacheManager, logger)
        {
        }

        public async Task AddAsync(RouteModel routeModel)
        {
            if (string.IsNullOrWhiteSpace(routeModel.ServiceName)) throw new GatewayException("服务名称不能为空");
            FileConfiguration fileConfiguration = await GetFileConfigurationAsync();
            FileRoute route = fileConfiguration.Routes.FirstOrDefault(m => m.ServiceName == routeModel.ServiceName);
            if (route != null) throw new GatewayException("该服务已存在");
            FileRoute newRoute = OcelotModelHelper.GetNewFileRoute();
            newRoute.ServiceName = routeModel.ServiceName;
            newRoute.DownstreamPathTemplate = "/api/{everything}";
            newRoute.UpstreamPathTemplate = "/" + routeModel.ServiceName + "/{everything}";
            newRoute.UpstreamHttpMethod = new List<string> { "Get", "Post" };
            newRoute = SetCacheConfig(newRoute, routeModel);
            fileConfiguration.Routes.Add(newRoute);
            await SetFileConfigurationAsync(fileConfiguration);
        }
        public async Task EditAsync(RouteModel routeModel)
        {
            FileConfiguration fileConfiguration = await GetFileConfigurationAsync();
            FileRoute route = GetRouteByServiceName(routeModel.ServiceName, fileConfiguration);
            SetCacheConfig(route, routeModel);
            await SetFileConfigurationAsync(fileConfiguration);
        }

        public async Task DeleteAsync(string serviceName)
        {
            if (string.IsNullOrWhiteSpace(serviceName)) throw new GatewayException("服务名称不能为空");
            FileConfiguration fileConfiguration = await GetFileConfigurationAsync();
            FileRoute route = GetRouteByServiceName(serviceName, fileConfiguration);
            fileConfiguration.Routes.Remove(route);
            await SetFileConfigurationAsync(fileConfiguration);
        }
        /// <summary>
        /// 获得路由
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public async Task<RouteModel> GetAsync(string serviceName)
        {
            if (string.IsNullOrWhiteSpace(serviceName)) throw new GatewayException("服务名称不能为空");
            FileRoute route = await GetRouteByServiceNameAsync(serviceName);
            RouteModel result = new RouteModel
            {
                ServiceName = route.ServiceName,
                EnableCache = !string.IsNullOrWhiteSpace(route.FileCacheOptions.Region)
            };
            if (result.EnableCache)
            {
                result.CacheTimer = route.FileCacheOptions.TtlSeconds;
            }
            return result;
        }
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public async Task<List<RouteListModel>> GetListAsync(QueryRouteFilterModel filterModel)
        {
            FileConfiguration fileConfiguration = await GetFileConfigurationAsync();
            List<FileRoute> routes = fileConfiguration.Routes;
            if (filterModel.ServiceName != null)
            {
                routes = routes.Where(m => !string.IsNullOrWhiteSpace(m.ServiceName) && m.ServiceName.Contains(filterModel.ServiceName)).ToList();
            }
            List<RouteListModel> result = routes.Select(m => new RouteListModel
            {
                ServiceName = m.ServiceName,
                EnableCache = !string.IsNullOrWhiteSpace(m.FileCacheOptions.Region)
            }).ToList();
            return result;
        }
        #region 私有方法

        /// <summary>
        /// 根据服务名称获得路由
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="fileConfiguration"></param>
        /// <returns></returns>
        private FileRoute GetRouteByServiceName(string serviceName, FileConfiguration fileConfiguration)
        {
            FileRoute route = fileConfiguration.Routes.FirstOrDefault(m => m.ServiceName == serviceName);
            if (route == null) throw new GatewayException("路由不存在");
            return route;
        }
        /// <summary>
        /// 根据服务名称获得路由
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        private async Task<FileRoute> GetRouteByServiceNameAsync(string serviceName)
        {
            FileConfiguration fileConfiguration = await GetFileConfigurationAsync();
            return GetRouteByServiceName(serviceName, fileConfiguration);
        }
        /// <summary>
        /// 设置缓存配置
        /// </summary>
        /// <param name="route"></param>
        /// <param name="routeModel"></param>
        private FileRoute SetCacheConfig(FileRoute route, RouteModel routeModel)
        {
            if (routeModel.EnableCache)
            {
                route.FileCacheOptions.Region = $"{routeModel.ServiceName}Region";
                if (routeModel.CacheTimer <= 0) throw new GatewayException("缓存时间必须大于0");
                route.FileCacheOptions.TtlSeconds = routeModel.CacheTimer;
            }
            else
            {
                route.FileCacheOptions.Region = string.Empty;
                route.FileCacheOptions.TtlSeconds = 0;
            }
            return route;
        }
        #endregion
    }
}
