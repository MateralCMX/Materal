using Materal.WPFCommon;
using System.Windows;

namespace Materal.WPFUI.CtrlTest.CornerRadiusButton
{
    public class CornerRadiusButtonViewModel : NotifyPropertyChanged
    {
        private CornerRadius _cornerRadius = new CornerRadius(0);
        private double _topLeftCornerRadius;
        private double _bottomLeftCornerRadius;
        private double _topRightCornerRadius;
        private double _bottomRightCornerRadius;

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
    }
}
