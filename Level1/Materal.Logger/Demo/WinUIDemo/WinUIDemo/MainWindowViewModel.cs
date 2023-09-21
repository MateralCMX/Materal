using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Dispatching;
using System;
using System.IO;
using System.Threading;

namespace WinUIDemo
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly ILogger<MainWindowViewModel> _logger;
        [ObservableProperty]
        private string _logContent = string.Empty;
        private readonly Timer _timer;
        private readonly string _rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "Log.log");
        public DispatcherQueue? DispatcherQueue { get; set; }
        public MainWindowViewModel(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger<MainWindowViewModel>>();
            _timer = new(LoadLogContent);
            _timer.Change(TimeSpan.FromMilliseconds(100), Timeout.InfiniteTimeSpan);
        }
        [RelayCommand]
        private void WriteLog(LogLevel logLevel) => _logger.Log(logLevel, "Hello World!");
        [RelayCommand]
        private void DeleteLog()
        {
            try
            {
                if (!File.Exists(_rootPath)) return;
                File.Delete(_rootPath);
                DispatcherQueue?.TryEnqueue(() => LogContent = string.Empty);
            }
            catch { }
        }
        private void LoadLogContent(object? state)
        {
            DispatcherQueue?.TryEnqueue(() =>
            {
                try
                {
                    if (!File.Exists(_rootPath)) return;
                    LogContent = File.ReadAllText(_rootPath);
                }
                catch { }
            });
            _timer.Change(TimeSpan.FromMilliseconds(100), Timeout.InfiniteTimeSpan);
        }
    }
}
