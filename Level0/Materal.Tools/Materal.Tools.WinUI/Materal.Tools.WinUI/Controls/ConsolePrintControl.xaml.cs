using Materal.Tools.Core;
using Materal.Tools.WinUI.Pages;
using Materal.Tools.WinUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;

namespace Materal.Tools.WinUI.Controls
{
    public sealed partial class ConsolePrintControl : UserControl
    {
        private ObservableCollection<MessageViewModel> Messages = [];
        private int ErrorMessageCount { get => (int)GetValue(ErrorMessageCountProperty); set => SetValue(ErrorMessageCountProperty, value); }
        public static readonly DependencyProperty ErrorMessageCountProperty = DependencyProperty.Register(nameof(ErrorMessageCount), typeof(int), typeof(MateralPublishPage), new PropertyMetadata(0));
        private int WarringMessageCount { get => (int)GetValue(WarringMessageCountProperty); set => SetValue(WarringMessageCountProperty, value); }
        public static readonly DependencyProperty WarringMessageCountProperty = DependencyProperty.Register(nameof(WarringMessageCount), typeof(int), typeof(MateralPublishPage), new PropertyMetadata(0));
        public ConsolePrintControl() => InitializeComponent();
        public void AddMessage(MessageLevel level, string? message) => AddMessage(new(level, message));
        public void AddMessage(MessageViewModel message) => DispatcherQueue.TryEnqueue(() =>
        {
            Messages.Add(message);
            if (message.Level == MessageLevel.Error)
            {
                ErrorMessageCount++;
            }
            else if (message.Level == MessageLevel.Warning)
            {
                WarringMessageCount++;
            }
        });
        public void ClearMessage() => DispatcherQueue.TryEnqueue(() =>
        {
            Messages.Clear();
            ErrorMessageCount = 0;
            WarringMessageCount = 0;
        });
        private void ItemsControl_SizeChanged(object sender, SizeChangedEventArgs e) => MessageViewer.ChangeView(null, double.MaxValue, null);
    }
}
