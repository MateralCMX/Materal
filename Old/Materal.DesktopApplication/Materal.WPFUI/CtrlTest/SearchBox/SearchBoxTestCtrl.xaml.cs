using System.Windows;
using System.Windows.Controls;
using Model;

namespace Materal.WPFUI.CtrlTest.SearchBox
{
    /// <summary>
    /// SearchBoxTestCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class SearchBoxTestCtrl : UserControl
    {
        public SearchBoxTestCtrl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MyControl.SearchFun = m => m is UserModel userModel && userModel.Name.Contains(MyControl.Text);
            MyControl.SelectedFun = m => m is UserModel userModel && userModel.Name.Equals(MyControl.Text);
        }
    }
}
