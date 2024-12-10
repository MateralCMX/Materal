using Materal.MergeBlock.GeneratorCode.Extensions;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Materal.MergeBlock.GeneratorCode.Models
{
    /// <summary>
    /// 方法模型
    /// </summary>
    public class MethodModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 返回类型
        /// </summary>
        public string ReturnType { get; set; }
        /// <summary>
        /// 是否Task返回类型
        /// </summary>
        public bool IsTaskReturnType => ReturnType.StartsWith("Task");
        /// <summary>
        /// 无Task返回类型
        /// </summary>
        public string NotTaskReturnType { get; set; }
        /// <summary>
        /// 注释
        /// </summary>
        public string? Annotation { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public List<MethodArgumentModel> Arguments { get; set; }
        /// <summary>
        /// 特性组
        /// </summary>
        public List<AttributeModel> Attributes { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public MethodModel()
        {
            Name = string.Empty;
            ReturnType = string.Empty;
            NotTaskReturnType = string.Empty;
            Annotation = null;
            Arguments = [];
            Attributes = [];
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="node"></param>
        public MethodModel(MethodDeclarationSyntax node)
        {
            Name = GetName(node);
            ReturnType = GetReturnType(node);
            NotTaskReturnType = GetNotTaskReturnType();
            Annotation = node.GetAnnotation();
            Arguments = GetArguments(node);
            Attributes = node.GetAttributes();
        }
        /// <summary>
        /// 获取名称
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string GetName(MethodDeclarationSyntax node)
            => node.Identifier.Text;
        /// <summary>
        /// 获取返回类型
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string GetReturnType(MethodDeclarationSyntax node)
            => node.ReturnType.ToString();
        /// <summary>
        /// 获取无Task返回类型
        /// </summary>
        /// <returns></returns>
        private string GetNotTaskReturnType()
        {
            if (!IsTaskReturnType) return ReturnType;
            if (ReturnType == "Task") return "void";
            return ReturnType[(ReturnType.IndexOf('<') + 1)..^1];
        }
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static List<MethodArgumentModel> GetArguments(MethodDeclarationSyntax node)
            => node.ParameterList.Parameters.Select(m => new MethodArgumentModel(m)).ToList();
    }
}
