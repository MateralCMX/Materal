using System.ComponentModel.DataAnnotations;

namespace Materal.Gateway.WebAPI.PresentationModel.Route
{
    /// <summary>
    /// 修改路由请求模型
    /// </summary>
    public class EditRouteRequestModel : AddRouteRequestModel
    {
        /// <summary>
        /// 路由唯一标识
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public Guid ID { get; set; }
    }
}
