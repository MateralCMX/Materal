using System.Numerics;

namespace RCManagementTool.Controls
{
    public partial class DrawerPanelViewModel : ObservableObject
    {
        /// <summary>
        /// 加载遮罩层
        /// </summary>
        public LoadingMaskViewModel LoadingMask { get; } = new();
        /// <summary>
        /// 抽屉面板显示状态
        /// </summary>
        [ObservableProperty]
        private Visibility _visibility = Visibility.Collapsed;
        /// <summary>
        /// 抽屉面板最小宽度
        /// </summary>
        [ObservableProperty, NotifyPropertyChangedFor(nameof(Translation))]
        private double _minWidth;
        /// <summary>
        /// 默认宽度
        /// </summary>
        public double DefaultMinWidth { get; set; } = 400;
        /// <summary>
        /// 抽屉面板位移
        /// </summary>
        public Vector3 Translation => new(Convert.ToSingle(MinWidth), 0, 0);
    }
}
