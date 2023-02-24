using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Materal.WeChatHelper.Model
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public abstract class MenuButtonModel : DefaultMenuButtonModel
    {
        /// <summary>
        /// 按钮类型
        /// </summary>
        public MenuButtonTypeEnum  typeEnum { get; set; }

        /// <summary>
        /// 按钮类型
        /// </summary>
        public string type => typeEnum.ToString();
    }
}
