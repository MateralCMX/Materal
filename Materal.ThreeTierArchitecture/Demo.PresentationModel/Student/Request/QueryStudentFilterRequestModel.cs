using System.ComponentModel.DataAnnotations;
using Materal.Model;

namespace Demo.PresentationModel.Student.Request
{
    public class QueryStudentFilterRequestModel : PageRequestModel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(100, ErrorMessage = @"名称长度不能超过100")]
        public string Name { get; set; }
        /// <summary>
        /// 最小年龄
        /// </summary>
        [Range(byte.MinValue, byte.MaxValue, ErrorMessage = @"最小年龄格式错误")]
        public byte? MinAge { get; set; }
        /// <summary>
        /// 最大年龄
        /// </summary>
        [Range(byte.MinValue, byte.MaxValue, ErrorMessage = @"最大年龄格式错误")]
        public byte? MaxAge { get; set; }
        /// <summary>
        /// 班级名称
        /// </summary>
        [Contains]
        [StringLength(100, ErrorMessage = @"班级名称长度不能超过100")]
        public string ClassName { get; set; }
    }
}
