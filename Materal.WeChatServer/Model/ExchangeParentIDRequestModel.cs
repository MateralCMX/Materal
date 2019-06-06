using System.ComponentModel.DataAnnotations;

namespace Model
{
    /// <summary>
    /// 更改父级请求模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExchangeParentIDRequestModel<T> where T:struct
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识1不可以为空")]
        public T ID { get; set; }
        /// <summary>
        /// 父级唯一标识
        /// </summary>
        public T? ParentID { get; set; }
        /// <summary>
        /// 位序唯一标识
        /// </summary>
        public T? TargetID { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        public bool ForUnder { get; set; } = true;
    }
}
