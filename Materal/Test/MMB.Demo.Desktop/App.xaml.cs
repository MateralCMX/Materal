using Materal.MergeBlock.WebHosting;
using Microsoft.UI.Xaml;

namespace MMB.Demo.Desktop
{
    public partial class App : Microsoft.UI.Xaml.Application
    {
        public App() => InitializeComponent();
        protected override async void OnLaunched(LaunchActivatedEventArgs args) => await MergeBlockProgram.RunAsync<MainWindow>(args);
    }
}
