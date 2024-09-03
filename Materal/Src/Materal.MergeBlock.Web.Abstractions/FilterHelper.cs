using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text;

namespace Materal.MergeBlock.Web.Abstractions
{
    /// <summary>
    /// 拦截器帮助类
    /// </summary>
    public static class FilterHelper
    {
        /// <summary>
        /// 获得IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIPAddress(ConnectionInfo connectionInfo)
        {
            if (connectionInfo.RemoteIpAddress == null) return string.Empty;
            string ipAddress = connectionInfo.RemoteIpAddress.ToString();
            if (ipAddress == "::1") ipAddress = "127.0.0.1";
            string port = $":{connectionInfo.RemotePort}";
            return $"{ipAddress}{port}";
        }
        /// <summary>
        /// 获得操作用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static Guid? GetOperatingUserID(ClaimsPrincipal user)
        {
            Guid? userID = null;
            Claim? userIDClaim = user.Claims.FirstOrDefault(m => m.Type == "UserID");
            if (userIDClaim != null && userIDClaim.Value.IsGuid())
            {
                userID = Guid.Parse(userIDClaim.Value);
            }
            return userID;
        }
        /// <summary>
        /// 获得请求内容
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<string> GetRequestContentAsync(HttpRequest request)
        {
            StringBuilder result = new();
            if (request.QueryString.HasValue)
            {
                result.AppendLine($"QueryParams:{request.QueryString.Value}");
            }
            if (!string.IsNullOrEmpty(request.ContentType) && request.ContentType.Contains("application/json"))
            {
                if (request.Body != null && request.Body.Length > 0)
                {
                    request.Body.Seek(0, SeekOrigin.Begin);
                    using StreamReader reader = new(request.Body, Encoding.UTF8);
                    result.AppendLine($"BodyParams:{await reader.ReadToEndAsync()}");
                }
            }
            return result.ToString();
        }
    }
}
