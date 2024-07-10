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
    [Menu("Materal版本", "\uF0B3", 1)]
    public sealed partial class MateralVersionPage : Page
    {
        public MateralVersionViewModel ViewModel { get; } = new();
        public MateralVersionPage()
        {
            ViewModel.OnMessage += ViewModel_OnMessage;
            ViewModel.OnClearMessage += ViewModel_OnClearMessage;
            InitializeComponent();
        }
        private void ViewModel_OnClearMessage() => consolePrint.ClearMessage();
        private void ViewModel_OnMessage(MessageLevel level, string? message)
        {
            if (string.IsNullOrWhiteSpace(message)) return;
            consolePrint.AddMessage(level, message);
        }
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
