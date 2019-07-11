using Materal.Common;

namespace Materal.Model
{
    public class ResultModel
    {
        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 对象类型
        /// </summary>
        public ResultTypeEnum ResultType { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public ResultModel()
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="resultType">返回类型</param>
        /// <param name="message">返回消息</param>
        public ResultModel(ResultTypeEnum resultType, string message = "")
        {
            ResultType = resultType;
            Message = message;
        }
        /// <summary>
        /// 获得一个成功返回对象
        /// </summary>
        /// <param name="message">返回消息</param>
        /// <returns>成功返回对象</returns>
        public static ResultModel Success(string message = "")
        {
            return new ResultModel(ResultTypeEnum.Success, message);
        }
        /// <summary>
        /// 获得一个失败返回对象
        /// </summary>
        /// <param name="message">返回消息</param>
        /// <returns>失败返回对象</returns>
        public static ResultModel Fail(string message = "")
        {
            return new ResultModel(ResultTypeEnum.Fail, message);
        }
    }

    public class ResultModel<T> : ResultModel
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ResultModel()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="resultType">返回类型</param>
        /// <param name="data">返回数据对象</param>
        /// <param name="message">返回消息</param>
        public ResultModel(ResultTypeEnum resultType, T data, string message = "") : base(resultType, message)
        {
            Data = data;
        }

        /// <summary>
        /// 获得一个成功返回对象
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="message">返回消息</param>
        /// <returns>成功返回对象</returns>
        public static ResultModel<T> Success(T data, string message = "")
        {
            return new ResultModel<T>(ResultTypeEnum.Success, data, message);
        }

        /// <summary>
        /// 获得一个成功返回对象
        /// </summary>
        /// <param name="message">返回消息</param>
        /// <returns>成功返回对象</returns>
        public new static ResultModel<T> Success(string message = "")
        {
            return new ResultModel<T>(ResultTypeEnum.Success, default(T), message);
        }

        /// <summary>
        /// 获得一个失败返回对象
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <param name="message">返回消息</param>
        /// <returns>失败返回对象</returns>
        public static ResultModel<T> Fail(T data, string message = "")
        {
            return new ResultModel<T>(ResultTypeEnum.Fail, data, message);
        }

        /// <summary>
        /// 获得一个失败返回对象
        /// </summary>
        /// <param name="message">返回消息</param>
        /// <returns>失败返回对象</returns>
        public new static ResultModel<T> Fail(string message = "")
        {
            return new ResultModel<T>(ResultTypeEnum.Fail, default(T), message);
        }
    }
}
