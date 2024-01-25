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
    }
}
