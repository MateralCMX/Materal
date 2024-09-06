using Materal.Extensions;
using Materal.Extensions.DependencyInjection;
using Materal.MergeBlock.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.MergeBlock.Authorization.Abstractions
{
    /// <summary>
    /// 控制器基类扩展
    /// </summary>
    public static class ControllerBaseExtension
    {
        /// <summary>
        /// 获得登录用户唯一标识
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static Guid GetLoginUserID(this ControllerBase controller)
        {
            if (controller.User.Claims == null) throw new MergeBlockException("未鉴权");
            var tokenService = MateralServices.ServiceProvider.GetRequiredService<ITokenService>();
            var claim = controller.User.Claims.FirstOrDefault(m => m.Type == tokenService.UserIDKey);
            if (claim == null || !claim.Value.IsGuid()) throw new MergeBlockException("非用户授权");
            Guid userID = Guid.Parse(claim.Value);
            return userID;
        }
        /// <summary>
        /// 获得服务名称
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static string GetLoginServerName(this ControllerBase controller)
        {
            if (controller.User.Claims == null) throw new MergeBlockException("未鉴权");
            var tokenService = MateralServices.ServiceProvider.GetRequiredService<ITokenService>();
            var claim = controller.User.Claims.FirstOrDefault(m => m.Type == tokenService.ServerNameKey);
            if (claim == null || string.IsNullOrWhiteSpace(claim.Value)) throw new MergeBlockException("非服务授权");
            return claim.Value;
        }
        /// <summary>
        /// 是用户登录
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static bool IsUserLogin(this ControllerBase controller)
        {
            if (controller.User.Claims == null) return false;
            var tokenService = MateralServices.ServiceProvider.GetRequiredService<ITokenService>();
            var claim = controller.User.Claims.FirstOrDefault(m => m.Type == tokenService.UserIDKey);
            if (claim == null || !claim.Value.IsGuid()) return false;
            return true;
        }
        /// <summary>
        /// 是服务登录
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static bool IsServerLogin(this ControllerBase controller)
        {
            if (controller.User.Claims == null) return false;
            var tokenService = MateralServices.ServiceProvider.GetRequiredService<ITokenService>();
            var claim = controller.User.Claims.FirstOrDefault(m => m.Type == tokenService.ServerNameKey);
            if (claim == null || string.IsNullOrWhiteSpace(claim.Value)) return false;
            return true;
        }
    }
}
