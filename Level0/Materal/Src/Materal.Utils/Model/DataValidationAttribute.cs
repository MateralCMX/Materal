using AspectCore.DynamicProxy;
using System.Reflection;

namespace Materal.Utils.Model
{
    /// <summary>
    /// 数据验证特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class DataValidationAttribute : AbstractInterceptorAttribute
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            InvokeBefore(context);
            await context.Invoke(next);
        }
        /// <summary>
        /// 执行之前
        /// </summary>
        private void InvokeBefore(AspectContext context)
        {
            ParameterInfo[] parameterInfos = context.ImplementationMethod.GetParameters();
            for (var i = 0; i < parameterInfos.Length; i++)
            {
                object contextParameter = context.Parameters[i];
                contextParameter.Validation();
            }
        }
    }
}
