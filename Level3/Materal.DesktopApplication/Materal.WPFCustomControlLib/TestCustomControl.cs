using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Materal.WPFCustomControlLib
{
    public class TestCustomControl : Control
    {

        public string Value { get => (string)GetValue(ValueProperty); set => SetValue(ValueProperty, value); }
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value),
            typeof(string),
            typeof(TestCustomControl),
            new FrameworkPropertyMetadata(
                string.Empty,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnValueChanged,
                CoerceText,
                true,
                UpdateSourceTrigger.LostFocus));
        private static void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is TestCustomControl userControl)
            {
                userControl.Value = (string)e.NewValue;
            }
        }
        private static object CoerceText(DependencyObject d, object value)
        {
            return value;
        }
        /// <inheritdoc />
        /// <summary>
        /// 构造方法
        /// </summary>
        static TestCustomControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TestCustomControl), new FrameworkPropertyMetadata(typeof(TestCustomControl)));
        }
    }
}
