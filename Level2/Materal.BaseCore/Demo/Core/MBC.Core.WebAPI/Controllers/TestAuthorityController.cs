#if DEBUG
using Materal.BaseCore.Common;
using Materal.BaseCore.WebAPI.Controllers;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MBC.Core.WebAPI.Controllers
{
    /// <summary>
    /// 测试用授权控制器
    /// </summary>
    [AllowAnonymous]
    public class TestAuthorityController : MateralCoreWebAPIControllerBase
    {
        /// <summary>
        /// 根据用户唯一标识获得一个Token
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<UserTokenModel> GetTokenByUserID(Guid? userID = null)
        {
            Guid targetID = userID ?? Guid.Parse("AD47F3EA-1095-454B-B6B2-445199A0A721");
            string token = MateralCoreConfig.JWTConfig.GetToken(targetID);
            UserTokenModel result = new()
            {
                ID = targetID,
                Token = $"Bearer {token}"
            };
            return ResultModel<UserTokenModel>.Success(result, "获取成功");
        }
    }
    /// <summary>
    /// 用户令牌模型
    /// </summary>
    public class UserTokenModel
    {
        /// <summary>
        /// 令牌
        /// </summary>
        public string Token { get; set; } = string.Empty;
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public Guid ID { get; set; }
    }
}
#endif