using System.ComponentModel.DataAnnotations;

namespace Materal.MergeBlock.SwaggerTest.Controllers
{
    public class TestRequestModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
