using Materal.Tools.Core;
using Materal.Tools.WinUI.ViewModels;
using Microsoft.UI.Xaml.Controls;
using System;

namespace Materal.Tools.WinUI.Pages
{
    [Menu("Materal发布", "\uE7B8", 0)]
    public sealed partial class MateralPublishPage : Page
    {
        public MateralPublishViewModel ViewModel { get; } = new();
        public MateralPublishPage()
        {
            ViewModel.OnMessage += ViewModel_OnMessage;
            ViewModel.OnClearMessage += ViewModel_OnClearMessage;
            InitializeComponent();
        }
        private void ViewModel_OnClearMessage() => consolePrint.ClearMessage();
        private void ViewModel_OnMessage(MessageLevel level, string? message) => DispatcherQueue.TryEnqueue(() =>
        {
            if (string.IsNullOrWhiteSpace(message)) return;
            if (message.Contains(" error ", StringComparison.OrdinalIgnoreCase))
            {
                level = MessageLevel.Error;
            }
            else if (message.Contains(" warning ", StringComparison.OrdinalIgnoreCase))
            {
                level = MessageLevel.Warning;
            }
            consolePrint.AddMessage(level, message);
        });
    }
}
