using Materal.Workflow.WorkflowCoreExtension.Extensions;
using Materal.Workflow.WorkflowCoreExtension.Models;
using System.Linq.Expressions;
using System.Reflection;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Services;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 节点处理器基类
    /// </summary>
    /// <typeparam name="TStepData"></typeparam>
    public abstract class BaseStepHandler<TStepData> : IStepHandler<TStepData>
        where TStepData : class, IStepData
    {
        private static MethodInfo? _getPropertyObjectValueMethodInfo;
        /// <summary>
        /// 获取属性值方法
        /// </summary>
        protected static MethodInfo GetPropertyObjectValueMethodInfo
        {
            get
            {
                if (_getPropertyObjectValueMethodInfo != null) return _getPropertyObjectValueMethodInfo;
                MethodInfo[] methodInfos = typeof(WorkflowCoreExtension.Extensions.ObjectExtension).GetMethods();
                foreach (MethodInfo methodInfo in methodInfos)
                {
                    if (methodInfo.Name != "GetPropertyValue") continue;
                    if (methodInfo.IsGenericMethod) continue;
                    if (methodInfo.ReturnType != typeof(object)) continue;
                    _getPropertyObjectValueMethodInfo = methodInfo;
                    break;
                }
                if (_getPropertyObjectValueMethodInfo == null) throw new WorkflowException($"获取方法{nameof(WorkflowCoreExtension.Extensions.ObjectExtension)}.{nameof(WorkflowCoreExtension.Extensions.ObjectExtension.GetPropertyValue)}失败");
                return _getPropertyObjectValueMethodInfo;
            }
        }
        /// <summary>
        /// 构建节点
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="stepData"></param>
        /// <param name="stepHandlerBus"></param>
        /// <returns></returns>
        public object Build(object stepBuilder, IStepData stepData, IStepHandlerBus stepHandlerBus)
        {
            if (stepData is TStepData targetStepData)
            {
                stepBuilder = BuildStep(stepBuilder, targetStepData, stepHandlerBus);
            }
            return stepBuilder;
        }
        /// <summary>
        /// 构建节点
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="stepData"></param>
        /// <param name="stepHandlerBus"></param>
        /// <returns></returns>
        public abstract object BuildStep(object stepBuilder, TStepData stepData, IStepHandlerBus stepHandlerBus);
        #region 节点动作
        /// <summary>
        /// 下一步
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="nextStepData"></param>
        /// <param name="stepHandlerBus"></param>
        /// <returns></returns>
        protected object Next(object stepBuilder, IStepData? nextStepData, IStepHandlerBus stepHandlerBus)
        {
            if (nextStepData == null) return stepBuilder;
            stepBuilder = stepHandlerBus.BuildStep(stepBuilder, nextStepData);
            return stepBuilder;
        }
        /// <summary>
        /// 执行子流程
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="stepData"></param>
        /// <param name="stepHandlerBus"></param>
        /// <returns></returns>
        protected object Do(object stepBuilder, IStepData stepData, IStepHandlerBus stepHandlerBus)
        {
            if (stepData is not StartStepData)
            {
                stepData = new StartStepData()
                {
                    Next = stepData
                };
            }
            Action<IWorkflowBuilder<Dictionary<string, object?>>> action = newStepBuilder => stepHandlerBus.BuildStep(newStepBuilder, stepData);
            stepBuilder = InvokeMethodByMethodName(stepBuilder, nameof(Do), new object[] { action });
            return stepBuilder;
        }
        /// <summary>
        /// 输入
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="stepData"></param>
        /// <param name="inputs"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException"></exception>
        protected object Input(object stepBuilder, IStepData stepData, ICollection<InputData>? inputs)
        {
            if (inputs == null || inputs.Count <= 0) return stepBuilder;
            foreach (InputData inputData in inputs)
            {
                stepBuilder = Input(stepBuilder, stepData, inputData);
            }
            return stepBuilder;
        }
        /// <summary>
        /// 输入
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="stepData"></param>
        /// <param name="inputData"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException"></exception>
        protected object Input(object stepBuilder, IStepData stepData, InputData inputData)
        {
            Type stepBuilderType = stepBuilder.GetType();
            Type[] genericTypes = stepBuilderType.GenericTypeArguments;
            if (genericTypes.Length != 2) throw new WorkflowException("stepBuilder类型错误");
            Type stepType = genericTypes[1];
            #region StepBodyData
            ParameterExpression mStepProperty = Expression.Parameter(stepType, "m");
            Expression tempExpression = Expression.Property(mStepProperty, inputData.StepProperty);
            LambdaExpression stepProperty = Expression.Lambda(tempExpression, mStepProperty);
            #endregion
            #region Source
            ParameterExpression mValue = Expression.Parameter(StepHandlerHelper.RuntimeDataType, "m");
            ParameterExpression nValue = Expression.Parameter(typeof(IStepExecutionContext), "n");
            LambdaExpression value;
            string stringValue = inputData.Value is string tempString ? tempString : inputData.Value.ToString();
            switch (inputData.ValueSource)
            {
                case InputValueSourceEnum.RuntimeDataProperty:
                    tempExpression = StepHandlerHelper.GetDictionaryIndexValueExpression(mValue, stringValue);
                    value = Expression.Lambda(tempExpression, mValue);
                    break;
                case InputValueSourceEnum.BuildDataProperty:
                    object dataValue = stepData.GetBuildDataValue<object>(stringValue);
                    tempExpression = Expression.Constant(dataValue);
                    value = Expression.Lambda(tempExpression, mValue);
                    break;
                case InputValueSourceEnum.Constant:
                    tempExpression = Expression.Constant(inputData.Value);
                    value = Expression.Lambda(tempExpression, mValue);
                    break;
                case InputValueSourceEnum.InputData:
                    tempExpression = Expression.Property(nValue, "Item");
                    if (!string.IsNullOrWhiteSpace(stringValue))
                    {
                        tempExpression = Expression.Call(GetPropertyObjectValueMethodInfo, tempExpression, Expression.Constant(stringValue));
                    }
                    value = Expression.Lambda(tempExpression, mValue, nValue);
                    break;
                default:
                    throw new WorkflowException("值类型错误");
            }
            #endregion
            WorkflowStep step = GetWorkflowStep(stepBuilder);
            step.Inputs.Add(new DictionaryMemberMapParameter(value, stepProperty));
            return stepBuilder;
        }
        /// <summary>
        /// 输出
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="outputs"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException"></exception>
        protected object Output(object stepBuilder, ICollection<OutputData>? outputs)
        {
            if (outputs == null || outputs.Count <= 0) return stepBuilder;
            foreach (OutputData outputData in outputs)
            {
                stepBuilder = Output(stepBuilder, outputData);
            }
            return stepBuilder;
        }
        /// <summary>
        /// 输出
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="outputData"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException"></exception>
        protected object Output(object stepBuilder, OutputData outputData)
        {
            Type stepBuilderType = stepBuilder.GetType();
            Type[] genericTypes = stepBuilderType.GenericTypeArguments;
            if (genericTypes.Length != 2) throw new WorkflowException("stepBuilder类型错误");
            Type runtimeDataType = genericTypes[0];
            Type stepType = genericTypes[1];
            #region StepBodyData
            ParameterExpression nValue = Expression.Parameter(runtimeDataType, "n");
            Expression tempExpression = StepHandlerHelper.GetDictionaryIndexValueExpression(nValue, outputData.RuntimeDataProperty);
            LambdaExpression dataProperty = Expression.Lambda(tempExpression, nValue);
            #endregion
            #region Source
            ParameterExpression mValue = Expression.Parameter(stepType, "m");
            tempExpression = Expression.Property(mValue, outputData.StepProperty);
            tempExpression = Expression.Convert(tempExpression, typeof(object));
            LambdaExpression value = Expression.Lambda(tempExpression, mValue);
            #endregion
            WorkflowStep step = GetWorkflowStep(stepBuilder);
            step.Outputs.Add(new DictionaryMemberMapParameter(value, dataProperty));
            return stepBuilder;
        }
        /// <summary>
        /// 补偿
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="stepData"></param>
        /// <param name="stepHandlerBus"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException"></exception>
        protected object CompensateWith(object stepBuilder, ThenStepData stepData, IStepHandlerBus stepHandlerBus)
        {
            Type? stepType = stepData.StepBodyType.GetTypeByTypeName(Array.Empty<object>());
            if (stepType == null) throw new WorkflowException($"找不到节点类型{stepData.StepBodyType}");
            MethodInfo[] methodInfos = stepBuilder.GetType().GetMethods().Where(m => m.Name == "CompensateWith" && m.IsGenericMethod && m.GetParameters().Length == 1).ToArray();
            MethodInfo? methodInfo = methodInfos.FirstOrDefault();
            if (methodInfo == null) throw new WorkflowException($"获取方法失败");
            methodInfo = methodInfo.MakeGenericMethod(stepType);
            Action<object> HandlerCompensateWith = m => HandlerThenStep(m, stepData, stepHandlerBus);
            stepBuilder = methodInfo.Invoke(stepBuilder, new object?[] { HandlerCompensateWith });
            return stepBuilder;
        }
        /// <summary>
        /// 处理普通节点
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="stepData"></param>
        /// <param name="stepHandlerBus"></param>
        /// <returns></returns>
        protected object HandlerThenStep(object stepBuilder, ThenStepData stepData, IStepHandlerBus stepHandlerBus)
        {
            stepBuilder = Input(stepBuilder, stepData, stepData.Inputs);
            stepBuilder = Output(stepBuilder, stepData.Outputs);
            stepBuilder = Next(stepBuilder, stepData.Next, stepHandlerBus);
            return stepBuilder;
        }
        #endregion
        #region Utils
        /// <summary>
        /// 获得决策表达式
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="stepData"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException"></exception>
        protected Expression<Func<Dictionary<string, object?>, bool>> GetDecisionConditionExpression(List<DecisionConditionData> conditions, IStepData stepData)
        {
            if (conditions == null || conditions.Count <= 0) return m => true;
            Expression<Func<Dictionary<string, object?>, bool>> result;
            ParameterExpression mValue = Expression.Parameter(typeof(Dictionary<string, object?>), "m");
            Expression? tempExpression = GetConditionExpression(conditions, stepData, mValue, null);
            LambdaExpression lambdaExpression = Expression.Lambda(tempExpression, mValue);
            if (lambdaExpression is not Expression<Func<Dictionary<string, object?>, bool>>) throw new WorkflowException("组合比较表达式失败");
            result = (Expression<Func<Dictionary<string, object?>, bool>>)lambdaExpression;
            return result;
        }
        /// <summary>
        /// 获得决策表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conditions"></param>
        /// <param name="stepData"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException"></exception>
        protected Expression<Func<Dictionary<string, object?>, T, bool>> GetDecisionConditionExpression<T>(List<DecisionConditionData> conditions, IStepData stepData)
        {
            if (conditions == null || conditions.Count <= 0) return (m, n) => true;
            Expression<Func<Dictionary<string, object?>, T, bool>> result;
            ParameterExpression mValue = Expression.Parameter(typeof(Dictionary<string, object?>), "m");
            ParameterExpression nValue = Expression.Parameter(typeof(T), "n");
            Expression? tempExpression = GetConditionExpression(conditions, stepData, mValue, nValue);
            LambdaExpression lambdaExpression = Expression.Lambda(tempExpression, mValue, nValue);
            if (lambdaExpression is not Expression<Func<Dictionary<string, object?>, T, bool>>) throw new WorkflowException("组合比较表达式失败");
            result = (Expression<Func<Dictionary<string, object?>, T, bool>>)lambdaExpression;
            return result;
        }
        /// <summary>
        /// 获得比较表达式
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="stepData"></param>
        /// <param name="mValue"></param>
        /// <param name="nValue"></param>
        /// <returns></returns>
        protected Expression? GetConditionExpression(List<DecisionConditionData> conditions, IStepData stepData, ParameterExpression mValue, ParameterExpression? nValue)
        {
            Expression? tempExpression = null;
            foreach (DecisionConditionData decisionConditionData in conditions)
            {
                Expression conditionExpression = GetConditionExpression(decisionConditionData, stepData, mValue, nValue);
                if (tempExpression == null)
                {
                    tempExpression = conditionExpression;
                }
                else
                {
                    if (decisionConditionData.Condition == ConditionEnum.And)
                    {
                        tempExpression = Expression.AndAlso(tempExpression, conditionExpression);
                    }
                    else
                    {
                        tempExpression = Expression.OrElse(tempExpression, conditionExpression);
                    }
                }
            }
            return tempExpression;
        }
        /// <summary>
        /// 获取比较表达式
        /// </summary>
        /// <param name="stepData"></param>
        /// <param name="decisionConditionData"></param>
        /// <param name="mValue"></param>
        /// <param name="nValue"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException"></exception>
        protected Expression GetConditionExpression(DecisionConditionData decisionConditionData, IStepData stepData, ParameterExpression mValue, ParameterExpression? nValue)
        {
            Expression leftValue = GetConditionValueExpression(decisionConditionData.LeftValue, decisionConditionData.LeftValueSource, stepData, mValue, nValue);
            Expression rightValue = GetConditionValueExpression(decisionConditionData.RightValue, decisionConditionData.RightValueSource, stepData, mValue, nValue);
            if(leftValue.Type != rightValue.Type)
            {
                Type tempType;
                if (leftValue.Type != typeof(object))
                {
                    tempType = leftValue.Type;
                    rightValue = SyncType(rightValue, tempType);
                    leftValue = SyncType(leftValue, tempType);
                }
                else
                {
                    tempType = rightValue.Type;
                    rightValue = SyncType(rightValue, tempType);
                    leftValue = SyncType(leftValue, tempType);
                }
            }
            Expression result = decisionConditionData.ComparisonType switch
            {
                ComparisonTypeEnum.Equal => Expression.Equal(leftValue, rightValue),
                ComparisonTypeEnum.NotEqual => Expression.NotEqual(leftValue, rightValue),
                ComparisonTypeEnum.GreaterThan => Expression.GreaterThanOrEqual(leftValue, rightValue),
                ComparisonTypeEnum.LessThan => Expression.LessThan(leftValue, rightValue),
                ComparisonTypeEnum.GreaterThanOrEqual => Expression.GreaterThanOrEqual(leftValue, rightValue),
                ComparisonTypeEnum.LessThanOrEqual => Expression.LessThanOrEqual(leftValue, rightValue),
                _ => throw new WorkflowException("未知比较类型"),
            };
            return result;
        }
        /// <summary>
        /// 同步类型
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        private Expression SyncType(Expression expression, Type targetType)
        {
            Expression result = expression;
            if (targetType == typeof(short) || targetType == typeof(int) || targetType == typeof(long) ||
                targetType == typeof(ushort) || targetType == typeof(uint) || targetType == typeof(ulong) ||
                targetType == typeof(ushort) || targetType == typeof(uint) || targetType == typeof(ulong) ||
                targetType == typeof(float) || targetType == typeof(double))
            {
                MethodInfo? convertToDecimalMethodInfo = GetConvertToDecimalMethodInfo(expression.Type);
                if(convertToDecimalMethodInfo != null)
                {
                    result = Expression.Call(null, convertToDecimalMethodInfo, expression);
                }
            }
            else if(targetType == typeof(string) && expression.Type != targetType)
            {
                result = Expression.Call(expression, StepHandlerHelper.ToStringMethodInfo);
            }
            return result;
        }
        /// <summary>
        /// 获得转换为Decimal方法
        /// </summary>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public MethodInfo? GetConvertToDecimalMethodInfo(Type sourceType) => typeof(Convert).GetMethod(nameof(Convert.ToDecimal), new Type[] { sourceType });
        /// <summary>
        /// 获得比较值表达式
        /// </summary>
        /// <param name="value"></param>
        /// <param name="valueSource"></param>
        /// <param name="stepData"></param>
        /// <param name="mValue"></param>
        /// <param name="nValue"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException"></exception>
        protected Expression GetConditionValueExpression(object value, ValueSourceEnum valueSource, IStepData stepData, ParameterExpression mValue, ParameterExpression? nValue)
        {
            Expression valueExpression;
            string stringValue = value is string tempString ? tempString : value.ToString();
            switch (valueSource)
            {
                case ValueSourceEnum.RuntimeDataProperty:
                    valueExpression = StepHandlerHelper.GetDictionaryIndexValueExpression(mValue, stringValue);
                    break;
                case ValueSourceEnum.BuildDataProperty:
                    object dataValue = stepData.GetBuildDataValue<object>(stringValue);
                    valueExpression = Expression.Constant(dataValue);
                    break;
                case ValueSourceEnum.Constant:
                    valueExpression = Expression.Constant(value);
                    break;
                default:
                    throw new WorkflowException("值类型错误");
            }
            return valueExpression;
        }
        /// <summary>
        /// 根据方法名称执行泛型方法
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="methodName"></param>
        /// <param name="genericTypes"></param>
        /// <param name="args"></param>
        /// <param name="argTypes"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException"></exception>
        protected object InvokeGenericMethodByMethodName(object stepBuilder, string methodName, Type[] genericTypes, object?[]? args = null, Type[]? argTypes = null)
        {
            InitArgs(ref args, ref argTypes);
            MethodInfo methodInfo = GetMethodInfo(stepBuilder, methodName, genericTypes, argTypes);
            if (!methodInfo.IsGenericMethod) throw new WorkflowException($"方法{methodName}不是泛型方法");
            methodInfo = methodInfo.MakeGenericMethod(genericTypes);
            stepBuilder = methodInfo.Invoke(stepBuilder, args);
            return stepBuilder;
        }
        /// <summary>
        /// 获得方法信息
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="methodName"></param>
        /// <param name="genericTypes"></param>
        /// <param name="argTypes"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException"></exception>
        protected MethodInfo GetMethodInfo(object stepBuilder, string methodName, Type[]? genericTypes = null, Type[]? argTypes = null)
        {
            Type stepBuilderType = stepBuilder.GetType();
            MethodInfo? methodInfo = stepBuilderType.GetMethod(methodName, argTypes);
            if (methodInfo == null)
            {
                bool isGenericMethod = genericTypes != null && genericTypes.Length > 0;
                MethodInfo[] methodInfos = stepBuilderType.GetMethods().Where(m => m.Name == methodName && m.IsGenericMethod == isGenericMethod).ToArray();
                MethodInfo? candidateMethodInfo = null;
                foreach (MethodInfo itemMethodInfo in methodInfos)
                {
                    if (itemMethodInfo.IsGenericMethod && itemMethodInfo.IsGenericMethodDefinition && genericTypes != null && genericTypes.Length > 0)
                    {
                        itemMethodInfo.MakeGenericMethod(genericTypes);
                    }
                    ParameterInfo[] parameterInfos = itemMethodInfo.GetParameters();
                    if (argTypes != null && argTypes.Length > 0)
                    {
                        if (parameterInfos.Length != argTypes.Length) continue;
                        bool[] isOk = new bool[parameterInfos.Length];
                        for (int i = 0; i < argTypes.Length; i++)
                        {
                            ParameterInfo parameterInfo = parameterInfos[i];
                            Type parameterType = parameterInfo.ParameterType;
                            Type argType = argTypes[i];
                            if (parameterType.IsInterface)
                            {
                                candidateMethodInfo = null;
                                Type interfaceType = argType.GetInterface(parameterType.Name);
                                if (interfaceType != null)
                                {
                                    isOk[i] = true;
                                }
                            }
                            else if (parameterInfo.ParameterType == typeof(object))
                            {
                                candidateMethodInfo = itemMethodInfo;
                                isOk[i] = false;
                            }
                            else if (argType.FullName == parameterType.FullName || argType.IsAssignableTo(parameterType))
                            {
                                candidateMethodInfo = null;
                                isOk[i] = true;
                            }
                        }
                        if (!isOk.Any(m => m == false))
                        {
                            methodInfo = itemMethodInfo;
                            break;
                        }
                    }
                    else
                    {
                        methodInfo = itemMethodInfo;
                        break;
                    }
                }
                methodInfo ??= candidateMethodInfo;
            }
            if (methodInfo == null) throw new WorkflowException($"获取方法{methodName}失败");
            return methodInfo;
        }
        /// <summary>
        /// 根据方法名称执行方法
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        /// <param name="argTypes"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException"></exception>
        protected object InvokeMethodByMethodName(object stepBuilder, string methodName, object?[]? args = null, Type[]? argTypes = null)
        {
            InitArgs(ref args, ref argTypes);
            MethodInfo methodInfo = GetMethodInfo(stepBuilder, methodName, null, argTypes);
            if (methodInfo.IsGenericMethod) throw new WorkflowException($"方法{methodName}是泛型方法");
            stepBuilder = methodInfo.Invoke(stepBuilder, args);
            return stepBuilder;
        }
        /// <summary>
        /// 初始化参数
        /// </summary>
        protected void InitArgs(ref object?[]? args, ref Type[]? argTypes)
        {
            if (argTypes == null)
            {
                if (args == null)
                {
                    args = Array.Empty<object?>();
                    argTypes = Array.Empty<Type>();
                }
                else
                {
                    List<Type> tempTypes = new();
                    foreach (object? arg in args)
                    {
                        if (arg == null) throw new WorkflowException("参数包含null,请手动指定argTypes");
                        tempTypes.Add(arg.GetType());
                    }
                    argTypes = tempTypes.ToArray();
                }
            }
            else
            {
                List<object?> tempArgs = new();
                if (args != null && args.Length > 0)
                {
                    for (int i = 0; i < args.Length; i++)
                    {
                        object? arg = args[i];
                        if (arg != null)
                        {
                            Type argType = arg.GetType();
                            if (argType.FullName != argTypes[i].FullName)
                            {
                                if (argType.IsClass) throw new WorkflowException($"第[{i}]个参数类型错误");
                                if (argTypes[i].Name == typeof(Nullable<>).Name && argTypes[i].IsGenericType)
                                {
                                    if (argType.FullName != argTypes[i].GenericTypeArguments[0].FullName) throw new WorkflowException($"第[{i}]个参数类型错误");
                                }
                            }
                        }
                        tempArgs.Add(arg);
                    }
                }
                while (tempArgs.Count < argTypes.Length)
                {
                    tempArgs.Add(null);
                }
                args = tempArgs.ToArray();
            }
            if (args.Length != argTypes.Length) throw new WorkflowException("初始化参数失败");
        }
        /// <summary>
        /// 实例化节点构建器
        /// </summary>
        /// <param name="workflowBuilder"></param>
        /// <param name="workflowStep"></param>
        /// <param name="stepType"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException"></exception>
        protected object InstantiationStepBuilder(IWorkflowBuilder workflowBuilder, WorkflowStep workflowStep, Type stepType)
        {
            Type workflowBuilderType = workflowBuilder.GetType();
            Type[] genericTypes = workflowBuilderType.GenericTypeArguments;
            if (genericTypes.Length != 1) throw new WorkflowException("workflowBuilder类型错误");
            Type runtimeDataType = genericTypes[0];
            Type stepBuilderType = typeof(StepBuilder<,>);
            stepBuilderType = stepBuilderType.MakeGenericType(runtimeDataType, stepType);
            object result = stepBuilderType.Instantiation(workflowBuilder, workflowStep);
            return result;
        }
        /// <summary>
        /// 实例化工作流构建器
        /// </summary>
        /// <param name="stepType"></param>
        /// <returns></returns>
        protected WorkflowStep InstantiationWorkflowStep(Type stepType)
        {
            Type workflowStepType = typeof(WorkflowStep<>);
            workflowStepType = workflowStepType.MakeGenericType(stepType);
            WorkflowStep result = (WorkflowStep)workflowStepType.Instantiation();
            return result;
        }
        /// <summary>
        /// 获得工作流构建器
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <returns></returns>
        protected IWorkflowBuilder<Dictionary<string, object?>> GetWorkflowBuilder(object stepBuilder) => stepBuilder.GetPropertyValue<IWorkflowBuilder<Dictionary<string, object?>>>(nameof(WorkflowBuilder));
        /// <summary>
        /// 获得工作流节点
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <returns></returns>
        protected WorkflowStep GetWorkflowStep(object stepBuilder) => stepBuilder.GetPropertyValue<WorkflowStep>(nameof(IStepExecutionContext.Step));
        #endregion
    }
}
