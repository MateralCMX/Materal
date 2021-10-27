using System.ComponentModel;

namespace Deploy.Enums
{
    public enum ApplicationTypeEnum : byte
    {
        /// <summary>
        /// 静态
        /// </summary>
        [Description("Static")]
        StaticDocument = 0,
        /// <summary>
        /// 可执行程序
        /// </summary>
        [Description("Exe")]
        Exe = 1,
        /// <summary>
        /// .NetCore
        /// </summary>
        [Description(".NetCore")]
        DotNetCore = 2,
        /// <summary>
        /// PM2
        /// </summary>
        [Description("PM2")]
        PM2 = 3
    }
}
