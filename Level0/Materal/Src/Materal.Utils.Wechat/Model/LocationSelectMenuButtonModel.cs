using System.Diagnostics.CodeAnalysis;

namespace Materal.WeChatHelper.Model
{
    /// <summary>
    /// 弹出地理位置选择器用户点击按钮后，微信客户端将调起地理位置选择工具，
    /// 完成选择操作后，将选择的地理位置发送给开发者的服务器，
    /// 同时收起位置选择工具，随后可能会收到开发者下发的消息。
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class LocationSelectMenuButtonModel : MenuButtonModel
    {
        public LocationSelectMenuButtonModel()
        {
            typeEnum = MenuButtonTypeEnum.location_select;
        }
        /// <summary>
        /// 值
        /// </summary>
        public string key { get; set; }
    }
}
