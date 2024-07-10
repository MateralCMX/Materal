using Materal.Tools.Core;
using Materal.Tools.Core.Logger;
using Materal.Tools.WinUI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml.Controls;
using System;

namespace Materal.Tools.WinUI.Pages
{
    [Menu("Materal版本", "\uF0B3", 1)]
    public sealed partial class MateralVersionPage : Page
    {
        public MateralVersionViewModel ViewModel { get; } = new();
        private readonly ILoggerListener _loggerListener;
        private readonly IDisposable _loggerEvent;
        public MateralVersionPage()
        {
            _loggerListener = App.ServiceProvider.GetRequiredService<ILoggerListener>();
            _loggerEvent = _loggerListener.Subscribe(OnLog);
            ViewModel.OnClearMessage += ViewModel_OnClearMessage;
            InitializeComponent();
        }
        private void OnLog(Log log)
        {
            MessageLevel level = log.Level switch
            {
                LogLevel.Warning => MessageLevel.Warning,
                LogLevel.Error => MessageLevel.Error,
                LogLevel.Critical => MessageLevel.Error,
                _ => MessageLevel.Information
            };
            consolePrint.AddMessage(level, log.Message);
        }
        private void ViewModel_OnClearMessage() => consolePrint.ClearMessage();
        private void Page_Unloaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            consolePrint.ClearMessage();
            _loggerEvent.Dispose();
        }
    }
}
