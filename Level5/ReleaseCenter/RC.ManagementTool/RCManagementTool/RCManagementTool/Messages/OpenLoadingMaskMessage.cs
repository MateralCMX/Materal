using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace RCManagementTool.Messages
{
    /// <summary>
    /// 打开加载遮罩消息
    /// </summary>
    public class OpenLoadingMaskMessage : ValueChangedMessage<OpenLoadingMaskModel>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="model"></param>
        public OpenLoadingMaskMessage(OpenLoadingMaskModel model) : base(model)
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
        public static void SendOpenLoadingMaskMessage(OpenLoadingMaskModel model)
            => WeakReferenceMessenger.Default.Send(new OpenLoadingMaskMessage(model));
    }
    public class OpenLoadingMaskModel
    {
        /// <summary>
        /// 打开后
        /// </summary>
        public event Action? OnOpen;
        /// <summary>
        /// 打开后
        /// </summary>
        public event Func<Task>? OnOpenAsync;
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 自动关闭
        /// </summary>
        public bool AutoClose { get; set; }
        public OpenLoadingMaskModel(string message = "加载中", bool autoClose = true)
        {
            Message = message;
            AutoClose = autoClose;
        }
        /// <summary>
        /// 执行
        /// </summary>
        /// <returns></returns>
        public async Task ExcuteAsync()
        {
            OnOpen?.Invoke();
            if(OnOpenAsync is not null)
            {
                await OnOpenAsync.Invoke();
            }
        }
    }
}
