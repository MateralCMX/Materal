using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using Materal.WPFCommon;
using Model;

namespace Materal.WPFUI.CtrlTest.SearchBox
{
    public class SearchBoxViewModel : NotifyPropertyChanged
    {
        private readonly Timer _addDataTimer = new Timer(1000);

        public List<UserModel> DataSource { get; set; } = new List<UserModel>();

        public UserModel SelectedData { get; set; }

        public SearchBoxViewModel()
        {
            _addDataTimer.Elapsed += _addDataTimer_Elapsed;
            for (var i = 0; i < 100; i++)
            {
                DataSource.Add(new UserModel
                {
                    ID = Guid.NewGuid(),
                    Name = "云A" + DataSource.Count
                });
            }
            //_addDataTimer.Start();
        }

        private void _addDataTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            DataSource.Add(new UserModel
            {
                ID = Guid.NewGuid(),
                Name = "云A" + DataSource.Count
            });
            OnPropertyChanged(nameof(DataSource));
        }

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
