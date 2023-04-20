namespace Materal.BaseCore.CodeGenerator.Models
{
    /// <summary>
    /// 参数模型
    /// </summary>
    public class ParamModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 请求模型名称
        /// </summary>
        public string RequestModelName { get; set; } = string.Empty;
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// 请求模型类型
        /// </summary>
        public string RequestModelType { get; set; } = string.Empty;
        /// <summary>
        /// 控制器参数名称
        /// </summary>
        public string ControllerName => string.IsNullOrWhiteSpace(RequestModelName) ? Name : RequestModelName;
        public ParamModel() { }
        public ParamModel(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) return;
            string[] codes = code.Split(' ');
            if (codes.Length != 2) throw new CodeGeneratorException($"解析{code}失败");
            Type = codes[0];
            Name = codes[1];
            if (Type.EndsWith("Model"))
            {
                RequestModelType = Type[..^5] + "RequestModel";
                RequestModelName = "requestModel";
            }
        }
    }
}
