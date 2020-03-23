using System.Text;
using System.Threading.Tasks;
using Materal.ConfigCenter.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Materal.Example
{
    public class Program
    {
        public static async Task Main()
        {
            IMateralConfigurationBuilder configurationBuilder = new MateralConfigurationBuilder("http://116.55.251.31:8201/", "IntegratedPlatform")
                .AddDefaultNamespace();
            IConfiguration _configuration = await configurationBuilder.BuildMateralConfigAsync();
            var a = _configuration.GetValueObject<JWTConfigModel>("JWT");
        }
    }
    /// <summary>
    /// JWT配置模型
    /// </summary>
    public class JWTConfigModel
    {
        /// <summary>
        /// 密钥
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public uint ExpiredTime { get; set; }
        /// <summary>
        /// 发布者
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// 接收者
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 二进制Key
        /// </summary>
        public SymmetricSecurityKey SigningKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
    }
}
