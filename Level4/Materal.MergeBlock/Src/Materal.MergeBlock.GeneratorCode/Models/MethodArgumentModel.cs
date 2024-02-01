namespace Materal.MergeBlock.GeneratorCode.Models
{
    /// <summary>
    /// 方法参数模型
    /// </summary>
    public class MethodArgumentModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 请求名称
        /// </summary>
        public string RequestName { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string PredefinedType { get; set; }
        /// <summary>
        /// 请求类型
        /// </summary>
        public string RequestPredefinedType { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string? Initializer { get; set; }
        /// <summary>
        /// 是否可空
        /// </summary>
        public bool CanNull { get; set; }
        /// <summary>
        /// 特性组
        /// </summary>
        public List<AttributeModel> Attributes { get; set; } = [];
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="code"></param>
        public MethodArgumentModel(string code)
        {
            string residualCode = code;
            int index;
            if (residualCode[0] == '[')
            {
                index = residualCode.LastIndexOf(']');
                string attributeCode = residualCode[..(index + 1)];
                Attributes = AttributeModel.GetAttributes(attributeCode);
                residualCode = residualCode[(index + 1)..].Trim();
            }
            index = residualCode.IndexOf('=');
            if (index > 0)
            {
                index = residualCode.IndexOf('=');
                Initializer = code[(index + 1)..].Trim();
                residualCode = residualCode[0..index].Trim();
            }
            index = residualCode.LastIndexOf(' ');
            PredefinedType = residualCode[0..index].Trim();
            CanNull = PredefinedType.EndsWith('?');
            Name = residualCode[(index + 1)..];
            if (PredefinedType.EndsWith("Model"))
            {
                RequestPredefinedType = PredefinedType[0..^5] + "RequestModel";
                RequestName = $"request{Name.FirstUpper()}";
            }
            else
            {
                RequestPredefinedType = PredefinedType;
                RequestName = Name;
            }
        }
    }
}
