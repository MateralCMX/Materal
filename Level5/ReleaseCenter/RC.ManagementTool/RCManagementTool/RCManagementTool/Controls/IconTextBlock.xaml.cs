namespace RCManagementTool.Controls
{
    public sealed partial class IconTextBlock : UserControl
    {
        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(IconTextBlock), new PropertyMetadata(string.Empty));
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get => (string)GetValue(IconProperty); set => SetValue(IconProperty, value); }
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon), typeof(string), typeof(IconTextBlock), new PropertyMetadata(string.Empty));
        public IconTextBlock() => InitializeComponent();
    }
}
