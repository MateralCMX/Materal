using System.Diagnostics.CodeAnalysis;

namespace Materal.WeChatHelper.Model
{
    /// <summary>
    /// 弹出拍照或者相册发图用户点击按钮后，
    /// 微信客户端将弹出选择器供用户选择“拍照”或者“从手机相册选择”。
    /// 用户选择后即走其他两种流程。
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class PicPhotoOrAlbumMenuButtonModel : MenuButtonModel
    {
        public PicPhotoOrAlbumMenuButtonModel()
        {
            typeEnum = MenuButtonTypeEnum.pic_photo_or_album;
        }
        /// <summary>
        /// 值
        /// </summary>
        public string key { get; set; }
    }
}
