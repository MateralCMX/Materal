using System;

namespace Materal.ConDep.Controllers.Models
{
    public class EditWebAppRequestModel : AddWebAppRequestModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
    }
}
