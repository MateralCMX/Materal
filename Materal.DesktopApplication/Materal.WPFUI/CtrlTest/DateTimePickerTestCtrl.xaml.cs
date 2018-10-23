using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Materal.WPFUI.CtrlTest
{
    /// <summary>
    /// DateTimePickerTestCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class DateTimePickerTestCtrl : UserControl
    {
        public DateTimePickerTestCtrl()
        {
            InitializeComponent();
        }

        private void GetViewModelValueCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show(ViewModel.TestValue.ToString(CultureInfo.InvariantCulture), "获取结果");
        }
    }
}
