using AspectCore.DynamicProxy;
using Materal.ConfigCenter.ControllerCore;
using Materal.ConfigCenter.ProtalServer.DataTransmitModel.User;
using Materal.ConfigCenter.ProtalServer.PresentationModel.User;
using Materal.ConfigCenter.ProtalServer.Services;
using Materal.DotNetty.ControllerBus.Attributes;
using Materal.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Materal.ConfigCenter.ProtalServer.Common;
using Materal.ConfigCenter.ProtalServer.Controllers.Models;
using Microsoft.IdentityModel.Tokens;

namespace Materal.ConfigCenter.ProtalServer.Controllers
{
    public class UserController : ConfigCenterBaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> AddUser(AddUserModel model)
        {
            try
            {
                await _userService.AddUserAsync(model);
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
        /// 修改用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> EditUser(EditUserModel model)
        {
            try
            {
                await _userService.EditUserAsync(model);
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
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel> DeleteUser(Guid id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
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
        /// 获得用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<UserDTO>> GetUserInfo(Guid id)
        {
            try
            {
                UserDTO result = await _userService.GetUserInfoAsync(id);
                return ResultModel<UserDTO>.Success(result, "查询成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel<UserDTO>.Fail(ex.InnerException?.Message);
            }
            catch (MateralConfigCenterException ex)
            {
                return ResultModel<UserDTO>.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 获得登录用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<UserDTO>> GetLoginUserInfo()
        {
            try
            {
                UserDTO result = await _userService.GetUserInfoAsync(GetLoginUserID());
                return ResultModel<UserDTO>.Success(result, "查询成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel<UserDTO>.Fail(ex.InnerException?.Message);
            }
            catch (MateralConfigCenterException ex)
            {
                return ResultModel<UserDTO>.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 获得用户列表
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResultModel<UserListDTO>> GetUserList(QueryUserFilterModel filterModel)
        {
            try
            {
                (List<UserListDTO> result, PageModel pageModel) = await _userService.GetUserListAsync(filterModel);
                return PageResultModel<UserListDTO>.Success(result, pageModel, "查询成功");
            }
            catch (AspectInvocationException ex)
            {
                return PageResultModel<UserListDTO>.Fail(ex.InnerException?.Message);
            }
            catch (MateralConfigCenterException ex)
            {
                return PageResultModel<UserListDTO>.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, AllowAuthority]
        public async Task<ResultModel<TokenResultModel>> Login(LoginModel model)
        {
            try
            {
                UserDTO user = await _userService.LoginAsync(model);
                string token = GetToken(user);
                var result = new TokenResultModel
                {
                    ExpiresSecond = ApplicationConfig.JWTConfig.ExpiredTime,
                    AccessToken = token
                };
                return ResultModel<TokenResultModel>.Success(result, "查询成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel<TokenResultModel>.Fail(ex.InnerException?.Message);
            }
            catch (MateralConfigCenterException ex)
            {
                return ResultModel<TokenResultModel>.Fail(ex.Message);
            }
        }
        #region 私有方法
        /// <summary>
        /// 获得Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string GetToken(UserListDTO user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(ApplicationConfig.JWTConfig.Key);
            DateTime authTime = DateTime.UtcNow;
            DateTime expiresAt = authTime.AddSeconds(ApplicationConfig.JWTConfig.ExpiredTime);
            var securityKey = new SymmetricSecurityKey(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Aud,ApplicationConfig.JWTConfig.Audience),
                    new Claim(JwtRegisteredClaimNames.Iss,ApplicationConfig.JWTConfig.Issuer),
                    new Claim("UserID",user.ID.ToString())
                }),
                Audience = ApplicationConfig.JWTConfig.Audience,
                Issuer = ApplicationConfig.JWTConfig.Issuer,
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
        #endregion
    }
}
