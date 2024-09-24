using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Materal.MergeBlock.Authorization.Abstractions
{
    /// <summary>
    /// Token服务
    /// </summary>
    /// <param name="authorizationConfig"></param>
    public abstract class TokenServiceBase(IOptionsMonitor<AuthorizationOptions> authorizationConfig) : ITokenService
    {
        /// <summary>
        /// 用户唯一标识键
        /// </summary>
        public string UserIDKey { get; } = "UserID";
        /// <summary>
        /// 服务名称键
        /// </summary>
        public string ServerNameKey { get; } = "ServerName";
        /// <summary>
        /// 获得Token
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public string GetToken(params Claim[] claims)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            DateTime authTime = DateTime.UtcNow;
            DateTime expiresAt = authTime.AddSeconds(authorizationConfig.CurrentValue.ExpiredTime);
            SymmetricSecurityKey securityKey = new(authorizationConfig.CurrentValue.KeyBytes);
            List<Claim> allClaims =
            [
                //new Claim(JwtRegisteredClaimNames.Aud, authorizationConfig.CurrentValue.Audience),
                new Claim(JwtRegisteredClaimNames.Iss, authorizationConfig.CurrentValue.Issuer),
                .. claims,
            ];
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(allClaims),
                Audience = authorizationConfig.CurrentValue.Audience,
                Issuer = authorizationConfig.CurrentValue.Issuer,
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
        /// <summary>
        /// 获得Token
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetToken(Guid userID) => GetToken(new Claim(UserIDKey, userID.ToString()));
        /// <summary>
        /// 获得Token
        /// </summary>
        /// <param name="serverName"></param>
        /// <returns></returns>
        public string GetToken(string serverName) => GetToken(new Claim(ServerNameKey, serverName));
    }
}
