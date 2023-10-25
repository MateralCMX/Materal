namespace RCManagementTool.Pages.User
{
    public sealed partial class UserListPage : Page
    {
        public UserListPage()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e) => await ViewModel.LoadDataAsync();
    }
}
