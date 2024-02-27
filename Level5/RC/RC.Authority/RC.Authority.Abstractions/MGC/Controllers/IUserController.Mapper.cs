using Materal.MergeBlock.GeneratorCode.Attributers;
using RC.Authority.Abstractions.DTO.User;
using RC.Authority.Abstractions.RequestModel.User;

namespace RC.Authority.Abstractions.Controllers
{
    /// <summary>
    /// 用户服务控制器
    /// </summary>
    public partial interface IUserController
    {
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        Task<ResultModel<string>> ResetPasswordAsync(Guid id);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        Task<ResultModel> ChangePasswordAsync(ChangePasswordRequestModel requestModel);
    }
}
