using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Materal.Gateway.Common.ConfigModel
{
    /// <summary>
    /// JWT配置模型
    /// </summary>
    public class JWTConfigModel
    {
        /// <summary>
        /// 用户唯一标识键
        /// </summary>
        public const string UserIDKey = "UserID";
        /// <summary>
        /// 服务名称键
        /// </summary>
        public const string ServerNameKey = "ServerName";
        /// <summary>
        /// 密钥
        /// </summary>
        public string Key { get; set; } = "CMXMateral";
        /// <summary>
        /// 有效期
        /// </summary>
        public uint ExpiredTime { get; set; } = 7200;
        /// <summary>
        /// 发布者
        /// </summary>
        public string Issuer { get; set; } = "MateralCore.Server";
        /// <summary>
        /// 接收者
        /// </summary>
        public string Audience { get; set; } = "MateralCore.WebAPI";
        /// <summary>
        /// 二进制密钥
        /// </summary>
        public byte[] KeyBytes => Encoding.UTF8.GetBytes(Key.ToMd5_32Encode(true));
        /// <summary>
        /// 获得Token
        /// </summary>
        /// <returns></returns>
        public string GetToken(params Claim[] claims)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            DateTime authTime = DateTime.UtcNow;
            DateTime expiresAt = authTime.AddSeconds(ExpiredTime);
            SymmetricSecurityKey securityKey = new(KeyBytes);
            List<Claim> allClaims =
            [
                new Claim(JwtRegisteredClaimNames.Aud,Audience),
                new Claim(JwtRegisteredClaimNames.Iss,Issuer),
                .. claims,
            ];
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(allClaims),
                Audience = Audience,
                Issuer = Issuer,
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
