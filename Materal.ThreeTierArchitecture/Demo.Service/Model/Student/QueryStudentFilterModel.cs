using Materal.Model;

namespace Demo.Service.Model.Student
{
    public class QueryStudentFilterModel : PageRequestModel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Contains]
        public string Name { get; set; }
        /// <summary>
        /// 最小年龄
        /// </summary>
        [GreaterThanOrEqual(nameof(Domain.Student.Age))]
        public byte? MinAge { get; set; }
        /// <summary>
        /// 最大年龄
        /// </summary>
        [LessThanOrEqual(nameof(Domain.Student.Age))]
        public byte? MaxAge { get; set; }
        /// <summary>
        /// 班级名称
        /// </summary>
        [Contains]
        public string ClassName { get; set; }
    }
}
