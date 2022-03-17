namespace Materal.APP.Core.ConfigModels
{
    public class ExternalUrlConfigModel : UrlConfigModel
    {
        /// <summary>
        /// 服务路径
        /// </summary>
        public override string Url
        {
            get
            {
                var result = ApplicationConfig.Config.GetSection("ExternalUrl").Value;
                if (string.IsNullOrWhiteSpace(result)) return ApplicationConfig.BaseUrlConfig.Url;
                return result;
            }
        }
    }
}
