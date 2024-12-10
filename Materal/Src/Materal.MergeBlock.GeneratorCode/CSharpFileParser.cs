using Materal.MergeBlock.GeneratorCode.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Materal.MergeBlock.GeneratorCode
{
    /// <summary>
    /// C#文件解析器
    /// </summary>
    public static class CSharpFileParser
    {
        /// <summary>
        /// 通过文件路径解析
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static List<CSharpCodeFileModel> ParseByFilePath(string filePath)
        {
            if (!File.Exists(filePath)) throw new FileNotFoundException("文件不存在");
            string sourceCode = File.ReadAllText(filePath);
            return Parse(sourceCode);
        }
        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="sourceCode"></param>
        /// <returns></returns>
        public static List<CSharpCodeFileModel> Parse(string sourceCode)
        {
            // 创建语法树
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
            CompilationUnitSyntax root = syntaxTree.GetCompilationUnitRoot();
            IEnumerable<SyntaxNode> allNodes = root.DescendantNodes();
            List<CSharpCodeFileModel> result = [];
            // 解析代码
            IEnumerable<InterfaceDeclarationSyntax> interfaceDeclarationSyntaxs = allNodes.OfType<InterfaceDeclarationSyntax>();
            foreach (InterfaceDeclarationSyntax interfaceDeclarationSyntax in interfaceDeclarationSyntaxs)
            {
                InterfaceModel interfaceModel;
                if (IsController(interfaceDeclarationSyntax))
                {
                    interfaceModel = new IControllerModel(interfaceDeclarationSyntax, allNodes);
                }
                else if (IsService(interfaceDeclarationSyntax))
                {
                    interfaceModel = new IServiceModel(interfaceDeclarationSyntax, allNodes);
                }
                else
                {
                    interfaceModel = new(interfaceDeclarationSyntax, allNodes);
                }
                result.Add(interfaceModel);
            }
            IEnumerable<ClassDeclarationSyntax> classDeclarationSyntaxes = allNodes.OfType<ClassDeclarationSyntax>();
            foreach (ClassDeclarationSyntax classDeclarationSyntax in classDeclarationSyntaxes)
            {
                ClassModel classModel;
                if (IsDomain(classDeclarationSyntax))
                {
                    classModel = new DomainModel(classDeclarationSyntax, allNodes);
                }
                else
                {
                    classModel = new(classDeclarationSyntax, allNodes);
                }
                result.Add(classModel);
            }
            IEnumerable<EnumDeclarationSyntax> enumDeclarationSyntaxes = allNodes.OfType<EnumDeclarationSyntax>();
            foreach (EnumDeclarationSyntax enumDeclarationSyntax in enumDeclarationSyntaxes)
            {
                EnumModel enumModel = new(enumDeclarationSyntax, allNodes);
                result.Add(enumModel);
            }
            return result;
        }
        private static bool IsController(InterfaceDeclarationSyntax node)
        {
            if (!node.Identifier.Text.StartsWith('I')) return false;
            if (!node.Identifier.Text.Contains("Controller")) return false;
            return true;
        }
        private static bool IsService(InterfaceDeclarationSyntax node)
        {
            if (!node.Identifier.Text.StartsWith('I')) return false;
            if (!node.Identifier.Text.Contains("Service")) return false;
            return true;
        }
        private static bool IsDomain(ClassDeclarationSyntax node)
        {
            if (node.BaseList is null) return false;
            foreach (BaseTypeSyntax baseType in node.BaseList.Types)
            {
                if (baseType is null || (baseType.Type is not SimpleNameSyntax && baseType.Type is not QualifiedNameSyntax)) continue;
                string typeName = baseType.Type.ToString();
                if (typeName == "IDomain" || typeName == "BaseDomain") return true;
            }
            return false;
        }
        /// <summary>
        /// 合并
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static List<T> Merge<T>(List<T> source)
            where T : CSharpCodeFileModel
        {
            List<T> result = [];
            foreach (T model in source)
            {
                T? existModel = result.FirstOrDefault(m => m.IsSameModel(model));
                if (existModel is null)
                {
                    result.Add(model);
                    continue;
                }
                int index = result.IndexOf(existModel);
                if (model is InterfaceModel interfaceModel && existModel is InterfaceModel existInterface)
                {
                    InterfaceModel newInterfaceModel = interfaceModel.Merge(existInterface);
                    if (newInterfaceModel is not T newInterfaceT) continue;
                    result[index] = newInterfaceT;
                }
                else if (model is ClassModel classModel && existModel is ClassModel existClass)
                {
                    ClassModel newClasseModel = classModel.Merge(existClass);
                    if (newClasseModel is not T newClassT) continue;
                    result[index] = newClassT;
                }
                else if (model is EnumModel enumModel && existModel is EnumModel existEnum)
                {
                    EnumModel newEnumModel = enumModel.Merge(existEnum);
                    if (newEnumModel is not T newEnumT) continue;
                    result[index] = newEnumT;
                }
            }
            return result;
        }
    }
}
