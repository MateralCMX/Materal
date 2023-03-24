using Materal.BaseCore.CodeGenerator;
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
        public static void Main()
        {
            string dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ModelData.json");
            if (!File.Exists(dataPath)) return;
            string executeModelJson = File.ReadAllText(dataPath);
            PlugProjectModelCollection? plugProjectModels = JsonConvert.DeserializeObject<PlugProjectModelCollection>(executeModelJson);
            if (plugProjectModels == null || plugProjectModels.Projects.Count <= 0) return;
            DomainPlugModel domainPlugModel = new()
            {
                WebAPIProject = plugProjectModels.WebAPIProject,
                CommonProject = plugProjectModels.CommonProject,
                DataTransmitModelProject = plugProjectModels.DataTransmitModelProject,
                DomainProject = plugProjectModels.DomainProject,
                Domains = plugProjectModels.Domains,
                EFRepositoryProject = plugProjectModels.EFRepositoryProject,
                Enums = plugProjectModels.Enums,
                EnumsProject = plugProjectModels.EnumsProject,
                PresentationModelProject = plugProjectModels.PresentationModelProject,
                ServiceImplProject = plugProjectModels.ServiceImplProject,
                ServicesProject = plugProjectModels.ServicesProject,
            };
            foreach (PlugProjectModel plugProjectModel in plugProjectModels.Projects)
            {
                string dllFilePath = BuildProject(plugProjectModel.Name);
                Assembly assembly = Assembly.LoadFile(dllFilePath);
                Type[] allTypes = assembly.GetTypes();
                foreach (PlugModel plugModel in plugProjectModel.Plugs)
                {
                    Type? type = allTypes.FirstOrDefault(m => m.Name == plugModel.Name);
                    if (type == null) continue;
                    object? typeObj = type.Instantiation();
                    if (typeObj == null) continue;
                    if (typeObj is not IMateralBaseCoreCodeGeneratorPlug plug) continue;
                    foreach (string domainName in plugModel.ExcuteDomainNames)
                    {
                        DomainModel? model = domainPlugModel.Domains.FirstOrDefault(m => m.Name == domainName);
                        if (model == null) continue;
                        domainPlugModel.Domain = model;
                        plug.PlugExecute(domainPlugModel);
                    }
                }
            }
        }
        /// <summary>
        /// 获得CS文件
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <returns></returns>
        private static List<FileInfo> GetCShaprCodeFiles(DirectoryInfo directoryInfo)
        {
            List<FileInfo> result = new();
            foreach (FileInfo item in directoryInfo.GetFiles().Where(m => m.Extension == ".cs"))
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
        /// 构建项目
        /// </summary>
        private static string BuildProject(string projectPath)
        {
            DirectoryInfo projectDirectoryInfo = new(projectPath);
            if (!projectDirectoryInfo.Exists) throw new Exception("项目文件夹不存在");
            FileInfo? csProjectFileInfo = projectDirectoryInfo.GetFiles().FirstOrDefault(m => m.Extension == ".csproj") ?? throw new Exception("项目文件不存在");
            List<FileInfo> csharpFileInfos = GetCShaprCodeFiles(projectDirectoryInfo);
            DateTime lastWriteTime = csharpFileInfos.Max(m => m.LastWriteTime);
            string dllFileName = Path.GetFileNameWithoutExtension(csProjectFileInfo.Name) + ".dll";
            string dllFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dllFileName);
            FileInfo dllFileInfo = new(dllFilePath);
            string debugDLLFilePath = Path.Combine(projectPath, "bin", "Debug", "net6.0", dllFileName);
            FileInfo debugDLLFileInfo = new(debugDLLFilePath);
            if (debugDLLFileInfo.Exists && lastWriteTime < debugDLLFileInfo.CreationTime)
            {
                dllFileInfo = debugDLLFileInfo;
            }
            string releaseDLLFilePath = Path.Combine(projectPath, "bin", "Release", "net6.0", dllFileName);
            FileInfo releaseDLLFileInfo = new(releaseDLLFilePath);
            if (releaseDLLFileInfo.Exists && lastWriteTime < releaseDLLFileInfo.CreationTime)
            {
                dllFileInfo = releaseDLLFileInfo;
            }
            if (dllFileInfo.Exists) return dllFileInfo.FullName;
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
            EmitResult emitResult = cSharpCompilation.Emit(dllFileInfo.FullName);
            dllFileInfo.Refresh();
            if (!emitResult.Success)
            {
                if (dllFileInfo.Exists) dllFileInfo.Delete();
                StringBuilder errorMessage = new();
                foreach (Diagnostic item in emitResult.Diagnostics)
                {
                    errorMessage.AppendLine(item.ToString());
                }
                throw new Exception(errorMessage.ToString());
            }
            return dllFileInfo.FullName;
        }
    }
}