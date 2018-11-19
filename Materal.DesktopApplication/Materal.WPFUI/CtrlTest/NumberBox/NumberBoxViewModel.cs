using System.Windows;
using Materal.WPFCommon;
using Materal.WPFCustomControlLib.NumberBox;

namespace Materal.WPFUI.CtrlTest.NumberBox
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
        private CornerRadius _cornerRadius = new CornerRadius(0);
        private double _topLeftCornerRadius;
        private double _bottomLeftCornerRadius;
        private double _topRightCornerRadius;
        private double _bottomRightCornerRadius;
        private double _buttonWidth = 10;

        /// <summary>
        /// 圆角
        /// </summary>
        public CornerRadius CornerRadius
        {
            get
            {
                _cornerRadius.TopLeft = TopLeftCornerRadius;
                _cornerRadius.TopRight = TopRightCornerRadius;
                _cornerRadius.BottomRight = BottomRightCornerRadius;
                _cornerRadius.BottomLeft = BottomLeftCornerRadius;
                return _cornerRadius;
            }
        }
        /// <summary>
        /// 左上圆角
        /// </summary>
        public double TopLeftCornerRadius
        {
            get => _topLeftCornerRadius;
            set
            {
                _topLeftCornerRadius = value;
                OnPropertyChanged(nameof(CornerRadius));
                OnPropertyChanged(nameof(TopLeftCornerRadius));
            }
        }
        /// <summary>
        /// 左下圆角
        /// </summary>
        public double BottomLeftCornerRadius
        {
            get => _bottomLeftCornerRadius;
            set
            {
                _bottomLeftCornerRadius = value;
                OnPropertyChanged(nameof(CornerRadius));
                OnPropertyChanged(nameof(BottomLeftCornerRadius));
            }
        }
        /// <summary>
        /// 右上圆角
        /// </summary>
        public double TopRightCornerRadius
        {
            get => _topRightCornerRadius;
            set
            {
                _topRightCornerRadius = value;
                OnPropertyChanged(nameof(CornerRadius));
                OnPropertyChanged(nameof(TopRightCornerRadius));
            }
        }
        /// <summary>
        /// 右下圆角
        /// </summary>
        public double BottomRightCornerRadius
        {
            get => _bottomRightCornerRadius;
            set
            {
                _bottomRightCornerRadius = value;
                OnPropertyChanged(nameof(CornerRadius));
                OnPropertyChanged(nameof(BottomRightCornerRadius));
            }
        }

        /// <summary>
        /// 按钮宽度
        /// </summary>
        public GridLength ButtonGridWidth => new GridLength(ButtonWidth);

        /// <summary>
        /// 按钮宽度
        /// </summary>
        public double ButtonWidth
        {
            get => _buttonWidth;
            set
            {
                _buttonWidth = value;
                OnPropertyChanged(nameof(ButtonGridWidth));
                OnPropertyChanged(nameof(ButtonWidth));
            }
        }
    }
}
