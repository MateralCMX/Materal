using System.ComponentModel.DataAnnotations;

namespace Materal.BusinessFlow.WebAPIControllers.Models.Node
{
    public class EditNodeModel : AddNodeModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required]
        public Guid ID { get; set; }
    }
}
