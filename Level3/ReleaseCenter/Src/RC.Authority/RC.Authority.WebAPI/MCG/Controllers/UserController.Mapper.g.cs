using Materal.Utils.Model;
using RC.Authority.DataTransmitModel.User;
using RC.Authority.PresentationModel.User;
using RC.Authority.Services.Models.User;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RC.Authority.WebAPI.Controllers
{
    public partial class UserController
    {
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel<string>> ResetPasswordAsync([Required(ErrorMessage = "id不能为空")] Guid id)
        {
            var result = await DefaultService.ResetPasswordAsync(id);
            return ResultModel<string>.Success(result);
        }
    }
}
