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
                projectPath = "D:\\Project\\古典部\\新明解\\Src\\XMJ.DataCenter\\XMJ.DataCenter.CodeGenerator";
                className = "ReoprtDomain.cs";
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
            string dllFilePath = BuildClass(projectPath);
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
        /// 获得CS文件
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <returns></returns>
        private static List<FileInfo> GetCShaprCodeFiles(DirectoryInfo directoryInfo)
        {
            List<FileInfo> result = new();
            foreach (FileInfo item in directoryInfo.GetFiles().Where(m=>m.Extension == ".cs"))
            {
                result.Add(item);
            }
            foreach (DirectoryInfo item in directoryInfo.GetDirectories())
            {
                if (item.Name == "bin") continue;
                if (item.Name == "obj") continue;
                result.AddRange(GetCShaprCodeFiles(item));
            }
            return result;
        }
        /// <summary>
        /// 构建Class
        /// </summary>
        private static string BuildClass(string projectPath)
        {
            DirectoryInfo projectDirectoryInfo = new(projectPath);
            if (!projectDirectoryInfo.Exists) throw new Exception("项目文件夹不存在");
            FileInfo? csProjectFileInfo = projectDirectoryInfo.GetFiles().FirstOrDefault(m => m.Extension == ".csproj");
            if(csProjectFileInfo == null) throw new Exception("项目文件不存在");
            string dllFileName = Path.GetFileNameWithoutExtension(csProjectFileInfo.Name) + ".dll";
            List<FileInfo> csharpFileInfos = GetCShaprCodeFiles(projectDirectoryInfo);
            string dllFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dllFileName);
            FileInfo dllFileInfo = new(dllFilePath);
            if (dllFileInfo.Exists) return dllFilePath;
            static string getRootDllPath(string dllName) => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dllName);
            List<string> usingAssemblies = new()
            {
                Assembly.Load("mscorlib").Location,
                Assembly.Load("netstandard").Location,
                Assembly.Load("System.Runtime").Location,
                Assembly.Load("System.Console").Location,
                Assembly.Load("System.Collections").Location,
                Assembly.Load("System.Linq").Location,
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
            foreach (FileInfo csFileInfo in csharpFileInfos)
            {
                string soureCode = File.ReadAllText(csFileInfo.FullName);
                cSharpCompilation = cSharpCompilation.AddSyntaxTrees(CSharpSyntaxTree.ParseText(soureCode));
            }
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