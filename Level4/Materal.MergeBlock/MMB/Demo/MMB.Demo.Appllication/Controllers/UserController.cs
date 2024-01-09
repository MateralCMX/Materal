using Microsoft.Extensions.Options;
using MMB.Demo.Abstractions.DTO.User;
using MMB.Demo.Abstractions.RequestModel.User;
using MMB.Demo.Abstractions.Services.Models.User;
using System.ComponentModel.DataAnnotations;

namespace MMB.Demo.Appllication.Controllers
{
    /// <summary>
    /// 测试控制器
    /// </summary>
    public class UserController(ITokenService tokenService, IOptionsMonitor<AuthorizationConfig> authorizationConfig) : MergeBlockControllerBase<AddUserRequestModel, EditUserRequestModel, QueryUserRequestModel, AddUserModel, EditUserModel, QueryUserModel, UserDTO, UserListDTO, IUserService>, IUserController
    {
        /// <summary>
        /// 获得用户列表
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResultModel<UserListDTO>> GetUserListAsync(QueryUserRequestModel requestModel)
        {
            QueryUserModel model0 = Mapper.Map<QueryUserModel>(requestModel);
            (List<UserListDTO>? result, PageModel pageInfo) = await DefaultService.GetUserListAsync(model0);
            return PageResultModel<UserListDTO>.Success(result, pageInfo);
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel<string>> ResetPasswordAsync([Required(ErrorMessage = "id不能为空")] Guid id)
        {
            string result = await DefaultService.ResetPasswordAsync(id);
            return ResultModel<string>.Success(result);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> TestChangePasswordAsync(ChangePasswordRequestModel requestModel)
        {
            ChangePasswordModel model0 = Mapper.Map<ChangePasswordModel>(requestModel);
            await DefaultService.TestChangePasswordAsync(model0);
            return ResultModel.Success();
        }
        /// <summary>
        /// 获得登录用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<UserDTO>> GetLoginUserInfoAsync()
        {
            Guid id = this.GetLoginUserID();
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
                LoginModel model = Mapper.Map<LoginModel>(requestModel);
                UserDTO userInfo = await DefaultService.LoginAsync(model);
                string token = tokenService.GetToken(userInfo.ID);
                LoginResultDTO result = new()
                {
                    Token = token,
                    ExpiredTime = authorizationConfig.CurrentValue.ExpiredTime
                };
                return ResultModel<LoginResultDTO>.Success(result, "登录成功");
            }
            catch (MMBException)
            {
                return ResultModel<LoginResultDTO>.Fail("账号或者密码错误");
            }
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel> ChangePasswordAsync(ChangePasswordRequestModel requestModel)
        {
            ChangePasswordModel model = Mapper.Map<ChangePasswordModel>(requestModel);
            model.ID = this.GetLoginUserID();
            await DefaultService.ChangePasswordAsync(model);
            return ResultModel.Success("修改成功");
        }
        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public Task<ResultModel<List<UserDTO>>> TestAsync(ChangePasswordRequestModel requestModel)
        {
            throw new NotImplementedException();
        }
    }
}
