using System.Windows;
using System.Windows.Controls;

namespace Materal.WPFCustomControlLib.CornerRadiusButton
{
    public class CornerRadiusButton : Button
    {
        /// <summary>
        /// 边框圆角
        /// </summary>
        public CornerRadius CornerRadius { get => (CornerRadius)GetValue(CornerRadiusProperty); set => SetValue(CornerRadiusProperty, value); }
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(CornerRadiusButton),
            new FrameworkPropertyMetadata(
                new CornerRadius(0),
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal));
        /// <inheritdoc />
        /// <summary>
        /// 构造方法
        /// </summary>
        static CornerRadiusButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerRadiusButton), new FrameworkPropertyMetadata(typeof(CornerRadiusButton)));
        }
    }
}
