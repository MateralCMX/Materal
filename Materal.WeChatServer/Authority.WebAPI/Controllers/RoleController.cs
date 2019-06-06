using Authority.DataTransmitModel.Role;
using Authority.PresentationModel;
using Authority.PresentationModel.Role.Request;
using Authority.Service;
using Authority.Service.Model.Role;
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
    /// 角色控制器
    /// </summary>
    [Route("api/[controller]/[action]"), ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;
        /// <summary>
        /// 构造方法
        /// </summary>
        public RoleController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <returns></returns>
        [HttpPost, AuthorityFilter(AuthorityAPIAuthorityConfig.AddRoleCode)]
        public async Task<ResultModel> AddRole(AddRoleRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<AddRoleModel>(requestModel);
                await _roleService.AddRoleAsync(model);
                return ResultModel.Success("添加成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <returns></returns>
        [HttpPost, AuthorityFilter(AuthorityAPIAuthorityConfig.EditRoleCode)]
        public async Task<ResultModel> EditRole(EditRoleRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<EditRoleModel>(requestModel);
                await _roleService.EditRoleAsync(model);
                return ResultModel.Success("修改成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <returns></returns>
        [HttpGet, AuthorityFilter(AuthorityAPIAuthorityConfig.DeleteRoleCode)]
        public async Task<ResultModel> DeleteRole(Guid id)
        {
            try
            {
                await _roleService.DeleteRoleAsync(id);
                return ResultModel.Success("删除成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 获得角色信息
        /// </summary>
        /// <returns></returns>
        [HttpGet, AuthorityFilter(AuthorityAPIAuthorityConfig.QueryUserCode)]
        public async Task<ResultModel<RoleDTO>> GetRoleInfo(Guid id)
        {
            try
            {
                RoleDTO result = await _roleService.GetRoleInfoAsync(id);
                return ResultModel<RoleDTO>.Success(result, "查询成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel<RoleDTO>.Fail(ex.Message);
            }
        }

        /// <summary>
        /// 获得角色权限树
        /// </summary>
        /// <returns></returns>
        [HttpGet, AuthorityFilter(AuthorityAPIAuthorityConfig.QueryUserCode)]
        public async Task<ResultModel<List<RoleTreeDTO>>> GetRoleTree()
        {
            try
            {
                List<RoleTreeDTO> result = await _roleService.GetRoleTreeAsync();
                return ResultModel<List<RoleTreeDTO>>.Success(result, "查询成功");
            }
            catch (ArgumentException ex)
            {
                return ResultModel<List<RoleTreeDTO>>.Fail(null, ex.Message);
            }
        }

        /// <summary>
        /// 更换角色权限父级
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost, AuthorityFilter(AuthorityAPIAuthorityConfig.EditRoleCode)]
        public async Task<ResultModel> ExchangeRoleParentID(ExchangeParentIDNotIndexIDRequestModel<Guid> requestModel)
        {
            try
            {
                await _roleService.ExchangeRoleParentIDAsync(requestModel.ID, requestModel.ParentID);
                return ResultModel.Success("修改成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
    }
}
