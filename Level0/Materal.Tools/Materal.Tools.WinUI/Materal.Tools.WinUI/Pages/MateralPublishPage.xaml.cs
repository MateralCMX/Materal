using Materal.Tools.Core;
using Materal.Tools.WinUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;

namespace Materal.Tools.WinUI.Pages
{
    [Menu("Materal发布", "\uE7B8", 0)]
    public sealed partial class MateralPublishPage : Page
    {
        public MateralPublishViewModel ViewModel { get; } = new();
        public ObservableCollection<MessageViewModel> Messages = [];
        public int ErrorMessageCount { get => (int)GetValue(ErrorMessageCountProperty); set => SetValue(ErrorMessageCountProperty, value); }
        public static readonly DependencyProperty ErrorMessageCountProperty = DependencyProperty.Register(nameof(ErrorMessageCount), typeof(int), typeof(MateralPublishPage), new PropertyMetadata(0));
        public int WarringMessageCount { get => (int)GetValue(WarringMessageCountProperty); set => SetValue(WarringMessageCountProperty, value); }
        public static readonly DependencyProperty WarringMessageCountProperty = DependencyProperty.Register(nameof(WarringMessageCount), typeof(int), typeof(MateralPublishPage), new PropertyMetadata(0));
        public MateralPublishPage()
        {
            ViewModel.OnMessage += ViewModel_OnMessage;
            ViewModel.OnClearMessage += ViewModel_OnClearMessage;
            InitializeComponent();
        }
        private void ViewModel_OnClearMessage() => DispatcherQueue.TryEnqueue(() =>
        {
            Messages.Clear();
            ErrorMessageCount = 0;
            WarringMessageCount = 0;
        });
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
            Messages.Add(new(level, message));
            if (level == MessageLevel.Error)
            {
                ErrorMessageCount++;
            }
            else if (level == MessageLevel.Warning)
            {
                WarringMessageCount++;
            }
        });
        private void ItemsControl_SizeChanged(object sender, SizeChangedEventArgs e) => MessageViewer.ChangeView(null, double.MaxValue, null);
    }
}
