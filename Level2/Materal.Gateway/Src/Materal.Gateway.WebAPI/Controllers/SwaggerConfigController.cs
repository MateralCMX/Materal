using Materal.Gateway.Common;
using Materal.Gateway.OcelotExtension.ConfigModel;
using Materal.Gateway.OcelotExtension.Services;
using Materal.Gateway.WebAPI.DataTransmitModel.SwaggerConfig;
using Materal.Gateway.WebAPI.PresentationModel.SwaggerConfig;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;

namespace Materal.Gateway.WebAPI.Controllers
{
    /// <summary>
    /// Swagger控制器
    /// </summary>
    public class SwaggerConfigController(IOcelotConfigService ocelotConfigService) : GatewayControllerBase
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel<Guid>> AddAsync(AddSwaggerConfigRequestModel requestModel)
        {
            SwaggerConfigModel model = new()
            {
                ID = Guid.NewGuid(),
                Config = [],
                Key = requestModel.Key,
                TakeServersFromDownstreamService = requestModel.TakeServersFromDownstreamService,
            };
            ocelotConfigService.OcelotConfig.SwaggerEndPoints.Add(model);
            await ocelotConfigService.SaveAsync();
            return ResultModel<Guid>.Success(model.ID, "添加成功");
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> EditAsync(EditSwaggerConfigRequestModel requestModel)
        {
            SwaggerConfigModel config = ocelotConfigService.OcelotConfig.SwaggerEndPoints.FirstOrDefault(m => m.ID == requestModel.ID) ?? throw new GatewayException("未找到配置项");
            requestModel.CopyProperties(config);
            await ocelotConfigService.SaveAsync();
            return ResultModel.Success("修改成功");
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ResultModel> DeleteAsync(Guid id)
        {
            SwaggerConfigModel config = ocelotConfigService.OcelotConfig.SwaggerEndPoints.FirstOrDefault(m => m.ID == id) ?? throw new GatewayException("未找到配置项");
            ocelotConfigService.OcelotConfig.SwaggerEndPoints.Remove(config);
            await ocelotConfigService.SaveAsync();
            return ResultModel.Success("删除成功");
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<SwaggerConfigDTO> GetInfo(Guid id)
        {
            SwaggerConfigModel config = ocelotConfigService.OcelotConfig.SwaggerEndPoints.FirstOrDefault(m => m.ID == id) ?? throw new GatewayException("未找到配置项");
            SwaggerConfigDTO result = config.CopyProperties<SwaggerConfigDTO>();
            return ResultModel<SwaggerConfigDTO>.Success(result, "获取成功");
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="questModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultModel<List<SwaggerConfigDTO>> GetList(QuerySwaggerConfigRequestModel questModel)
        {
            Func<SwaggerConfigModel, bool> filter = questModel.GetSearchDelegate<SwaggerConfigModel>();
            List<SwaggerConfigModel> configs = ocelotConfigService.OcelotConfig.SwaggerEndPoints.Where(filter.Invoke).ToList();
            List<SwaggerConfigDTO> result = configs.Select(m => m.CopyProperties<SwaggerConfigDTO>()).ToList();
            return ResultModel<List<SwaggerConfigDTO>>.Success(result, "获取成功");
        }
        /// <summary>
        /// 添加服务项
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel<Guid>> AddServiceItemAsync(AddSwaggerServiceItemConfigRequestModel requestModel)
        {
            SwaggerConfigModel config = ocelotConfigService.OcelotConfig.SwaggerEndPoints.FirstOrDefault(m => m.ID == requestModel.SwaggerConfigID) ?? throw new GatewayException("未找到配置项");
            SwaggerItemConfigModel itemConfig = new()
            {
                Name = requestModel.Name,
                Version = requestModel.Version,
                Service = new()
                {
                    Name = requestModel.ServiceName,
                    Path = requestModel.ServicePath
                }
            };
            config.Config.Add(itemConfig);
            await ocelotConfigService.SaveAsync();
            return ResultModel<Guid>.Success(itemConfig.ID, "添加成功");
        }
        /// <summary>
        /// 添加Json项
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel<Guid>> AddJsonItemAsync(AddSwaggerJsonItemConfigRequestModel requestModel)
        {
            SwaggerConfigModel config = ocelotConfigService.OcelotConfig.SwaggerEndPoints.FirstOrDefault(m => m.ID == requestModel.SwaggerConfigID) ?? throw new GatewayException("未找到配置项");
            SwaggerItemConfigModel itemConfig = new()
            {
                Name = requestModel.Name,
                Version = requestModel.Version,
                Url = requestModel.Url
            };
            config.Config.Add(itemConfig);
            await ocelotConfigService.SaveAsync();
            return ResultModel<Guid>.Success(itemConfig.ID, "添加成功");
        }
        /// <summary>
        /// 修改服务项
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> EditServiceItemAsync(EditSwaggerServiceItemConfigRequestModel requestModel)
        {
            SwaggerConfigModel config = ocelotConfigService.OcelotConfig.SwaggerEndPoints.FirstOrDefault(m => m.ID == requestModel.SwaggerConfigID) ?? throw new GatewayException("未找到配置项");
            SwaggerItemConfigModel itemConfig = config.Config.FirstOrDefault(m => m.ID == requestModel.ID) ?? throw new GatewayException("未找到配置项");
            requestModel.CopyProperties(itemConfig);
            itemConfig.Url = null;
            itemConfig.Service = new()
            {
                Name = requestModel.ServiceName,
                Path = requestModel.ServicePath
            };
            await ocelotConfigService.SaveAsync();
            return ResultModel.Success("修改成功");
        }
        /// <summary>
        /// 修改Json项
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> EditJsonItemAsync(EditSwaggerJsonItemConfigRequestModel requestModel)
        {
            SwaggerConfigModel config = ocelotConfigService.OcelotConfig.SwaggerEndPoints.FirstOrDefault(m => m.ID == requestModel.SwaggerConfigID) ?? throw new GatewayException("未找到配置项");
            SwaggerItemConfigModel itemConfig = config.Config.FirstOrDefault(m => m.ID == requestModel.ID) ?? throw new GatewayException("未找到配置项");
            requestModel.CopyProperties(itemConfig);
            itemConfig.Url = requestModel.Url;
            itemConfig.Service = null;
            await ocelotConfigService.SaveAsync();
            return ResultModel.Success("修改成功");
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="swaggerConfigID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ResultModel> DeleteItemAsync(Guid swaggerConfigID, Guid id)
        {
            SwaggerConfigModel config = ocelotConfigService.OcelotConfig.SwaggerEndPoints.FirstOrDefault(m => m.ID == swaggerConfigID) ?? throw new GatewayException("未找到配置项");
            SwaggerItemConfigModel itemConfig = config.Config.FirstOrDefault(m => m.ID == id) ?? throw new GatewayException("未找到配置项");
            config.Config.Remove(itemConfig);
            await ocelotConfigService.SaveAsync();
            return ResultModel.Success("删除成功");
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<SwaggerItemConfigDTO> GetItemInfo(Guid swaggerConfigID, Guid id)
        {
            SwaggerConfigModel config = ocelotConfigService.OcelotConfig.SwaggerEndPoints.FirstOrDefault(m => m.ID == swaggerConfigID) ?? throw new GatewayException("未找到配置项");
            SwaggerItemConfigModel itemConfig = config.Config.FirstOrDefault(m => m.ID == id) ?? throw new GatewayException("未找到配置项");
            SwaggerItemConfigDTO result = itemConfig.CopyProperties<SwaggerItemConfigDTO>();
            return ResultModel<SwaggerItemConfigDTO>.Success(result, "获取成功");
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="questModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultModel<List<SwaggerItemConfigDTO>> GetItemList(QuerySwaggerItemConfigRequestModel questModel)
        {
            SwaggerConfigModel config = ocelotConfigService.OcelotConfig.SwaggerEndPoints.FirstOrDefault(m => m.ID == questModel.SwaggerConfigID) ?? throw new GatewayException("未找到配置项");
            List<SwaggerItemConfigDTO> result = config.Config.Select(m =>
            {
                SwaggerItemConfigDTO dto = m.CopyProperties<SwaggerItemConfigDTO>();
                if (m.Service != null)
                {
                    dto.Url = null;
                    dto.ServiceName = m.Service.Name;
                    dto.ServicePath = m.Service.Path;
                }
                return dto;
            }).ToList();
            return ResultModel<List<SwaggerItemConfigDTO>>.Success(result, "获取成功");
        }
    }
}
