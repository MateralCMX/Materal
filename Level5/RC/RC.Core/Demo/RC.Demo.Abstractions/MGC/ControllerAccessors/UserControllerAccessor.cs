using RC.Demo.Abstractions.DTO.User;
using RC.Demo.Abstractions.RequestModel.User;

namespace RC.Demo.Abstractions.HttpClient
{
    /// <summary>
    /// 用户控制器访问器
    /// </summary>
    public partial class UserControllerAccessor(IServiceProvider serviceProvider) : BaseControllerAccessor<IUserController, AddUserRequestModel, EditUserRequestModel, QueryUserRequestModel, UserDTO, UserListDTO>(serviceProvider), IUserController
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public override string ProjectName => "RC";
        /// <summary>
        /// 模块名称
        /// </summary>
        public override string ModuleName => "Demo";
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResultModel<string>> ResetPasswordAsync(Guid id)
            => await HttpHelper.SendAsync<IUserController, ResultModel<string>>(ProjectName, ModuleName, nameof(ResetPasswordAsync), new() {[nameof(id)] = id.ToString()});
        /// <summary>
        /// 获得登录用户信息
        /// </summary>
        /// <returns></returns>
        public async Task<ResultModel<UserDTO>> GetLoginUserInfoAsync()
            => await HttpHelper.SendAsync<IUserController, ResultModel<UserDTO>>(ProjectName, ModuleName, nameof(GetLoginUserInfoAsync), []);
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task<ResultModel<LoginResultDTO>> LoginAsync(LoginRequestModel requestModel)
            => await HttpHelper.SendAsync<IUserController, ResultModel<LoginResultDTO>>(ProjectName, ModuleName, nameof(LoginAsync), [], requestModel);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task<ResultModel> ChangePasswordAsync(ChangePasswordRequestModel requestModel)
            => await HttpHelper.SendAsync<IUserController, ResultModel>(ProjectName, ModuleName, nameof(ChangePasswordAsync), [], requestModel);
    }
}
