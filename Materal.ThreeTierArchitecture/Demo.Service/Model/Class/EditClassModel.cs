using System;

namespace Demo.Service.Model.Class
{
    /// <summary>
    /// 修改班级模型
    /// </summary>
    public class EditClassModel : AddClassModel
    {

        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
    }
}
