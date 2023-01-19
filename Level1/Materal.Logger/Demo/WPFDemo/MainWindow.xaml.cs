using Microsoft.Extensions.Logging;
using System;
using System.Windows;

namespace WPFDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ILogger<MainWindow>? _logger;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _logger = WPFService.GetServiceOrDefatult<ILogger<MainWindow>>();
        }

        private void BtnTrace_Click(object sender, EventArgs e)
        {
            _logger?.LogTrace(textMessage.Text);
        }
        private void BtnDebug_Click(object sender, EventArgs e)
        {
            if (_logger == null) return;
            using IDisposable? socpe = _logger.BeginScope("BtnDebug_Click");
            _logger.LogDebug(textMessage.Text);
        }
        private void BtnInfo_Click(object sender, EventArgs e)
        {
            if (_logger == null) return;
            using IDisposable? socpe = _logger.BeginScope("BtnInfo_Click");
            _logger.LogInformation(textMessage.Text);
        }
        private void BtnWarning_Click(object sender, EventArgs e)
        {
            if (_logger == null) return;
            using IDisposable? socpe = _logger.BeginScope("BtnWarning_Click");
            _logger.LogWarning(textMessage.Text);
        }
        private void BtnError_Click(object sender, EventArgs e)
        {
            if (_logger == null) return;
            using IDisposable? socpe = _logger.BeginScope("BtnError_Click");
            _logger.LogError(textMessage.Text);
        }
        private void BtnCritical_Click(object sender, EventArgs e)
        {
            if (_logger == null) return;
            using IDisposable? socpe = _logger.BeginScope("BtnCritical_Click");
            _logger.LogCritical(textMessage.Text);
        }
    }
}
