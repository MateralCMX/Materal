using CommunityToolkit.Mvvm.ComponentModel;
using Materal.Tools.Core;

namespace Materal.Tools.WinUI.ViewModels
{
    public partial class MessageDialogViewModel : ObservableObject
    {
        [ObservableProperty]
        public partial string? Title { get; set; } = "提示";
        [ObservableProperty]
        public partial string Message { get; set; } = string.Empty;
        public void ChangeMessage(Exception ex)
        {
            if (ex is ToolsException toolsException)
            {
                Title = "提示";
                Message = toolsException.Message;
            }
            else
            {
                Title = "错误";
                Message = $"{ex.Message}\r\n{ex.StackTrace}";
            }
        }
    }
}
