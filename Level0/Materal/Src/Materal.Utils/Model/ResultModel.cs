namespace Materal.Utils.Model
{
    /// <summary>
    /// 返回模型
    /// </summary>
    public class ResultModel
    {
        /// <summary>
        /// 返回消息
        /// </summary>
        public string? Message { get; set; }
        /// <summary>
        /// 对象类型
        /// </summary>
        public ResultTypeEnum ResultType { get; set; } = ResultTypeEnum.Success;
        /// <summary>
        /// 构造函数
        /// </summary>
        public ResultModel() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="resultType">返回类型</param>
        /// <param name="message">返回消息</param>
        public ResultModel(ResultTypeEnum resultType, string? message = null)
        {
            ResultType = resultType;
            Message = message;
        }
        /// <summary>
        /// 获得一个成功返回对象
        /// </summary>
        /// <param name="message">返回消息</param>
        /// <returns>成功返回对象</returns>
        public static ResultModel Success(string? message = null) => new(ResultTypeEnum.Success, message);
        /// <summary>
        /// 获得一个失败返回对象
        /// </summary>
        /// <param name="message">返回消息</param>
        /// <returns>失败返回对象</returns>
        public static ResultModel Fail(string? message = null) => new(ResultTypeEnum.Fail, message);
    }
    /// <summary>
    /// 返回模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultModel<T> : ResultModel
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public T? Data { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public ResultModel() : base() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="resultType">返回类型</param>
        /// <param name="data">返回数据对象</param>
        /// <param name="message">返回消息</param>
        public ResultModel(ResultTypeEnum resultType, T? data, string? message = null) : base(resultType, message) => Data = data;
        /// <summary>
        /// 获得一个成功返回对象
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="message">返回消息</param>
        /// <returns>成功返回对象</returns>
        public static ResultModel<T> Success(T data, string? message = null) => new(ResultTypeEnum.Success, data, message);
        /// <summary>
        /// 获得一个成功返回对象
        /// </summary>
        /// <param name="message">返回消息</param>
        /// <returns>成功返回对象</returns>
        public new static ResultModel<T> Success(string? message = null) => new(ResultTypeEnum.Success, default, message);
        /// <summary>
        /// 获得一个失败返回对象
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="message">返回消息</param>
        /// <returns>失败返回对象</returns>
        public static ResultModel<T> Fail(T data, string? message = null) => new(ResultTypeEnum.Fail, data, message);
        /// <summary>
        /// 获得一个失败返回对象
        /// </summary>
        /// <param name="message">返回消息</param>
        /// <returns>失败返回对象</returns>
        public new static ResultModel<T> Fail(string? message = null) => new(ResultTypeEnum.Fail, default, message);
    }
}
