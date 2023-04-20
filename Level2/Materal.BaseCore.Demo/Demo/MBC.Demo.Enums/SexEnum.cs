﻿using System.ComponentModel;

namespace MBC.Demo.Enums
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum SexEnum : byte
    {
        /// <summary>
        /// 女性
        /// </summary>
        [Description("女性")]
        Woman = 0,
        /// <summary>
        /// 男性
        /// </summary>
        [Description("男性")]
        Man = 1
    }
}