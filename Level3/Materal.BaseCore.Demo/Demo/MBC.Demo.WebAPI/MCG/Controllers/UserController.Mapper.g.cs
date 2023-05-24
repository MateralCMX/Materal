#nullable enable
using Materal.BaseCore.PresentationModel;
using Materal.BaseCore.Services.Models;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Materal.BaseCore.CodeGenerator;
using MBC.Demo.DataTransmitModel.User;
using MBC.Demo.Services.Models.User;

namespace MBC.Demo.WebAPI.Controllers
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
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> TestChangePasswordAsync(ChangePasswordRequestModel requestModel)
        {
            var model0 = Mapper.Map<ChangePasswordModel>(requestModel);
            await DefaultService.TestChangePasswordAsync(model0);
            return ResultModel.Success();
        }
    }
}
