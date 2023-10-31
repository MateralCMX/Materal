using Microsoft.UI.Xaml.Input;
using Newtonsoft.Json.Linq;
using System.Numerics;

namespace RCManagementTool.Controls
{
    public sealed partial class DrawerPanel : UserControl
    {
        /// <summary>
        /// 视图模型
        /// </summary>
        public DrawerPanelViewModel ViewModel
        {
            get => (DrawerPanelViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                UpdateWidth();
            }
        }
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel), typeof(DrawerPanelViewModel), typeof(DrawerPanel), new PropertyMetadata(null));
        /// <summary>
        /// 面板内容
        /// </summary>
        public object PanelContent
        {
            get => GetValue(PanelContentProperty);
            set
            {
                SetValue(PanelContentProperty, value);
                UpdateWidth();
            }
        }
        public static readonly DependencyProperty PanelContentProperty = DependencyProperty.Register(nameof(PanelContent), typeof(object), typeof(DrawerPanel), new PropertyMetadata(null));
        public DrawerPanel()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 抽屉遮罩点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawerMask_PointerPressed(object sender, PointerRoutedEventArgs e) => ViewModel.Visibility = Visibility.Collapsed;
        /// <summary>
        /// 更新宽度
        /// </summary>
        private void UpdateWidth()
        {
            if (ViewModel is null || PanelContent is not FrameworkElement element) return;
            if (element.Width is not double.NaN && element.Width is not double.PositiveInfinity && element.Width is not double.NegativeInfinity)
            {
                ViewModel.MinWidth = element.Width;
            }
            else if (element.MinWidth !=0 && element.MinWidth is not double.NaN && element.MinWidth is not double.PositiveInfinity && element.MinWidth is not double.NegativeInfinity)
            {
                ViewModel.MinWidth = element.MinWidth;
            }
            else if (element.MaxWidth is not double.NaN && element.MaxWidth is not double.PositiveInfinity && element.MaxWidth is not double.NegativeInfinity)
            {
                ViewModel.MinWidth = element.MaxWidth;
            }
            else
            {
                ViewModel.MinWidth = ViewModel.DefaultMinWidth;
            }
        }
    }
}
