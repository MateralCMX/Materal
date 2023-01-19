using System;

namespace Materal.ConDep.Services.Models
{
    /// <summary>
    /// WebApp模型
    /// </summary>
    public class WebAppModel
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
        /// 参数
        /// </summary>
        public string Parameters { get; set; }
    }
}
