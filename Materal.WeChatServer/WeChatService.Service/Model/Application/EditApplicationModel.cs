using System;
namespace WeChatService.Service.Model.Application
{
    /// <summary>
    /// 应用修改模型
    /// </summary>
    public class EditApplicationModel : AddApplicationModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
    }
}
