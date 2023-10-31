namespace RCManagementTool.Controls
{
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
