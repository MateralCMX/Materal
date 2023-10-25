using CommunityToolkit.Mvvm.Messaging;

namespace RCManagementTool.Messages
{
    /// <summary>
    /// 工作流消息管理器
    /// </summary>
    public static partial class RCMessageManager
    {
        /// <summary>
        /// 反注册所有消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="recipient"></param>
        /// <param name="handler"></param>
        public static void UnregisterAll<T>(T recipient)
            where T : class
            => WeakReferenceMessenger.Default.UnregisterAll(recipient);
    }
}
