using Materal.Gateway.Common;
using Materal.Gateway.DataTransmitModel.Swagger;
using Materal.Gateway.OcelotExtension.ConfigModel;
using Materal.Gateway.OcelotExtension.Repositories;
using Materal.Gateway.Service.Models.Route;
using Materal.Gateway.Service.Models.Sync;
using Materal.Gateway.WebAPI.PresentationModel.Swagger;
using Materal.Utils.Consul;
using Materal.Utils.Consul.Models;
using System.Text.RegularExpressions;

namespace Materal.Gateway.Service
{
    /// <summary>
    /// Ocelot配置服务
    /// </summary>
    public class OcelotConfigServiceImpl(IOcelotConfigRepository ocelotConfigRepository, IConsulService consulService) : IOcelotConfigService
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public async Task InitAsync()
        {
            ocelotConfigRepository.SetRoutesIndex();
            await ocelotConfigRepository.SaveAsync();
        }
        /// <summary>
        /// 设置BaseUrl
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        public async Task SetBaseUrlAsync(string? baseUrl)
        {
            ocelotConfigRepository.OcelotConfig.GlobalConfiguration.BaseUrl = baseUrl;
            await ocelotConfigRepository.SaveAsync();
        }
        /// <summary>
        /// 获得BaseUrl
        /// </summary>
        /// <returns></returns>
        public string? GetBaseUrl() => ocelotConfigRepository.OcelotConfig.GlobalConfiguration.BaseUrl;
        /// <summary>
        /// 设置全局限流配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task SetGlobalRateLimitOptionsAsync(GlobalRateLimitOptionsModel? model)
        {
            ocelotConfigRepository.OcelotConfig.GlobalConfiguration.RateLimitOptions = model;
            await ocelotConfigRepository.SaveAsync();
        }
        /// <summary>
        /// 获得全局限流配置
        /// </summary>
        /// <returns></returns>
        public GlobalRateLimitOptionsModel? GetGlobalRateLimitOptions() => ocelotConfigRepository.OcelotConfig.GlobalConfiguration.RateLimitOptions;
        /// <summary>
        /// 设置服务发现提供者数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task SetServiceDiscoveryProviderAsync(ServiceDiscoveryProviderModel? model)
        {
            ocelotConfigRepository.OcelotConfig.GlobalConfiguration.ServiceDiscoveryProvider = model;
            await ocelotConfigRepository.SaveAsync();
        }
        /// <summary>
        /// 获取服务发现提供者数据
        /// </summary>
        /// <returns></returns>
        public ServiceDiscoveryProviderModel? GetServiceDiscoveryProviderConfig() => ocelotConfigRepository.OcelotConfig.GlobalConfiguration.ServiceDiscoveryProvider;
        /// <summary>
        /// 添加路由
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Guid> AddRouteAsync(AddRouteModel model)
        {
            RouteConfigModel route = model.CopyProperties<RouteConfigModel>();
            route.ID = Guid.NewGuid();
            route.Index = ocelotConfigRepository.OcelotConfig.Routes.Count;
            ocelotConfigRepository.OcelotConfig.Routes.Add(route);
            await ocelotConfigRepository.SaveAsync();
            return route.ID;
        }
        /// <summary>
        /// 修改路由
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task EditRouteAsync(EditRouteModel model)
        {
            RouteConfigModel routeConfig = ocelotConfigRepository.OcelotConfig.Routes.FirstOrDefault(m => m.ID == model.ID) ?? throw new GatewayException("路由不存在");
            model.CopyProperties(routeConfig);
            await ocelotConfigRepository.SaveAsync();
        }
        /// <summary>
        /// 删除路由
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteRouteAsync(Guid id)
        {
            RouteConfigModel routeConfig = ocelotConfigRepository.OcelotConfig.Routes.FirstOrDefault(m => m.ID == id) ?? throw new GatewayException("路由不存在");
            ocelotConfigRepository.OcelotConfig.Routes.Remove(routeConfig);
            await ocelotConfigRepository.SaveAsync();
        }
        /// <summary>
        /// 获取路由数据
        /// </summary>
        /// <returns></returns>
        public RouteConfigModel GetRouteInfo(Guid id) => ocelotConfigRepository.OcelotConfig.Routes.FirstOrDefault(m => m.ID == id) ?? throw new GatewayException("未找到路由");
        /// <summary>
        /// 获取路由列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<RouteConfigModel> GetRouteList(QueryRouteModel model)
        {
            Func<RouteConfigModel, bool> filter = model.GetSearchDelegate<RouteConfigModel>();
            List<RouteConfigModel> configs = ocelotConfigRepository.OcelotConfig.Routes.Where(filter.Invoke).ToList();
            if (model.EnableCache is not null)
            {
                configs = model.EnableCache.Value
                    ? configs.Where(m => m.FileCacheOptions is not null).ToList()
                    : configs.Where(m => m.FileCacheOptions is null).ToList();
            }
            return configs;
        }
        /// <summary>
        /// 上移路由
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task MoveUpRouteAsync(Guid id)
        {
            RouteConfigModel config = ocelotConfigRepository.OcelotConfig.Routes.FirstOrDefault(m => m.ID == id) ?? throw new GatewayException("未找到路由");
            int upIndex = config.Index - 1;
            if (upIndex < 0) return;
            ocelotConfigRepository.OcelotConfig.Routes.Remove(config);
            ocelotConfigRepository.OcelotConfig.Routes.Insert(upIndex, config);
            ocelotConfigRepository.SetRoutesIndex();
            await ocelotConfigRepository.SaveAsync();
        }
        /// <summary>
        /// 下移路由
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task MoveNextRouteAsync(Guid id)
        {
            RouteConfigModel config = ocelotConfigRepository.OcelotConfig.Routes.FirstOrDefault(m => m.ID == id) ?? throw new GatewayException("未找到路由");
            int nextIndex = config.Index + 1;
            if (nextIndex >= ocelotConfigRepository.OcelotConfig.Routes.Count) return;
            ocelotConfigRepository.OcelotConfig.Routes.Remove(config);
            ocelotConfigRepository.OcelotConfig.Routes.Insert(nextIndex, config);
            ocelotConfigRepository.SetRoutesIndex();
            await ocelotConfigRepository.SaveAsync();
        }
        /// <summary>
        /// 添加Swagger
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Guid> AddSwaggerAsync(AddSwaggerModel model)
        {
            SwaggerConfigModel config = new()
            {
                ID = Guid.NewGuid(),
                Config = [],
                Key = model.Key,
                TakeServersFromDownstreamService = model.TakeServersFromDownstreamService,
            };
            ocelotConfigRepository.OcelotConfig.SwaggerEndPoints.Add(config);
            await ocelotConfigRepository.SaveAsync();
            return config.ID;
        }
        /// <summary>
        /// 修改Swagger
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task EditSwaggerAsync(EditSwaggerModel model)
        {
            SwaggerConfigModel config = ocelotConfigRepository.OcelotConfig.SwaggerEndPoints.FirstOrDefault(m => m.ID == model.ID) ?? throw new GatewayException("未找到Swagger配置");
            model.CopyProperties(config);
            await ocelotConfigRepository.SaveAsync();
        }
        /// <summary>
        /// 删除Swagger
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteSwaggerAsync(Guid id)
        {
            SwaggerConfigModel config = ocelotConfigRepository.OcelotConfig.SwaggerEndPoints.FirstOrDefault(m => m.ID == id) ?? throw new GatewayException("未找到Swagger配置");
            ocelotConfigRepository.OcelotConfig.SwaggerEndPoints.Remove(config);
            await ocelotConfigRepository.SaveAsync();
        }
        /// <summary>
        /// 获取Swagger数据
        /// </summary>
        /// <returns></returns>
        public SwaggerDTO GetSwaggerInfo(Guid id)
        {
            SwaggerConfigModel config = ocelotConfigRepository.OcelotConfig.SwaggerEndPoints.FirstOrDefault(m => m.ID == id) ?? throw new GatewayException("未找到Swagger配置");
            SwaggerDTO result = config.CopyProperties<SwaggerDTO>();
            result.Items = config.Config.Select(GetSwaggerItemDTO).ToList();
            return result;
        }
        /// <summary>
        /// 获取Swagger列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<SwaggerDTO> GetSwaggerList(QuerySwaggerModel model)
        {
            Func<SwaggerConfigModel, bool> filter = model.GetSearchDelegate<SwaggerConfigModel>();
            List<SwaggerConfigModel> configs = ocelotConfigRepository.OcelotConfig.SwaggerEndPoints.Where(filter.Invoke).ToList();
            List<SwaggerDTO> result = configs.Select(m =>
            {
                SwaggerDTO dto = m.CopyProperties<SwaggerDTO>();
                dto.Items = m.Config.Select(GetSwaggerItemDTO).ToList();
                return dto;
            }).ToList();
            return result;
        }
        /// <summary>
        /// 添加Swagger服务项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Guid> AddSwaggerServiceItemAsync(AddSwaggerServiceItemModel model)
        {
            SwaggerConfigModel config = ocelotConfigRepository.OcelotConfig.SwaggerEndPoints.FirstOrDefault(m => m.ID == model.SwaggerConfigID) ?? throw new GatewayException("未找到Swagger配置");
            SwaggerItemConfigModel itemConfig = new()
            {
                Name = model.Name,
                Version = model.Version,
                Service = new()
                {
                    Name = model.ServiceName,
                    Path = model.ServicePath
                }
            };
            config.Config.Add(itemConfig);
            await ocelotConfigRepository.SaveAsync();
            return itemConfig.ID;
        }
        /// <summary>
        /// 添加SwaggerJson项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Guid> AddSwaggerJsonItemAsync(AddSwaggerJsonItemModel model)
        {
            SwaggerConfigModel config = ocelotConfigRepository.OcelotConfig.SwaggerEndPoints.FirstOrDefault(m => m.ID == model.SwaggerConfigID) ?? throw new GatewayException("未找到Swagger配置");
            SwaggerItemConfigModel itemConfig = new()
            {
                Name = model.Name,
                Version = model.Version,
                Url = model.Url
            };
            config.Config.Add(itemConfig);
            await ocelotConfigRepository.SaveAsync();
            return itemConfig.ID;
        }
        /// <summary>
        /// 修改Swagger服务项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task EditSwaggerServiceItemAsync(EditSwaggerServiceItemModel model)
        {
            SwaggerConfigModel config = ocelotConfigRepository.OcelotConfig.SwaggerEndPoints.FirstOrDefault(m => m.ID == model.SwaggerConfigID) ?? throw new GatewayException("未找到Swagger配置");
            SwaggerItemConfigModel itemConfig = config.Config.FirstOrDefault(m => m.ID == model.ID) ?? throw new GatewayException("未找到Swagger项");
            model.CopyProperties(itemConfig);
            itemConfig.Url = null;
            itemConfig.Service = new()
            {
                Name = model.ServiceName,
                Path = model.ServicePath
            };
            await ocelotConfigRepository.SaveAsync();
        }
        /// <summary>
        /// 修改SwaggerJson项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task EditSwaggerJsonItemAsync(EditSwaggerJsonItemModel model)
        {
            SwaggerConfigModel config = ocelotConfigRepository.OcelotConfig.SwaggerEndPoints.FirstOrDefault(m => m.ID == model.SwaggerConfigID) ?? throw new GatewayException("未找到Swagger配置");
            SwaggerItemConfigModel itemConfig = config.Config.FirstOrDefault(m => m.ID == model.ID) ?? throw new GatewayException("未找到Swagger项");
            model.CopyProperties(itemConfig);
            itemConfig.Url = model.Url;
            itemConfig.Service = null;
            await ocelotConfigRepository.SaveAsync();
        }
        /// <summary>
        /// 删除Swagger项
        /// </summary>
        /// <param name="swaggerConfigID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteSwaggerItemAsync(Guid swaggerConfigID, Guid id)
        {
            SwaggerConfigModel config = ocelotConfigRepository.OcelotConfig.SwaggerEndPoints.FirstOrDefault(m => m.ID == swaggerConfigID) ?? throw new GatewayException("未找到Swagger配置");
            SwaggerItemConfigModel itemConfig = config.Config.FirstOrDefault(m => m.ID == id) ?? throw new GatewayException("未找到Swagger项");
            config.Config.Remove(itemConfig);
            await ocelotConfigRepository.SaveAsync();
        }
        /// <summary>
        /// 获取Swagger项数据
        /// </summary>
        /// <returns></returns>
        public SwaggerItemDTO GetSwaggerItemInfo(Guid swaggerConfigID, Guid id)
        {
            SwaggerConfigModel config = ocelotConfigRepository.OcelotConfig.SwaggerEndPoints.FirstOrDefault(m => m.ID == swaggerConfigID) ?? throw new GatewayException("未找到Swagger配置");
            SwaggerItemConfigModel itemConfig = config.Config.FirstOrDefault(m => m.ID == id) ?? throw new GatewayException("未找到Swagger项");
            SwaggerItemDTO result = GetSwaggerItemDTO(itemConfig);
            return result;
        }
        /// <summary>
        /// 获取Swagger项列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<SwaggerItemDTO> GetSwaggerItemList(QuerySwaggerItemModel model)
        {
            SwaggerConfigModel config = ocelotConfigRepository.OcelotConfig.SwaggerEndPoints.FirstOrDefault(m => m.ID == model.SwaggerConfigID) ?? throw new GatewayException("未找到Swagger配置");
            List<SwaggerItemDTO> result = config.Config.Select(GetSwaggerItemDTO).ToList();
            return result;
        }
        /// <summary>
        /// 同步Swagger
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="GatewayException"></exception>
        public async Task SyncSwaggerAsync(SyncSwaggerModel model)
        {
            if (ocelotConfigRepository.OcelotConfig.GlobalConfiguration.ServiceDiscoveryProvider is null) throw new GatewayException("未配置服务发现");
            ServiceDiscoveryProviderModel consulModel = ocelotConfigRepository.OcelotConfig.GlobalConfiguration.ServiceDiscoveryProvider;
            string consulUrl = $"{(consulModel.IsSSL ? "https" : "http")}://{consulModel.Host}:{consulModel.Port}";
            List<ConsulServiceModel> servies = await consulService.GetServiceListAsync(consulUrl);
            if (ocelotConfigRepository.OcelotConfig.SwaggerEndPoints == null) ocelotConfigRepository.OcelotConfig.SwaggerEndPoints = [];
            if (model.Clear) ocelotConfigRepository.OcelotConfig.SwaggerEndPoints.Clear();
            foreach (ConsulServiceModel service in servies)
            {
                if (string.IsNullOrWhiteSpace(service.Service)) continue;
                try
                {
                    if (!string.IsNullOrWhiteSpace(model.Name) && !Regex.IsMatch(service.Service, model.Name)) continue;
                }
                catch (Exception ex)
                {
                    throw new GatewayException($"正则表达式错误:{ex.Message}");
                }
                bool next = true;
                if (!string.IsNullOrWhiteSpace(model.Tag))
                {
                    next = false;
                    if (service.Tags == null || service.Tags.Length == 0) continue;
                    foreach (string tag in service.Tags)
                    {
                        if (!Regex.IsMatch(tag, model.Tag)) continue;
                        next = true;
                        break;
                    }
                }
                if (!next) continue;
                if (ocelotConfigRepository.OcelotConfig.SwaggerEndPoints.Exists(m => m.Key == service.Service)) continue;
                SwaggerConfigModel swaggerConfig = new()
                {
                    TakeServersFromDownstreamService = true,
                    Key = service.Service
                };
                swaggerConfig.Config.Add(new()
                {
                    Name = service.Service,
                    Url = null,
                    Version = "v1",
                    Service = new()
                    {
                        Name = service.Service,
                        Path = "/swagger/v1/swagger.json"
                    }
                });
                ocelotConfigRepository.OcelotConfig.SwaggerEndPoints.Add(swaggerConfig);
            }
            ocelotConfigRepository.OcelotConfig.SwaggerEndPoints = [.. ocelotConfigRepository.OcelotConfig.SwaggerEndPoints.OrderBy(m => m.Key)];
            ocelotConfigRepository.Save();
        }
        /// <summary>
        /// 同步路由
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task SyncRouteAsync(SyncRouteModel model)
        {
            if (ocelotConfigRepository.OcelotConfig.GlobalConfiguration.ServiceDiscoveryProvider is null) throw new GatewayException("未配置服务发现");
            ServiceDiscoveryProviderModel consulModel = ocelotConfigRepository.OcelotConfig.GlobalConfiguration.ServiceDiscoveryProvider;
            string consulUrl = $"{(consulModel.IsSSL ? "https" : "http")}://{consulModel.Host}:{consulModel.Port}";
            List<ConsulServiceModel> servies = await consulService.GetServiceListAsync(consulUrl);
            if (ocelotConfigRepository.OcelotConfig.Routes == null) ocelotConfigRepository.OcelotConfig.Routes = [];
            if (model.Mode == SyncModeEnum.Cover) ocelotConfigRepository.OcelotConfig.Routes.Clear();
            List<RouteConfigModel> routeConfigs = [];
            foreach (ConsulServiceModel service in servies)
            {
                if (string.IsNullOrWhiteSpace(service.Service)) continue;
                try
                {
                    if (!string.IsNullOrWhiteSpace(model.Name) && !Regex.IsMatch(service.Service, model.Name)) continue;
                }
                catch (Exception ex)
                {
                    throw new GatewayException($"正则表达式错误:{ex.Message}");
                }
                bool next = true;
                if (!string.IsNullOrWhiteSpace(model.Tag))
                {
                    next = false;
                    if (service.Tags == null || service.Tags.Length == 0) continue;
                    foreach (string tag in service.Tags)
                    {
                        if (!Regex.IsMatch(tag, model.Tag)) continue;
                        next = true;
                        break;
                    }
                }
                if (!next) continue;
                if (routeConfigs.Exists(m => !string.IsNullOrWhiteSpace(m.ServiceName) && m.ServiceName == service.Service)) continue;
                RouteConfigModel? routeConfigModel = ocelotConfigRepository.OcelotConfig.Routes.FirstOrDefault(m => !string.IsNullOrWhiteSpace(m.ServiceName) && m.ServiceName == service.Service);
                if (routeConfigModel != null)
                {
                    if (model.Mode == SyncModeEnum.Replace)
                    {
                        ocelotConfigRepository.OcelotConfig.Routes.Remove(routeConfigModel);
                        routeConfigModel = null;
                    }
                    else
                    {
                        continue;
                    }
                }
                routeConfigModel = new()
                {
                    UpstreamPathTemplate = $"/{service.Service}{model.UpstreamPathTemplate}",
                    DownstreamPathTemplate = model.DownstreamPathTemplate,
                    DownstreamScheme = model.DownstreamScheme.ToString().ToLower(),
                    DownstreamHttpVersion = model.HttpVersion.GetDescription(),
                    UpstreamHttpMethod = ["GET", "POST", "PUT", "DELETE", "OPTIONS"],
                    ServiceName = service.Service,
                    LoadBalancerOptions = new() { Type = "LeastConnection" },
                    AuthenticationOptions = null,
                    DangerousAcceptAnyServerCertificateValidator = model.DangerousAcceptAnyServerCertificateValidator,
                    DownstreamHostAndPorts = null,
                    FileCacheOptions = null,
                    RateLimitOptions = null,
                    QoSOptions = null,
                };
                if (ocelotConfigRepository.OcelotConfig.SwaggerEndPoints.Exists(m => m.Key == service.Service))
                {
                    routeConfigModel.SwaggerKey = service.Service;
                }
                routeConfigs.Add(routeConfigModel);
            }
            routeConfigs = [.. routeConfigs.OrderBy(m => m.ServiceName)];
            ocelotConfigRepository.OcelotConfig.Routes.AddRange(routeConfigs);
            ocelotConfigRepository.SetRoutesIndex();
            ocelotConfigRepository.Save();
        }
        /// <summary>
        /// 获得Swagger项DTO
        /// </summary>
        /// <param name="swaggerItemConfig"></param>
        /// <returns></returns>
        private static SwaggerItemDTO GetSwaggerItemDTO(SwaggerItemConfigModel swaggerItemConfig)
        {
            SwaggerItemDTO result = swaggerItemConfig.CopyProperties<SwaggerItemDTO>();
            if (swaggerItemConfig.Service is not null)
            {
                result.Url = null;
                result.ServiceName = swaggerItemConfig.Service.Name;
                result.ServicePath = swaggerItemConfig.Service.Path;
            }
            return result;
        }
    }
}
