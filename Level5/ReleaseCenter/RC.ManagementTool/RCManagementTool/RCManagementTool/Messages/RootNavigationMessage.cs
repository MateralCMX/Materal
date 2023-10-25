using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace RCManagementTool.Messages
{
    /// <summary>
    /// 根导航消息
    /// </summary>
    public class RootNavigationMessage : ValueChangedMessage<Type>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="exception"></param>
        public RootNavigationMessage(Type exception) : base(exception)
        {
        }
    }
    /// <summary>
    /// RC消息管理器
    /// </summary>
    public static partial class RCMessageManager
    {
        /// <summary>
        /// 注册根导航消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="recipient"></param>
        /// <param name="handler"></param>
        public static void RegisterRootNavigationMessage<T>(T recipient, MessageHandler<T, RootNavigationMessage> handler)
            where T : class
            => WeakReferenceMessenger.Default.Register(recipient, handler);
        /// <summary>
        /// 发送根导航消息
        /// </summary>
        /// <param name="pageType"></param>
        public static void SendRootNavigationMessage(Type pageType)
            => WeakReferenceMessenger.Default.Send(new RootNavigationMessage(pageType));
        /// <summary>
        /// 发送根导航消息
        /// </summary>
        /// <param name="pageType"></param>
        public static void SendRootNavigationMessage<T>()
            where T : Page => SendRootNavigationMessage(typeof(T));
    }
}
