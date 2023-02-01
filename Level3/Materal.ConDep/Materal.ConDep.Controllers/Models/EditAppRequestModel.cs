using System;

namespace Materal.ConDep.Controllers.Models
{
    public class EditAppRequestModel : AddAppRequestModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
    }
}
