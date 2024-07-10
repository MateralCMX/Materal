using CommunityToolkit.Mvvm.Input;
using Materal.Tools.Core;
using Materal.Tools.WinUI.ViewModels;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using WinRT.Interop;

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
            if (!ViewModel.IsMateralProjectPath(folder.Path)) throw new ToolsException("不是Materal项目路径");
            ViewModel.ProjectPath = folder.Path;
        }
    }
}
