using Materal.Gateway.DataTransmitModel.Swagger;
using Materal.Gateway.OcelotExtension.ConfigModel;
using Materal.Gateway.Service.Models.Route;
using Materal.Gateway.Service.Models.Sync;
using Materal.Gateway.WebAPI.PresentationModel.Swagger;

namespace Materal.Gateway.Service
{
    /// <summary>
    /// Ocelot配置服务
    /// </summary>
    public interface IOcelotConfigService
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        Task InitAsync();
        /// <summary>
        /// 设置BaseUrl
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        Task SetBaseUrlAsync(string? baseUrl);
        /// <summary>
        /// 获得BaseUrl
        /// </summary>
        /// <returns></returns>
        string? GetBaseUrl();
        /// <summary>
        /// 设置全局限流配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task SetGlobalRateLimitOptionsAsync(GlobalRateLimitOptionsModel? model);
        /// <summary>
        /// 获得全局限流配置
        /// </summary>
        /// <returns></returns>
        GlobalRateLimitOptionsModel? GetGlobalRateLimitOptions();
        /// <summary>
        /// 设置服务发现提供者数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task SetServiceDiscoveryProviderAsync(ServiceDiscoveryProviderModel? model);
        /// <summary>
        /// 获取服务发现提供者数据
        /// </summary>
        /// <returns></returns>
        ServiceDiscoveryProviderModel? GetServiceDiscoveryProviderConfig();
        /// <summary>
        /// 添加路由
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Guid> AddRouteAsync(AddRouteModel model);
        /// <summary>
        /// 修改路由
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task EditRouteAsync(EditRouteModel model);
        /// <summary>
        /// 删除路由
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteRouteAsync(Guid id);
        /// <summary>
        /// 获取路由数据
        /// </summary>
        /// <returns></returns>
        RouteConfigModel GetRouteInfo(Guid id);
        /// <summary>
        /// 获取路由列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<RouteConfigModel> GetRouteList(QueryRouteModel model);
        /// <summary>
        /// 上移路由
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task MoveUpRouteAsync(Guid id);
        /// <summary>
        /// 下移路由
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task MoveNextRouteAsync(Guid id);
        /// <summary>
        /// 添加Swagger
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Guid> AddSwaggerAsync(AddSwaggerModel model);
        /// <summary>
        /// 修改Swagger
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task EditSwaggerAsync(EditSwaggerModel model);
        /// <summary>
        /// 删除Swagger
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteSwaggerAsync(Guid id);
        /// <summary>
        /// 获取Swagger数据
        /// </summary>
        /// <returns></returns>
        SwaggerDTO GetSwaggerInfo(Guid id);
        /// <summary>
        /// 获取Swagger列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<SwaggerDTO> GetSwaggerList(QuerySwaggerModel model);
        /// <summary>
        /// 添加Swagger服务项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Guid> AddSwaggerServiceItemAsync(AddSwaggerServiceItemModel model);
        /// <summary>
        /// 添加SwaggerJson项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Guid> AddSwaggerJsonItemAsync(AddSwaggerJsonItemModel model);
        /// <summary>
        /// 修改Swagger服务项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task EditSwaggerServiceItemAsync(EditSwaggerServiceItemModel model);
        /// <summary>
        /// 修改SwaggerJson项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task EditSwaggerJsonItemAsync(EditSwaggerJsonItemModel model);
        /// <summary>
        /// 删除Swagger项
        /// </summary>
        /// <param name="swaggerConfigID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteSwaggerItemAsync(Guid swaggerConfigID, Guid id);
        /// <summary>
        /// 获取Swagger项数据
        /// </summary>
        /// <returns></returns>
        SwaggerItemDTO GetSwaggerItemInfo(Guid swaggerConfigID, Guid id);
        /// <summary>
        /// 获取Swagger项列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<SwaggerItemDTO> GetSwaggerItemList(QuerySwaggerItemModel model);
        /// <summary>
        /// 同步Swagger
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task SyncSwaggerAsync(SyncSwaggerModel model);
        /// <summary>
        /// 同步路由
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task SyncRouteAsync(SyncRouteModel model);
    }
}
