using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Reflection;

namespace Materal.MergeBlock.Swagger
{
    /// <summary>
    /// 控制器模型约定
    /// </summary>
    public class SwaggerControllerModelConvention(MergeBlockContext context) : IControllerModelConvention
    {
        /// <summary>
        /// 应用
        /// </summary>
        /// <param name="controller"></param>
        public void Apply(ControllerModel controller)
        {
            if (controller.ApiExplorer.GroupName is not null && !string.IsNullOrWhiteSpace(controller.ApiExplorer.GroupName)) return;
            Assembly? assembly = context.MergeBlockAssemblies.FirstOrDefault(m => m == controller.ControllerType.Assembly);
            if (assembly is null) return;
            string? groupName = GetGroupName(assembly);
            if (string.IsNullOrWhiteSpace(groupName)) return;
            controller.ApiExplorer.GroupName = groupName;
        }
        /// <summary>
        /// 获取组名
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static string? GetGroupName(Assembly assembly)
        {
            string? groupName = assembly.GetName().Name;
            if (string.IsNullOrWhiteSpace(groupName)) return null;
            string[] assemblyNames = groupName.Split(".");
            if (assemblyNames.Length > 1)
            {
                return $"{assemblyNames[0]}.{assemblyNames[1]}";
            }
            else if (assemblyNames.Length == 1)
            {
                return assemblyNames[0];
            }
            return null;
        }
    }
}
