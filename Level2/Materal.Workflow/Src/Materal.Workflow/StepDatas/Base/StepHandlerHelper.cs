using System.Linq.Expressions;
using System.Reflection;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 节点处理器基类数据
    /// </summary>
    internal static class StepHandlerHelper
    {
        /// <summary>
        /// 运行时数据类型
        /// </summary>
        public static Type RuntimeDataType { get; } = typeof(Dictionary<string, object>);
        /// <summary>
        /// 运行时数据索引器信息
        /// </summary>
        public static PropertyInfo RuntimeDataIndexerPropertyInfo { get; }
        /// <summary>
        /// ToString方法
        /// </summary>
        public static MethodInfo ToStringMethodInfo { get; } = typeof(object).GetMethod(nameof(object.ToString));
        static StepHandlerHelper()
        {
            RuntimeDataIndexerPropertyInfo = RuntimeDataType.GetProperty("Item");
        }
        /// <summary>
        /// 获得字典索引值表达式
        /// </summary>
        /// <param name="mValue"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IndexExpression GetDictionaryIndexValueExpression(ParameterExpression mValue, string name)
        {
            IndexExpression tempExpression = Expression.MakeIndex(mValue, RuntimeDataIndexerPropertyInfo, new[] { Expression.Constant(name) });
            return tempExpression;
        }
    }
}
