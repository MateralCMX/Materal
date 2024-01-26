namespace Materal.MergeBlock.GeneratorCode.Models.AnalysisCodeHandlers
{
    /// <summary>
    /// 属性分析代码处理器
    /// </summary>
    public class PropertyAnalysisCodeHandler : AnalysisCodeHandler
    {
        /// <summary>
        /// 分析代码
        /// </summary>
        /// <param name="cSharpCodeFileModel"></param>
        /// <param name="code"></param>
        /// <param name="codes"></param>
        /// <param name="currentLine"></param>
        /// <returns></returns>
        protected override bool AnalysisCodes(CSharpCodeFileModel cSharpCodeFileModel, string code, string[] codes, int currentLine)
        {
            if (!code.StartsWith("public ")) return true;
            if (cSharpCodeFileModel is not InterfaceModel interfaceModel) return true;
            int index = code.IndexOf(" { get; set; }");
            if (index < 0) return true;
            PropertyModel property = new();
            string propertyCode = code["public ".Length..];
            index = propertyCode.IndexOf(' ');
            property.PredefinedType = propertyCode[..index];
            property.CanNull = property.PredefinedType.EndsWith("?");
            propertyCode = propertyCode[(index + 1)..];
            index = propertyCode.IndexOf(' ');
            property.Name = propertyCode[..index];
            propertyCode = propertyCode[(index + 1)..];
            if(propertyCode.StartsWith("{ get; set; } = ") && propertyCode.EndsWith(";"))
            {
                property.Initializer = propertyCode["{ get; set; } = ".Length..^1];
            }
            property.Annotation = StringHelper.GetAnnotationSetAttributes(codes, currentLine, property.Attributes);
            interfaceModel.Properties.Add(property);
            return false;
        }
    }
}
