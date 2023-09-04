using Materal.BaseCore.CodeGenerator.Models;

namespace Materal.BaseCore.Test.CodeGenerator
{
    /// <summary>
    /// Domain测试
    /// </summary>
    [TestClass]
    public class DomainModelTest : BaseTest
    {
        [TestMethod]
        public void CustomTest()
        {
            const string code = @"using Materal.BaseCore.CodeGenerator;
using Materal.BaseCore.Domain;
using Materal.Utils.Model;
using System.ComponentModel.DataAnnotations;
using XMJ.Core.CodeGenerator;
using XMJ.Courseware.Enums;

namespace XMJ.Courseware.Domain
{
    /// <summary>
    /// 课件文件错误记录
    /// </summary>
    [MultipleRecord(nameof(CoursewareFileID), UserIDName = nameof(CreateUserID))]
    public class CoursewareFileErrorRecord : BaseDomain, IDomain
    {
        /// <summary>
        /// 创建用户唯一标识
        /// </summary>
        [Required(ErrorMessage = ""创建用户唯一标识必填"")]
        [Equal]
        [NotAddGenerator, NotEditGenerator]
        public Guid CreateUserID { get; set; }
        /// <summary>
        /// 课件文件唯一标识
        /// </summary>
        [Required(ErrorMessage = ""课件文件唯一标识必填"")]
        [Equal]
        public Guid CoursewareFileID { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Required(ErrorMessage = ""描述必填""), StringLength(1000, ErrorMessage = ""描述长度必须小于等于1000"")]
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// 状态
        /// </summary>
        [Required(ErrorMessage = ""状态必填"")]
        [Equal]
        [NotAddGenerator]
        public CoursewareFileErrorStatusEnum Status { get; set; }
    }
}";
            string[] codes = code.Split("\r\n");
            DomainModel domainModel = new(codes, 13);
        }
    }
}
