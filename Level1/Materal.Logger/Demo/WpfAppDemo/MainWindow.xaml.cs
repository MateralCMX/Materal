using System.Windows;

namespace WpfAppDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel { get; } = new(App.ServiceProvider);
        public MainWindow()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
