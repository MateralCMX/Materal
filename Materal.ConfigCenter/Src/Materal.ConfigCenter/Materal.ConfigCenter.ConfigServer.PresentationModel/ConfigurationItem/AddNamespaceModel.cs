namespace Materal.ConfigCenter.ConfigServer.PresentationModel.ConfigurationItem
{
    public class AddConfigurationItemModel
    {
        /// <summary>
        /// 命名空间唯一标识
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 命名空间唯一标识
        /// </summary>
        public string NamespaceName { get; set; }
        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
    }
}
