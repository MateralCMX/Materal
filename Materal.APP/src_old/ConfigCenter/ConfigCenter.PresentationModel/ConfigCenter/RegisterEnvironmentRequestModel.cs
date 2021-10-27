namespace ConfigCenter.PresentationModel.ConfigCenter
{
    /// <summary>
    /// 注册请求模型
    /// </summary>
    public class RegisterEnvironmentRequestModel
    {
        /// <summary>
        /// 密钥
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 连接地址
        /// </summary>
        public string Url { get; set; }
    }
}
