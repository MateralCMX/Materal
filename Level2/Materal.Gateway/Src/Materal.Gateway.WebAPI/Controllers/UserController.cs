using Materal.Gateway.Common;
using Materal.Gateway.WebAPI.ConfigModel;
using Materal.Gateway.WebAPI.DataTransmitModel;
using Materal.Gateway.WebAPI.PresentationModel;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;

namespace Materal.Gateway.WebAPI.Controllers
{
    /// <summary>
    /// �û�������
    /// </summary>
    public class UserController : GatewayControllerBase
    {
        /// <summary>
        /// ��¼
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public ResultModel<LoginResultDTO> Login(LoginRequestModel requestModel)
        {
            try
            {
                UserConfigModel user = WebAPIConfig.Users.FirstOrDefault(m => m.Account == requestModel.Account && m.Password == requestModel.Password) ?? throw new GatewayException("�û������������");
                string token = WebAPIConfig.JWTConfig.GetToken(Guid.NewGuid());
                var result = new LoginResultDTO
                {
                    Token = token,
                    ExpiredTime = WebAPIConfig.JWTConfig.ExpiredTime
                };
                return ResultModel<LoginResultDTO>.Success(result, "��¼�ɹ�");
            }
            catch (GatewayException)
            {
                return ResultModel<LoginResultDTO>.Fail("�û������������");
            }
        }
    }
}
