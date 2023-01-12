﻿using AspectCore.DynamicProxy;
using Materal.BaseCore.Common;
using Materal.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RC.Core.Common;
using RC.Demo.DataTransmitModel.User;
using RC.Demo.PresentationModel.User;
using RC.Demo.Services.Models.User;
using System.ComponentModel.DataAnnotations;

namespace RC.Demo.WebAPI.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    public partial class UserController
    {
        /// <summary>
        /// 获得登录用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<UserDTO>> GetLoginUserInfoAsync()
        {
            Guid id = GetLoginUserID();
            UserDTO result = await DefaultService.GetInfoAsync(id);
            return ResultModel<UserDTO>.Success(result, "查询成功");
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public async Task<ResultModel<LoginResultDTO>> LoginAsync(LoginRequestModel requestModel)
        {
            try
            {
                var model = Mapper.Map<LoginModel>(requestModel);
                UserDTO userInfo = await DefaultService.LoginAsync(model);
                string token = MateralCoreConfig.JWTConfig.GetToken(userInfo.ID);
                var result = new LoginResultDTO
                {
                    Token = token,
                    ExpiredTime = MateralCoreConfig.JWTConfig.ExpiredTime
                };
                return ResultModel<LoginResultDTO>.Success(result, "登录成功");
            }
            catch (AspectInvocationException exception)
            {
                if (exception.InnerException is RCException)
                {
                    return ResultModel<LoginResultDTO>.Fail("账号或者密码错误");
                }
                throw;
            }
            catch (RCException)
            {
                return ResultModel<LoginResultDTO>.Fail("账号或者密码错误");
            }
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel<string>> ResetPasswordAsync([Required(ErrorMessage = "唯一标识不能为空")] Guid id)
        {
            string result = await DefaultService.ResetPasswordAsync(id);
            return ResultModel<string>.Success(result, "重置成功");
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel> ChangePasswordAsync(ChangePasswordRequestModel requestModel)
        {
            var model = Mapper.Map<ChangePasswordModel>(requestModel);
            model.ID = GetLoginUserID();
            await DefaultService.ChangePasswordAsync(model);
            return ResultModel.Success("修改成功");
        }
        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel<List<UserDTO>>> TestAsync(ChangePasswordRequestModel requestModel)
        {
            throw new NotImplementedException();
        }
    }
}
