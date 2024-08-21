using Newtonsoft.Json;

namespace Materal.Utils.Model
{
    /// <summary>
    /// 范围请求模型
    /// </summary>
    public abstract class RangeRequestModel : FilterModel
    {
        /// <summary>
        /// 跳过数量
        /// </summary>
        public virtual long Skip { get; set; } = 0;
        /// <summary>
        /// 跳过数量
        /// </summary>
        [JsonIgnore, System.Text.Json.Serialization.JsonIgnore]
        public int SkipInt => Skip > int.MaxValue ? int.MaxValue : (int)Skip;
        /// <summary>
        /// 获取数量
        /// </summary>
        public virtual long Take { get; set; } = 10;
        /// <summary>
        /// 跳过数量
        /// </summary>
        [JsonIgnore, System.Text.Json.Serialization.JsonIgnore]
        public int TakeInt => Take > int.MaxValue ? int.MaxValue : (int)Take;
        /// <summary>
        /// 构造方法
        /// </summary>
        protected RangeRequestModel() { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        protected RangeRequestModel(long skip, long take)
        {
            Skip = skip;
            Take = take;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="sortPropertyName"></param>
        /// <param name="isAsc"></param>
        protected RangeRequestModel(long skip, long take, string? sortPropertyName, bool isAsc) : this(skip, take)
        {
            SortPropertyName = sortPropertyName;
            IsAsc = isAsc;
        }
    }
}
