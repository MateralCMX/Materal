using System.Diagnostics.CodeAnalysis;

namespace Materal.WeChatHelper.Model
{
    /// <summary>
    /// 跳转图文消息URL用户点击view_limited类型按钮后，
    /// 微信客户端将打开开发者在按钮中填写的永久素材id对应的图文消息URL，永久素材类型只支持图文消息。
    /// 请注意：永久素材id必须是在“素材管理/新增永久素材”接口上传后获得的合法id。
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ViewLimitedMenuButtonModel : MenuButtonModel
    {
        public ViewLimitedMenuButtonModel()
        {
            typeEnum = MenuButtonTypeEnum.view_limited;
        }
        /// <summary>
        /// 素材唯一标识
        /// </summary>
        public string media_id { get; set; }
    }
}
