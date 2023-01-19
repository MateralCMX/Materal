using System.Diagnostics.CodeAnalysis;

namespace Materal.WeChatHelper.Model
{
    /// <summary>
    /// 扫码推事件且弹出“消息接收中”提示框用户点击按钮后，
    /// 微信客户端将调起扫一扫工具，完成扫码操作后，
    /// 将扫码的结果传给开发者，同时收起扫一扫工具，
    /// 然后弹出“消息接收中”提示框，随后可能会收到开发者下发的消息。
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ScanCodeWaitMsgMenuButtonModel : MenuButtonModel
    {
        public ScanCodeWaitMsgMenuButtonModel()
        {
            typeEnum = MenuButtonTypeEnum.scancode_waitmsg;
        }
        /// <summary>
        /// 值
        /// </summary>
        public string key { get; set; }
    }
}
