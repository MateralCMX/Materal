using System.ComponentModel.DataAnnotations;

namespace Model
{
    /// <summary>
    /// 调换位序请求模型
    /// </summary>
    public sealed class ExchangeIndexNotFoUnderRequestModel<T>
    {
        /// <summary>
        /// 唯一标识1
        /// </summary>
        [Required(ErrorMessage = "唯一标识1不可以为空")]
        public T ExchangeID { get; set; }
        /// <summary>
        /// 唯一标识2
        /// </summary>
        [Required(ErrorMessage = "唯一标识2不可以为空")]
        public T TargetID { get; set; }
    }
}
