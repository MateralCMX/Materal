using CommunityToolkit.Mvvm.Input;
using Materal.Tools.Core;
using Materal.Tools.WinUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace Materal.Tools.WinUI.Pages
{
    [Menu("Materal版本", "\uF0B3", 1)]
    public sealed partial class MateralVersionPage : Page
    {
        public MateralVersionViewModel ViewModel { get; } = new();
        public ObservableCollection<MessageViewModel> Messages = [];
        public int ErrorMessageCount { get => (int)GetValue(ErrorMessageCountProperty); set => SetValue(ErrorMessageCountProperty, value); }
        public static readonly DependencyProperty ErrorMessageCountProperty = DependencyProperty.Register(nameof(ErrorMessageCount), typeof(int), typeof(MateralPublishPage), new PropertyMetadata(0));
        public int WarringMessageCount { get => (int)GetValue(WarringMessageCountProperty); set => SetValue(WarringMessageCountProperty, value); }
        public static readonly DependencyProperty WarringMessageCountProperty = DependencyProperty.Register(nameof(WarringMessageCount), typeof(int), typeof(MateralPublishPage), new PropertyMetadata(0));
        public MateralVersionPage()
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
        [RelayCommand]
        private async Task SelectionProjectFolderAsync()
        {
            FolderPicker folderPicker = new()
            {
                ViewMode = PickerViewMode.List,
                SuggestedStartLocation = PickerLocationId.ComputerFolder
            };
            IntPtr hWnd = WindowNative.GetWindowHandle(App.MainWindow);
            InitializeWithWindow.Initialize(folderPicker, hWnd);
            folderPicker.FileTypeFilter.Add("*");
            StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            if (folder is null) return;
            ViewModel.ProjectPath = folder.Path;
        }
    }
}
