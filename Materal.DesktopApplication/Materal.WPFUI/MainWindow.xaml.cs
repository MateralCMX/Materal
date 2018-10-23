using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
    }
}
