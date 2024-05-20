using Newtonsoft.Json;

namespace Materal.Utils.Model
{
    /// <summary>
    /// 范围模型
    /// </summary>
    public class RangeModel : RangeRequestModel
    {
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
        public RangeModel() { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="skip">页面位序</param>
        /// <param name="take">显示数量</param>
        public RangeModel(long skip, long take) : this(skip, take, 0)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="pageRequestModel"></param>
        /// <param name="dataCount">数据总数</param>
        public RangeModel(RangeRequestModel pageRequestModel, long dataCount) : this(pageRequestModel.Skip, pageRequestModel.Take, dataCount, pageRequestModel.SortPropertyName, pageRequestModel.IsAsc)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="skip">页面位序</param>
        /// <param name="take">显示数量</param>
        /// <param name="dataCount">数据总数</param>
        public RangeModel(long skip, long take, long dataCount) : base(skip, take)
        {
            DataCount = dataCount;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="skip">页面位序</param>
        /// <param name="take">显示数量</param>
        /// <param name="dataCount">数据总数</param>
        /// <param name="sortPropertyName"></param>
        /// <param name="isAsc"></param>
        public RangeModel(long skip, long take, long dataCount, string? sortPropertyName, bool isAsc = true) : base(skip, take, sortPropertyName, isAsc)
        {
            DataCount = dataCount;
        }
    }
}
