using Materal.WPFCommon;

namespace Materal.WPFUI.CtrlTest
{
    public class NumberBoxViewModel : NotifyPropertyChanged
    {
        /// <summary>
        /// 测试值
        /// </summary>
        private decimal _testValue = 40;

        /// <summary>
        /// 测试值
        /// </summary>
        public decimal TestValue
        {
            get => _testValue;
            set
            {
                _testValue = value;
                OnPropertyChanged(nameof(TestValue));
            }
        }
    }
}
