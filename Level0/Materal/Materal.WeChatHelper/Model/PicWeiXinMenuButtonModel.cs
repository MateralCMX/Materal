using System.Diagnostics.CodeAnalysis;

namespace Materal.WeChatHelper.Model
{
    /// <summary>
    /// 弹出微信相册发图器用户点击按钮后，
    /// 微信客户端将调起微信相册，
    /// 完成选择操作后，将选择的相片发送给开发者的服务器，
    /// 并推送事件给开发者，同时收起相册，随后可能会收到开发者下发的消息。
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class PicWeiXinMenuButtonModel : MenuButtonModel
    {
        public PicWeiXinMenuButtonModel()
        {
            typeEnum = MenuButtonTypeEnum.pic_weixin;
        }
        /// <summary>
        /// 值
        /// </summary>
        public string key { get; set; }
    }
}
