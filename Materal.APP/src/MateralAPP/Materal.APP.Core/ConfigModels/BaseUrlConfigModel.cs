namespace Materal.APP.Core.ConfigModels
{
    public class BaseUrlConfigModel : UrlConfigModel
    {
        /// <summary>
        /// 服务路径
        /// </summary>
        public override string Url
        {
            get
            {
                string result = ApplicationConfig.Config.GetSection("URLS").Value;
                if (string.IsNullOrWhiteSpace(result))
                {
                    result = "http://localhost";
                }
                return result;
            }
        }
    }
}
