using System.Linq.Expressions;
using WorkflowCore.Interface;

namespace Materal.Workflow.WorkflowCoreExtension.Models
{
    /// <summary>
    /// 字典成员映射参数
    /// </summary>
    public class DictionaryMemberMapParameter : IStepParameter
    {
        private readonly LambdaExpression _source;
        private readonly LambdaExpression _target;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public DictionaryMemberMapParameter(LambdaExpression source, LambdaExpression target)
        {
            _source = source;
            _target = target;
        }
        /// <summary>
        /// 指派输入
        /// </summary>
        /// <param name="data"></param>
        /// <param name="body"></param>
        /// <param name="context"></param>
        public void AssignInput(object data, IStepBody body, IStepExecutionContext context)
        {
            Assign(data, _source, body, _target, context);
        }
        /// <summary>
        /// 指派输出
        /// </summary>
        /// <param name="data"></param>
        /// <param name="body"></param>
        /// <param name="context"></param>
        public void AssignOutput(object data, IStepBody body, IStepExecutionContext context)
        {
            Assign(body, _source, data, _target, context);
        }
        /// <summary>
        /// 指派成员
        /// </summary>
        /// <param name="sourceObject"></param>
        /// <param name="sourceExpr"></param>
        /// <param name="targetObject"></param>
        /// <param name="targetExpr"></param>
        /// <param name="context"></param>
        /// <exception cref="ArgumentException"></exception>
        private void Assign(object sourceObject, LambdaExpression sourceExpr, object targetObject, LambdaExpression targetExpr, IStepExecutionContext context)
        {
            object? resolvedValue = sourceExpr.Parameters.Count switch
            {
                1 => sourceExpr.Compile().DynamicInvoke(sourceObject),
                2 => sourceExpr.Compile().DynamicInvoke(sourceObject, context),
                _ => throw new ArgumentException(),
            };
            if (resolvedValue == null)
            {
                var defaultAssign = Expression.Lambda(Expression.Assign(targetExpr.Body, Expression.Default(targetExpr.ReturnType)), targetExpr.Parameters.Single());
                defaultAssign.Compile().DynamicInvoke(targetObject);
                return;
            }
            UnaryExpression valueExpr = Expression.Convert(Expression.Constant(resolvedValue), targetExpr.ReturnType);
            LambdaExpression assign = Expression.Lambda(Expression.Assign(targetExpr.Body, valueExpr), targetExpr.Parameters.Single());
            assign.Compile().DynamicInvoke(targetObject);
        }
    }
}
