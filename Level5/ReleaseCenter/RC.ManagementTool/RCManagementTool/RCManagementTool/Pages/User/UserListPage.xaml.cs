namespace RCManagementTool.Pages.User
{
    public sealed partial class UserListPage : Page
    {
        public UserListPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) => ViewModel.LoadData(1);
    }
}
