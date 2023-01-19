using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Materal.WPFCustomControlLib.ImageButton
{
    public class ImageButton : ButtonBase
    {
        /// <summary>
        /// 图标路径
        /// </summary>
        public ImageSource ImagePath { get => (ImageSource)GetValue(ImagePathProperty); set => SetValue(ImagePathProperty, value); }
        public static readonly DependencyProperty ImagePathProperty = DependencyProperty.Register(nameof(ImagePath), typeof(ImageSource), typeof(ImageButton));
        /// <summary>
        /// 图标路径
        /// </summary>
        public ImageSource HoverImagePath { get => (ImageSource)GetValue(HoverImagePathProperty); set => SetValue(HoverImagePathProperty, value); }
        public static readonly DependencyProperty HoverImagePathProperty = DependencyProperty.Register(nameof(HoverImagePath), typeof(ImageSource), typeof(ImageButton), new PropertyMetadata(null));
        /// <summary>
        /// 鼠标移动上去的颜色
        /// </summary>
        public Brush HoverColor { get => (Brush)GetValue(HoverColorProperty); set => SetValue(HoverColorProperty, value); }
        public static readonly DependencyProperty HoverColorProperty = DependencyProperty.Register(nameof(HoverColor), typeof(Brush), typeof(ImageButton));
        /// <summary>
        /// 图片高度
        /// </summary>
        public double ImageHeight { get => (double)GetValue(ImageHeightProperty); set => SetValue(ImageHeightProperty, value); }
        public static readonly DependencyProperty ImageHeightProperty = DependencyProperty.Register(nameof(ImageHeight), typeof(double), typeof(ImageButton), new PropertyMetadata(15d));
        /// <summary>
        /// 图片宽度
        /// </summary>
        public double ImageWidth { get => (double)GetValue(ImageWidthProperty); set => SetValue(ImageWidthProperty, value); }
        public static readonly DependencyProperty ImageWidthProperty = DependencyProperty.Register(nameof(ImageWidth), typeof(double), typeof(ImageButton), new PropertyMetadata(15d));
        /// <inheritdoc />
        /// <summary>
        /// 构造方法
        /// </summary>
        static ImageButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageButton), new FrameworkPropertyMetadata(typeof(ImageButton)));
        }
    }
}
