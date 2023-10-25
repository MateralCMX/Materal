using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace RCManagementTool.Messages
{
    /// <summary>
    /// 异常消息
    /// </summary>
    public class ExceptionMessage : ValueChangedMessage<Exception>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="exception"></param>
        public ExceptionMessage(Exception exception) : base(exception)
        {
        }
    }
    /// <summary>
    /// RC消息管理器
    /// </summary>
    public static partial class RCMessageManager
    {
        /// <summary>
        /// 注册异常消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="recipient"></param>
        /// <param name="handler"></param>
        public static void RegisterExceptionMessage<T>(T recipient, MessageHandler<T, ExceptionMessage> handler)
            where T : class
            => WeakReferenceMessenger.Default.Register(recipient, handler);
        /// <summary>
        /// 发送异常消息
        /// </summary>
        /// <param name="exception"></param>
        public static void SendExceptionMessage(Exception exception)
            => WeakReferenceMessenger.Default.Send(new ExceptionMessage(exception));
    }
}
