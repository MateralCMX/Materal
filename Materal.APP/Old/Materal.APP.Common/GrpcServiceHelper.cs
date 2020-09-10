using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using Grpc.Core;
using Materal.StringHelper;

namespace Materal.APP.Common
{
    public static class GrpcServiceHelper
    {
        /// <summary>
        /// JWT配置
        /// </summary>
        public static JWTConfigModel JWTConfig{ get; set; }
        /// <summary>
        /// 处理
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static async Task<TResult> HandlerAsync<TResult>(Func<Task<TResult>> func, string errorMessage = null) where TResult : class, new()
        {
            try
            {
                return await func();
            }
            catch (MateralAPPException exception)
            {
                return GetResultModel<TResult>(errorMessage ?? exception.Message, 1);
            }
            catch (AspectInvocationException exception)
            {
                if (exception.InnerException is MateralAPPException)
                {
                    return GetResultModel<TResult>(errorMessage ?? exception.InnerException.Message, 1);
                }
                throw;
            }
        }
        /// <summary>
        /// 获得返回模型
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="message"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static TResult GetResultModel<TResult>(string message, int status = 0) where TResult : class, new()
        {
            Type tType = typeof(TResult);
            var result = new TResult();
            PropertyInfo statusPropertyInfo = tType.GetProperty("Status");
            statusPropertyInfo?.SetValue(result, status);
            PropertyInfo messagePropertyInfo = tType.GetProperty("Message");
            messagePropertyInfo?.SetValue(result, message);
            return result;
        }

        /// <summary>
        /// 获得Token
        /// </summary>
        /// <returns></returns>
        public static string GetToken(ServerCallContext context)
        {
            Metadata.Entry authorization = context.RequestHeaders.FirstOrDefault(m => m.Key.Equals("Authorization", StringComparison.OrdinalIgnoreCase));
            if (authorization == null) throw new MateralAPPException("未识别Token");
            string[] tokens = authorization.Value.Split(' ');
            if (tokens.Length != 2 || tokens[0] != "Bearer") throw new MateralAPPException("未识别Token");
            return tokens[1];
        }
        /// <summary>
        /// 获得JWTToken对象
        /// </summary>
        /// <returns></returns>
        public static JwtSecurityToken GetJwtSecurityToken(ServerCallContext context)
        {
            string token = GetToken(context);
            var jwtSecurityToken = new JwtSecurityToken(token);
            if (!jwtSecurityToken.Audiences.Contains(JWTConfig.Audience)) throw new MateralAPPException("未识别Token");
            if (!jwtSecurityToken.Issuer.Equals(JWTConfig.Issuer)) throw new MateralAPPException("未识别Token");
            if (jwtSecurityToken.ValidTo < DateTime.UtcNow) throw new MateralAPPException("未识别Token");
            return jwtSecurityToken;
        }
        /// <summary>
        /// 获取登录用户唯一标识
        /// </summary>
        /// <returns></returns>
        public static Guid GetLoginUserID(ServerCallContext context)
        {
            try
            {
                JwtSecurityToken jwtSecurityToken = GetJwtSecurityToken(context);
                if (!jwtSecurityToken.Payload.TryGetValue("UserID", out object value)) throw new MateralAPPException("未识别Token");
                if (value == null) throw new MateralAPPException("未识别Token");
                if (!(value is string valueStr) || !valueStr.IsGuid()) throw new MateralAPPException("未识别Token");
                Guid loginUserID = Guid.Parse(valueStr);
                return loginUserID;
            }
            catch (MateralAPPException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MateralAPPException("未识别Token", ex);
            }
        }
    }
}
