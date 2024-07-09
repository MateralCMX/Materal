using Materal.Tools.Core;
using Materal.Tools.WinUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;

namespace Materal.Tools.WinUI.Pages
{
    [Menu("Materal发布", "\uE7B8")]
    public sealed partial class MateralPublishPage : Page
    {
        public MateralPublishViewModel ViewModel { get; } = new();
        public ObservableCollection<MessageViewModel> Messages = [];
        public MateralPublishPage()
        {
            ViewModel.OnMessage += ViewModel_OnMessage;
            ViewModel.OnClearMessage += ViewModel_OnClearMessage;
            InitializeComponent();
        }
        private void ViewModel_OnClearMessage() => DispatcherQueue.TryEnqueue(Messages.Clear);
        private void ViewModel_OnMessage(MessageLevel level, string? message) => DispatcherQueue.TryEnqueue(() =>
        {
            if (string.IsNullOrWhiteSpace(message)) return;
            Messages.Add(new(level, message));
        });

        private void ItemsControl_SizeChanged(object sender, SizeChangedEventArgs e) => MessageViewer.ChangeView(null, double.MaxValue, null);
    }
}
