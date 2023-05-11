using Materal.BusinessFlow.Abstractions.DTO;
using System.ComponentModel.DataAnnotations;

namespace Materal.BusinessFlow.Abstractions.Domain
{
    public class User : BaseDomain, IDTO
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required, StringLength(40)]
        public string Name { get; set; } = string.Empty;
    }
}