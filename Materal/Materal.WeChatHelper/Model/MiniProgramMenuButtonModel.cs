using System.Diagnostics.CodeAnalysis;

namespace Materal.WeChatHelper.Model
{
    /// <summary>
    /// 小程序菜单按钮
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    public class MiniProgramMenuButtonModel : MenuButtonModel
    {
        public MiniProgramMenuButtonModel()
        {
            typeEnum = MenuButtonTypeEnum.miniprogram;
        }
        /// <summary>
        /// Url地址
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 小程序的Appid
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 小程序页面路径
        /// </summary>
        public string pagepath { get; set; }
    }
}
