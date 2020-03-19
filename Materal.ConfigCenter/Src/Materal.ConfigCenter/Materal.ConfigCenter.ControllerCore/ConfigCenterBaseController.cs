using DotNetty.Codecs.Http;
using DotNetty.Common.Utilities;
using Materal.DotNetty.ControllerBus;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace Materal.ConfigCenter.ControllerCore
{
    public class ConfigCenterBaseController : BaseController
    {
        private Guid? loginUserID;
        /// <summary>
        /// 获得Token
        /// </summary>
        /// <returns></returns>
        public string GetToken()
        {
            try
            {
                ICharSequence authorization = Request.Headers.Get(HttpHeaderNames.Authorization, null);
                if (authorization == null) throw new MateralConfigCenterException("未识别Token");
                string token = authorization.ToString();
                string[] tokens = token.Split(' ');
                if (tokens.Length != 2 || tokens[0] != "Bearer") throw new MateralConfigCenterException("未识别Token");
                return tokens[1];
            }
            catch (Exception ex)
            {
                throw new MateralConfigCenterException("未识别Token", ex);
            }
        }
        /// <summary>
        /// 获取登录用户唯一标识
        /// </summary>
        /// <returns></returns>
        public Guid GetLoginUserID()
        {
            try
            {
                if (loginUserID != null) return loginUserID.Value;
                string token = GetToken();
                var jwtSecurityToken = new JwtSecurityToken(token);
                if (!jwtSecurityToken.Payload.TryGetValue("UserID", out object value)) throw new MateralConfigCenterException("未识别Token");
                loginUserID = Guid.Parse(value.ToString());
                return loginUserID.Value;
            }
            catch (Exception ex)
            {
                throw new MateralConfigCenterException("未识别Token", ex);
            }
        }
    }
}
