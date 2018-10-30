using System;
using Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Materal.WPFUI.CtrlTest
{
    /// <summary>
    /// TestCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class TestCtrl : UserControl
    {
        public TestCtrl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //SearchBox.SearchFun = m => m is UserModel userModel && userModel.Name.Contains(SearchBox.Text);
            //var data = new List<object>();
            //for (var i = 0; i < 100000; i++)
            //{
            //    data.Add(new UserModel
            //    {
            //        ID = Guid.NewGuid(),
            //        Name = "云A" + i
            //    });
            //}
            //SearchBox.CandidateData = data;
        }
    }
}
