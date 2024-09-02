using Materal.MergeBlock.WebHosting;
using Microsoft.UI.Xaml;

namespace Materal.MergeBlock.WinUITest
{
    public partial class App : Application
    {
        public App() => InitializeComponent();
        protected override async void OnLaunched(LaunchActivatedEventArgs args) => await MergeBlockProgram.RunAsync<MainWindow>(args);
    }
}
