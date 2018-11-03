using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Materal.WPFUI.CtrlTest.DateTimePicker
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

        private void UpdateViewModelValueCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var random = new Random();
            int year = random.Next(1900, 3000);
            int month = random.Next(1, 13);
            int day = random.Next(1, 29);
            int hour = random.Next(1, 24);
            int minute = random.Next(1, 60);
            int second = random.Next(1, 60);
            ViewModel.TestValue = new DateTime(year, month, day, hour, minute, second);
        }
    }
}
