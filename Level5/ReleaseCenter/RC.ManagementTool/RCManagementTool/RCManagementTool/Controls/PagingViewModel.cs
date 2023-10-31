using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace RCManagementTool.Controls
{
    /// <summary>
    /// 基础查询模型
    /// </summary>
    public partial class PagingViewModel : ObservableObject
    {
        /// <summary>
        /// 每页显示数量列表
        /// </summary>
        public List<int> PageSizeList { get; } = new() { 10, 20, 50, 100 };
        /// <summary>
        /// 页码列表
        /// </summary>
        public ObservableCollection<PagingButtonModel> PageIndexList { get; } = new();
        /// <summary>
        /// 当前页码
        /// </summary>
        [ObservableProperty]
        private int _pageIndex = 1;
        /// <summary>
        /// 每页显示数量
        /// </summary>
        [ObservableProperty]
        private int _pageSize = 10;
        /// <summary>
        /// 页数
        /// </summary>
        [ObservableProperty]
        private int _pageCount = int.MaxValue;
        /// <summary>
        /// 查询数据命令
        /// </summary>
        public IAsyncRelayCommand<int?>? SearchDataCommand { get; set; }
        /// <summary>
        /// 上一页 
        /// </summary>
        [RelayCommand]
        private void UpPage()
        {
            if (PageIndex <= 1) return;
            SearchDataCommand?.Execute(PageIndex - 1);
        }
        /// <summary>
        /// 下一页
        /// </summary>
        [RelayCommand]
        private void NextPage()
        {
            if (PageIndex >= PageCount) return;
            SearchDataCommand?.Execute(PageIndex + 1);
        }
        partial void OnPageSizeChanged(int value) => SearchDataCommand?.Execute(PageIndex);
        partial void OnPageCountChanged(int value) => UpdatePageIndexList();
        partial void OnPageIndexChanged(int value) => UpdatePageIndexList();
        private void UpdatePageIndexList()
        {
            PageIndexList.Clear();
            for (int i = 1; i <= 4; i++)
            {
                if (i > PageCount) break;
                if (i <= 0) continue;
                PagingButtonModel temp = new(i, PageIndex);
                temp.OnChangeIndex += IndexButton_OnChangeIndex;
                PageIndexList.Add(temp);
            }
            for (int i = PageCount - 3; i <= PageCount; i++)
            {
                if (i > PageCount) break;
                if (i <= 0 || PageIndexList.Any(m => m.Index == i)) continue;
                PagingButtonModel temp = new(i, PageIndex);
                temp.OnChangeIndex += IndexButton_OnChangeIndex;
                PageIndexList.Add(temp);
            }
            for (int i = PageIndex - 2; i <= PageIndex + 2; i++)
            {
                if (i > PageCount) break;
                if (i <= 0 || PageIndexList.Any(m => m.Index == i)) continue;
                PagingButtonModel temp = new(i, PageIndex);
                temp.OnChangeIndex += IndexButton_OnChangeIndex;
                PageIndexList.Add(temp);
            }
        }
        private void IndexButton_OnChangeIndex(int index) => SearchDataCommand?.Execute(index);
    }
}
