using Newtonsoft.Json;

namespace Materal.Utils.Model
{
    /// <summary>
    /// 分页模型
    /// </summary>
    public class PageModel : PageRequestModel
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public long PageCount => (long)Math.Ceiling(DataCount / (decimal)PageSize);
        /// <summary>
        /// 总页数
        /// </summary>
        [JsonIgnore, System.Text.Json.Serialization.JsonIgnore]
        public int PageCountInt => PageCount > int.MaxValue ? int.MaxValue : (int)PageCount;
        /// <summary>
        /// 开始序号
        /// </summary>
        public long StartIndex => (PageIndex - PageStartNumber) * PageSize + 1;
        /// <summary>
        /// 开始序号
        /// </summary>
        [JsonIgnore, System.Text.Json.Serialization.JsonIgnore]
        public int StartIndexInt => StartIndex > int.MaxValue ? int.MaxValue : (int)StartIndex;
        /// <summary>
        /// 数据总数
        /// </summary>
        public long DataCount { get; set; }
        /// <summary>
        /// 数据总数
        /// </summary>
        [JsonIgnore, System.Text.Json.Serialization.JsonIgnore]
        public int DataCountInt => DataCount > int.MaxValue ? int.MaxValue : (int)DataCount;
        /// <summary>
        /// 构造方法
        /// </summary>
        public PageModel() { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="pagingIndex">页面位序</param>
        /// <param name="pagingSize">显示数量</param>
        public PageModel(long pagingIndex, long pagingSize) : this(pagingIndex, pagingSize, 0)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="pageRequestModel"></param>
        /// <param name="dataCount">数据总数</param>
        public PageModel(PageRequestModel pageRequestModel, long dataCount) : this(pageRequestModel.PageIndex, pageRequestModel.PageSize, dataCount, pageRequestModel.SortPropertyName, pageRequestModel.IsAsc)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="pagingIndex">页面位序</param>
        /// <param name="pagingSize">显示数量</param>
        /// <param name="dataCount">数据总数</param>
        public PageModel(long pagingIndex, long pagingSize, long dataCount) : base(pagingIndex, pagingSize)
        {
            DataCount = dataCount;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="pagingIndex">页面位序</param>
        /// <param name="pagingSize">显示数量</param>
        /// <param name="dataCount">数据总数</param>
        /// <param name="sortPropertyName"></param>
        /// <param name="isAsc"></param>
        public PageModel(long pagingIndex, long pagingSize, long dataCount, string? sortPropertyName, bool isAsc = true) : base(pagingIndex, pagingSize, sortPropertyName, isAsc)
        {
            DataCount = dataCount;
        }
    }
}
