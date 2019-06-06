namespace WeChatService.DataTransmitModel.Application
{
    /// <summary>
    /// 应用数据传输模型
    /// </summary>
    public class ApplicationDTO : ApplicationListDTO
    {
        /// <summary>
        /// EncodingAESKey
        /// </summary>
        public string EncodingAESKey { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
