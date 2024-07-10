using CommunityToolkit.Mvvm.ComponentModel;
using Materal.Tools.Core.Logger;
using Materal.Tools.WinUI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Materal.Tools.WinUI.Controls
{
    public sealed partial class LogPrintControl : UserControl
    {
        public LogPrintViewModel ViewModel { get; } = new();
        private readonly ILoggerListener _loggerListener;
        private readonly IDisposable _logEvent;
        public LogPrintControl()
        {
            _loggerListener = App.ServiceProvider.GetRequiredService<ILoggerListener>();
            _logEvent = _loggerListener.Subscribe(OnLog);
            InitializeComponent();
        }
        private void OnLog(Log log) => DispatcherQueue.TryEnqueue(() => ViewModel.AddLog(log));
        public void ClearLogs() => DispatcherQueue.TryEnqueue(ViewModel.ClearLogs);
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            _logEvent.Dispose();
            ClearLogs();
        }
        private void ItemsControl_SizeChanged(object sender, SizeChangedEventArgs e) => MessageViewer.ChangeView(null, double.MaxValue, null);
        public partial class LogPrintViewModel : ObservableObject
        {
            public ObservableCollection<LogViewModel> Logs { get; } = [];
            public int WarringCount => Logs.Count(m => m.Level == LogLevel.Warning);
            public int ErrorCount => Logs.Count(m => m.Level >= LogLevel.Error);
            public LogPrintViewModel()
            {
                Logs.CollectionChanged += Logs_CollectionChanged;
            }
            private void Logs_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
            {
                OnPropertyChanged(nameof(WarringCount));
                OnPropertyChanged(nameof(ErrorCount));
            }
            public void AddLog(Log log) => Logs.Add(new(log));
            public void ClearLogs() => Logs.Clear();
        }
    }
}
