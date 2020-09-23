using AutoMapper;
using Deploy.DataTransmitModel.DefaultData;
using Deploy.HttpManage;
using Deploy.PresentationModel.DefaultData;
using Deploy.Services;
using Deploy.Services.Models.DefaultData;
using Materal.APP.WebAPICore;
using Materal.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Deploy.Server.Controllers
{
    /// <summary>
    /// 默认数据控制器
    /// </summary>
    [Route("api/[controller]/[action]"), ApiController]
    public class DefaultDataController : WebAPIControllerBase, IDefaultDataManage
    {
        private readonly IMapper _mapper;
        private readonly IDefaultDataService _defaultDataService;
        /// <summary>
        /// 默认数据控制器
        /// </summary>
        public DefaultDataController(IMapper mapper, IDefaultDataService defaultDataService)
        {
            _mapper = mapper;
            _defaultDataService = defaultDataService;
        }
        /// <summary>
        /// 添加默认数据
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel> AddAsync(AddDefaultDataRequestModel requestModel)
        {
            var model = _mapper.Map<AddDefaultDataModel>(requestModel);
            await _defaultDataService.AddAsync(model);
            return ResultModel.Success("添加成功");
        }
        /// <summary>
        /// 修改默认数据
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel> EditAsync(EditDefaultDataRequestModel requestModel)
        {
            var model = _mapper.Map<EditDefaultDataModel>(requestModel);
            await _defaultDataService.EditAsync(model);
            return ResultModel.Success("修改成功");
        }
        /// <summary>
        /// 删除默认数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ResultModel> DeleteAsync([Required(ErrorMessage = "唯一标识不能为空")] Guid id)
        {
            await _defaultDataService.DeleteAsync(id);
            return ResultModel.Success("删除成功");
        }
        /// <summary>
        /// 获得默认数据信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<DefaultDataDTO>> GetInfoAsync([Required(ErrorMessage = "唯一标识不能为空")] Guid id)
        {
            DefaultDataDTO result = await _defaultDataService.GetInfoAsync(id);
            return ResultModel<DefaultDataDTO>.Success(result, "查询成功");
        }
        /// <summary>
        /// 获得默认数据列表
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public async Task<PageResultModel<DefaultDataListDTO>> GetListAsync(QueryDefaultDataFilterRequestModel requestModel)
        {
            var model = _mapper.Map<QueryDefaultDataFilterModel>(requestModel);
            (List<DefaultDataListDTO> result, PageModel pageModel) = await _defaultDataService.GetListAsync(model);
            return PageResultModel<DefaultDataListDTO>.Success(result, pageModel, "查询成功");
        }
    }
}
