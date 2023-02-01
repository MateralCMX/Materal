using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Materal.StringHelper;

namespace Materal.WPFUI.CtrlTest.CornerRadiusTextBox
{
    /// <summary>
    /// CornerRadiusTextBoxTestCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class CornerRadiusTextBoxTestCtrl : UserControl
    {
        public CornerRadiusTextBoxTestCtrl()
        {
            InitializeComponent();
        }

        private void GetViewModelValueCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show(ViewModel.TestValue.ToString(CultureInfo.InvariantCulture), "获取结果");
        }

        private void UpdateViewModelValueCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.TestValue = StringManager.GetRandomStrByDictionary(10);
        }
    }
}
