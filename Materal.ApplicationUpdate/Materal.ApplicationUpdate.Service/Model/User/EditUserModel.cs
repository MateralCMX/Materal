using System;

namespace Materal.ApplicationUpdate.Service.Model.User
{
    public class EditUserModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
