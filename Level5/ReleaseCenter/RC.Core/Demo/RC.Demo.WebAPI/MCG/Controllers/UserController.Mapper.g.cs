#nullable enable
using Materal.BaseCore.PresentationModel;
using Materal.BaseCore.Services.Models;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Materal.BaseCore.CodeGenerator;
using RC.Demo.DataTransmitModel.User;
using RC.Demo.Services.Models.User;
using RC.Demo.PresentationModel.User;

namespace RC.Demo.WebAPI.Controllers
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
