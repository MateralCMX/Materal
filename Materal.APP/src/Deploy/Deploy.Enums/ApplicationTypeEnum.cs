﻿using System.ComponentModel;

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
        DotNet = 2,
        /// <summary>
        /// NodeJS
        /// </summary>
        [Description("NodeJS")]
        NodeJS = 3,
        ///// <summary>
        ///// Java
        ///// </summary>
        //[Description("Java")]
        //Java = 4,
    }
}
