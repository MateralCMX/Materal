using Materal.Extensions.DependencyInjection;

namespace Materal.Test.ExtensionsTests.AOPDITests
{
    public class TestServiceImpl : IService
    {
#pragma warning disable CS8603 // 可能返回 null 引用。
        public readonly ServiceImpl Service;
        public TestServiceImpl(ServiceImpl service) => Service = service;
        public void SayHello() => InterceptorHelper.Handler<IService>(nameof(SayHello), Service, Array.Empty<object>(), Array.Empty<Type>(), Array.Empty<Type>());
        public void SayHello(string name) => InterceptorHelper.Handler<IService>(nameof(SayHello), Service, new object[] { name }, new Type[] { typeof(string) }, Array.Empty<Type>());
        public void SayHello(string name1, string name2) => InterceptorHelper.Handler<IService>(nameof(SayHello), Service, new object[] { name1, name2 }, new Type[] { typeof(string), typeof(string) }, Array.Empty<Type>());
        public string GetMessage() => InterceptorHelper.Handler<IService, string>(nameof(GetMessage), Service, Array.Empty<object>(), Array.Empty<Type>(), Array.Empty<Type>());
        public string GetMessage(string name) => InterceptorHelper.Handler<IService, string>(nameof(GetMessage), Service, new object[] { name }, new Type[] { typeof(string) }, Array.Empty<Type>());
        public string GetMessage(string name1, string name2) => InterceptorHelper.Handler<IService, string>(nameof(GetMessage), Service, new object[] { name1, name2 }, new Type[] { typeof(string), typeof(string) }, Array.Empty<Type>());
        public int GetIndex() => InterceptorHelper.Handler<IService, int>(nameof(GetIndex), Service, Array.Empty<object>(), Array.Empty<Type>(), Array.Empty<Type>());
        public T GetValue<T>(T value) => InterceptorHelper.Handler<IService, T>(nameof(GetValue), Service, new object?[] { value }, new Type[] { typeof(T) }, new Type[] { typeof(T) });
        public T GetTypeByName<T>(string value) => InterceptorHelper.Handler<IService, T>(nameof(GetTypeByName), Service, new object?[] { value }, new Type[] { typeof(string) }, new Type[] { typeof(T) });
        public (bool boolValue, int intValue, float floatValue, double doubleValue, decimal decimalValue, string stringValue, DateTime dateTimeValue, Guid guidValue, object objectValue, MyClassValue customValue, object[] objectValues) TestParams(bool boolValue, int intValue, float floatValue, double doubleValue, decimal decimalValue, string stringValue, DateTime dateTimeValue, Guid guidValue, object objectValue, MyClassValue customValue, params object[] objectValues)
            => InterceptorHelper.Handler<IService, (bool boolValue, int intValue, float floatValue, double doubleValue, decimal decimalValue, string stringValue, DateTime dateTimeValue, Guid guidValue, object objectValue, MyClassValue customValue, object[] objectValues)>(
                nameof(TestParams), 
                Service, 
                new object[] { boolValue, intValue, floatValue, doubleValue, decimalValue, stringValue, dateTimeValue, guidValue, objectValue, customValue, objectValues }, 
                new Type[] { typeof(bool), typeof(int), typeof(float), typeof(double), typeof(decimal), typeof(string), typeof(DateTime), typeof(Guid), typeof(object), typeof(MyClassValue), typeof(object) },
                Array.Empty<Type>());
        public Task<(bool boolValue, int intValue, float floatValue, double doubleValue, decimal decimalValue, string stringValue, DateTime dateTimeValue, Guid guidValue, object objectValue, MyClassValue customValue, object[] objectValues)> TestParamsAsync(bool boolValue, int intValue, float floatValue, double doubleValue, decimal decimalValue, string stringValue, DateTime dateTimeValue, Guid guidValue, object objectValue, MyClassValue customValue, params object[] objectValues) 
            => InterceptorHelper.Handler<IService, Task<(bool boolValue, int intValue, float floatValue, double doubleValue, decimal decimalValue, string stringValue, DateTime dateTimeValue, Guid guidValue, object objectValue, MyClassValue customValue, object[] objectValues)>>(
                nameof(TestParams),
                Service,
                new object[] { boolValue, intValue, floatValue, doubleValue, decimalValue, stringValue, dateTimeValue, guidValue, objectValue, customValue, objectValues },
                new Type[] { typeof(bool), typeof(int), typeof(float), typeof(double), typeof(decimal), typeof(string), typeof(DateTime), typeof(Guid), typeof(object), typeof(MyClassValue), typeof(object) },
                Array.Empty<Type>());
#pragma warning restore CS8603 // 可能返回 null 引用。
    }
}