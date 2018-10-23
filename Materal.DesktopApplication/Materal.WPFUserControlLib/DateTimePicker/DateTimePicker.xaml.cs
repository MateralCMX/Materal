using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Materal.WPFUserControlLib.DateTimePicker
{
    /// <summary>
    /// DateTimePicker.xaml 的交互逻辑
    /// </summary>
    public partial class DateTimePicker
    {
        /// <summary>
        /// 值
        /// </summary>
        public DateTime Value { get => (DateTime)GetValue(ValueProperty); set => SetValue(ValueProperty, value); }
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value),
            typeof(DateTime), typeof(DateTimePicker), new FrameworkPropertyMetadata(DateTime.Now, OnValueChanged));
        private static void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is DateTimePicker numberBox)) return;
            if (numberBox.Value > numberBox.MaxValue) numberBox.SetValue(ValueProperty, numberBox.MaxValue);
            if (numberBox.Value < numberBox.MinValue) numberBox.SetValue(ValueProperty, numberBox.MinValue);
        }
        /// <summary>
        /// 最大值
        /// </summary>
        public DateTime MaxValue { get => (DateTime)GetValue(MaxValueProperty); set => SetValue(MaxValueProperty, value); }
        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register(nameof(MaxValue),
            typeof(DateTime), typeof(DateTimePicker), new PropertyMetadata(DateTime.MaxValue, OnMaxValueChanged));
        private static void OnMaxValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is DateTimePicker numberBox)) return;
            if (numberBox.Value > numberBox.MaxValue) numberBox.SetValue(ValueProperty, numberBox.MaxValue);
        }

        /// <summary>
        /// 最小值
        /// </summary>
        public DateTime MinValue { get => (DateTime)GetValue(MinValueProperty); set => SetValue(MinValueProperty, value); }
        public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register(nameof(MinValue),
            typeof(DateTime), typeof(DateTimePicker), new PropertyMetadata(DateTime.MinValue, OnMinValueChanged));
        private static void OnMinValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is DateTimePicker numberBox)) return;
            if (numberBox.Value < numberBox.MinValue) numberBox.SetValue(ValueProperty, numberBox.MinValue);
        }

        /// <summary>
        /// 格式化
        /// </summary>
        public string Format { get => (string)GetValue(FormatProperty); set => SetValue(FormatProperty, value); }
        public static readonly DependencyProperty FormatProperty = DependencyProperty.Register(nameof(Format),
            typeof(string), typeof(DateTimePicker), new PropertyMetadata("yyyy/MM/dd HH:mm:ss"));

        public DateTime Date
        {
            get => Value.Date;
            set
            {
                value = value.AddHours(Hour);
                value = value.AddMinutes(Minute);
                value = value.AddSeconds(Second);
                Value = value;
            }
        }
        /// <summary>
        /// 小时
        /// </summary>
        public int Hour
        {
            get => Value.Hour;
            set => Value = new DateTime(Value.Year, Value.Month, Value.Day, value, Value.Minute, Value.Second);
        }
        /// <summary>
        /// 分钟
        /// </summary>
        public int Minute
        {
            get => Value.Minute;
            set => Value = new DateTime(Value.Year, Value.Month, Value.Day, Value.Hour, value, Value.Second);
        }
        /// <summary>
        /// 秒
        /// </summary>
        public int Second
        {
            get => Value.Second;
            set => Value = new DateTime(Value.Year, Value.Month, Value.Day, Value.Hour, Value.Minute, value);
        }
        ///// <summary>
        ///// 小时
        ///// </summary>
        //public int Hour { get => (int)GetValue(HourProperty); set => SetValue(HourProperty, value); }
        //public static readonly DependencyProperty HourProperty = DependencyProperty.Register(nameof(Hour),
        //    typeof(int), typeof(DateTimePicker), new PropertyMetadata(DateTime.Now.Hour));

        /// <summary>
        /// 构造方法
        /// </summary>
        public DateTimePicker()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 切换弹窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwitchPopupCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (IsEnabled)
            {
                PopupPanel.IsOpen = !PopupPanel.IsOpen;
            }
        }
        /// <summary>
        /// 是否可以切换弹窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwitchPopupCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = IsEnabled;
        }
        /// <summary>
        /// 控件加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
        }
    }
}
