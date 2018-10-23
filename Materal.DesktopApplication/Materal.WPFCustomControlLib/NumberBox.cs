using System.Windows;
using System.Windows.Controls;

namespace Materal.WPFCustomControlLib
{
    public class NumberBox : Control
    {
        /// <summary>
        /// 值
        /// </summary>
        public decimal Value { get => (decimal)GetValue(ValueProperty); set => SetValue(ValueProperty, value); }
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(decimal), typeof(NumberBox), new FrameworkPropertyMetadata(0m, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal, OnValueChanged));
        private static void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is NumberBox numberBox)) return;
            decimal placesValue = decimal.Round(numberBox.Value, numberBox.DecimalPlaces);
            if (!placesValue.Equals(numberBox.Value)) numberBox.SetValue(ValueProperty, placesValue);
            if (numberBox.Value > numberBox.MaxValue) numberBox.SetValue(ValueProperty, numberBox.MaxValue);
            if (numberBox.Value < numberBox.MinValue) numberBox.SetValue(ValueProperty, numberBox.MinValue);
        }
        /// <summary>
        /// 最大值
        /// </summary>
        public decimal MaxValue { get => (decimal)GetValue(MaxValueProperty); set => SetValue(MaxValueProperty, value); }
        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register(nameof(MaxValue),
            typeof(decimal), typeof(NumberBox), new PropertyMetadata(decimal.MaxValue, OnMaxValueChanged));
        private static void OnMaxValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is NumberBox numberBox)) return;
            if (numberBox.Value > numberBox.MaxValue) numberBox.SetValue(ValueProperty, numberBox.MaxValue);
        }

        /// <summary>
        /// 最小值
        /// </summary>
        public decimal MinValue { get => (decimal)GetValue(MinValueProperty); set => SetValue(MinValueProperty, value); }
        public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register(nameof(MinValue),
            typeof(decimal), typeof(NumberBox), new PropertyMetadata(decimal.MinValue, OnMinValueChanged));
        private static void OnMinValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is NumberBox numberBox)) return;
            if (numberBox.Value < numberBox.MinValue) numberBox.SetValue(ValueProperty, numberBox.MinValue);
        }

        /// <summary>
        /// 递增递减的值
        /// </summary>
        public decimal Increment { get => (decimal)GetValue(IncrementProperty); set => SetValue(IncrementProperty, value); }
        public static readonly DependencyProperty IncrementProperty = DependencyProperty.Register(nameof(Increment),
            typeof(decimal), typeof(NumberBox), new PropertyMetadata(1m));


        /// <summary>
        /// 保留位数
        /// </summary>
        public int DecimalPlaces { get => (int)GetValue(DecimalPlacesProperty); set => SetValue(DecimalPlacesProperty, value); }
        public static readonly DependencyProperty DecimalPlacesProperty = DependencyProperty.Register(nameof(DecimalPlaces),
            typeof(int), typeof(NumberBox), new PropertyMetadata(0));
        /// <inheritdoc />
        /// <summary>
        /// 构造方法
        /// </summary>
        static NumberBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumberBox), new FrameworkPropertyMetadata(typeof(NumberBox)));
        }
    }
}
