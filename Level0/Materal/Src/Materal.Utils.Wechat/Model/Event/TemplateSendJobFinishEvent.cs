namespace Materal.Utils.Wechat.Model.Event
{
    /// <summary>
    /// 模版发送完毕事件
    /// </summary>
    public class TemplateSendJobFinishEvent : WechatServerEvent
    {
        /// <summary>
        /// 消息唯一标识
        /// </summary>
        public string MessageID { get; set; }
        /// <summary>
        /// 发送状态
        /// 
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="xmlDocument"></param>
        public TemplateSendJobFinishEvent(XmlDocument xmlDocument) : base(xmlDocument)
        {
            MessageID = GetXmlValue("MsgId");
            Status = GetXmlValue(nameof(Status));
        }
    }
}
