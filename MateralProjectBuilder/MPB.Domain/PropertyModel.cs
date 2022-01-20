namespace MPB.Domain
{
    /// <summary>
    /// 属性模型
    /// </summary>
    public class PropertyModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 注释
        /// </summary>
        public string Annotation { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 可空类型
        /// </summary>
        public string NullType
        {
            get
            {
                var result = Type;
                if (!result.EndsWith("?") && result != "string")
                {
                    result += "?";
                }
                return result;
            }
        }
    }
}
