namespace Materal.StringHelper
{
    /// <summary>
    /// 连接和端口号模型
    /// </summary>
    public class UrlAndPortModel
    {
        /// <summary>
        /// 连接地址
        /// </summary>
        public string Url { get; } = string.Empty;
        /// <summary>
        /// 端口号
        /// </summary>
        public int? Port { get; }
        public UrlAndPortModel(string url)
        {
            if (!url.IsUrl()) return;
            string[] address = url.Split(':');
            switch (address.Length)
            {
                case 2:
                    Url = $"{address[0]}:{address[1]}";
                    Port = null;
                    break;
                case 3 when address[2].IsInteger():
                    Url = $"{address[0]}:{address[1]}";
                    Port = Convert.ToInt32(address[2]);
                    break;
            }
        }
    }
}
