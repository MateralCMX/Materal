using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace RCManagementTool.Messages
{
    /// <summary>
    /// 打开加载遮罩消息
    /// </summary>
    public class OpenLoadingMaskMessage : ValueChangedMessage<string>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public OpenLoadingMaskMessage(string message) : base(message)
        {
        }
    }
    /// <summary>
    /// RC消息管理器
    /// </summary>
    public static partial class RCMessageManager
    {
        /// <summary>
        /// 注册打开加载遮罩消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="recipient"></param>
        /// <param name="handler"></param>
        public static void RegisterOpenLoadingMaskMessage<T>(T recipient, MessageHandler<T, OpenLoadingMaskMessage> handler)
            where T : class
            => WeakReferenceMessenger.Default.Register(recipient, handler);
        /// <summary>
        /// 发送打开加载遮罩消息
        /// </summary>
        /// <param name="message"></param>
        public static void SendOpenLoadingMaskMessage(string message = "加载中")
            => WeakReferenceMessenger.Default.Send(new OpenLoadingMaskMessage(message));
    }
}
