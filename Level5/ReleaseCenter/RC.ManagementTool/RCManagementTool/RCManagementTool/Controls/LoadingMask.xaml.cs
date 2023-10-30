namespace RCManagementTool.Controls
{
    public sealed partial class LoadingMask : UserControl
    {
        /// <summary>
        /// 视图模型
        /// </summary>
        public LoadingMaskViewModel ViewModel { get => (LoadingMaskViewModel)GetValue(ViewModelProperty); set => SetValue(ViewModelProperty, value); }
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel), typeof(LoadingMaskViewModel), typeof(LoadingMask), new PropertyMetadata(null));
        /// <summary>
        /// 面板内容
        /// </summary>
        public object PanelContent { get => GetValue(PanelContentProperty); set => SetValue(PanelContentProperty, value); }
        public static readonly DependencyProperty PanelContentProperty = DependencyProperty.Register(nameof(PanelContent), typeof(object), typeof(LoadingMask), new PropertyMetadata(null));
        public LoadingMask()
        {
            InitializeComponent();
        }
    }
    public partial class LoadingMaskViewModel : ObservableObject
    {
        /// <summary>
        /// 是否显示
        /// </summary>
        [ObservableProperty, NotifyPropertyChangedFor(nameof(Visibility))]
        private bool _isShow;
        /// <summary>
        /// 消息
        /// </summary>
        [ObservableProperty]
        private string _message = "正在加载";
        /// <summary>
        /// 显示状态
        /// </summary>
        public Visibility Visibility => IsShow ? Visibility.Visible : Visibility.Collapsed;
    }
}
