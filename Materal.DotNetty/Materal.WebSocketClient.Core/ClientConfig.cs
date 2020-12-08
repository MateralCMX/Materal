using System.Text;

namespace Materal.WebSocketClient.Core
{
    public class ClientConfig
    {
        /// <summary>
        /// 连接地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 编码格式
        /// </summary>
        public Encoding EncodingType { get; set; } = Encoding.UTF8;
    }
}
