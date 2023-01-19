using AspectCore.DynamicProxy;
using System.Reflection;

namespace Materal.Model
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DataValidationAttribute : AbstractInterceptorAttribute
    {
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
