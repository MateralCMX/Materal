using System.Reflection.Emit;
using System.Reflection;
using Materal.Abstractions;
using System.Collections.Concurrent;
using System.Text;
using System.Data.SqlTypes;

namespace Materal.Extensions.DependencyInjection
{
    /// <summary>
    /// 装饰器构建器
    /// </summary>
    public static class DecoratorBuilder
    {
        /// <summary>
        /// 空Object数组方法
        /// </summary>
        private static readonly MethodInfo _emptyObjectArrayMethodInfo;
        /// <summary>
        /// 空Object数组方法
        /// </summary>
        private static readonly MethodInfo _emptyTypeArrayMethodInfo;
        /// <summary>
        /// 执行拦截器方法(一个泛型)
        /// </summary>
        private static readonly MethodInfo _interceptorHelperHandlerMethodInfo1;
        /// <summary>
        /// 执行拦截器方法(两个泛型)
        /// </summary>
        private static readonly MethodInfo _interceptorHelperHandlerMethodInfo2;
        /// <summary>
        /// typeof方法
        /// </summary>
        private static readonly MethodInfo _typeofMethodInfo;
        /// <summary>
        /// 类型存储器
        /// </summary>
        private static readonly ConcurrentDictionary<Type, Type> _typeStore = new();
        /// <summary>
        /// 程序集构建器
        /// </summary>
        private static AssemblyBuilder? _assemblyBuilder;
        /// <summary>
        /// 模块构建器
        /// </summary>
        private static ModuleBuilder? _moduleBuilder;
        /// <summary>
        /// 命名空间
        /// </summary>
        private const string _namespace = "Materal.Extensions.DependencyInjection.DecoratorObjects";
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <exception cref="MateralException"></exception>
        static DecoratorBuilder()
        {
            MethodInfo emptyArrayMethodInfo = typeof(Array).GetMethod("Empty") ?? throw new MateralException("获取Array.Empty方法失败");
            _emptyObjectArrayMethodInfo = emptyArrayMethodInfo.MakeGenericMethod(typeof(object));
            _emptyTypeArrayMethodInfo = emptyArrayMethodInfo.MakeGenericMethod(typeof(Type));
            _typeofMethodInfo = typeof(Type).GetMethod("GetTypeFromHandle", new Type[] { typeof(RuntimeTypeHandle) }) ?? throw new MateralException("获取方法typeof失败");
            MethodInfo[] methodInfos = typeof(InterceptorHelper).GetMethods().Where(m => m.Name == "Handler").ToArray();
            foreach (MethodInfo methodInfo in methodInfos)
            {
                if (!methodInfo.IsGenericMethod) continue;
                Type[] genericArgumentTypes = methodInfo.GetGenericArguments();
                switch (genericArgumentTypes.Length)
                {
                    case 1:
                        _interceptorHelperHandlerMethodInfo1 = methodInfo;
                        break;
                    case 2:
                        _interceptorHelperHandlerMethodInfo2 = methodInfo;
                        break;
                }
            }
            if (_interceptorHelperHandlerMethodInfo1 is null || _interceptorHelperHandlerMethodInfo2 is null) throw new MateralException("获取InterceptorHelper.Handler方法失败");
        }
        /// <summary>
        /// 定义程序集
        /// </summary>
        /// <returns></returns>
        private static AssemblyBuilder DefineAssembly()
        {
            if (_assemblyBuilder is not null) return _assemblyBuilder;
            AssemblyName assemblyName = new(_namespace);
            _assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            return _assemblyBuilder;
        }
        /// <summary>
        /// 定义模块
        /// </summary>
        /// <param name="assemblyBuilder"></param>
        /// <returns></returns>
        private static ModuleBuilder DefineModule(AssemblyBuilder assemblyBuilder)
        {
            if (_moduleBuilder is not null) return _moduleBuilder;
            _moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyBuilder.GetName().Name + ".dll");
            return _moduleBuilder;
        }
        /// <summary>
        /// 定义类型
        /// </summary>
        /// <param name="moduleBuilder"></param>
        /// <param name="objType"></param>
        /// <returns></returns>
        private static TypeBuilder DefineType(ModuleBuilder moduleBuilder, Type objType)
        {
            Type[] interfaceTypes = objType.GetInterfaces();
            TypeBuilder typeBuilder = moduleBuilder.DefineType($"{_namespace}.{objType.Name}", TypeAttributes.Public, null, interfaceTypes);
            FieldBuilder serviceFieldBuilder = DefineServiceField(typeBuilder, objType);
            FieldBuilder interceptorHelperFieldBuilder = DefineInterceptorHelperField(typeBuilder, objType);
            DefineConstructor(typeBuilder, serviceFieldBuilder, interceptorHelperFieldBuilder, objType);
            foreach (Type interfaceType in interfaceTypes)
            {
                DefineMethods(typeBuilder, serviceFieldBuilder, interceptorHelperFieldBuilder, interfaceType, objType);
            }
            return typeBuilder;
        }
        /// <summary>
        /// 定义Service字段
        /// </summary>
        /// <param name="typeBuilder"></param>
        /// <param name="objType"></param>
        /// <returns></returns>
        private static FieldBuilder DefineServiceField(TypeBuilder typeBuilder, Type objType)
        {
            FieldBuilder serviceFieldBuilder = typeBuilder.DefineField("Service", objType, FieldAttributes.Public | FieldAttributes.InitOnly);
            return serviceFieldBuilder;
        }
        /// <summary>
        /// 定义_interceptorHelper字段
        /// </summary>
        /// <param name="typeBuilder"></param>
        /// <param name="objType"></param>
        /// <returns></returns>
        private static FieldBuilder DefineInterceptorHelperField(TypeBuilder typeBuilder, Type objType)
        {
            FieldBuilder serviceFieldBuilder = typeBuilder.DefineField("_interceptorHelper", objType, FieldAttributes.Public | FieldAttributes.InitOnly);
            return serviceFieldBuilder;
        }
        /// <summary>
        /// 定义构造方法
        /// </summary>
        /// <returns></returns>
        private static ConstructorBuilder DefineConstructor(TypeBuilder typeBuilder, FieldBuilder serviceFieldBuilder, FieldBuilder interceptorHelperFieldBuilder, Type objType)
        {
            ConstructorBuilder constructorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, new Type[] { objType, typeof(InterceptorHelper) });
            ILGenerator il = constructorBuilder.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            ConstructorInfo? constructorInfo = typeof(object).GetConstructor(Type.EmptyTypes);
            if (constructorInfo is not null)
            {
                il.Emit(OpCodes.Call, constructorInfo);
            }
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Stfld, serviceFieldBuilder);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_2);
            il.Emit(OpCodes.Stfld, interceptorHelperFieldBuilder);
            il.Emit(OpCodes.Ret);
            return constructorBuilder;
        }
        /// <summary>
        /// 定义方法
        /// </summary>
        /// <param name="typeBuilder"></param>
        /// <param name="serviceFieldBuilder"></param>
        /// <param name="interceptorHelperFieldBuilder"></param>
        /// <param name="interfaceType"></param>
        /// <param name="objType"></param>
        /// <returns></returns>
        private static List<MethodBuilder> DefineMethods(TypeBuilder typeBuilder, FieldBuilder serviceFieldBuilder, FieldBuilder interceptorHelperFieldBuilder, Type interfaceType, Type objType)
        {
            List<MethodBuilder> result = new();
            MethodInfo[] methodInfos = interfaceType.GetMethods();
            foreach (MethodInfo methodInfo in methodInfos)
            {
                MethodBuilder? methodBuilder = DefineMethod(typeBuilder, serviceFieldBuilder, interceptorHelperFieldBuilder, methodInfo, interfaceType, objType);
                if (methodBuilder is null) continue;
                result.Add(methodBuilder);
            }
            return result;
        }
        /// <summary>
        /// 定义方法
        /// </summary>
        /// <param name="typeBuilder"></param>
        /// <param name="serviceFieldBuilder"></param>
        /// <param name="interceptorHelperFieldBuilder"></param>
        /// <param name="interfaceMethodInfo"></param>
        /// <param name="interfaceType"></param>
        /// <param name="objType"></param>
        /// <returns></returns>
        /// <exception cref="MateralException"></exception>
        private static MethodBuilder? DefineMethod(TypeBuilder typeBuilder, FieldBuilder serviceFieldBuilder, FieldBuilder interceptorHelperFieldBuilder, MethodInfo interfaceMethodInfo, Type interfaceType, Type objType)
        {
            Type[] interfaceMethodParameterTypes = interfaceMethodInfo.GetParameters().Select(m => m.ParameterType).ToArray();
            MethodInfo? objMethodInfo = null;
            Type[] genericTypes = Array.Empty<Type>();
            if (interfaceMethodInfo.IsGenericMethod)
            {
                genericTypes = interfaceMethodInfo.GetGenericArguments();
            }
            objMethodInfo = InterceptorHelper.GetMethodInfo(objType, interfaceMethodInfo.Name, interfaceMethodParameterTypes, genericTypes);
            if (objMethodInfo is null) return null;
            MethodAttributes methodAttributes = MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual | MethodAttributes.Final;
            MethodBuilder methodBuilder = typeBuilder.DefineMethod(interfaceMethodInfo.Name, methodAttributes, interfaceMethodInfo.ReturnType, interfaceMethodParameterTypes);
            if (interfaceMethodInfo.IsGenericMethod)
            {
                methodBuilder.DefineGenericParameters(new[] { "T" });
                methodBuilder.DefineParameter(1, ParameterAttributes.None, "value");
            }
            ILGenerator ilGenerator = methodBuilder.GetILGenerator();
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldfld, interceptorHelperFieldBuilder);
            ilGenerator.Emit(OpCodes.Ldstr, interfaceMethodInfo.Name);
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldfld, serviceFieldBuilder);

            if (interfaceMethodParameterTypes.Length <= 0)
            {
                ilGenerator.Emit(OpCodes.Call, _emptyObjectArrayMethodInfo);
                ilGenerator.Emit(OpCodes.Call, _emptyTypeArrayMethodInfo);
            }
            else
            {
                ilGenerator.Emit(OpCodes.Ldc_I4, interfaceMethodParameterTypes.Length);
                ilGenerator.Emit(OpCodes.Newarr, typeof(object));
                for (int i = 0; i < interfaceMethodParameterTypes.Length; i++)
                {
                    ilGenerator.Emit(OpCodes.Dup);
                    ilGenerator.Emit(OpCodes.Ldc_I4, i);
                    ilGenerator.Emit(OpCodes.Ldarg, i + 1);
                    ilGenerator.Emit(OpCodes.Box, interfaceMethodParameterTypes[i]);
                    ilGenerator.Emit(OpCodes.Stelem_Ref);
                }
                ilGenerator.Emit(OpCodes.Ldc_I4, interfaceMethodParameterTypes.Length);
                ilGenerator.Emit(OpCodes.Newarr, typeof(Type));
                for (int i = 0; i < interfaceMethodParameterTypes.Length; i++)
                {
                    ilGenerator.Emit(OpCodes.Dup);
                    ilGenerator.Emit(OpCodes.Ldc_I4, i);
                    ilGenerator.Emit(OpCodes.Ldtoken, interfaceMethodParameterTypes[i]);
                    ilGenerator.Emit(OpCodes.Call, _typeofMethodInfo);
                    ilGenerator.Emit(OpCodes.Stelem_Ref);
                }
            }
            if (!interfaceMethodInfo.IsGenericMethod)
            {
                ilGenerator.Emit(OpCodes.Call, _emptyTypeArrayMethodInfo);
            }
            else
            {
                ilGenerator.Emit(OpCodes.Ldc_I4, genericTypes.Length);
                ilGenerator.Emit(OpCodes.Newarr, typeof(Type));
                for (int i = 0; i < genericTypes.Length; i++)
                {
                    ilGenerator.Emit(OpCodes.Dup);
                    ilGenerator.Emit(OpCodes.Ldc_I4, i);
                    ilGenerator.Emit(OpCodes.Ldtoken, genericTypes[i]);
                    ilGenerator.Emit(OpCodes.Call, _typeofMethodInfo);
                    ilGenerator.Emit(OpCodes.Stelem_Ref);
                }
            }
            if (interfaceMethodInfo.ReturnType == typeof(void))
            {
                ilGenerator.Emit(OpCodes.Callvirt, _interceptorHelperHandlerMethodInfo1.MakeGenericMethod(interfaceType));
                ilGenerator.Emit(OpCodes.Pop);
            }
            else
            {
                ilGenerator.Emit(OpCodes.Callvirt, _interceptorHelperHandlerMethodInfo2.MakeGenericMethod(interfaceType, interfaceMethodInfo.ReturnType));
            }
            ilGenerator.Emit(OpCodes.Ret);
            return methodBuilder;
        }
        /// <summary>
        /// 获得装饰器类型
        /// </summary>
        /// <param name="objType"></param>
        /// <returns></returns>
        /// <exception cref="MateralException"></exception>
        public static Type GetDecoratorType(Type objType)
        {
            if (_typeStore.ContainsKey(objType)) return _typeStore[objType];
            AssemblyBuilder assemblyBuilder = DefineAssembly();
            ModuleBuilder moduleBuilder = DefineModule(assemblyBuilder);
            TypeBuilder typeBuilder = DefineType(moduleBuilder, objType);
            Type decoratorType = typeBuilder.CreateType() ?? throw new MateralException($"[{objType.FullName}]创建装饰器类失败");
            _typeStore.TryAdd(objType, decoratorType);
            return decoratorType;
        }
        /// <summary>
        /// 构建装饰器对象
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        /// <exception cref="MateralException"></exception>
        public static object BuildDecoratorObject(object obj, IServiceProvider serviceProvider)
        {
            Type objType = obj.GetType();
            Type decoratorType = GetDecoratorType(objType);
            object myClassInstance = decoratorType.Instantiation(serviceProvider, obj) ?? throw new MateralException($"[{objType.FullName}]实例化装饰器失败");
            return myClassInstance;
        }
    }
}
