using CommunityToolkit.Mvvm.ComponentModel;
using Materal.Tools.Core;
using System;

namespace Materal.Tools.WinUI.ViewModels
{
    public partial class MessageViewModel(MessageLevel messageLevel, string? message) : ObservableObject
    {
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime DateTime { get; } = DateTime.Now;
        /// <summary>
        /// 消息
        /// </summary>
        public string? Message { get; } = message;
        /// <summary>
        /// 等级
        /// </summary>
        public MessageLevel Level { get; } = messageLevel;
        /// <summary>
        /// 组合消息
        /// </summary>
        public string CompositeMessage => string.IsNullOrWhiteSpace(Message) ? string.Empty : $"[{DateTime:yyyy/MM/dd HH:mm:ss}]{Message}";
    }
}
