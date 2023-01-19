using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Materal.WeChatHelper.Model
{
    /// <summary>
    /// 创建菜单模型
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class CreateMenuModel
    {
        /// <summary>
        /// 按钮
        /// </summary>
        public List<DefaultMenuButtonModel> button { get; set; } = new List<DefaultMenuButtonModel>();
    }
}
