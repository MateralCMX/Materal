namespace Materal.Utils.Model
{
    /// <summary>
    /// 集合返回模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CollectionResultModel<T> : ResultModel
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public ICollection<T>? Data { get; set; }
        /// <summary>
        /// 分页模型
        /// </summary>
        public PageModel? PageModel { get; set; }
        /// <summary>
        /// 范围模型
        /// </summary>
        public RangeModel? RangeModel { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public CollectionResultModel()
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="resultType">返回类型</param>
        /// <param name="data">返回数据对象</param>
        /// <param name="message">返回消息</param>
        public CollectionResultModel(ResultTypeEnum resultType, ICollection<T>? data, string? message = null) : base(resultType, message)
        {
            Data = data;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="resultType">返回类型</param>
        /// <param name="data">返回数据对象</param>
        /// <param name="pageModel">分页模型</param>
        /// <param name="message">返回消息</param>
        public CollectionResultModel(ResultTypeEnum resultType, ICollection<T>? data, PageModel pageModel, string? message = null) : base(resultType, message)
        {
            Data = data;
            PageModel = pageModel;
            RangeModel = new RangeModel(pageModel.Skip, pageModel.Take, pageModel.DataCount)
            {
                SortPropertyName = pageModel.SortPropertyName,
                IsAsc = pageModel.IsAsc
            };
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="resultType">返回类型</param>
        /// <param name="data">返回数据对象</param>
        /// <param name="rangeModel">分页模型</param>
        /// <param name="message">返回消息</param>
        public CollectionResultModel(ResultTypeEnum resultType, ICollection<T>? data, RangeModel rangeModel, string? message = null) : base(resultType, message)
        {
            Data = data;
            RangeModel = rangeModel;
            PageModel = new(rangeModel.Skip / rangeModel.Take + PageRequestModel.PageStartNumber, rangeModel.Take, rangeModel.DataCount)
            {
                SortPropertyName = rangeModel.SortPropertyName,
                IsAsc = rangeModel.IsAsc
            };
        }
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="pageModel">分页模型</param>
        /// <param name="message">返回消息</param>
        /// <returns></returns>
        public static CollectionResultModel<T> Success(ICollection<T> data, PageModel pageModel, string? message = null)
            => new(ResultTypeEnum.Success, data, pageModel, message);
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="pageRequestModel">分页请求模型</param>
        /// <param name="dataCount">数据总数</param>
        /// <param name="message">返回消息</param>
        /// <returns></returns>
        public static CollectionResultModel<T> Success(ICollection<T> data, PageRequestModel pageRequestModel, int dataCount, string? message = null)
            => new(ResultTypeEnum.Success, data, new PageModel(pageRequestModel, dataCount), message);
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="pageModel">分页模型</param>
        /// <param name="message">返回消息</param>
        /// <returns></returns>
        public static CollectionResultModel<T> Fail(ICollection<T> data, PageModel pageModel, string? message = null)
            => new(ResultTypeEnum.Fail, data, pageModel, message);
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="rangeRequestModel">分页请求模型</param>
        /// <param name="dataCount">数据总数</param>
        /// <param name="message">返回消息</param>
        /// <returns></returns>
        public static CollectionResultModel<T> Fail(ICollection<T> data, PageRequestModel rangeRequestModel, int dataCount, string? message = null)
            => new(ResultTypeEnum.Fail, data, new PageModel(rangeRequestModel, dataCount), message);
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="rangeModel">分页模型</param>
        /// <param name="message">返回消息</param>
        /// <returns></returns>
        public static CollectionResultModel<T> Success(ICollection<T> data, RangeModel rangeModel, string? message = null)
            => new(ResultTypeEnum.Success, data, rangeModel, message);
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="rangeRequestModel">分页请求模型</param>
        /// <param name="dataCount">数据总数</param>
        /// <param name="message">返回消息</param>
        /// <returns></returns>
        public static CollectionResultModel<T> Success(ICollection<T> data, RangeRequestModel rangeRequestModel, int dataCount, string? message = null)
            => new(ResultTypeEnum.Success, data, new RangeModel(rangeRequestModel, dataCount), message);
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="message">返回消息</param>
        /// <returns></returns>
        public new static CollectionResultModel<T> Fail(string? message = null) => new(ResultTypeEnum.Fail, null, message);
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="pageModel">分页模型</param>
        /// <param name="message">返回消息</param>
        /// <returns></returns>
        public static CollectionResultModel<T> Fail(ICollection<T> data, RangeModel pageModel, string? message = null)
            => new(ResultTypeEnum.Fail, data, pageModel, message);
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="rangeRequestModel">分页请求模型</param>
        /// <param name="dataCount">数据总数</param>
        /// <param name="message">返回消息</param>
        /// <returns></returns>
        public static CollectionResultModel<T> Fail(ICollection<T> data, RangeRequestModel rangeRequestModel, int dataCount, string? message = null)
            => new(ResultTypeEnum.Fail, data, new RangeModel(rangeRequestModel, dataCount), message);
    }
}
