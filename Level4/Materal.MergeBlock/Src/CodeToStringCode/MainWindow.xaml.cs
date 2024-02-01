using System.Windows;

namespace CodeToStringCode
{
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();
        private void Button_Click(object sender, RoutedEventArgs e) => Clipboard.SetText(ViewModel.StringCode);
    }
}   