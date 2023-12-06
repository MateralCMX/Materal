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
        Http,
        /// <summary>
        /// Https
        /// </summary>
        [Description("Https")]
        Https,
        /// <summary>
        /// WS
        /// </summary>
        [Description("WS")]
        WS,
        /// <summary>
        /// WSS
        /// </summary>
        [Description("WSS")]
        WSS,
        /// <summary>
        /// Grpc
        /// </summary>
        [Description("Grpc")]
        Grpc,
        /// <summary>
        /// Grpcs
        /// </summary>
        [Description("Grpcs")]
        Grpcs
    }
}
