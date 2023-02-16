using Materal.Gateway.Common;
using Materal.Gateway.Common.ConfigModels;
using Materal.StringHelper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Materal.Gateway.Controllers
{
    /// <summary>
    /// WebAPI控制器基类
    /// </summary>
    [Route("api/[controller]/[action]"), ApiController]
    public abstract class GatewayWebAPIControllerBase : ControllerBase
    {
        /// <summary>
        /// 获得登录用户唯一标识
        /// </summary>
        /// <returns></returns>
        protected Guid GetLoginUserID()
        {
            if (User.Claims == null) throw new MateralGatewayException("未鉴权");
            Claim? claim = User.Claims.FirstOrDefault(m => m.Type == JWTConfigModel.UserIDKey);
            if (claim == null || !claim.Value.IsGuid()) throw new MateralGatewayException("非用户授权");
            Guid userID = Guid.Parse(claim.Value);
            return userID;
        }
        /// <summary>
        /// 获得服务名称
        /// </summary>
        /// <returns></returns>
        protected string GetLoginServerName()
        {
            if (User.Claims == null) throw new MateralGatewayException("未鉴权");
            Claim? claim = User.Claims.FirstOrDefault(m => m.Type == JWTConfigModel.ServerNameKey);
            if (claim == null || string.IsNullOrWhiteSpace(claim.Value)) throw new MateralGatewayException("非服务授权");
            return claim.Value;
        }
        /// <summary>
        /// 是用户登录
        /// </summary>
        /// <returns></returns>
        protected bool IsUserLogin()
        {
            if (User.Claims == null) return false;
            Claim? claim = User.Claims.FirstOrDefault(m => m.Type == JWTConfigModel.UserIDKey);
            if (claim == null || !claim.Value.IsGuid()) return false;
            return true;
        }
        /// <summary>
        /// 是服务登录
        /// </summary>
        /// <returns></returns>
        protected bool IsServerLogin()
        {
            if (User.Claims == null) return false;
            Claim? claim = User.Claims.FirstOrDefault(m => m.Type == JWTConfigModel.ServerNameKey);
            if (claim == null || string.IsNullOrWhiteSpace(claim.Value)) return false;
            return true;
        }
    }
}