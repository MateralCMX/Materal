using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Materal.SystemHelper
{
    public class SystemInfoManager
    {
        /// <summary>
        /// 获得本机IPV4地址
        /// 可能有多个
        /// </summary>
        /// <returns>IPV4地址组</returns>
        public static List<string> GetLocalIPv4()
        {
            string name = Dns.GetHostName();
            IPAddress[] ipAddresses = Dns.GetHostAddresses(name);
            return (from ip in ipAddresses where ip.AddressFamily == AddressFamily.InterNetwork select ip.ToString()).ToList();
        }
    }
}
