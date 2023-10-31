using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls.Primitives;

namespace RCManagementTool.Controls
{
    public sealed partial class Paging : UserControl
    {
        /// <summary>
        /// 视图模型
        /// </summary>
        public PagingViewModel ViewModel { get => (PagingViewModel)GetValue(ViewModelProperty); set => SetValue(ViewModelProperty, value); }
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel), typeof(PagingViewModel), typeof(Paging), new PropertyMetadata(null));
        public Paging()
        {
            InitializeComponent();
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not ToggleButton button) return;
            button.IsChecked = !button.IsChecked;
        }
    }
    public partial class PagingButtonModel : ObservableObject
    {
        /// <summary>
        /// 选中的位序
        /// </summary>
        [ObservableProperty, NotifyPropertyChangedFor(nameof(IsChecked))]
        public int _selectedIndex;
        /// <summary>
        /// 位序
        /// </summary>
        [ObservableProperty, NotifyPropertyChangedFor(nameof(IsChecked))]
        public int _index;
        /// <summary>
        /// 选中
        /// </summary>
        public bool IsChecked => Index == SelectedIndex;
        public event Action<int>? OnChangeIndex;
        [RelayCommand]
        private void ChangeIndex()
        {
            if (Index == SelectedIndex) return;
            OnChangeIndex?.Invoke(Index);
        }

        public PagingButtonModel(int index, int selectedIndex)
        {
            Index = index;
            SelectedIndex = selectedIndex;
        }
    }
}
