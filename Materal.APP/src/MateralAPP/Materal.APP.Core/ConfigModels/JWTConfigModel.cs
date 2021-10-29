using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Materal.APP.Core.ConfigModels
{
    public class JWTConfigModel
    {
        /// <summary>
        /// 配置键
        /// </summary>
        private const string ConfigKey = "JWT";
        /// <summary>
        /// 密钥
        /// </summary>
        public string Key => GetConfigValue(nameof(Key));
        /// <summary>
        /// 有效期
        /// </summary>
        public uint ExpiredTime => Convert.ToUInt32(GetConfigValue(nameof(ExpiredTime)));
        /// <summary>
        /// 发布者
        /// </summary>
        public string Issuer => GetConfigValue(nameof(Issuer));
        /// <summary>
        /// 接收者
        /// </summary>
        public string Audience => GetConfigValue(nameof(Audience));
        /// <summary>
        /// 二进制密钥
        /// </summary>
        public byte[] KeyBytes => Encoding.UTF8.GetBytes(Key);
        /// <summary>
        /// 获得配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetConfigValue(string key)
        {
            return ApplicationConfig.Config.GetSection($"{ConfigKey}:{key}").Value;
        }
        /// <summary>
        /// 获得Token
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetToken(string userID)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            DateTime authTime = DateTime.UtcNow;
            DateTime expiresAt = authTime.AddSeconds(ExpiredTime);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(KeyBytes);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Aud,Audience),
                    new Claim(JwtRegisteredClaimNames.Iss,Issuer),
                    new Claim("UserID",userID)
                }),
                Audience = Audience,
                Issuer = Issuer,
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
