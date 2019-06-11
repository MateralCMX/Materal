using System.Collections.Generic;

namespace Materal.WeChatHelper.Model
{
    public class NetworkDetectionResultModel
    {
        /// <summary>
        /// DNS
        /// </summary>
        public List<NetworkDetectionDNSModel> DNS { get; set; }
        /// <summary>
        /// Ping
        /// </summary>
        public List<NetworkDetectionPingModel> Ping { get; set; }
    }
}
