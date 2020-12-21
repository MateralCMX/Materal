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
        /// 重连间隔(毫秒)
        /// </summary>
        public int ReconnectionInterval { get; set; } = 100000;
        /// <summary>
        /// 重连次数
        /// </summary>
        public int ReconnectionNumber { get; set; } = 20;
        /// <summary>
        /// 编码格式
        /// </summary>
        public Encoding EncodingType { get; set; } = Encoding.UTF8;
    }
}
