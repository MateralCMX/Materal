using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Windows;

namespace WpfAppDemo
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly ILogger<MainWindowViewModel> _logger;
        [ObservableProperty]
        private string _logContent = string.Empty;
        private readonly Timer _timer;
        private readonly string _rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "Log.log");
        public MainWindowViewModel(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger<MainWindowViewModel>>();
            _timer = new(LoadLogContent);
            _timer.Change(TimeSpan.FromMilliseconds(100), Timeout.InfiniteTimeSpan);
        }
        [RelayCommand]
        private void DeleteLog()
        {
            try
            {
                if (!File.Exists(_rootPath)) return;
                File.Delete(_rootPath);
                Application.Current?.Dispatcher.BeginInvoke(new Action(() => LogContent = string.Empty));
            }
            catch { }
        }
        private void LoadLogContent(object? state)
        {
            Application.Current?.Dispatcher.BeginInvoke(new Action(() =>
            {
                try
                {
                    if (!File.Exists(_rootPath)) return;
                    LogContent = File.ReadAllText(_rootPath);
                }
                catch { }
            }));
            _timer.Change(TimeSpan.FromMilliseconds(100), Timeout.InfiniteTimeSpan);
        }
        [RelayCommand]
        private void WriteTraceLog() => _logger.LogTrace("Hello World!");
        [RelayCommand]
        private void WriteDebugLog() => _logger.LogDebug("Hello World!");
        [RelayCommand]
        private void WriteInformationLog() => _logger.LogInformation("Hello World!");
        [RelayCommand]
        private void WriteWarningLog() => _logger.LogWarning("Hello World!");
        [RelayCommand]
        private void WriteErrorLog() => _logger.LogError("Hello World!");
        [RelayCommand]
        private void WriteCriticalLog() => _logger.LogCritical("Hello World!");
    }
}
