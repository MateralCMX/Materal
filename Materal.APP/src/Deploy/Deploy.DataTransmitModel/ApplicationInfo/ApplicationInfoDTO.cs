using Deploy.Enums;
using System;

namespace Deploy.DataTransmitModel.ApplicationInfo
{
    /// <summary>
    /// 应用程序信息数据传输模型
    /// </summary>
    public class ApplicationInfoDTO
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 主模块
        /// </summary>
        public string MainModule { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public ApplicationTypeEnum ApplicationType { get; set; }
        /// <summary>
        /// 运行参数
        /// </summary>
        public string RunParams { get; set; }
    }
}
