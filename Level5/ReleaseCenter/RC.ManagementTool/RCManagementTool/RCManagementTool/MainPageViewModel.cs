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
    }
}
