using System;
using System.Collections.Generic;
using System.Text;

namespace Materal.Utils.Wechat.Model
{
    public class NetworkDetectionDNSModel
    {
        /// <summary>
        /// IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 服务商
        /// </summary>
        public NetworkDetectionOperatorEnum RealOperator { get; set; }
    }
}
