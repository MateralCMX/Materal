using Microsoft.UI.Xaml;

namespace WinUIDemo
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel { get; } = new(App.ServiceProvider);
        public MainWindow()
        {
            InitializeComponent();
            ViewModel.DispatcherQueue = DispatcherQueue;
        }
    }
}
