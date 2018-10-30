using Materal.WPFCommon;
using Model;
using System;
using System.Collections.ObjectModel;

namespace Materal.WPFUI.CtrlTest
{
    public class SearchBoxViewModel : NotifyPropertyChanged
    {
        public ObservableCollection<UserModel> DataSource { get; set; } = new ObservableCollection<UserModel>();

        public SearchBoxViewModel()
        {
            for (var i = 0; i < 100000; i++)
            {
                DataSource.Add(new UserModel
                {
                    ID = Guid.NewGuid(),
                    Name = "云A" + DataSource.Count
                });
            }
        }

        public void AddData(UserModel userModel)
        {
            DataSource.Add(userModel);
            OnPropertyChanged(nameof(DataSource));
        }
    }
}
