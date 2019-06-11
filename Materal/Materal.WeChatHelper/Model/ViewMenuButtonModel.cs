using System.Diagnostics.CodeAnalysis;

namespace Materal.WeChatHelper.Model
{
    /// <summary>
    /// 跳转URL用户点击view类型按钮后，
    /// 微信客户端将会打开开发者在按钮中填写的网页URL，
    /// 可与网页授权获取用户基本信息接口结合，获得用户基本信息。
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ViewMenuButtonModel : MenuButtonModel
    {
        public ViewMenuButtonModel()
        {
            typeEnum = MenuButtonTypeEnum.view;
        }
        /// <summary>
        /// Url地址
        /// </summary>
        public string url { get; set; }
    }
}
