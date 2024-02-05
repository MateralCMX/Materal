using System.ComponentModel.DataAnnotations;

namespace Materal.MergeBlock.SwaggerTest
{
    /// <summary>
    /// 测试请求模型
    /// </summary>
    public class TestRequestModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
