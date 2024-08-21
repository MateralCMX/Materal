using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace Materal.Tools.WinUI.Controls
{
    public sealed partial class FolderPickerButton : UserControl
    {
        public object Header { get => GetValue(HeaderProperty); set => SetValue(HeaderProperty, value); }
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(nameof(Header), typeof(object), typeof(FolderPickerButton), new PropertyMetadata(string.Empty));
        public string FolderPath { get => (string)GetValue(FolderPathProperty); set => SetValue(FolderPathProperty, value); }
        public static readonly DependencyProperty FolderPathProperty = DependencyProperty.Register(nameof(FolderPath), typeof(string), typeof(FolderPickerButton), new PropertyMetadata(string.Empty));
        public FolderPickerButton() => InitializeComponent();
        [RelayCommand]
        private async Task SelectionFolderAsync()
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
            FolderPath = folder.Path;
        }
    }
}
