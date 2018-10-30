using Model;
using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace Materal.WPFUI.CtrlTest
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
            SearchBox.SearchFun = m => m is UserModel userModel && userModel.Name.Contains(SearchBox.Text);
            SearchBox.SelectedFun = m => m is UserModel userModel && userModel.Name.Equals(SearchBox.Text);
        }
    }
}
