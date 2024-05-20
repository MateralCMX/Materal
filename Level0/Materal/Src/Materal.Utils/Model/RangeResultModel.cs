namespace Materal.Utils.Model
{
    /// <summary>
    /// 范围返回模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RangeResultModel<T> : ResultModel
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public ICollection<T>? Data { get; set; }
        /// <summary>
        /// 分页模型
        /// </summary>
        public RangeModel? PageModel { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public RangeResultModel()
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="resultType">返回类型</param>
        /// <param name="data">返回数据对象</param>
        /// <param name="rangeModel">分页模型</param>
        /// <param name="message">返回消息</param>
        public RangeResultModel(ResultTypeEnum resultType, ICollection<T>? data, RangeModel? rangeModel, string? message = null) : base(resultType, message)
        {
            Data = data;
            PageModel = rangeModel;
        }
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="rangeModel">分页模型</param>
        /// <param name="message">返回消息</param>
        /// <returns></returns>
        public static RangeResultModel<T> Success(ICollection<T> data, RangeModel rangeModel, string? message = null) => new(ResultTypeEnum.Success, data, rangeModel, message);
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="rangeRequestModel">分页请求模型</param>
        /// <param name="dataCount">数据总数</param>
        /// <param name="message">返回消息</param>
        /// <returns></returns>
        public static RangeResultModel<T> Success(ICollection<T> data, RangeRequestModel rangeRequestModel, int dataCount, string? message = null) => new(ResultTypeEnum.Success, data, new RangeModel(rangeRequestModel, dataCount), message);
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="message">返回消息</param>
        /// <returns></returns>
        public new static RangeResultModel<T> Fail(string? message = null) => new(ResultTypeEnum.Fail, null, null, message);
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="rangeModel">分页模型</param>
        /// <param name="message">返回消息</param>
        /// <returns></returns>
        public static RangeResultModel<T> Fail(ICollection<T> data, RangeModel rangeModel, string? message = null) => new(ResultTypeEnum.Fail, data, rangeModel, message);
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="rangeRequestModel">分页请求模型</param>
        /// <param name="dataCount">数据总数</param>
        /// <param name="message">返回消息</param>
        /// <returns></returns>
        public static RangeResultModel<T> Fail(ICollection<T> data, RangeRequestModel rangeRequestModel, int dataCount, string? message = null) => new(ResultTypeEnum.Fail, data, new RangeModel(rangeRequestModel, dataCount), message);
    }
}
