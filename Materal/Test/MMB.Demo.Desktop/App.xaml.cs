using Materal.MergeBlock;
using Materal.MergeBlock.WinUI.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

namespace MMB.Demo.Desktop
{
    public partial class App : Microsoft.UI.Xaml.Application
    {
        private CancellationToken? _hostTaskCancellationToken;
        public MergeBlockWinUIApp? APP { get; private set; }
        public App() => InitializeComponent();
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            MergeBlockProgram.OnConfigureServices += MergeBlockProgram_OnConfigureServices;
            MergeBlockProgram.OnApplicationInitialization += MergeBlockProgram_OnApplicationInitialization;
            _hostTaskCancellationToken = new CancellationToken();
            _ = MergeBlockProgram.RunAsync([], _hostTaskCancellationToken.Value);
        }
        private void MergeBlockProgram_OnApplicationInitialization(IServiceProvider service)
        {
            MergeBlockWinUIApp app = service.GetRequiredService<MergeBlockWinUIApp>();
            app.MainWindow.Activate();
        }
        private void MergeBlockProgram_OnConfigureServices(IServiceCollection services) => services.AddSingleton(new MergeBlockWinUIApp(new MainWindow()));
        private void CurrentDomain_ProcessExit(object? sender, EventArgs e)
        {
            if (_hostTaskCancellationToken is null) return;
            _hostTaskCancellationToken.Value.ThrowIfCancellationRequested();
        }
    }
}
