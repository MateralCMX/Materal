using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConfigCenter.PresentationModel.ConfigCenter
{
    /// <summary>
    /// 同步请求模型
    /// </summary>
    public class SyncConfigRequestModel : SyncAllConfigRequestModel
    {
        /// <summary>
        /// 要同步的项目
        /// </summary>
        [Required(ErrorMessage = "要同步的项目不能为空")]
        public List<SyncProjectRequestModel> Projects{ get; set; }
    }
}
