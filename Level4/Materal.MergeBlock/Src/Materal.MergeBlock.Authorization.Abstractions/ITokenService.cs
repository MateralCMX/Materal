using System.Security.Claims;

namespace Materal.MergeBlock.Authorization.Abstractions
{
    /// <summary>
    /// Token服务
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// 用户唯一标识键
        /// </summary>
        string UserIDKey { get; }
        /// <summary>
        /// 服务名称键
        /// </summary>
        string ServerNameKey { get; }
        /// <summary>
        /// 获得Token
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        string GetToken(params Claim[] claims);
        /// <summary>
        /// 获得Token
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        string GetToken(Guid userID);
        /// <summary>
        /// 获得Token
        /// </summary>
        /// <param name="serverName"></param>
        /// <returns></returns>
        string GetToken(string serverName);
    }
}
