using Newtonsoft.Json;

namespace Materal.Utils.Model
{
    /// <summary>
    /// 分页请求模型
    /// </summary>
    public abstract class PageRequestModel : FilterModel
    {
        private long pageSize = 10;
        /// <summary>
        /// 起始页码
        /// </summary>
        public static long PageStartNumber { get; set; } = 1;
        /// <summary>
        /// 起始页码
        /// </summary>
        public static int PageStartNumberInt => PageStartNumber > int.MaxValue ? int.MaxValue : (int)PageStartNumber;
        /// <summary>
        /// 页面位序
        /// </summary>
        public long PageIndex { get; set; } = PageStartNumber;
        /// <summary>
        /// 页面位序
        /// </summary>
        [JsonIgnore, System.Text.Json.Serialization.JsonIgnore]
        public int PageIndexInt => PageIndex > int.MaxValue ? int.MaxValue : (int)PageIndex;
        /// <summary>
        /// 显示数量
        /// </summary>
        public long PageSize
        {
            get => pageSize > 0 ? pageSize : 1;
            set => pageSize = value;
        }
        /// <summary>
        /// 显示数量
        /// </summary>
        [JsonIgnore, System.Text.Json.Serialization.JsonIgnore]
        public int PageSizeInt => PageSize > int.MaxValue ? int.MaxValue : (int)PageSize;
        /// <summary>
        /// 跳过数量
        /// </summary>
        [JsonIgnore, System.Text.Json.Serialization.JsonIgnore]
        public long Skip => (PageIndex - PageStartNumber) * PageSize;
        /// <summary>
        /// 跳过数量
        /// </summary>
        [JsonIgnore, System.Text.Json.Serialization.JsonIgnore]
        public int SkipInt => Skip > int.MaxValue ? int.MaxValue : (int)Skip;
        /// <summary>
        /// 获取数量
        /// </summary>
        [JsonIgnore, System.Text.Json.Serialization.JsonIgnore]
        public long Take => PageSize;
        /// <summary>
        /// 跳过数量
        /// </summary>
        [JsonIgnore, System.Text.Json.Serialization.JsonIgnore]
        public int TakeInt => Take > int.MaxValue ? int.MaxValue : (int)Take;
        /// <summary>
        /// 构造方法
        /// </summary>
        protected PageRequestModel() { }
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
    }
}
