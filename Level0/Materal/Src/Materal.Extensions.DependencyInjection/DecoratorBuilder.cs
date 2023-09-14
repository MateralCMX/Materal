using System.Reflection.Emit;
using System.Reflection;

namespace Materal.Extensions.DependencyInjection
{
    /// <summary>
    /// 装饰器构建器
    /// </summary>
    public static class DecoratorBuilder
    {
        /// <summary>
        /// 构建装饰器对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static object BuildDecoratorObject(object obj)
        {
            ILGenerator il;
            Type objType = obj.GetType();
            Type[] interfaceTypes = objType.GetInterfaces();
            AssemblyName assemblyName = new($"Materal.AOP");
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.FullName + ".dll");
            TypeBuilder typeBuilder = moduleBuilder.DefineType($"Materal.AOP.{objType.Name}", TypeAttributes.Public, null, interfaceTypes);
            #region 定义字段
            FieldBuilder serviceFieldBuilder = typeBuilder.DefineField("_service", objType, FieldAttributes.Private | FieldAttributes.InitOnly);
            #endregion
            #region 定义构造方法
            ConstructorBuilder constructorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, new Type[] { objType });
            il = constructorBuilder.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            ConstructorInfo constructorInfo = typeof(object).GetConstructor(Type.EmptyTypes);
            if (constructorInfo is not null)
            {
                il.Emit(OpCodes.Call, constructorInfo);
            }
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Stfld, serviceFieldBuilder);
            il.Emit(OpCodes.Ret);
            #endregion
            #region 定义方法
            MethodInfo emptyArrayMethodInfo = typeof(Array).GetMethod("Empty") ?? throw new Exception("获取Array.Empty方法失败");
            emptyArrayMethodInfo = emptyArrayMethodInfo.MakeGenericMethod(typeof(object));

            MethodInfo handlerMethodInfo = typeof(InterceptorHelper).GetMethod("Handler", new Type[] { typeof(string), typeof(object), typeof(object[]) }) ?? throw new Exception("获取Array.Empty方法失败");
            foreach (Type interfaceType in interfaceTypes)
            {
                MethodInfo[] methodInfos = interfaceType.GetMethods();
                foreach (MethodInfo methodInfo in methodInfos)
                {
                    Type[] parameterTypes = methodInfo.GetParameters().Select(m => m.ParameterType).ToArray();
                    MethodInfo method = objType.GetMethod(methodInfo.Name, parameterTypes);
                    if (method is null) continue;
                    MethodAttributes methodAttributes = MethodAttributes.Public | MethodAttributes.ReuseSlot | MethodAttributes.Virtual | MethodAttributes.HideBySig;
                    MethodBuilder methodBuilder = typeBuilder.DefineMethod(methodInfo.Name, methodAttributes, methodInfo.ReturnType, parameterTypes);
                    il = methodBuilder.GetILGenerator();
                    il.Emit(OpCodes.Ldstr, methodInfo.Name);
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldfld, serviceFieldBuilder);
                    if (parameterTypes.Length <= 0)
                    {
                        il.Emit(OpCodes.Call, emptyArrayMethodInfo);
                    }
                    else
                    {
                        il.Emit(OpCodes.Ldc_I4_1);
                        il.Emit(OpCodes.Newarr, typeof(object));
                        il.Emit(OpCodes.Dup);
                        il.Emit(OpCodes.Ldc_I4_0);
                        il.Emit(OpCodes.Ldarg_1);
                        il.Emit(OpCodes.Stelem_Ref);
                    }
                    il.Emit(OpCodes.Call, handlerMethodInfo.MakeGenericMethod(interfaceType));
                    il.Emit(OpCodes.Pop);
                    il.Emit(OpCodes.Ret);












                    //MethodAttributes methodAttributes = MethodAttributes.Public | MethodAttributes.ReuseSlot | MethodAttributes.Virtual | MethodAttributes.HideBySig;
                    //MethodBuilder methodBuilder = typeBuilder.DefineMethod(methodInfo.Name, methodAttributes, methodInfo.ReturnType, parameterTypes);
                    //il = methodBuilder.GetILGenerator();
                    //if (method is not null)
                    //{
                    //    il.Emit(OpCodes.Ldarg_0);
                    //    il.Emit(OpCodes.Ldfld, serviceFieldBuilder);
                    //    for (int i = 0; i < parameterTypes.Length; i++)
                    //    {
                    //        il.Emit(OpCodes.Ldarg, i + 1);
                    //    }
                    //    il.Emit(OpCodes.Callvirt, method);
                    //}
                    //il.Emit(OpCodes.Ret);
                }
            }
            #endregion
            Type myClassType = typeBuilder.CreateTypeInfo() ?? throw new Exception($"[{objType.FullName}]创建装饰器类失败");
            object myClassInstance = Activator.CreateInstance(myClassType, obj) ?? throw new Exception($"[{objType.FullName}]实例化装饰器失败");
            return myClassInstance;
        }
    }
}
