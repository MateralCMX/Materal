namespace Materal.MergeBlock.GeneratorCode.Models
{
    /// <summary>
    /// 特性参数模型
    /// </summary>
    public class AttributeArgumentModel
    {
        /// <summary>
        /// 目标
        /// </summary>
        public string? Target { get; set; } = null;
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; } = string.Empty;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="argument"></param>
        public AttributeArgumentModel(string argument)
        {
            string[] temp = argument.Split('=');
            if (temp.Length == 1)
            {
                Value = HandlerStringValue(temp[0].Trim());
            }
            else if (temp.Length == 2)
            {
                Target = temp[0].Trim();
                Value = HandlerStringValue(temp[1].Trim());
            }
        }
        private string HandlerStringValue(string value)
        {
            if (value.StartsWith("nameof(") && value.EndsWith(")"))
            {
                value = $"\"{value["nameof(".Length..^1]}\"";
            }
            return value;
        }
    }
}
