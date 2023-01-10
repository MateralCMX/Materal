#nullable enable
using Materal.Model;
using Materal.BaseCore.Services;
using RC.Demo.Enums;

namespace RC.Demo.Services.Models.User
{
    /// <summary>
    /// 用户查询模型
    /// </summary>
    public partial class QueryUserModel : PageRequestModel, IQueryServiceModel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Contains]
        public string? Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Equal]
        public SexEnum? Sex { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        [Equal]
        public string? Account { get; set; }
        /// <summary>
        /// 唯一标识组
        /// </summary>
        [Contains("ID")]
        public List<Guid>? IDs { get; set; }
        /// <summary>
        /// 最大创建时间
        /// </summary>
        [LessThanOrEqual("CreateTime")]
        public DateTime? MaxCreateTime { get; set; }
        /// <summary>
        /// 最小创建时间
        /// </summary>
        [GreaterThanOrEqual("CreateTime")]
        public DateTime? MinCreateTime { get; set; }
    }
}
