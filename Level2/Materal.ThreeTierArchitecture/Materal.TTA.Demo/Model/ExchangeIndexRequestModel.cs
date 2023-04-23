using System.ComponentModel.DataAnnotations;

namespace Model
{
    /// <summary>
    /// 调换网页菜单权限位序请求模型
    /// </summary>
    public sealed class ExchangeIndexRequestModel<T>
    {
        /// <summary>
        /// 唯一标识1
        /// </summary>
        [Required(ErrorMessage = "唯一标识1不可以为空")]
        public T ID1 { get; set; }
        /// <summary>
        /// 唯一标识2
        /// </summary>
        [Required(ErrorMessage = "唯一标识2不可以为空")]
        public T ID2 { get; set; }
    }
}
