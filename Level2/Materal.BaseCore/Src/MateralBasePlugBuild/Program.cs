using Materal.BaseCore.CodeGenerator;
using Materal.BaseCore.CodeGenerator.Extensions;
using Materal.BaseCore.CodeGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace MateralBasePlugBuild
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string projectPath;
            string className;
            string dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ModelData.json");
            if (!File.Exists(dataPath)) throw new Exception("未找到数据");
            string executeModelJson = File.ReadAllText(dataPath);
#if DEBUG
            if (args.Length == 2)
            {
                projectPath = args[0];
                className = args[1];
            }
            else
            {
                projectPath = "D:\\Project\\测试项目\\RC.Core\\Demo\\RC.Demo.CodeGenerator";
                className = "TestPlug.cs";
            }
#else
            if (args.Length != 2) return;
            projectPath = args[0];
            className = args[1];
#endif
            string typeName = className;
            if (typeName.EndsWith(".cs"))
            {
                typeName = typeName[0..^3];
            }
            string dllFilePath = BuildClass(projectPath, className);
            Assembly assembly = Assembly.LoadFile(dllFilePath);
            Type[] allTypes = assembly.GetTypes();
            Type? type = allTypes.FirstOrDefault(m => m.Name == typeName);
            if (type == null) return;
            object? typeObj = type.Instantiation();
            if (typeObj == null) return;
            if (typeObj is not IMateralBaseCoreCodeGeneratorPlug plug) return;
            DomainPlugModel? model = JsonConvert.DeserializeObject<DomainPlugModel>(executeModelJson);
            if (model == null) return;
            plug.PlugExecute(model);
        }
        /// <summary>
        /// 构建Class
        /// </summary>
        private static string BuildClass(string projectPath, string className)
        {
            string classFileName = className;
            string dllFileName = className;
            if (!classFileName.EndsWith(".cs"))
            {
                classFileName += ".cs";
                dllFileName += ".dll";
            }
            else
            {
                dllFileName = dllFileName[..dllFileName.LastIndexOf('.')] + ".dll";
            }
            string filePath = Path.Combine(projectPath, classFileName);
            string dllFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dllFileName);
            if (File.Exists(dllFilePath)) return dllFilePath;
            FileInfo csFileInfo = new(filePath);
            if (!csFileInfo.Exists) throw new Exception(".cs文件不存在");
            FileInfo dllFileInfo = new(dllFilePath);
            if (dllFileInfo.Exists) dllFileInfo.Delete();
            static string getRootDllPath(string dllName) => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dllName);
            List<string> usingAssemblies = new()
            {
                Assembly.Load("mscorlib").Location,
                Assembly.Load("netstandard").Location,
                Assembly.Load("System.Runtime").Location,
                Assembly.Load("System.Console").Location,
                getRootDllPath("Materal.BaseCore.CodeGenerator.dll"),
                typeof(object).Assembly.Location
            };
            DirectoryInfo dllLibDirectoryInfo = new(Path.Combine(projectPath, "Libs"));
            if (dllLibDirectoryInfo.Exists)
            {
                foreach (FileInfo fileInfo in dllLibDirectoryInfo.GetFiles())
                {
                    if (fileInfo.Extension.ToUpper() != ".DLL") continue;
                    Assembly assembly = Assembly.Load(File.ReadAllBytes(fileInfo.FullName));
                    usingAssemblies.Add(assembly.Location);
                }
            }
            List<MetadataReference> refs = usingAssemblies.Select(m => MetadataReference.CreateFromFile(m)).ToList<MetadataReference>();
            var cSharpCompilation = CSharpCompilation
                            .Create(dllFileName)
                            .WithOptions(new CSharpCompilationOptions(
                                OutputKind.DynamicallyLinkedLibrary,
                                usings: null,
                                optimizationLevel: OptimizationLevel.Release,
                                checkOverflow: false,
                                allowUnsafe: true,
                                platform: Platform.AnyCpu,
                                warningLevel: 4,
                                xmlReferenceResolver: null
                            ))
                            .AddReferences(refs);
            string soureCode = File.ReadAllText(csFileInfo.FullName);
            cSharpCompilation = cSharpCompilation.AddSyntaxTrees(CSharpSyntaxTree.ParseText(soureCode));
            EmitResult emitResult = cSharpCompilation.Emit(dllFilePath);
            if (!emitResult.Success)
            {
                if (File.Exists(dllFilePath)) File.Delete(dllFilePath);
                StringBuilder errorMessage = new();
                foreach (Diagnostic item in emitResult.Diagnostics)
                {
                    errorMessage.AppendLine(item.ToString());
                }
                throw new Exception(errorMessage.ToString());
            }
            return dllFilePath;
        }
    }
}