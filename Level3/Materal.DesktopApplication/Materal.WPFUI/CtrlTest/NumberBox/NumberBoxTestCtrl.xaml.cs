using System;
using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace Materal.WPFUI.CtrlTest.NumberBox
{
    /// <summary>
    /// NumberBoxTestCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class NumberBoxTestCtrl
    {
        public NumberBoxTestCtrl()
        {
            InitializeComponent();
        }

        private void GetViewModelValueCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show(ViewModel.TestValue.ToString(CultureInfo.InvariantCulture), "获取结果");
        }

        private void UpdateViewModelValueCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.TestValue = new Random().Next((int)MyControl.MinValue, (int)MyControl.MaxValue);
        }
    }
}
