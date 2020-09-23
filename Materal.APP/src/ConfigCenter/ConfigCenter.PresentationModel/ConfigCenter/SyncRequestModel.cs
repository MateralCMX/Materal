using System.Collections.Generic;

namespace ConfigCenter.PresentationModel.ConfigCenter
{
    /// <summary>
    /// 同步请求模型
    /// </summary>
    public class SyncRequestModel
    {
        /// <summary>
        /// 源Key
        /// </summary>
        public string SourceKey{ get; set; }
        /// <summary>
        /// 目标Key
        /// </summary>
        public ICollection<string> TargetKeys { get; set; }
        /// <summary>
        /// 项目
        /// </summary>
        public ICollection<SyncProjectRequestModel> Projects { get; set; }
    }
}
