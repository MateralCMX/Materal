using System.Diagnostics.CodeAnalysis;

namespace Materal.WeChatHelper.Model
{
    /// <summary>
    /// 点击推事件用户点击click类型按钮后，
    /// 微信服务器会通过消息接口推送消息类型为event的结构给开发者（参考消息接口指南），
    /// 并且带上按钮中开发者填写的key值，开发者可以通过自定义的key值与用户进行交互；
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ClickMenuButtonModel : MenuButtonModel
    {
        public ClickMenuButtonModel()
        {
            typeEnum = MenuButtonTypeEnum.click;
        }
        /// <summary>
        /// 值
        /// </summary>
        public string key { get; set; }
    }
}
