using Materal.BaseCore.Domain;

namespace MBC.Demo.Domain
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User : BaseDomain, IDomain, IIndexDomain
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; } = string.Empty;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = string.Empty;
        /// <summary>
        /// 排序
        /// </summary>
        public int Index { get; set; }
    }
}
