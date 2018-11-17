using Materal.StringHelper;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Materal.WPFCustomControlLib.NumberBox
{
    public class NumberBox : Control
    {
        public event Action<NumberBox, decimal> OnValueChange;
        /// <summary>
        /// 按钮宽度
        /// </summary>
        public GridLength ButtonWidth { get => (GridLength)GetValue(ButtonWidthProperty); set => SetValue(ButtonWidthProperty, value); }
        public static readonly DependencyProperty ButtonWidthProperty = DependencyProperty.Register(nameof(ButtonWidth),
            typeof(GridLength), typeof(NumberBox), new FrameworkPropertyMetadata(new GridLength(10)));

        /// <summary>
        /// 按钮宽度
        /// </summary>
        public bool IsReadOnly { get => (bool)GetValue(IsReadOnlyProperty); set => SetValue(IsReadOnlyProperty, value); }
        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register(nameof(IsReadOnly),
            typeof(bool), typeof(NumberBox), new FrameworkPropertyMetadata(false));
        /// <summary>
        /// 边框圆角
        /// </summary>
        public CornerRadius BorderCornerRadius { get => (CornerRadius)GetValue(BorderCornerRadiusProperty); set => SetValue(BorderCornerRadiusProperty, value); }
        public static readonly DependencyProperty BorderCornerRadiusProperty = DependencyProperty.Register(nameof(BorderCornerRadius), typeof(CornerRadius), typeof(NumberBox),
            new FrameworkPropertyMetadata(
                new CornerRadius(2),
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal));
        /// <summary>
        /// 左边边框圆角
        /// </summary>
        public CornerRadius LeftBorderCornerRadius { get => (CornerRadius)GetValue(LeftBorderCornerRadiusProperty); set => SetValue(LeftBorderCornerRadiusProperty, value); }
        public static readonly DependencyProperty LeftBorderCornerRadiusProperty = DependencyProperty.Register(nameof(LeftBorderCornerRadius), typeof(CornerRadius), typeof(NumberBox),
            new FrameworkPropertyMetadata(
                new CornerRadius(1, 0, 0, 1),
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal));

        /// <summary>
        /// 值
        /// </summary>
        public decimal Value { get => (decimal)GetValue(ValueProperty); set => SetValue(ValueProperty, value); }
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(decimal), typeof(NumberBox),
            new FrameworkPropertyMetadata(
                0m,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal,
                OnValueChanged));
        private static void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is NumberBox numberBox)) return;
            decimal placesValue = decimal.Round(numberBox.Value, numberBox.DecimalPlaces);
            if (!placesValue.Equals(numberBox.Value)) numberBox.SetValue(ValueProperty, placesValue);
            if (numberBox.Value > numberBox.MaxValue) numberBox.SetValue(ValueProperty, numberBox.MaxValue);
            if (numberBox.Value < numberBox.MinValue) numberBox.SetValue(ValueProperty, numberBox.MinValue);
            numberBox.OnValueChange?.Invoke(numberBox, numberBox.Value);
        }
        /// <summary>
        /// 最大值
        /// </summary>
        public decimal MaxValue { get => (decimal)GetValue(MaxValueProperty); set => SetValue(MaxValueProperty, value); }
        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register(nameof(MaxValue),
            typeof(decimal), typeof(NumberBox), new FrameworkPropertyMetadata(
                decimal.MaxValue,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal,
                OnMaxValueChanged));
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
            typeof(decimal), typeof(NumberBox), new FrameworkPropertyMetadata(
                decimal.MinValue,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal,
                OnMinValueChanged));
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
            typeof(decimal), typeof(NumberBox), new FrameworkPropertyMetadata(
                1m,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal));

        /// <summary>
        /// 保留位数
        /// </summary>
        public int DecimalPlaces { get => (int)GetValue(DecimalPlacesProperty); set => SetValue(DecimalPlacesProperty, value); }
        public static readonly DependencyProperty DecimalPlacesProperty = DependencyProperty.Register(nameof(DecimalPlaces),
            typeof(int), typeof(NumberBox), new FrameworkPropertyMetadata(
                0,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal,
                OnDecimalPlacesChanged));
        private static void OnDecimalPlacesChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is NumberBox numberBox)) return;
            decimal placesValue = decimal.Round(numberBox.Value, numberBox.DecimalPlaces);
            if (!placesValue.Equals(numberBox.Value)) numberBox.SetValue(ValueProperty, placesValue);
        }
        /// <summary>
        /// 应用模版
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (GetTemplateChild("BtnUp") is Path btnUp) btnUp.MouseLeftButtonDown += BtnUp_Click;
            if (GetTemplateChild("BtnDown") is Path btnDown) btnDown.MouseLeftButtonDown += BtnDown_Click;
            if (GetTemplateChild("RootElement") is Grid rootElement) rootElement.MouseWheel += RootElement_MouseWheel;
            if (GetTemplateChild("TextValue") is TextBox textValue) textValue.LostFocus += TextValue_LostFocus;
        }

        /// <summary>
        /// 值失去焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextValue_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!(sender is TextBox textValue)) return;
            if (textValue.Text.IsNumber())
            {
                Value = Convert.ToDecimal(textValue.Text);
            }
            else
            {
                MatchCollection matchCollection = textValue.Text.GetNumberInStr();
                Value = matchCollection.Count > 0 ? Convert.ToDecimal(matchCollection[0].Value) : 0;
            }
        }

        /// <summary>
        /// 鼠标滚轮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RootElement_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                AddValue();
            }
            else
            {
                DownValue();
            }
        }
        /// <summary>
        /// 增加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUp_Click(object sender, RoutedEventArgs e)
        {
            AddValue();
        }
        /// <summary>
        /// 减少按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDown_Click(object sender, RoutedEventArgs e)
        {
            DownValue();
        }
        /// <summary>
        /// 添加值
        /// </summary>
        private void AddValue()
        {
            if (Value + Increment <= MaxValue)
            {
                Value += Increment;
            }
        }
        /// <summary>
        /// 减少值
        /// </summary>
        private void DownValue()
        {
            if (Value - Increment >= MinValue)
            {
                Value -= Increment;
            }
        }

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
