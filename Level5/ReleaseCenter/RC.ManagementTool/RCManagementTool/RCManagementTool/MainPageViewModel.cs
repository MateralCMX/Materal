namespace RCManagementTool
{
    public partial class MainPageViewModel : ObservableObject
    {
        /// <summary>
        /// 提示框是否打开
        /// </summary>
        [ObservableProperty]
        private bool _infoBarIsOpen = false;
        /// <summary>
        /// 提示框标题
        /// </summary>
        [ObservableProperty]
        private string _infoBarTitle = string.Empty;
        /// <summary>
        /// 提示框信息
        /// </summary>
        [ObservableProperty]
        private string _infoBarMessage = string.Empty;
        /// <summary>
        /// 提示框严重性
        /// </summary>
        [ObservableProperty]
        private InfoBarSeverity _infoBarSeverity = InfoBarSeverity.Informational;
        /// <summary>
        /// 加载遮罩层显示状态
        /// </summary>
        [ObservableProperty]
        private Visibility _loadingMaskVisibility = Visibility.Collapsed;
        /// <summary>
        /// 加载遮罩层信息
        /// </summary>
        [ObservableProperty]
        private string _loadingMaskMessage = string.Empty;
    }
}
