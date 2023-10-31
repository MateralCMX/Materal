namespace RCManagementTool.Pages.User
{
    public sealed partial class UserOptions : UserControl
    {
        /// <summary>
        /// 视图模型
        /// </summary>
        public UserOptionsViewModel ViewModel { get => (UserOptionsViewModel)GetValue(ViewModelProperty); set => SetValue(ViewModelProperty, value); }
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel), typeof(UserOptionsViewModel), typeof(UserOptions), new PropertyMetadata(null));
        public UserOptions() => InitializeComponent();
    }
}
