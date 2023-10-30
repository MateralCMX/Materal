using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace RCManagementTool.Messages
{
    /// <summary>
    /// 关闭加载遮罩消息
    /// </summary>
    public class CloseLoadingMaskMessage : ValueChangedMessage<object?>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="obj"></param>
        public CloseLoadingMaskMessage(object? obj) : base(obj)
        {
        }
    }
    /// <summary>
    /// RC消息管理器
    /// </summary>
    public static partial class RCMessageManager
    {
        /// <summary>
        /// 注册关闭加载遮罩消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="recipient"></param>
        /// <param name="handler"></param>
        public static void RegisterCloseLoadingMaskMessage<T>(T recipient, MessageHandler<T, CloseLoadingMaskMessage> handler)
            where T : class
            => WeakReferenceMessenger.Default.Register(recipient, handler);
        /// <summary>
        /// 发送关闭加载遮罩消息
        /// </summary>
        /// <param name="message"></param>
        public static void SendCloseLoadingMaskMessage()
            => WeakReferenceMessenger.Default.Send(new CloseLoadingMaskMessage(null));
    }
}
