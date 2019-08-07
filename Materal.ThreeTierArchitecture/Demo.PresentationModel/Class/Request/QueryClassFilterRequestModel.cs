using Materal.Model;
using System.ComponentModel.DataAnnotations;

namespace Demo.PresentationModel.Class.Request
{
    /// <summary>
    /// 查询班级过滤器模型
    /// </summary>
    public class QueryClassFilterRequestModel : PageRequestModel
    {
        [StringLength(100, ErrorMessage = @"名称长度不能超过100")]
        public string Name { get; set; } = string.Empty;
    }
}
