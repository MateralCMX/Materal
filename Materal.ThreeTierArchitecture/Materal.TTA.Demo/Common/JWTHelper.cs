using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
// ReSharper disable All

namespace Common
{
#pragma warning disable IDE1006 // 命名样式
    /// <summary>
    /// JWT帮助类
    /// </summary>
    public static class JWTHelper
    {
        /// <summary>
        /// 解密JWT
        /// </summary>
        /// <param name="token"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IDictionary<string, object> DecodeJWT(string token, string key)
        {
            var webClient = new WebClient();
            string endpoint = ApplicationConfig.IdentityServer.DocumentUrl;
            string json = webClient.DownloadString(endpoint);
            var metadata = JsonConvert.DeserializeObject<JObject>(json);
            string hawksUri = metadata["jwks_uri"].ToString();
            json = webClient.DownloadString(hawksUri);
            var keys = JsonConvert.DeserializeObject<JWKModel>(json);
            var tokenParts = token.Split('.');
            var bytes = FromBase64Url(tokenParts[0]);
            string head = Encoding.UTF8.GetString(bytes);
            string kid = JsonConvert.DeserializeObject<JObject>(head)["kid"].ToString();
            var jwkItem = keys.keys.FirstOrDefault(t => t.kid == kid);
            if (jwkItem == null)
            {
                throw new InvalidOperationException("未找到匹配的kid");
            }
            return DecodeRs256(token, key, jwkItem.e, jwkItem.n);
        }
        /// <summary>
        /// 解密RS256
        /// </summary>
        /// <param name="token"></param>
        /// <param name="secret"></param>
        /// <param name="exponent"></param>
        /// <param name="modulus"></param>
        /// <returns></returns>
        private static IDictionary<string, object> DecodeRs256(string token, string secret, string exponent, string modulus)
        {
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                var rS256Algorithm = new RSAlgorithmFactory(() =>
                {
                    var rsa = new RSACryptoServiceProvider();
                    rsa.ImportParameters(
                      new RSAParameters()
                      {
                          Modulus = FromBase64Url(modulus),
                          Exponent = FromBase64Url(exponent)
                      });
                    byte[] rsaBytes = rsa.ExportCspBlob(true);
                    var cert = new X509Certificate2(rsaBytes);
                    return cert;
                });
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, rS256Algorithm);
                var json = decoder.DecodeToObject(token, secret, verify: false);
                return json;
            }
            catch (TokenExpiredException ex)
            {
                throw new InvalidOperationException("token已过期", ex);
            }
            catch (SignatureVerificationException ex)
            {
                throw new InvalidOperationException("token验证失败", ex);
            }
        }
        /// <summary>
        /// 解密Base64
        /// </summary>
        /// <param name="base64Url"></param>
        /// <returns></returns>
        private static byte[] FromBase64Url(string base64Url)
        {
            string padded = base64Url.Length % 4 == 0 ? base64Url : base64Url + "====".Substring(base64Url.Length % 4);
            string base64 = padded.Replace("_", "/").Replace("-", "+");
            return Convert.FromBase64String(base64);
        }
    }
    /// <summary>
    /// JTW模型
    /// </summary>
    public class JWKModel
    {
        /// <summary>
        /// 键组
        /// </summary>
        public JWKItemModel[] keys { get; set; }
    }
    /// <summary>
    /// JTW子模型
    /// </summary>
    public class JWKItemModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string kty { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string use { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string kid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string e { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string n { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string alg { get; set; }
    }
#pragma warning restore IDE1006 // 命名样式
}
