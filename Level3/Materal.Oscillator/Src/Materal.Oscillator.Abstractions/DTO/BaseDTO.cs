using Materal.Oscillator.Abstractions.Domain;
using System.ComponentModel.DataAnnotations;

namespace Materal.Oscillator.Abstractions.DTO
{
    /// <summary>
    /// 基础数据传输模型
    /// </summary>
    public abstract class BaseDTO
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识为空")]
        public Guid ID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Required(ErrorMessage = "创建时间为空")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        protected BaseDTO() { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="domain"></param>
        protected BaseDTO(IDomain domain)
        {
            ID = domain.ID;
            CreateTime = domain.CreateTime;
            UpdateTime = domain.UpdateTime;
        }
    }
}
