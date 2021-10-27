using System.Collections.Generic;

namespace ConfigCenter.Services.Models.ConfigCenter
{
    /// <summary>
    /// 同步请求模型
    /// </summary>
    public class SyncModel
    {
        /// <summary>
        /// 源Key
        /// </summary>
        public string SourceKey{ get; set; }
        /// <summary>
        /// 目标Key
        /// </summary>
        public ICollection<string> TargetKey { get; set; }
        /// <summary>
        /// 项目
        /// </summary>
        public ICollection<SyncProjectModel> Projects { get; set; }
    }
}
