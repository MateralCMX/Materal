namespace Materal.BusinessFlow.AutoNodes
{
    public class HttpAutoNodeDataModel
    {
        /// <summary>
        /// 请求地址
        /// </summary>
        public string Url { get; set; } = string.Empty;
        /// <summary>
        /// 请求方法
        /// </summary>
        public string Method { get; set; } = "GET";
        /// <summary>
        /// 请求头
        /// </summary>
        public Dictionary<string, string>? Headers { get; set; }
        /// <summary>
        /// 查询请求参数
        /// </summary>
        public Dictionary<string, string>? QueryParams { get; set; }
        /// <summary>
        /// 请求体
        /// </summary>
        public object? Body { get; set; }
        /// <summary>
        /// 结果映射
        /// </summary>
        public List<HttpAutoNodeMapperModel> ResultMappers { get; set; } = new();
    }
    /// <summary>
    /// Http自动节点映射模型
    /// </summary>
    public class HttpAutoNodeMapperModel
    {
        /// <summary>
        /// 返回名称
        /// </summary>
        public string ResultName { get; set; } = string.Empty;
        /// <summary>
        /// 数据模型字段名称
        /// </summary>
        public string DataModelFieldName { get; set; } = string.Empty;
    }
}
