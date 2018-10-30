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
            var timer = new Timer(1000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ViewModel.AddData(new UserModel
            {
                ID = Guid.NewGuid(),
                Name = "云A" + ViewModel.DataSource.Count
            });
        }
    }
}
