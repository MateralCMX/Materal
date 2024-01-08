namespace Materal.Abstractions
{
    /// <summary>
    /// Materal基础异常类
    /// </summary>
    public class MateralException : Exception
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public MateralException()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        public MateralException(string message) : base(message)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        public MateralException(string message, Exception innerException) : base(message, innerException)
        {
        }
        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString() => this.GetErrorMessage();
        /// <summary>
        /// 获取详细消息
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public virtual string GetDetailMessage(string prefix) => Message;
    }
}
