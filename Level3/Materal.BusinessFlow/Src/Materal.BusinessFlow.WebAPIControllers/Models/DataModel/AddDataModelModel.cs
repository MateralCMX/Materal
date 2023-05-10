using System.ComponentModel.DataAnnotations;

namespace Materal.BusinessFlow.WebAPIControllers.Models.DataModel
{
    public class AddDataModelModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required, StringLength(40)]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }
    }
}
