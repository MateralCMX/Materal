using Authority.DataTransmitModel.APIAuthority;
using Authority.PresentationModel;
using Authority.PresentationModel.APIAuthority.Request;
using Authority.Service;
using Authority.Service.Model.APIAuthority;
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
    /// API权限控制器
    /// </summary>
    [Route("api/[controller]/[action]"), ApiController]
    public class APIAuthorityController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAPIAuthorityService _apiAuthorityService;
        /// <summary>
        /// 构造方法
        /// </summary>
        public APIAuthorityController(IAPIAuthorityService apiAuthorityService, IMapper mapper)
        {
            _apiAuthorityService = apiAuthorityService;
            _mapper = mapper;
        }
        /// <summary>
        /// 添加API权限
        /// </summary>
        /// <returns></returns>
        [HttpPost, AuthorityFilter(AuthorityAPIAuthorityConfig.AddAPIAuthorityCode)]
        public async Task<ResultModel> AddAPIAuthority(AddAPIAuthorityRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<AddAPIAuthorityModel>(requestModel);
                await _apiAuthorityService.AddAPIAuthorityAsync(model);
                return ResultModel.Success("添加成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 修改API权限
        /// </summary>
        /// <returns></returns>
        [HttpPost, AuthorityFilter(AuthorityAPIAuthorityConfig.EditAPIAuthorityCode)]
        public async Task<ResultModel> EditAPIAuthority(EditAPIAuthorityRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<EditAPIAuthorityModel>(requestModel);
                await _apiAuthorityService.EditAPIAuthorityAsync(model);
                return ResultModel.Success("修改成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 删除API权限
        /// </summary>
        /// <returns></returns>
        [HttpGet, AuthorityFilter(AuthorityAPIAuthorityConfig.DeleteAPIAuthorityCode)]
        public async Task<ResultModel> DeleteAPIAuthority(Guid id)
        {
            try
            {
                await _apiAuthorityService.DeleteAPIAuthorityAsync(id);
                return ResultModel.Success("删除成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 获得API权限信息
        /// </summary>
        /// <returns></returns>
        [HttpGet, AuthorityFilter(AuthorityAPIAuthorityConfig.QueryAPIAuthorityCode)]
        public async Task<ResultModel<APIAuthorityDTO>> GetAPIAuthorityInfo(Guid id)
        {
            try
            {
                APIAuthorityDTO result = await _apiAuthorityService.GetAPIAuthorityInfoAsync(id);
                return ResultModel<APIAuthorityDTO>.Success(result, "查询成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel<APIAuthorityDTO>.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 获得API权限树
        /// </summary>
        /// <returns></returns>
        [HttpGet, AuthorityFilter(AuthorityAPIAuthorityConfig.QueryAPIAuthorityCode)]
        public async Task<ResultModel<List<APIAuthorityTreeDTO>>> GetAPIAuthorityTree()
        {
            try
            {
                List<APIAuthorityTreeDTO> result = await _apiAuthorityService.GetAPIAuthorityTreeAsync();
                return ResultModel<List<APIAuthorityTreeDTO>>.Success(result, "查询成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel<List<APIAuthorityTreeDTO>>.Fail(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return ResultModel<List<APIAuthorityTreeDTO>>.Fail(ex.Message);
            }
        }

        /// <summary>
        /// 更换API权限父级
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost, AuthorityFilter(AuthorityAPIAuthorityConfig.EditAPIAuthorityCode)]
        public async Task<ResultModel> ExchangeAPIAuthorityParentID(ExchangeParentIDNotIndexIDRequestModel<Guid> requestModel)
        {
            try
            {
                await _apiAuthorityService.ExchangeAPIAuthorityParentIDAsync(requestModel.ID, requestModel.ParentID);
                return ResultModel.Success("修改成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
    }
}
