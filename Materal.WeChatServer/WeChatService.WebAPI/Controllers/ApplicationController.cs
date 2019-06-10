using Authority.PresentationModel;
using AutoMapper;
using Common.Model.APIAuthorityConfig;
using Materal.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeChatService.DataTransmitModel.Application;
using WeChatService.PresentationModel.Application.Request;
using WeChatService.Service;
using WeChatService.Service.Model.Application;

namespace WeChatService.WebAPI.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// 应用控制器
    /// </summary>
    [Route("api/[controller]/[action]"), ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IApplicationService _applicationService;

        /// <summary>
        /// 构造方法
        /// </summary>
        public ApplicationController(IApplicationService applicationService, IMapper mapper)
        {
            _applicationService = applicationService;
            _mapper = mapper;
        }
        /// <summary>
        /// 添加应用
        /// </summary>
        /// <returns></returns>
        [HttpPost, AuthorityFilter(WeChatServiceAPIAuthorityConfig.AddApplicationCode)]
        public async Task<ResultModel> AddApplication(AddApplicationRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<AddApplicationModel>(requestModel);
                await _applicationService.AddApplicationAsync(model);
                return ResultModel.Success("添加成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 修改应用
        /// </summary>
        /// <returns></returns>
        [HttpPost, AuthorityFilter(WeChatServiceAPIAuthorityConfig.EditApplicationCode)]
        public async Task<ResultModel> EditApplication(EditApplicationRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<EditApplicationModel>(requestModel);
                await _applicationService.EditApplicationAsync(model);
                return ResultModel.Success("修改成功");

            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 删除应用
        /// </summary>
        /// <returns></returns>
        [HttpGet, AuthorityFilter(WeChatServiceAPIAuthorityConfig.DeleteApplicationCode)]
        public async Task<ResultModel> DeleteApplication(Guid id)
        {
            try
            {
                await _applicationService.DeleteApplicationAsync(id);
                return ResultModel.Success("删除成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 获得应用信息
        /// </summary>
        /// <returns></returns>
        [HttpGet, AuthorityFilter(WeChatServiceAPIAuthorityConfig.QueryApplicationCode)]
        public async Task<ResultModel<ApplicationDTO>> GetApplicationInfo(Guid id)
        {
            try
            {
                ApplicationDTO result = await _applicationService.GetApplicationInfoAsync(id);
                return ResultModel<ApplicationDTO>.Success(result, "查询成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel<ApplicationDTO>.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 获得应用列表
        /// </summary>
        /// <returns></returns>
        [HttpPost, AuthorityFilter(WeChatServiceAPIAuthorityConfig.QueryApplicationCode)]
        public async Task<PageResultModel<ApplicationListDTO>> GetApplicationList(QueryApplicationFilterRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<QueryApplicationFilterModel>(requestModel);
                (List<ApplicationListDTO> result, PageModel pageModel) = await _applicationService.GetApplicationListAsync(model);
                return PageResultModel<ApplicationListDTO>.Success(result, pageModel,"查询成功");
            }
            catch (InvalidOperationException ex)
            {
                return PageResultModel<ApplicationListDTO>.Fail(ex.Message);
            }
        }
    }
}
