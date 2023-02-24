using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Materal.WeChatHelper.Model
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class DefaultMenuButtonModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 子级按钮
        /// </summary>
        public List<DefaultMenuButtonModel> sub_button { get; set; } = new List<DefaultMenuButtonModel>();
    }
}
