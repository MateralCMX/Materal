using AspectCore.DynamicProxy;
using Materal.ConfigCenter.ConfigServer.DataTransmitModel.ConfigurationItem;
using Materal.ConfigCenter.ConfigServer.PresentationModel.ConfigurationItem;
using Materal.ConfigCenter.ConfigServer.Services;
using Materal.ConfigCenter.ControllerCore;
using Materal.DotNetty.ControllerBus.Attributes;
using Materal.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Materal.ConfigCenter.ConfigServer.Controllers
{
    public class ConfigurationItemController : ConfigCenterBaseController
    {
        private readonly IConfigurationItemService configurationItemService;
        public ConfigurationItemController(IConfigurationItemService configurationItemService)
        {
            this.configurationItemService = configurationItemService;
        }
        /// <summary>
        /// 添加配置项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> AddConfigurationItem(AddConfigurationItemModel model)
        {
            try
            {
                await configurationItemService.AddConfigurationItemAsync(model);
                return ResultModel.Success("添加成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel.Fail(ex.InnerException?.Message);
            }
            catch (MateralConfigCenterException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 初始化配置项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> InitConfigurationItems(List<AddConfigurationItemModel> model)
        {
            try
            {
                await configurationItemService.InitConfigurationItemsAsync(model);
                return ResultModel.Success("添加成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel.Fail(ex.InnerException?.Message);
            }
            catch (MateralConfigCenterException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 初始化配置项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> InitConfigurationItemsByNamespace(InitConfigurationItemsByNamespaceModel model)
        {
            try
            {
                await configurationItemService.InitConfigurationItemsByNamespaceAsync(model);
                return ResultModel.Success("添加成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel.Fail(ex.InnerException?.Message);
            }
            catch (MateralConfigCenterException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 修改配置项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> EditConfigurationItem(EditConfigurationItemModel model)
        {
            try
            {
                await configurationItemService.EditConfigurationItemAsync(model);
                return ResultModel.Success("修改成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel.Fail(ex.InnerException?.Message);
            }
            catch (MateralConfigCenterException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 删除配置项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel> DeleteConfigurationItem(Guid id)
        {
            try
            {
                await configurationItemService.DeleteConfigurationItemAsync(id);
                return ResultModel.Success("删除成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel.Fail(ex.InnerException?.Message);
            }
            catch (MateralConfigCenterException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 获得配置项信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<ConfigurationItemDTO>> GetConfigurationItemInfo(Guid id)
        {
            try
            {
                ConfigurationItemDTO result = await configurationItemService.GetConfigurationItemInfoAsync(id);
                return ResultModel<ConfigurationItemDTO>.Success(result, "查询成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel<ConfigurationItemDTO>.Fail(ex.InnerException?.Message);
            }
            catch (MateralConfigCenterException ex)
            {
                return ResultModel<ConfigurationItemDTO>.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 获得配置项列表
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        [HttpPost, AllowAuthority]
        public async Task<ResultModel<List<ConfigurationItemListDTO>>> GetConfigurationItemList(QueryConfigurationItemFilterModel filterModel)
        {
            try
            {
                List<ConfigurationItemListDTO> result = await configurationItemService.GetConfigurationItemListAsync(filterModel);
                return ResultModel<List<ConfigurationItemListDTO>>.Success(result, "查询成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel<List<ConfigurationItemListDTO>>.Fail(ex.InnerException?.Message);
            }
            catch (MateralConfigCenterException ex)
            {
                return ResultModel<List<ConfigurationItemListDTO>>.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 根据项目唯一标识删除配置项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel> DeleteConfigurationItemByProjectID(Guid id)
        {
            try
            {
                await configurationItemService.DeleteConfigurationItemByProjectIDAsync(id);
                return ResultModel.Success("删除成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel.Fail(ex.InnerException?.Message);
            }
            catch (MateralConfigCenterException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 根据命名空间唯一标识删除配置项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel> DeleteConfigurationItemByNamespaceID(Guid id)
        {
            try
            {
                await configurationItemService.DeleteConfigurationItemByNamespaceIDAsync(id);
                return ResultModel.Success("删除成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel.Fail(ex.InnerException?.Message);
            }
            catch (MateralConfigCenterException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
    }
}
