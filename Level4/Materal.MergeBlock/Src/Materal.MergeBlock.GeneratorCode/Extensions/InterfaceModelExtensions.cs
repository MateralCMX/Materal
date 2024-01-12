using Materal.MergeBlock.GeneratorCode.Models;
using System.Reflection;

namespace Materal.MergeBlock.GeneratorCode.Extensions
{
    /// <summary>
    /// 接口模型扩展
    /// </summary>
    public static class InterfaceModelExtensions
    {
        /// <summary>
        /// 获得所有控制器接口
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <param name="isTargetDirectory"></param>
        /// <param name="isTargetFile"></param>
        /// <param name="models"></param>
        /// <returns></returns>
        public static List<T> GetAllInterfaceModels<T>(this DirectoryInfo directoryInfo, Func<DirectoryInfo, bool> isTargetDirectory, Func<FileInfo, bool> isTargetFile, List<T>? models = null)
            where T : InterfaceModel
        {
            models ??= [];
            foreach (DirectoryInfo? item in directoryInfo.GetDirectories())
            {
                if (item is null) continue;
                if (isTargetDirectory(item))
                {
                    item.FillInterfaceModels(isTargetFile, models);
                }
                else
                {
                    item.GetAllInterfaceModels(isTargetDirectory, isTargetFile, models);
                }
            }
            return models;
        }
        /// <summary>
        /// 填充控制器接口
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <param name="isTargetFile"></param>
        /// <param name="models"></param>
        /// <returns></returns>
        private static void FillInterfaceModels<T>(this DirectoryInfo directoryInfo, Func<FileInfo, bool> isTargetFile, List<T> models)
             where T : InterfaceModel
        {
            foreach (DirectoryInfo? item in directoryInfo.GetDirectories())
            {
                if (item is null) continue;
                item.FillInterfaceModels(isTargetFile, models);
            }
            foreach (FileInfo? item in directoryInfo.GetFiles())
            {
                if (item is null) continue;
                if (isTargetFile(item))
                {
                    string[] codes = File.ReadAllLines(item.FullName);
                    T model = typeof(T).Instantiation<T>(new[] { codes });
                    T? oldModel = models.FirstOrDefault(m => m.Name == model.Name);
                    if (oldModel is null)
                    {
                        models.Add(model);
                    }
                    else
                    {
                        oldModel.Annotation ??= model.Annotation;
                        oldModel.Methods.AddRange(model.Methods);
                        oldModel.Properties.AddRange(model.Properties);
                        oldModel.Usings.AddRange(model.Usings);
                        oldModel.Usings = oldModel.Usings.Distinct().ToList();
                        oldModel.Interfaces.AddRange(model.Interfaces);
                    }
                }
            }
        }
    }
}
