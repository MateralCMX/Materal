using Authority.DataTransmitModel.WebMenuAuthority;
using Authority.PresentationModel;
using Authority.PresentationModel.WebMenuAuthority.Request;
using Authority.Service;
using Authority.Service.Model.WebMenuAuthority;
using AutoMapper;
using Common.Model.APIAuthorityConfig;
using Materal.Common;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authority.WebAPI.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// 网页菜单权限控制器
    /// </summary>
    [Route("api/[controller]/[action]"), ApiController]
    public class WebMenuAuthorityController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWebMenuAuthorityService _webMenuAuthorityService;
        private readonly IUserService _userService;
        /// <summary>
        /// 构造方法
        /// </summary>
        public WebMenuAuthorityController(IWebMenuAuthorityService webMenuAuthorityService, IMapper mapper, IUserService userService)
        {
            _webMenuAuthorityService = webMenuAuthorityService;
            _mapper = mapper;
            _userService = userService;
        }
        /// <summary>
        /// 添加网页菜单权限
        /// </summary>
        /// <returns></returns>
        [HttpPost, AuthorityFilter(AuthoritySystemAPIAuthorityConfig.AddWebMenuAuthorityCode)]
        public async Task<ResultModel> AddWebMenuAuthority(AddWebMenuAuthorityRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<AddWebMenuAuthorityModel>(requestModel);
                await _webMenuAuthorityService.AddWebMenuAuthorityAsync(model);
                return ResultModel.Success("添加成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 修改网页菜单权限
        /// </summary>
        /// <returns></returns>
        [HttpPost, AuthorityFilter(AuthoritySystemAPIAuthorityConfig.EditWebMenuAuthorityCode)]
        public async Task<ResultModel> EditWebMenuAuthority(EditWebMenuAuthorityRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<EditWebMenuAuthorityModel>(requestModel);
                await _webMenuAuthorityService.EditWebMenuAuthorityAsync(model);
                return ResultModel.Success("修改成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 删除网页菜单权限
        /// </summary>
        /// <returns></returns>
        [HttpGet, AuthorityFilter(AuthoritySystemAPIAuthorityConfig.DeleteWebMenuAuthorityCode)]
        public async Task<ResultModel> DeleteWebMenuAuthority(Guid id)
        {
            try
            {
                await _webMenuAuthorityService.DeleteWebMenuAuthorityAsync(id);
                return ResultModel.Success("删除成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 获得网页菜单权限信息
        /// </summary>
        /// <returns></returns>
        [HttpGet, AuthorityFilter(AuthoritySystemAPIAuthorityConfig.QueryWebMenuAuthorityCode)]
        public async Task<ResultModel<WebMenuAuthorityDTO>> GetWebMenuAuthorityInfo(Guid id)
        {
            try
            {
                WebMenuAuthorityDTO result = await _webMenuAuthorityService.GetWebMenuAuthorityInfoAsync(id);
                return ResultModel<WebMenuAuthorityDTO>.Success(result, "查询成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel<WebMenuAuthorityDTO>.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 调换网页菜单权限位序
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost, AuthorityFilter(AuthoritySystemAPIAuthorityConfig.EditWebMenuAuthorityCode)]
        public async Task<ResultModel> ExchangeWebMenuAuthorityIndex(ExchangeIndexRequestModel<Guid> requestModel)
        {
            try
            {
                await _webMenuAuthorityService.ExchangeWebMenuAuthorityIndexAsync(requestModel.ID1, requestModel.ID2);
                return ResultModel.Success("调换成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 获得网页菜单权限树
        /// </summary>
        /// <returns></returns>
        [HttpGet, AuthorityFilter(AuthoritySystemAPIAuthorityConfig.QueryWebMenuAuthorityCode)]
        public async Task<ResultModel<List<WebMenuAuthorityTreeDTO>>> GetWebMenuAuthorityTree()
        {
            try
            {
                List<WebMenuAuthorityTreeDTO> result = await _webMenuAuthorityService.GetWebMenuAuthorityTreeAsync();
                return ResultModel<List<WebMenuAuthorityTreeDTO>>.Success(result, "查询成功");
            }
            catch (ArgumentException ex)
            {
                return ResultModel<List<WebMenuAuthorityTreeDTO>>.Fail(null, ex.Message);
            }
        }
        /// <summary>
        /// 获得用户拥有的网页菜单权限树
        /// </summary>
        /// <returns></returns>
        [HttpPost, AuthorityFilter(AuthoritySystemAPIAuthorityConfig.QueryWebMenuAuthorityCode)]
        public async Task<ResultModel<List<WebMenuAuthorityTreeDTO>>> GetUserHasWebMenuAuthorityTree(GetUserHasWebMenuAuthorityTreeRequestModel requestModel)
        {
            try
            {
                Guid userID = _userService.GetUserID(requestModel.Token);
                List<WebMenuAuthorityTreeDTO> result =
                    await _webMenuAuthorityService.GetWebMenuAuthorityTreeAsync(userID);
                return ResultModel<List<WebMenuAuthorityTreeDTO>>.Success(result, "查询成功");
            }
            catch (ArgumentException ex)
            {
                return ResultModel<List<WebMenuAuthorityTreeDTO>>.Fail(null, ex.Message);
            }
        }

        /// <summary>
        /// 更换网页菜单权限父级
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost, AuthorityFilter(AuthoritySystemAPIAuthorityConfig.EditWebMenuAuthorityCode)]
        public async Task<ResultModel> ExchangeWebMenuAuthorityParentID(ExchangeParentIDRequestModel<Guid> requestModel)
        {
            try
            {
                await _webMenuAuthorityService.ExchangeWebMenuAuthorityParentIDAsync(requestModel.ID, requestModel.ParentID, requestModel.IndexID, requestModel.ForUnder);
                return ResultModel.Success("修改成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
    }
}
