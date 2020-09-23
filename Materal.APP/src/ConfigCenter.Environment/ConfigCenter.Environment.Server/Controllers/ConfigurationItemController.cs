using AutoMapper;
using ConfigCenter.Environment.DataTransmitModel.ConfigurationItem;
using ConfigCenter.Environment.HttpManage;
using ConfigCenter.Environment.PresentationModel.ConfigurationItem;
using ConfigCenter.Environment.Services;
using ConfigCenter.Environment.Services.Models.ConfigurationItem;
using Materal.APP.WebAPICore;
using Materal.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ConfigCenter.Environment.Server.Controllers
{
    /// <summary>
    /// 配置项控制器
    /// </summary>
    [Route("api/[controller]/[action]"), ApiController]
    public class ConfigurationItemController : WebAPIControllerBase, IConfigurationItemManage
    {
        private readonly IConfigurationItemService _configurationItemService;
        private readonly IMapper _mapper;
        /// <summary>
        /// 配置项控制器
        /// </summary>
        public ConfigurationItemController(IConfigurationItemService configurationItemService, IMapper mapper)
        {
            _configurationItemService = configurationItemService;
            _mapper = mapper;
        }
        /// <summary>
        /// 添加配置项
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel> AddConfigurationItemAsync(AddConfigurationItemRequestModel requestModel)
        {
            var model = _mapper.Map<AddConfigurationItemModel>(requestModel);
            await _configurationItemService.AddConfigurationItemAsync(model);
            return ResultModel.Success("添加成功");
        }

        /// <summary>
        /// 修改配置项
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel> EditConfigurationItemAsync(EditConfigurationItemRequestModel requestModel)
        {
            var model = _mapper.Map<EditConfigurationItemModel>(requestModel);
            await _configurationItemService.EditConfigurationItemAsync(model);
            return ResultModel.Success("修改成功");
        }
        /// <summary>
        /// 删除配置项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ResultModel> DeleteConfigurationItemAsync([Required(ErrorMessage = "唯一标识不能为空")] Guid id)
        {
            await _configurationItemService.DeleteConfigurationItemAsync(id);
            return ResultModel.Success("删除成功");
        }
        /// <summary>
        /// 获得配置项信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<ConfigurationItemDTO>> GetConfigurationItemInfoAsync([Required(ErrorMessage = "唯一标识不能为空")] Guid id)
        {
            ConfigurationItemDTO result = await _configurationItemService.GetConfigurationItemInfoAsync(id);
            return ResultModel<ConfigurationItemDTO>.Success(result, "查询成功");
        }
        /// <summary>
        /// 获得配置项列表
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public async Task<ResultModel<List<ConfigurationItemListDTO>>> GetConfigurationItemListAsync(QueryConfigurationItemFilterRequestModel requestModel)
        {
            var model = _mapper.Map<QueryConfigurationItemFilterModel>(requestModel);
            List<ConfigurationItemListDTO> result = await _configurationItemService.GetConfigurationItemListAsync(model);
            return ResultModel<List<ConfigurationItemListDTO>>.Success(result, "查询成功");
        }
    }
}
