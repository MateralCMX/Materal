namespace Materal.Utils.Model
{
    /// <summary>
    /// 分页返回模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageResultModel<T> : ResultModel
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
        /// 构造函数
        /// </summary>
        public PageResultModel()
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="resultType">返回类型</param>
        /// <param name="data">返回数据对象</param>
        /// <param name="pageModel">分页模型</param>
        /// <param name="message">返回消息</param>
        public PageResultModel(ResultTypeEnum resultType, ICollection<T>? data, PageModel? pageModel, string? message = null) : base(resultType, message)
        {
            Data = data;
            PageModel = pageModel;
        }
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="pageModel">分页模型</param>
        /// <param name="message">返回消息</param>
        /// <returns></returns>
        public static PageResultModel<T> Success(ICollection<T> data, PageModel pageModel, string? message = null) => new(ResultTypeEnum.Success, data, pageModel, message);
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="pageRequestModel">分页请求模型</param>
        /// <param name="dataCount">数据总数</param>
        /// <param name="message">返回消息</param>
        /// <returns></returns>
        public static PageResultModel<T> Success(ICollection<T> data, PageRequestModel pageRequestModel, int dataCount, string? message = null) => new(ResultTypeEnum.Success, data, new PageModel(pageRequestModel, dataCount), message);
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="message">返回消息</param>
        /// <returns></returns>
        public new static PageResultModel<T> Fail(string? message = null) => new(ResultTypeEnum.Fail, null, null, message);
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="pageModel">分页模型</param>
        /// <param name="message">返回消息</param>
        /// <returns></returns>
        public static PageResultModel<T> Fail(ICollection<T> data, PageModel pageModel, string? message = null) => new(ResultTypeEnum.Fail, data, pageModel, message);
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="pageRequestModel">分页请求模型</param>
        /// <param name="dataCount">数据总数</param>
        /// <param name="message">返回消息</param>
        /// <returns></returns>
        public static PageResultModel<T> Fail(ICollection<T> data, PageRequestModel pageRequestModel, int dataCount, string? message = null) => new(ResultTypeEnum.Fail, data, new PageModel(pageRequestModel, dataCount), message);
    }
}
