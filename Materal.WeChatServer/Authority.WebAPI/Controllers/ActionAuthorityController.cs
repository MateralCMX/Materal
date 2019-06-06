using Authority.DataTransmitModel.ActionAuthority;
using Authority.PresentationModel;
using Authority.PresentationModel.ActionAuthority.Request;
using Authority.Service;
using Authority.Service.Model.ActionAuthority;
using AutoMapper;
using Common.Model.APIAuthorityConfig;
using Materal.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authority.WebAPI.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// 功能权限控制器
    /// </summary>
    [Route("api/[controller]/[action]"), ApiController]
    public class ActionAuthorityController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IActionAuthorityService _actionAuthorityService;

        /// <summary>
        /// 构造方法
        /// </summary>
        public ActionAuthorityController(IActionAuthorityService actionAuthorityService, IMapper mapper, IUserService userService)
        {
            _actionAuthorityService = actionAuthorityService;
            _mapper = mapper;
            _userService = userService;
        }
        /// <summary>
        /// 添加功能权限
        /// </summary>
        /// <returns></returns>
        [HttpPost, AuthorityFilter(AuthorityAPIAuthorityConfig.AddActionAuthorityCode)]
        public async Task<ResultModel> AddActionAuthority(AddActionAuthorityRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<AddActionAuthorityModel>(requestModel);
                await _actionAuthorityService.AddActionAuthorityAsync(model);
                return ResultModel.Success("添加成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 修改功能权限
        /// </summary>
        /// <returns></returns>
        [HttpPost, AuthorityFilter(AuthorityAPIAuthorityConfig.EditActionAuthorityCode)]
        public async Task<ResultModel> EditActionAuthority(EditActionAuthorityRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<EditActionAuthorityModel>(requestModel);
                await _actionAuthorityService.EditActionAuthorityAsync(model);
                return ResultModel.Success("修改成功");

            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 删除功能权限
        /// </summary>
        /// <returns></returns>
        [HttpGet, AuthorityFilter(AuthorityAPIAuthorityConfig.DeleteActionAuthorityCode)]
        public async Task<ResultModel> DeleteActionAuthority(Guid id)
        {
            try
            {
                await _actionAuthorityService.DeleteActionAuthorityAsync(id);
                return ResultModel.Success("删除成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 获得功能权限信息
        /// </summary>
        /// <returns></returns>
        [HttpGet, AuthorityFilter(AuthorityAPIAuthorityConfig.QueryActionAuthorityCode)]
        public async Task<ResultModel<ActionAuthorityDTO>> GetActionAuthorityInfo(Guid id)
        {
            try
            {
                ActionAuthorityDTO result = await _actionAuthorityService.GetActionAuthorityInfoAsync(id);
                return ResultModel<ActionAuthorityDTO>.Success(result, "查询成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel<ActionAuthorityDTO>.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 获得功能权限列表
        /// </summary>
        /// <returns></returns>
        [HttpPost, AuthorityFilter(AuthorityAPIAuthorityConfig.QueryActionAuthorityCode)]
        public async Task<PageResultModel<ActionAuthorityListDTO>> GetActionAuthorityList(QueryActionAuthorityFilterRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<QueryActionAuthorityFilterModel>(requestModel);
                (List<ActionAuthorityListDTO> result, PageModel pageModel) = await _actionAuthorityService.GetActionAuthorityListAsync(model);
                return PageResultModel<ActionAuthorityListDTO>.Success(result, pageModel, "查询成功");
            }
            catch (InvalidOperationException ex)
            {
                return PageResultModel<ActionAuthorityListDTO>.Fail(null, null, ex.Message);
            }
        }
        /// <summary>
        /// 获得用户拥有的功能权限列表
        /// </summary>
        /// <returns></returns>
        [HttpPost, AuthorityFilter(AuthorityAPIAuthorityConfig.BaseAPICode)]
        public async Task<ResultModel<List<ActionAuthorityListDTO>>> GetUserHasActionAuthorityList(GetUserOwnedActionAuthorityListRequestModel requestModel)
        {
            try
            {
                Guid userID = _userService.GetUserID(requestModel.Token);
                List<ActionAuthorityListDTO> result = await _actionAuthorityService.GetUserOwnedActionAuthorityListAsync(userID, requestModel.ActionGroupCode);
                return ResultModel<List<ActionAuthorityListDTO>>.Success(result, "查询成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel<List<ActionAuthorityListDTO>>.Fail(null, ex.Message);
            }
        }
    }
}
