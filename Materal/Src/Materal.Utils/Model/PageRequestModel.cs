using Newtonsoft.Json;

namespace Materal.Utils.Model
{
    /// <summary>
    /// 分页请求模型
    /// </summary>
    public abstract class PageRequestModel : RangeRequestModel
    {
        /// <summary>
        /// 起始页码
        /// </summary>
        public static long PageStartNumber { get; set; } = 1;
        /// <summary>
        /// 起始页码
        /// </summary>
        public static int PageStartNumberInt => PageStartNumber > int.MaxValue ? int.MaxValue : (int)PageStartNumber;
        private long _pageIndex = 0;
        /// <summary>
        /// 页面位序
        /// </summary>
        public long PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = value >= PageStartNumber ? value : PageStartNumber;
        }
        /// <summary>
        /// 页面位序
        /// </summary>
        [JsonIgnore, System.Text.Json.Serialization.JsonIgnore]
        public int PageIndexInt => PageIndex > int.MaxValue ? int.MaxValue : (int)PageIndex;
        private long pageSize = 10;
        /// <summary>
        /// 显示数量
        /// </summary>
        public long PageSize
        {
            get => pageSize;
            set => pageSize = value > 0 ? value : 1;
        }
        /// <summary>
        /// 显示数量
        /// </summary>
        [JsonIgnore, System.Text.Json.Serialization.JsonIgnore]
        public int PageSizeInt => PageSize > int.MaxValue ? int.MaxValue : (int)PageSize;
        /// <summary>
        /// 分页跳过数量
        /// </summary>
        [JsonIgnore, System.Text.Json.Serialization.JsonIgnore]
        public long PageSkip => (PageIndex - PageStartNumber) * PageSize;
        /// <summary>
        /// 分页跳过数量
        /// </summary>
        [JsonIgnore, System.Text.Json.Serialization.JsonIgnore]
        public int PageSkipInt => PageSkip > int.MaxValue ? int.MaxValue : (int)PageSkip;
        private long? _skip;
        /// <summary>
        /// 跳过数量
        /// </summary>
        public override long Skip
        {
            get => _skip ?? PageSkip;
            set
            {
                _skip = value;
                PageIndex = value / PageSize + PageStartNumber;
            }
        }
        /// <summary>
        /// 获取数量
        /// </summary>
        public override long Take { get => PageSize; set => PageSize = value; }
        /// <summary>
        /// 构造方法
        /// </summary>
        protected PageRequestModel() { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        protected PageRequestModel(long pageIndex, long pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            if (PageIndex < PageStartNumber)
            {
                PageIndex = PageStartNumber;
            }
            if (PageSize < 0)
            {
                PageSize = 1;
            }
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortPropertyName"></param>
        /// <param name="isAsc"></param>
        protected PageRequestModel(long pageIndex, long pageSize, string? sortPropertyName, bool isAsc) : this(pageIndex, pageSize)
        {
            SortPropertyName = sortPropertyName;
            IsAsc = isAsc;
        }
    }
}
