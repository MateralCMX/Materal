using AspectCore.DynamicProxy;
using Authority.Common;
using Authority.DataTransmitModel.User;
using Authority.PresentationModel.User;
using Authority.Services;
using Authority.Services.Models.User;
using AutoMapper;
using Materal.APP.Core;
using Materal.APP.WebAPICore.Controllers;
using Materal.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Authority.Server.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    [Route("api/[controller]/[action]"), ApiController]
    public class UserController : WebAPIControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        /// <summary>
        /// 用户控制器
        /// </summary>
        public UserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel> AddUserAsync(AddUserRequestModel requestModel)
        {
            AddUserModel model = _mapper.Map<AddUserModel>(requestModel);
            await _userService.AddUserAsync(model);
            return ResultModel.Success("添加成功");
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel> EditUserAsync(EditUserRequestModel requestModel)
        {
            EditUserModel model = _mapper.Map<EditUserModel>(requestModel);
            await _userService.EditUserAsync(model);
            return ResultModel.Success("修改成功");
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ResultModel> DeleteUserAsync([Required(ErrorMessage = "唯一标识不能为空")] Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return ResultModel.Success("删除成功");
        }

        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<UserDTO>> GetUserInfoAsync([Required(ErrorMessage = "唯一标识不能为空")] Guid id)
        {
            UserDTO result = await _userService.GetUserInfoAsync(id);
            return ResultModel<UserDTO>.Success(result, "查询成功");
        }

        /// <summary>
        /// 获得我的信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<UserDTO>> GetMyUserInfoAsync()
        {
            Guid id = GetLoginUserID();
            UserDTO result = await _userService.GetUserInfoAsync(id);
            return ResultModel<UserDTO>.Success(result, "查询成功");
        }

        /// <summary>
        /// 获得用户列表
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResultModel<UserListDTO>> GetUserListAsync(QueryUserFilterRequestModel requestModel)
        {
            var model = _mapper.Map<QueryUserFilterModel>(requestModel);
            (List<UserListDTO> result, PageModel pageModel) = await _userService.GetUserListAsync(model);
            return PageResultModel<UserListDTO>.Success(result, pageModel, "查询成功");
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public async Task<ResultModel<LoginResultModel>> LoginAsync(LoginRequestModel requestModel)
        {
            try
            {
                LoginModel model = _mapper.Map<LoginModel>(requestModel);
                UserDTO userInfo = await _userService.LoginAsync(model);
                string token = ApplicationConfig.JWTConfig.GetToken(userInfo.ID.ToString());
                LoginResultModel result = new LoginResultModel
                {
                    Token = token,
                    ExpiredTime = ApplicationConfig.JWTConfig.ExpiredTime
                };
                return ResultModel<LoginResultModel>.Success(result, "登录成功");
            }
            catch (AspectInvocationException exception)
            {
                if (exception.InnerException is AuthorityException)
                {
                    return ResultModel<LoginResultModel>.Fail("账号或者密码错误");
                }
                throw;
            }
            catch (AuthorityException)
            {
                return ResultModel<LoginResultModel>.Fail("账号或者密码错误");
            }
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel<string>> ResetPasswordAsync([Required(ErrorMessage = "唯一标识不能为空")] Guid id)
        {
            string result = await _userService.ResetPasswordAsync(id);
            return ResultModel<string>.Success(result, "重置成功");
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> ChangePasswordAsync(ChangePasswordRequestModel requestModel)
        {
            var model = _mapper.Map<ChangePasswordModel>(requestModel);
            model.ID = GetLoginUserID();
            await _userService.ChangePasswordAsync(model);
            return ResultModel.Success("修改成功");
        }
    }
}
