using System.Collections.Generic;

namespace Materal.Services.Models
{
    public class ServiceModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 连接
        /// </summary>
        public List<LinkModel> Links { get; set; }
        /// <summary>
        /// 脚本
        /// </summary>
        public List<string> Scripts { get; set; }
    }
}
