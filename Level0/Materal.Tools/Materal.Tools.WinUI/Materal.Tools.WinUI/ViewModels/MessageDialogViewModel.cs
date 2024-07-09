using CommunityToolkit.Mvvm.ComponentModel;
using Materal.Tools.Core;
using System;

namespace Materal.Tools.WinUI.ViewModels
{
    public partial class MessageDialogViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _title = "提示";
        [ObservableProperty]
        private string _message = string.Empty;
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
