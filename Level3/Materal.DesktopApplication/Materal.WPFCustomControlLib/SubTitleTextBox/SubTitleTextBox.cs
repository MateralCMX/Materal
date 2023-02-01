using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Materal.WPFCustomControlLib.SubTitleTextBox
{
    public class SubTitleTextBox : Control
    {
        /// <summary>
        /// 显示文本
        /// </summary>
        public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(SubTitleTextBox),
            new FrameworkPropertyMetadata(
                string.Empty,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal));
        /// <summary>
        /// 副标题
        /// </summary>
        public string SubTitle { get => (string)GetValue(SubTitleProperty); set => SetValue(SubTitleProperty, value); }
        public static readonly DependencyProperty SubTitleProperty = DependencyProperty.Register(nameof(SubTitle), typeof(string), typeof(SubTitleTextBox),
            new FrameworkPropertyMetadata(
                string.Empty,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal));
        /// <summary>
        /// 边框圆角
        /// </summary>
        public CornerRadius BorderCornerRadius { get => (CornerRadius)GetValue(BorderCornerRadiusProperty); set => SetValue(BorderCornerRadiusProperty, value); }
        public static readonly DependencyProperty BorderCornerRadiusProperty = DependencyProperty.Register(nameof(BorderCornerRadius), typeof(CornerRadius), typeof(SubTitleTextBox),
            new FrameworkPropertyMetadata(
                new CornerRadius(2),
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal));
        /// <summary>
        /// 左边边框圆角
        /// </summary>
        public CornerRadius LeftBorderCornerRadius { get => (CornerRadius)GetValue(LeftBorderCornerRadiusProperty); set => SetValue(LeftBorderCornerRadiusProperty, value); }
        public static readonly DependencyProperty LeftBorderCornerRadiusProperty = DependencyProperty.Register(nameof(LeftBorderCornerRadius), typeof(CornerRadius), typeof(SubTitleTextBox),
            new FrameworkPropertyMetadata(
                new CornerRadius(1, 0, 0, 1),
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal));
        /// <summary>
        /// 右边边框圆角
        /// </summary>
        public CornerRadius RightBorderCornerRadius { get => (CornerRadius)GetValue(RightBorderCornerRadiusProperty); set => SetValue(RightBorderCornerRadiusProperty, value); }
        public static readonly DependencyProperty RightBorderCornerRadiusProperty = DependencyProperty.Register(nameof(RightBorderCornerRadius), typeof(CornerRadius), typeof(SubTitleTextBox),
            new FrameworkPropertyMetadata(
                new CornerRadius(0, 1, 1, 0),
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal));
        /// <summary>
        /// 副标题宽度
        /// </summary>
        public GridLength SubTitleWidth { get => (GridLength)GetValue(SubTitleWidthProperty); set => SetValue(SubTitleWidthProperty, value); }
        public static readonly DependencyProperty SubTitleWidthProperty = DependencyProperty.Register(nameof(SubTitleWidth), typeof(GridLength), typeof(SubTitleTextBox),
            new FrameworkPropertyMetadata(
                new GridLength(25),
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal));
        /// <summary>
        /// 副标题背景色
        /// </summary>
        public Brush SubTitleBackground { get => (Brush)GetValue(SubTitleBackgroundProperty); set => SetValue(SubTitleBackgroundProperty, value); }
        public static readonly DependencyProperty SubTitleBackgroundProperty = DependencyProperty.Register(nameof(SubTitleBackground), typeof(Brush), typeof(SubTitleTextBox),
            new FrameworkPropertyMetadata(
                new SolidColorBrush(Colors.Gray),
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal));
        /// <summary>
        /// 副标题前景色
        /// </summary>
        public Brush SubTitleForeground { get => (Brush)GetValue(SubTitleForegroundProperty); set => SetValue(SubTitleForegroundProperty, value); }
        public static readonly DependencyProperty SubTitleForegroundProperty = DependencyProperty.Register(nameof(SubTitleForeground), typeof(Brush), typeof(SubTitleTextBox),
            new FrameworkPropertyMetadata(
                new SolidColorBrush(Colors.Gray),
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal));
        /// <inheritdoc />
        /// <summary>
        /// 构造方法
        /// </summary>
        static SubTitleTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SubTitleTextBox), new FrameworkPropertyMetadata(typeof(SubTitleTextBox)));
        }
    }
}
