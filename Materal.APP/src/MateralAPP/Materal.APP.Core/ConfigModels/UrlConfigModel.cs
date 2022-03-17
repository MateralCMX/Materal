using Materal.ConvertHelper;
using Materal.StringHelper;

namespace Materal.APP.Core.ConfigModels
{
    public abstract class UrlConfigModel
    {
        /// <summary>
        /// 服务路径
        /// </summary>
        public abstract string Url { get; }
        /// <summary>
        /// 主机
        /// </summary>
        public string Host => GetHost();
        /// <summary>
        /// 端口
        /// </summary>
        public int Port => GetPort();
        /// <summary>
        /// SSL
        /// </summary>
        public bool IsSSL => Url.Contains("https://");
        /// <summary>
        /// 获得主机号
        /// </summary>
        /// <returns></returns>
        private string GetHost()
        {
            string url = IsSSL ? Url.Substring("https://".Length) : Url.Substring("http://".Length);
            string[] temp = url.Split(':');
            return temp.Length != 2 ? "localhost" : temp[0];
        }
        /// <summary>
        /// 端口号
        /// </summary>
        /// <returns></returns>
        private int GetPort()
        {
            string url = IsSSL ? Url.Substring("https://".Length) : Url.Substring("http://".Length);
            string[] temp = url.Split(':');
            if (temp.Length == 2 && temp[1].IsNumberPositive())
            {
                return temp[1].ConvertTo<int>();
            }
            return 80;
        }
    }
}
