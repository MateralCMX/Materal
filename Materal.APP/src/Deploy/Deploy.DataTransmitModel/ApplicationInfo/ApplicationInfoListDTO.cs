using System;
using Deploy.Enums;
using Materal.Common;

namespace Deploy.DataTransmitModel.ApplicationInfo
{
    /// <summary>
    /// 应用程序信息列表数据传输模型
    /// </summary>
    public class ApplicationInfoListDTO
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
        /// <summary>
        /// 应用程序状态
        /// </summary>
        public ApplicationStatusEnum Status { get; set; }
        /// <summary>
        /// 应用程序状态字符
        /// </summary>
        public string StatusString => Status.GetDescription();
        /// <summary>
        /// 类型字符
        /// </summary>
        public string ApplicationTypeString => ApplicationType.GetDescription();
    }
}
