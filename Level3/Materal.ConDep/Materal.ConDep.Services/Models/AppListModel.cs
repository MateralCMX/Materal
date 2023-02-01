using Materal.Common;
using Materal.ConDep.Services.Enums;
using System;

namespace Materal.ConDep.Services.Models
{
    /// <summary>
    /// 应用列表
    /// </summary>
    public class AppListModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// APP状态
        /// </summary>
        public AppStatusEnum AppStatus { get; set; }
        /// <summary>
        /// APP状态文本
        /// </summary>
        public string AppStatusText => AppStatus.GetDescription();
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string AppPath { get; set; }
        /// <summary>
        /// 主模组名称
        /// </summary>
        public string MainModuleName { get; set; }
        /// <summary>
        /// 运行参数
        /// </summary>
        public string Parameters { get; set; }
    }
}
