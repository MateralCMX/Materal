using System;
using Materal.WPFCommon;

namespace Materal.WPFUI.CtrlTest
{
    public class DateTimePickerViewModel : NotifyPropertyChanged
    {
        /// <summary>
        /// 测试值
        /// </summary>
        private DateTime _testValue = new DateTime(1993, 4, 20, 8, 22, 43);

        /// <summary>
        /// 测试值
        /// </summary>
        public DateTime TestValue
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
