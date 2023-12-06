using System.ComponentModel;

namespace Materal.Gateway.Service.Models.Sync
{
    /// <summary>
    /// 协议类型
    /// </summary>
    public enum SchemeTypeEnum
    {
        /// <summary>
        /// Http
        /// </summary>
        [Description("Http")]
        Http = 0,
        /// <summary>
        /// Https
        /// </summary>
        [Description("Https")]
        Https = 1,
        /// <summary>
        /// WS
        /// </summary>
        [Description("WS")]
        WS = 2,
        /// <summary>
        /// WSS
        /// </summary>
        [Description("WSS")]
        WSS = 3,
        /// <summary>
        /// Grpc
        /// </summary>
        [Description("Grpc")]
        Grpc = 4,
        /// <summary>
        /// Grpcs
        /// </summary>
        [Description("Grpcs")]
        Grpcs = 5
    }
}
