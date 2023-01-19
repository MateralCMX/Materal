using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Materal.WPFCustomControlLib.DateTimePicker
{
    public class DateTimePicker : Control
    {
        /// <summary>
        /// 边框圆角
        /// </summary>
        public CornerRadius CornerRadius { get => (CornerRadius)GetValue(CornerRadiusProperty); set => SetValue(CornerRadiusProperty, value); }
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(DateTimePicker),
            new FrameworkPropertyMetadata(
                new CornerRadius(0),
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal));
        /// <summary>
        /// 显示值
        /// </summary>
        public string ShowValue { get => (string)GetValue(ShowValueProperty); set => SetValue(ShowValueProperty, value); }
        public static readonly DependencyProperty ShowValueProperty = DependencyProperty.Register(nameof(ShowValue),
            typeof(string), typeof(DateTimePicker), new FrameworkPropertyMetadata(
                string.Empty,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal
            ));
        /// <summary>
        /// 值
        /// </summary>
        public DateTime Value { get => (DateTime)GetValue(ValueProperty); set => SetValue(ValueProperty, value); }
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value),
            typeof(DateTime), typeof(DateTimePicker), new FrameworkPropertyMetadata(
                DateTime.Now,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal, 
                OnValueChanged));
        private static void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is DateTimePicker dateTimePicker)) return;
            if (dateTimePicker.Value > dateTimePicker.MaxValue) dateTimePicker.SetValue(ValueProperty, dateTimePicker.MaxValue);
            if (dateTimePicker.Value < dateTimePicker.MinValue) dateTimePicker.SetValue(ValueProperty, dateTimePicker.MinValue);
            dateTimePicker.ShowValue =
                dateTimePicker.Value.ToString(dateTimePicker.Format, DateTimeFormatInfo.InvariantInfo);
            if (dateTimePicker.GetTemplateChild("CalendarDate") is System.Windows.Controls.Calendar calendarDate)
            {
                calendarDate.DisplayDate = dateTimePicker.Value.Date;
                calendarDate.SelectedDate = dateTimePicker.Value.Date;
            }
            if (dateTimePicker.GetTemplateChild("NumberHour") is NumberBox.NumberBox numberHour)
            {
                numberHour.Value = dateTimePicker.Value.Hour;
            }
            if (dateTimePicker.GetTemplateChild("NumberMinute") is NumberBox.NumberBox numberMinute)
            {
                numberMinute.Value = dateTimePicker.Value.Minute;
            }
            if (dateTimePicker.GetTemplateChild("NumberSecond") is NumberBox.NumberBox numberSecond)
            {
                numberSecond.Value = dateTimePicker.Value.Second;
            }
        }
        /// <summary>
        /// 最大值
        /// </summary>
        public DateTime MaxValue { get => (DateTime)GetValue(MaxValueProperty); set => SetValue(MaxValueProperty, value); }
        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register(nameof(MaxValue),
            typeof(DateTime), typeof(DateTimePicker), new FrameworkPropertyMetadata(
                DateTime.MaxValue,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal,
                OnMaxValueChanged));
        private static void OnMaxValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is DateTimePicker dateTimePicker)) return;
            if (dateTimePicker.Value > dateTimePicker.MaxValue) dateTimePicker.SetValue(ValueProperty, dateTimePicker.MaxValue);
        }
        /// <summary>
        /// 最小值
        /// </summary>
        public DateTime MinValue { get => (DateTime)GetValue(MinValueProperty); set => SetValue(MinValueProperty, value); }
        public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register(nameof(MinValue),
            typeof(DateTime), typeof(DateTimePicker), new FrameworkPropertyMetadata(
                DateTime.MinValue,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal,
                OnMinValueChanged));
        private static void OnMinValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is DateTimePicker dateTimePicker)) return;
            if (dateTimePicker.Value < dateTimePicker.MinValue) dateTimePicker.SetValue(ValueProperty, dateTimePicker.MinValue);
        }
        /// <summary>
        /// 格式化
        /// </summary>
        public string Format { get => (string)GetValue(FormatProperty); set => SetValue(FormatProperty, value); }
        public static readonly DependencyProperty FormatProperty = DependencyProperty.Register(nameof(Format),
            typeof(string), typeof(DateTimePicker), new FrameworkPropertyMetadata(
                "yyyy/MM/dd HH:mm:ss",
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal,
                OnFormatChanged));
        private static void OnFormatChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is DateTimePicker dateTimePicker)) return;
            dateTimePicker.ShowValue = 
                dateTimePicker.Value.ToString(dateTimePicker.Format, DateTimeFormatInfo.InvariantInfo);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if(GetTemplateChild("BtnOpenPopup") is Button btnOpenPopup) btnOpenPopup.Click += BtnOpenPopup_Click;
            if (GetTemplateChild("CalendarDate") is System.Windows.Controls.Calendar calendarDate)
            {
                calendarDate.SelectedDatesChanged += CalendarDate_SelectedDatesChanged;
                calendarDate.DisplayDate = Value.Date;
                calendarDate.SelectedDate = Value.Date;
            }

            if (GetTemplateChild("NumberHour") is NumberBox.NumberBox numberHour)
            {
                numberHour.OnValueChange += NumberHour_OnValueChange;
                numberHour.Value = Value.Hour;
            }
            if (GetTemplateChild("NumberMinute") is NumberBox.NumberBox numberMinute)
            {
                numberMinute.OnValueChange += NumberMinute_OnValueChange;
                numberMinute.Value = Value.Minute;
            }
            if (GetTemplateChild("NumberSecond") is NumberBox.NumberBox numberSecond)
            {
                numberSecond.OnValueChange += NumberSecond_OnValueChange;
                numberSecond.Value = Value.Second;
            }
        }

        private void NumberSecond_OnValueChange(NumberBox.NumberBox sender, decimal value)
        {
            Value = new DateTime(Value.Year, Value.Month, Value.Day, Value.Hour, Value.Minute, (int)value);
        }

        private void NumberMinute_OnValueChange(NumberBox.NumberBox sender, decimal value)
        {
            Value = new DateTime(Value.Year, Value.Month, Value.Day, Value.Hour, (int)value, Value.Second);
        }

        private void NumberHour_OnValueChange(NumberBox.NumberBox sender, decimal value)
        {
            Value = new DateTime(Value.Year, Value.Month, Value.Day, (int)value, Value.Minute, Value.Second);
        }

        private void CalendarDate_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(sender is System.Windows.Controls.Calendar calendarDate)) return;
            if (!calendarDate.SelectedDate.HasValue) return;
            DateTime dt = calendarDate.SelectedDate.Value;
            dt = dt.AddHours(Value.Hour);
            dt = dt.AddMinutes(Value.Minute);
            dt = dt.AddSeconds(Value.Second);
            Value = dt;
        }

        private void BtnOpenPopup_Click(object sender, RoutedEventArgs e)
        {
            if (IsEnabled && GetTemplateChild("PopupPanel") is Popup popupPanel)
            {
                popupPanel.IsOpen = !popupPanel.IsOpen;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// 构造方法
        /// </summary>
        static DateTimePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DateTimePicker), new FrameworkPropertyMetadata(typeof(DateTimePicker)));
        }
    }
}
