namespace Materal.BaseCore.CodeGenerator
{
    /// <summary>
    /// 代码生成插件
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class CodeGeneratorPlugAttribute : Attribute
    {
        /// <summary>
        /// DLL名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// Class名称
        /// </summary>
        public string ClassName { get; set; }
        public CodeGeneratorPlugAttribute(string projectName, string className)
        {
            ProjectName = projectName;
            ClassName = className;
        }
    }
}
