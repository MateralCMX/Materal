using System;
using System.Collections.Generic;
using System.Timers;
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
    }
}
