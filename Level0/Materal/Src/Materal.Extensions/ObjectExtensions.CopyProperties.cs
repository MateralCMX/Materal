using System.Collections.Concurrent;
using System.Reflection.Emit;

namespace Materal.Extensions
{
    /// <summary>
    /// Object扩展
    /// </summary>
    public static partial class ObjectExtensions
    {
        private readonly static ConcurrentDictionary<Type, Delegate> _copyPropertiesFunc = [];
        private const string _copyPropertiesFuncName = "CopyPropertiesByIL";
        private static readonly Type _isCopyFuncType = typeof(Func<string, bool>);
        private static readonly MethodInfo _isCopyFuncInvokeMethodInfo = _isCopyFuncType.GetMethod("Invoke") ?? throw new InvalidOperationException("获取Invoke方法失败");
        /// <summary>
        /// 创建属性复制方法
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        /// <exception cref="MateralException"></exception>
        private static DynamicMethod CreateCopyPropertiesDynamicMethod(Type sourceType, Type targetType)
        {
            DynamicMethod dynamicMethod = new("CopyProperties", null, [sourceType, targetType, _isCopyFuncType], sourceType.Module);
            ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
            int index = 0;
            foreach (PropertyInfo sourcePropertyInfo in sourceType.GetProperties())
            {
                if (!sourcePropertyInfo.CanRead) continue;
                PropertyInfo? targetPropertyInfo = targetType.GetProperty(sourcePropertyInfo.Name);
                if (targetPropertyInfo is null || !targetPropertyInfo.CanWrite) continue;
                MethodInfo? getMethod = sourcePropertyInfo.GetGetMethod();
                if (getMethod is null) continue;
                MethodInfo? setMethod = targetPropertyInfo.GetSetMethod();
                if (setMethod is null) continue;
                if (sourcePropertyInfo.PropertyType == targetPropertyInfo.PropertyType)
                {
                    WriteILTheSameType(ilGenerator, sourcePropertyInfo.Name, getMethod, setMethod);
                }
                else if (targetPropertyInfo.PropertyType.IsNullableType(sourcePropertyInfo.PropertyType))
                {
                    WriteILTheTargetNullType(ilGenerator, sourcePropertyInfo.Name, getMethod, setMethod, sourcePropertyInfo, targetPropertyInfo);
                }
                else if (sourcePropertyInfo.PropertyType.IsNullableType(targetPropertyInfo.PropertyType))
                {
                    index = WriteILTheSourceNullType(ilGenerator, sourcePropertyInfo.Name, getMethod, setMethod, sourcePropertyInfo, index);
                }
            }
            // 返回
            ilGenerator.Emit(OpCodes.Ret);
            return dynamicMethod;
        }
        /// <summary>
        /// 写入IL-源是可空类型
        /// </summary>
        /// <param name="ilGenerator"></param>
        /// <param name="name"></param>
        /// <param name="getMethod"></param>
        /// <param name="setMethod"></param>
        /// <param name="sourcePropertyInfo"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static int WriteILTheSourceNullType(ILGenerator ilGenerator, string name, MethodInfo getMethod, MethodInfo setMethod, PropertyInfo sourcePropertyInfo, int index)
        {
            /*
            if ((isCopy == null || isCopy("Age")) && source.Age.HasValue)
            {
                target.Age = source.Age.Value;
            }
             */
            // 定义本地变量
            ilGenerator.DeclareLocal(sourcePropertyInfo.PropertyType);
            // 加载第三个参数（isCopy）到计算堆栈
            ilGenerator.Emit(OpCodes.Ldarg_2);
            // 如果isCopy为null，跳转到指定标签
            Label isCopyNullLabel = ilGenerator.DefineLabel();
            ilGenerator.Emit(OpCodes.Brfalse_S, isCopyNullLabel);
            // 加载isCopy和name到计算堆栈
            ilGenerator.Emit(OpCodes.Ldarg_2);
            ilGenerator.Emit(OpCodes.Ldstr, name);
            // 调用Func<string, bool>的Invoke方法
            ilGenerator.Emit(OpCodes.Callvirt, _isCopyFuncInvokeMethodInfo);
            // 如果Invoke返回false，跳转到指定标签
            Label isCopyFalseLabel = ilGenerator.DefineLabel();
            ilGenerator.Emit(OpCodes.Brfalse_S, isCopyFalseLabel);
            // 标记isCopyNullLabel的位置
            ilGenerator.MarkLabel(isCopyNullLabel);
            // 加载第一个参数（source）到计算堆栈
            ilGenerator.Emit(OpCodes.Ldarg_0);
            // 调用获取属性方法
            ilGenerator.Emit(OpCodes.Callvirt, getMethod);
            // 将结果存储到本地变量
            ilGenerator.Emit(OpCodes.Stloc, index);
            // 加载本地变量的地址到计算堆栈
            ilGenerator.Emit(OpCodes.Ldloca_S, index);
            // 调用Source的get_HasValue方法
            MethodInfo getHasValueMethod = sourcePropertyInfo.PropertyType.GetProperty(nameof(Nullable<int>.HasValue))?.GetGetMethod() ?? throw new Exception("获取HasValue失败");
            ilGenerator.Emit(OpCodes.Call, getHasValueMethod);
            // 如果get_HasValue返回false，跳转到指定标签
            ilGenerator.Emit(OpCodes.Brfalse_S, isCopyFalseLabel);
            // 加载第二个参数（target）和第一个参数（source）到计算堆栈
            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.Emit(OpCodes.Ldarg_0);
            // 调用BClass的get_Age方法
            ilGenerator.Emit(OpCodes.Callvirt, getMethod);
            // 将结果存储到本地变量
            ilGenerator.Emit(OpCodes.Stloc, index);
            // 加载本地变量的地址到计算堆栈
            ilGenerator.Emit(OpCodes.Ldloca_S, index);
            // 调用Source的get_Value方法
            MethodInfo getValueMethod = sourcePropertyInfo.PropertyType.GetProperty(nameof(Nullable<int>.Value))?.GetGetMethod() ?? throw new Exception("获取Value失败");
            ilGenerator.Emit(OpCodes.Call, getValueMethod);
            // 调用AClass的set_Age方法
            ilGenerator.Emit(OpCodes.Callvirt, setMethod);
            // 标记isCopyFalseLabel的位置
            ilGenerator.MarkLabel(isCopyFalseLabel);
            return index + 1;
        }
        /// <summary>
        /// 写入IL-目标是可空类型
        /// </summary>
        /// <param name="ilGenerator"></param>
        /// <param name="name"></param>
        /// <param name="getMethod"></param>
        /// <param name="setMethod"></param>
        /// <param name="sourcePropertyInfo"></param>
        /// <param name="targetPropertyInfo"></param>
        /// <returns></returns>
        private static void WriteILTheTargetNullType(ILGenerator ilGenerator, string name, MethodInfo getMethod, MethodInfo setMethod, PropertyInfo sourcePropertyInfo, PropertyInfo targetPropertyInfo)
        {
            /*
            if (isCopy == null || isCopy("Age"))
            {
                target.Age = new int?(source.Age);
            }
             */
            // 加载第三个参数（isCopy）到计算堆栈
            ilGenerator.Emit(OpCodes.Ldarg_2);
            // 如果isCopy为null，跳转到指定标签
            Label isCopyNullLabel = ilGenerator.DefineLabel();
            ilGenerator.Emit(OpCodes.Brfalse_S, isCopyNullLabel);
            // 加载isCopy和name到计算堆栈
            ilGenerator.Emit(OpCodes.Ldarg_2);
            ilGenerator.Emit(OpCodes.Ldstr, name);
            // 调用Func<string, bool>的Invoke方法
            ilGenerator.Emit(OpCodes.Callvirt, _isCopyFuncInvokeMethodInfo);
            // 如果Invoke返回false，跳转到指定标签
            Label isCopyFalseLabel = ilGenerator.DefineLabel();
            ilGenerator.Emit(OpCodes.Brfalse_S, isCopyFalseLabel);
            // 标记isCopyNullLabel的位置
            ilGenerator.MarkLabel(isCopyNullLabel);
            // 加载第二个参数（target）和第一个参数（source）到计算堆栈
            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.Emit(OpCodes.Ldarg_0);
            // 调用获取属性方法
            ilGenerator.Emit(OpCodes.Callvirt, getMethod);
            // 创建新的可空类型对象
            ConstructorInfo constructorInfo = targetPropertyInfo.PropertyType.GetConstructor([sourcePropertyInfo.PropertyType]) ?? throw new Exception("获取构造函数失败");
            ilGenerator.Emit(OpCodes.Newobj, constructorInfo);
            // 调用设置属性方法
            ilGenerator.Emit(OpCodes.Callvirt, setMethod);
            // 标记isCopyFalseLabel的位置
            ilGenerator.MarkLabel(isCopyFalseLabel);
        }
        /// <summary>
        /// 写入IL-相同类型
        /// </summary>
        /// <param name="ilGenerator"></param>
        /// <param name="name"></param>
        /// <param name="getMethod"></param>
        /// <param name="setMethod"></param>
        /// <returns></returns>
        private static void WriteILTheSameType(ILGenerator ilGenerator, string name, MethodInfo getMethod, MethodInfo setMethod)
        {
            /*
            if (isCopy == null || isCopy("Age"))
            {
                target.Age = source.Age;
            }
             */
            // 加载第三个参数（isCopy）到计算堆栈
            ilGenerator.Emit(OpCodes.Ldarg_2);
            // 如果isCopy为null，跳转到指定标签
            Label isCopyNullLabel = ilGenerator.DefineLabel();
            ilGenerator.Emit(OpCodes.Brfalse_S, isCopyNullLabel);
            // 加载isCopy和name到计算堆栈
            ilGenerator.Emit(OpCodes.Ldarg_2);
            ilGenerator.Emit(OpCodes.Ldstr, name);
            // 调用Func<string, bool>的Invoke方法
            ilGenerator.Emit(OpCodes.Callvirt, _isCopyFuncInvokeMethodInfo);
            // 如果Invoke返回false，跳转到指定标签
            Label isCopyFalseLabel = ilGenerator.DefineLabel();
            ilGenerator.Emit(OpCodes.Brfalse_S, isCopyFalseLabel);
            // 标记isCopyNullLabel的位置
            ilGenerator.MarkLabel(isCopyNullLabel);
            // 加载第二个参数（target）和第一个参数（source）到计算堆栈
            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.Emit(OpCodes.Ldarg_0);
            // 调用获取属性方法
            ilGenerator.Emit(OpCodes.Callvirt, getMethod);
            // 调用设置属性方法
            ilGenerator.Emit(OpCodes.Callvirt, setMethod);
            // 标记isCopyFalseLabel的位置
            ilGenerator.MarkLabel(isCopyFalseLabel);
        }
        /// <summary>
        /// 根据Type创建委托
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        private static Delegate CreateCopyPropertiesDelegate(Type sourceType, Type targetType)
        {
            DynamicMethod dynamicMethod = CreateCopyPropertiesDynamicMethod(sourceType, targetType);
            Type actionType = GetActionType(sourceType, targetType);
            Delegate result = dynamicMethod.CreateDelegate(actionType);
            return result;
        }
        /// <summary>
        /// 获取Action类型
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        private static Type GetActionType(Type sourceType, Type targetType) => typeof(Action<,,>).MakeGenericType(sourceType, targetType, typeof(Func<string, bool>));
        /// <summary>
        /// 属性复制sourceM->targetM
        /// </summary>
        /// <typeparam name="T">复制的模型</typeparam>
        /// <param name="source">复制源头对象</param>
        /// <param name="target">复制目标对象</param>
        /// <param name="isCopy">是否复制</param>
        /// <returns>复制的对象</returns>
        public static void CopyProperties<T>(this object source, T target, Func<string, bool>? isCopy)
        {
            if (source is null || target is null) return;
            Type sourceType = source.GetType();
            Type targetType = target.GetType();
            Type actionType = GetActionType(sourceType, targetType);
            if (!_copyPropertiesFunc.TryGetValue(actionType, out Delegate? action))
            {
                action = CreateCopyPropertiesDelegate(sourceType, targetType);
                if (action.GetType() != actionType) throw new InvalidOperationException("创建的委托类型不正确");
                _copyPropertiesFunc.TryAdd(actionType, action);
            }
            if (action is null) return;
            action.DynamicInvoke(source, target, isCopy);
        }
        /// <summary>
        /// 属性复制
        /// </summary>
        /// <typeparam name="T">复制的模型</typeparam>
        /// <param name="source">复制源头对象</param>
        /// <param name="isCopy">是否复制</param>
        /// <returns>复制的对象</returns>
        public static T CopyProperties<T>(this object source, Func<string, bool>? isCopy)
        {
            T result = typeof(T).Instantiation<T>();
            source.CopyProperties(target: result, isCopy: isCopy);
            return result;
        }
        /// <summary>
        /// 属性复制sourceM->targetM
        /// </summary>
        /// <typeparam name="T">复制的模型</typeparam>
        /// <param name="source">复制源头对象</param>
        /// <param name="target">复制目标对象</param>
        /// <param name="notCopyPropertyNames">不复制的属性名称</param>
        /// <returns>复制的对象</returns>
        public static void CopyProperties<T>(this object source, T target, params string[] notCopyPropertyNames)
        {
            if (notCopyPropertyNames.Length > 0)
            {
                source.CopyProperties(target: target, isCopy: prop => !notCopyPropertyNames.Any(m => m.Equals(prop)));
            }
            else
            {
                source.CopyProperties(target: target, isCopy: null);
            }
        }
        /// <summary>
        /// 属性复制
        /// </summary>
        /// <typeparam name="T">复制的模型</typeparam>
        /// <param name="source">复制源头对象</param>
        /// <param name="notCopyPropertyNames">不复制的属性名称</param>
        /// <returns>复制的对象</returns>
        public static T CopyProperties<T>(this object source, params string[] notCopyPropertyNames)
        {
            T result = typeof(T).Instantiation<T>();
            source.CopyProperties(target: result, notCopyPropertyNames: notCopyPropertyNames);
            return result;
        }
    }
}
