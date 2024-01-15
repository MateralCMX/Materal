namespace RC.Deploy.Abstractions.Enums
{
    /// <summary>
    /// 应用程序类型
    /// </summary>
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
        /// .Net
        /// </summary>
        [Description(".Net")]
        DotNet = 2,
        /// <summary>
        /// NodJS
        /// </summary>
        [Description("NodJS")]
        NodJS = 3,
    }
}
