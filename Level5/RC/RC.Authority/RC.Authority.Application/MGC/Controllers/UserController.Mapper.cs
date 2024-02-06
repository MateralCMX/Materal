using RC.Authority.Abstractions.DTO.User;
using RC.Authority.Abstractions.RequestModel.User;
using RC.Authority.Abstractions.Services.Models.User;

namespace RC.Authority.Application.Controllers
{
    /// <summary>
    /// 用户服务控制器
    /// </summary>
    public partial class UserController
    {
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel<string>> ResetPasswordAsync(Guid id)
        {
            string result = await DefaultService.ResetPasswordAsync(id);
            return ResultModel<string>.Success(result, "重置密码成功");
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> ChangePasswordAsync(ChangePasswordRequestModel requestModel)
        {
            ChangePasswordModel model = Mapper.Map<ChangePasswordModel>(requestModel) ?? throw new RCException("映射失败");
            BindLoginUserID(model);
            await DefaultService.ChangePasswordAsync(model);
            return ResultModel.Success("修改密码成功");
        }
    }
}
