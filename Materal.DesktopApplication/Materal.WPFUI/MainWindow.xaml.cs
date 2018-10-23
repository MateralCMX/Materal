using System.Globalization;
using System.Windows;

namespace Materal.WPFUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public string StringValue { get => (string)GetValue(StringValueProperty); set => SetValue(StringValueProperty, value); }
        public static readonly DependencyProperty StringValueProperty = DependencyProperty.Register(nameof(StringValue), typeof(string), typeof(MainWindow), new FrameworkPropertyMetadata("1234"));
        public decimal DecimalValue { get => (decimal)GetValue(DecimalValueProperty); set => SetValue(DecimalValueProperty, value); }
        public static readonly DependencyProperty DecimalValueProperty = DependencyProperty.Register(nameof(DecimalValue), typeof(decimal), typeof(MainWindow), new FrameworkPropertyMetadata(1234m));
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(qwer.Value.ToString("yyyy/MM/dd HH:mm:ss", DateTimeFormatInfo.CurrentInfo));
        }
    }
}
