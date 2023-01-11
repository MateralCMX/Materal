using Materal.BaseCore.Common;
using Materal.ConvertHelper;
using Materal.TTA.SqliteRepository.Model;

namespace RC.Demo.Common
{
    public class ApplicationConfig
    {
        /// <summary>
        /// 数据库配置
        /// </summary>
        public static SqliteConfigModel DBConfig
        {
            get
            {
                SqliteConfigModel result = MateralCoreConfig.GetValueObject<SqliteConfigModel>(nameof(DBConfig));
                if (result.Source.StartsWith("./"))
                {
                    result.Source = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, result.Source[2..]);
                }
                return result;
            }
        }
        /// <summary>
        /// 默认密码
        /// </summary>
        public static string DefaultPassword => MateralCoreConfig.GetValue("DefaultPassword");
        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string EncodePassword(string inputString)
        {
            return $"Materal{inputString}Materal".ToMd5_32Encode();
        }
    }
}
