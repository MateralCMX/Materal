using Materal.Gateway.DataTransmitModel.Swagger;
using Materal.Gateway.Service;
using Materal.Gateway.Service.Models.Swagger;

namespace Materal.Gateway.Application.Controllers
{
    /// <summary>
    /// Swagger控制器
    /// </summary>
    public class SwaggerConfigController(IOcelotConfigService ocelotConfigService) : GatewayControllerBase
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel<Guid>> AddAsync(AddSwaggerModel model)
        {
            Guid result = await ocelotConfigService.AddSwaggerAsync(model);
            return ResultModel<Guid>.Success(result, "添加成功");
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> EditAsync(EditSwaggerModel model)
        {
            await ocelotConfigService.EditSwaggerAsync(model);
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
            await ocelotConfigService.DeleteSwaggerAsync(id);
            return ResultModel.Success("删除成功");
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<SwaggerDTO> GetInfo(Guid id)
        {
            SwaggerDTO result = ocelotConfigService.GetSwaggerInfo(id);
            return ResultModel<SwaggerDTO>.Success(result, "获取成功");
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultModel<List<SwaggerDTO>> GetList(QuerySwaggerModel model)
        {
            List<SwaggerDTO> result = ocelotConfigService.GetSwaggerList(model);
            return ResultModel<List<SwaggerDTO>>.Success(result, "获取成功");
        }
        /// <summary>
        /// 添加服务项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel<Guid>> AddServiceItemAsync(AddSwaggerServiceItemModel model)
        {
            Guid result = await ocelotConfigService.AddSwaggerServiceItemAsync(model);
            return ResultModel<Guid>.Success(result, "添加成功");
        }
        /// <summary>
        /// 添加Json项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel<Guid>> AddJsonItemAsync(AddSwaggerJsonItemModel model)
        {
            Guid result = await ocelotConfigService.AddSwaggerJsonItemAsync(model);
            return ResultModel<Guid>.Success(result, "添加成功");
        }
        /// <summary>
        /// 修改服务项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> EditServiceItemAsync(EditSwaggerServiceItemModel model)
        {
            await ocelotConfigService.EditSwaggerServiceItemAsync(model);
            return ResultModel.Success("修改成功");
        }
        /// <summary>
        /// 修改Json项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> EditJsonItemAsync(EditSwaggerJsonItemModel model)
        {
            await ocelotConfigService.EditSwaggerJsonItemAsync(model);
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
            await ocelotConfigService.DeleteSwaggerItemAsync(swaggerConfigID, id);
            return ResultModel.Success("删除成功");
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<SwaggerItemDTO> GetItemInfo(Guid swaggerConfigID, Guid id)
        {
            SwaggerItemDTO result = ocelotConfigService.GetSwaggerItemInfo(swaggerConfigID, id);
            return ResultModel<SwaggerItemDTO>.Success(result, "获取成功");
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultModel<List<SwaggerItemDTO>> GetItemList(QuerySwaggerItemModel model)
        {
            List<SwaggerItemDTO> result = ocelotConfigService.GetSwaggerItemList(model);
            return ResultModel<List<SwaggerItemDTO>>.Success(result, "获取成功");
        }
    }
}
