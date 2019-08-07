using Materal.Model;

namespace Demo.Service.Model.Class
{
    /// <summary>
    /// 查询班级过滤器模型
    /// </summary>
    public class QueryClassFilterModel : PageRequestModel
    {
        [Contains]
        public string Name { get; set; }
    }
}
