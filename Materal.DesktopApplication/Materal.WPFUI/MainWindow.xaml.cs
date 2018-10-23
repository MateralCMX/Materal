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
        public string DisplayValue { get => (string)GetValue(DisplayValueProperty); set => SetValue(DisplayValueProperty, value); }
        public static readonly DependencyProperty DisplayValueProperty = DependencyProperty.Register(nameof(DisplayValue),
            typeof(string), typeof(MainWindow),
            new FrameworkPropertyMetadata(
                "12345",
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnValueChanged,
                CoerceText,
                true,
                UpdateSourceTrigger.LostFocus));
        private static void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is MainWindow mainWindow)
            {
            }
        }
        private static object CoerceText(DependencyObject d, object value)
        {
            return value;
        }
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
