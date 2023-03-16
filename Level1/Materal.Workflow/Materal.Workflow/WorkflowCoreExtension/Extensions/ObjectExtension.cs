using System.Reflection;

namespace Materal.Workflow.WorkflowCoreExtension.Extensions
{
    /// <summary>
    /// Object扩展
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// 获得属性值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException"></exception>
        public static object GetPropertyValue(this object? obj, string name)
        {
            if (obj == null) throw new WorkflowException($"获取{name}属性值类型错误,目标对象为空");
            Type setpBuilderType = obj.GetType();
            PropertyInfo? propertyInfo = setpBuilderType.GetProperty(name);
            if (propertyInfo == null) throw new WorkflowException($"获取{setpBuilderType}.{name}属性失败");
            object? result = propertyInfo.GetValue(obj);
            if (result == null) throw new WorkflowException($"获取{setpBuilderType}.{name}属性值失败");
            return result;
        }
        /// <summary>
        /// 获得属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException"></exception>
        public static T GetPropertyValue<T>(this object? obj, string name)
        {
            if (obj == null) throw new WorkflowException($"获取{name}属性值类型错误,目标对象为空");
            Type setpBuilderType = obj.GetType();
            object tempProperty = obj.GetPropertyValue(name);
            if (tempProperty is not T result) throw new WorkflowException($"获取{setpBuilderType}.{name}属性值类型错误");
            return result;
        }
    }
}
