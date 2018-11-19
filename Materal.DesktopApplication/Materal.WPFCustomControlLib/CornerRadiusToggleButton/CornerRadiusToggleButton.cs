using System.Windows;
using System.Windows.Controls.Primitives;

namespace Materal.WPFCustomControlLib.CornerRadiusToggleButton
{
    public class CornerRadiusToggleButton : ToggleButton
    {
        /// <summary>
        /// 边框圆角
        /// </summary>
        public CornerRadius CornerRadius { get => (CornerRadius)GetValue(CornerRadiusProperty); set => SetValue(CornerRadiusProperty, value); }
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(CornerRadiusToggleButton),
            new FrameworkPropertyMetadata(
                new CornerRadius(0),
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal));
        /// <inheritdoc />
        /// <summary>
        /// 构造方法
        /// </summary>
        static CornerRadiusToggleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerRadiusToggleButton), new FrameworkPropertyMetadata(typeof(CornerRadiusToggleButton)));
        }
    }
}
