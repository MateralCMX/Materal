using Materal.WPFCommon;
using System;
using System.Windows;

namespace Materal.WPFUserControlLib.DateTimePicker
{
    public class DateTimePickerViewModel : NotifyPropertyChanged
    {
        #region 属性
        /// <summary>
        /// 内边距
        /// </summary>
        private Thickness _textPadding;
        /// <summary>
        /// 内边距
        /// </summary>
        public Thickness TextPadding
        {
            get => _textPadding;
            set
            {
                _textPadding = value;
                OnPropertyChanged(nameof(TextPadding));
            }
        }

        /// <summary>
        /// 值
        /// </summary>
        private DateTime _value = DateTime.Now;
        /// <summary>
        /// 值
        /// </summary>
        public DateTime Value
        {
            get => _value;
            set
            {
                _value = value;
                if (_value > _maxValue) _value = _maxValue;
                if (_value < _mintValue) _value = _mintValue;
                OnPropertyChanged(nameof(Value));
                OnPropertyChanged(nameof(ShowValue));
                OnPropertyChanged(nameof(Minute));
                OnPropertyChanged(nameof(Second));
            }
        }

        /// <summary>
        /// 最大值
        /// </summary>
        private DateTime _maxValue = DateTime.MaxValue;
        /// <summary>
        /// 最大值
        /// </summary>
        public DateTime MaxValue
        {
            get => _maxValue;
            set
            {
                _maxValue = value;
                if (_value > _maxValue) _value = _maxValue;
                OnPropertyChanged(nameof(Value));
                OnPropertyChanged(nameof(MaxValue));
                OnPropertyChanged(nameof(ShowValue));
            }
        }

        /// <summary>
        /// 最小值
        /// </summary>
        private DateTime _mintValue = DateTime.MinValue;
        /// <summary>
        /// 最小值
        /// </summary>
        public DateTime MinValue
        {
            get => _mintValue;
            set
            {
                _mintValue = value;
                if (_value < _mintValue) _value = _mintValue;
                OnPropertyChanged(nameof(Value));
                OnPropertyChanged(nameof(MaxValue));
                OnPropertyChanged(nameof(ShowValue));
            }
        }

        /// <summary>
        /// 格式化
        /// </summary>
        private string _format = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 格式化
        /// </summary>
        public string Format
        {
            get => _format;
            set
            {
                _format = value;
                OnPropertyChanged(nameof(ShowValue));
            }
        }

        /// <summary>
        /// 显示的值
        /// </summary>
        public string ShowValue => Value.ToString(_format);

        /// <summary>
        /// 小时
        /// </summary>
        public int Hour
        {
            get => Value.Hour;
            set
            {
                Value = new DateTime(Value.Year, Value.Month, Value.Day, value, Value.Minute, Value.Second);
                OnPropertyChanged(nameof(Hour));
            }
        }

        /// <summary>
        /// 分钟
        /// </summary>
        public int Minute
        {
            get => Value.Hour;
            set => Value = new DateTime(Value.Year, Value.Month, Value.Day, Value.Hour, value, Value.Second);
        }

        /// <summary>
        /// 秒
        /// </summary>
        public int Second
        {
            get => Value.Hour;
            set => Value = new DateTime(Value.Year, Value.Month, Value.Day, Value.Hour, Value.Minute, value);
        }

        #endregion
    }
}
