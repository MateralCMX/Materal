namespace WebAPP.MateralUI.Models
{
    public interface IFromDataPropertyModel
    {
        object ModelValue{ get; set; }
        bool CanError { get; set; }
        string ErrorMessage { get; set; }
    }
    public class FromDataPropertyModel<T> : IFromDataPropertyModel
    {
        /// <summary>
        /// 模型值
        /// </summary>
        public object ModelValue { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public T Value
        {
            get
            {
                if (ModelValue == null) return default;
                return (T) ModelValue;
            }
            set => ModelValue = value;
        }
        /// <summary>
        /// 是否错误
        /// </summary>
        public bool CanError { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
