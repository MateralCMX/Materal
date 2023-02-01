using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Materal.ConDep.Center.Common;
using Materal.ConDep.Center.Controllers.Models;
using Materal.ConDep.Center.DataTransmitModel.User;
using Materal.ConDep.Center.PresentationModel.User;
using Materal.ConDep.Center.Services;
using Materal.ConDep.Center.Services.Models.User;
using Materal.DotNetty.ControllerBus.Attributes;
using Materal.Model;
using Microsoft.IdentityModel.Tokens;

namespace Materal.ConDep.Center.Controllers
{
    public class UserController : ConDepBaseController
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public UserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="requestModel">请求模型</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> AddUser(AddUserRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<AddUserModel>(requestModel);
                await _userService.AddUserAsync(model);
                return ResultModel.Success("添加成功");
            }
            catch (MateralConDepException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="requestModel">请求模型</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> EditUser(EditUserRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<EditUserModel>(requestModel);
                await _userService.EditUserAsync(model);
                return ResultModel.Success("修改成功");
            }
            catch (MateralConDepException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel> DeleteUser(Guid id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return ResultModel.Success("删除成功");
            }
            catch (MateralConDepException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<UserDTO>> GetUserInfo(Guid id)
        {
            try
            {
                UserDTO userInfo = await _userService.GetUserInfoAsync(id);
                return ResultModel<UserDTO>.Success(userInfo, "获取成功");
            }
            catch (MateralConDepException ex)
            {
                return ResultModel<UserDTO>.Fail(ex.Message);
            }
        }

        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<UserDTO>> GetLoginUserInfo()
        {
            try
            {
                Guid id = GetLoginUserID();
                UserDTO userInfo = await _userService.GetUserInfoAsync(id);
                return ResultModel<UserDTO>.Success(userInfo, "获取成功");
            }
            catch (MateralConDepException ex)
            {
                return ResultModel<UserDTO>.Fail(ex.Message);
            }
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="requestModel">请求模型</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResultModel<UserListDTO>> GetUserList(QueryUserFilterRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<QueryUserFilterModel>(requestModel);
                (List<UserListDTO> userInfos, PageModel pageModel) = await _userService.GetUserListAsync(model);
                return PageResultModel<UserListDTO>.Success(userInfos, pageModel, "获取成功");
            }
            catch (MateralConDepException ex)
            {
                return PageResultModel<UserListDTO>.Fail(ex.Message);
            }
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel> ResetPassword(Guid id)
        {
            try
            {
                string password = await _userService.ResetPasswordAsync(id);
                return ResultModel.Success($"密码已重置为:{password}");
            }
            catch (MateralConDepException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="requestModel">请求模型</param>
        /// <returns></returns>
        [HttpPost, AllowAuthority]
        public async Task<ResultModel<TokenResultModel>> Login(LoginRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<LoginModel>(requestModel);
                UserDTO userInfo = await _userService.LoginAsync(model);
                string token = GetToken(userInfo);
                return ResultModel<TokenResultModel>.Success(new TokenResultModel
                {
                    AccessToken = token,
                    ExpiresSecond = ApplicationConfig.JWTConfig.ExpiredTime
                }, "登录成功");
            }
            catch (MateralConDepException)
            {
                return ResultModel<TokenResultModel>.Fail("账号或密码错误");
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="requestModel">请求模型</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> ChangePassword(ChangePasswordRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<ChangePasswordModel>(requestModel);
                model.ID = GetLoginUserID();
                await _userService.ChangePasswordAsync(model);
                return ResultModel.Success("修改成功");
            }
            catch (MateralConDepException ex)
            {
                return ResultModel.Fail(ex.Message);
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
