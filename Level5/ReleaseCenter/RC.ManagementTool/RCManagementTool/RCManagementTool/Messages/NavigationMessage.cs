using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace RCManagementTool.Messages
{
    /// <summary>
    /// 导航消息
    /// </summary>
    public class NavigationMessage : ValueChangedMessage<Type>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="exception"></param>
        public NavigationMessage(Type exception) : base(exception)
        {
        }
    }
    /// <summary>
    /// RC消息管理器
    /// </summary>
    public static partial class RCMessageManager
    {
        /// <summary>
        /// 注册导航消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="recipient"></param>
        /// <param name="handler"></param>
        public static void RegisterNavigationMessage<T>(T recipient, MessageHandler<T, NavigationMessage> handler)
            where T : class
            => WeakReferenceMessenger.Default.Register(recipient, handler);
        /// <summary>
        /// 发送导航消息
        /// </summary>
        /// <param name="pageType"></param>
        public static void SendNavigationMessage(Type pageType)
            => WeakReferenceMessenger.Default.Send(new NavigationMessage(pageType));
        /// <summary>
        /// 发送导航消息
        /// </summary>
        /// <param name="pageType"></param>
        public static void SendNavigationMessage<T>()
            where T : Page => SendNavigationMessage(typeof(T));
    }
}
