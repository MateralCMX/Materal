using System;

namespace Materal.TTA.Common
{
    /// <summary>
    /// 实体特性
    /// </summary>
    public class EntityAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="primaryKeyName">主键名称</param>
        /// <param name="isView">视图标识</param>
        public EntityAttribute(string primaryKeyName = "ID", bool isView = false)
        {
            PrimaryKeyName = primaryKeyName;
            IsView = isView;
        }

        /// <summary>
        /// 主键名称
        /// </summary>
        public string PrimaryKeyName { get; }

        /// <summary>
        /// 视图标识
        /// </summary>
        public bool IsView { get; }
    }
}
