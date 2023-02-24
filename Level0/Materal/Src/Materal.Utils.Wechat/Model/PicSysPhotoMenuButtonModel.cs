using System.Diagnostics.CodeAnalysis;

namespace Materal.WeChatHelper.Model
{
    /// <summary>
    /// 弹出系统拍照发图用户点击按钮后，微信客户端将调起系统相机，
    /// 完成拍照操作后，会将拍摄的相片发送给开发者，并推送事件给开发者，
    /// 同时收起系统相机，随后可能会收到开发者下发的消息。
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class PicSysPhotoMenuButtonModel : MenuButtonModel
    {
        public PicSysPhotoMenuButtonModel()
        {
            typeEnum = MenuButtonTypeEnum.pic_sysphoto;
        }
        /// <summary>
        /// 值
        /// </summary>
        public string key { get; set; }
    }
}
