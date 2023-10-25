namespace RCManagementTool.Pages.Layer
{
    public sealed partial class NavigationPage : Page
    {
        private Type? _nowPageType;
        public NavigationPage()
        {
            InitializeComponent();
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem is not NavigationViewItem item) return;
            string typeName = $"RCManagementTool.Pages.{item.Tag}";
            Type? type = Type.GetType(typeName);
            if (type is null || _nowPageType == type) return;
            _nowPageType = type;
            contentFrame.Navigate(type);
        }
    }
}
