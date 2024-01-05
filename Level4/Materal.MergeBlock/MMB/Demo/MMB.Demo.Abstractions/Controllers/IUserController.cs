//using MMB.Demo.Abstractions.DTO.User;
//using MMB.Demo.Abstractions.Services.Models.User;

//namespace MMB.Demo.Abstractions.Controllers
//{
//    /// <summary>
//    /// 用户控制器
//    /// </summary>
//    public interface IUserController : IMergeBlockControllerBase<AddUserRequestModel, EditUserRequestModel, QueryUserRequestModel, AddUserModel, EditUserModel, QueryUserModel, UserDTO, UserListDTO, IUserService>
//    {
//        /// <summary>
//        /// 获得用户列表
//        /// </summary>
//        /// <param name="requestModel"></param>
//        /// <returns></returns>
//        [HttpPost]
//        Task<PageResultModel<UserListDTO>> GetUserListAsync(QueryUserRequestModel requestModel);
//        /// <summary>
//        /// 重置密码
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        [HttpPut]
//        Task<ResultModel<string>> ResetPasswordAsync([Required(ErrorMessage = "id不能为空")] Guid id);
//        /// <summary>
//        /// 修改密码
//        /// </summary>
//        /// <param name="requestModel"></param>
//        /// <returns></returns>
//        [HttpPost]
//        Task<ResultModel> TestChangePasswordAsync(ChangePasswordRequestModel requestModel);
//        /// <summary>
//        /// 获得登录用户信息
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet]
//        Task<ResultModel<UserDTO>> GetLoginUserInfoAsync();
//        /// <summary>
//        /// 登录
//        /// </summary>
//        /// <param name="requestModel"></param>
//        /// <returns></returns>
//        [HttpPost, AllowAnonymous]
//        Task<ResultModel<LoginResultDTO>> LoginAsync(LoginRequestModel requestModel);
//        /// <summary>
//        /// 修改密码
//        /// </summary>
//        /// <param name="requestModel"></param>
//        /// <returns></returns>
//        [HttpPut]
//        Task<ResultModel> ChangePasswordAsync(ChangePasswordRequestModel requestModel);
//        /// <summary>
//        /// 测试
//        /// </summary>
//        /// <param name="requestModel"></param>
//        /// <returns></returns>
//        [HttpPut]
//        Task<ResultModel<List<UserDTO>>> TestAsync(ChangePasswordRequestModel requestModel);
//    }
//}
