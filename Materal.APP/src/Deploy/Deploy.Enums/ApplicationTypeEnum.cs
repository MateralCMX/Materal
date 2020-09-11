using System.ComponentModel;

namespace Deploy.Enums
{
    public enum ApplicationTypeEnum : byte
    {
        /// <summary>
        /// .NetCore 3.x
        /// </summary>
        [Description(".NetCore 3.x")]
        DotNetCore3 = 0,
        /// <summary>
        /// .NetCore 2.x
        /// </summary>
        [Description(".NetCore 2.x")]
        DotNetCore2 = 1,
        /// <summary>
        /// PM2
        /// </summary>
        [Description("PM2")]
        PM2 = 2,
        /// <summary>
        /// 静态文档
        /// </summary>
        [Description("静态文档")]
        StaticDocument = byte.MaxValue,
    }
}
