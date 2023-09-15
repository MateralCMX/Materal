using Materal.Extensions.DependencyInjection;

namespace Materal.Test.ExtensionsTests.AOPDITests
{
    public class TestServiceImpl : IService
    {
        public readonly ServiceImpl Service;
        private readonly InterceptorHelper _interceptorHelper;
        public string Name { get => Service.Name; set => Service.Name = value; }
        public string GetName => Service.Name;
        public string SetName { set => Service.SetName = value; }
        public TestServiceImpl(ServiceImpl service, InterceptorHelper interceptorHelper)
        {
            Service = service;
            _interceptorHelper = interceptorHelper;
        }
        public void SayHello() => _interceptorHelper.Handler<IService>(nameof(SayHello), Service, Array.Empty<object>(), Array.Empty<Type>(), Array.Empty<Type>());
        public void SayHello(string name) => _interceptorHelper.Handler<IService>(nameof(SayHello), Service, new object[] { name }, new Type[] { typeof(string) }, Array.Empty<Type>());
        public void SayHello(string name1, string name2) => _interceptorHelper.Handler<IService>(nameof(SayHello), Service, new object[] { name1, name2 }, new Type[] { typeof(string), typeof(string) }, Array.Empty<Type>());
        public string GetMessage() => _interceptorHelper.Handler<IService, string>(nameof(GetMessage), Service, Array.Empty<object>(), Array.Empty<Type>(), Array.Empty<Type>());
        public string GetMessage(string name) => _interceptorHelper.Handler<IService, string>(nameof(GetMessage), Service, new object[] { name }, new Type[] { typeof(string) }, Array.Empty<Type>());
        public string GetMessage(string name1, string name2) => _interceptorHelper.Handler<IService, string>(nameof(GetMessage), Service, new object[] { name1, name2 }, new Type[] { typeof(string), typeof(string) }, Array.Empty<Type>());
        public int GetIndex() => _interceptorHelper.Handler<IService, int>(nameof(GetIndex), Service, Array.Empty<object>(), Array.Empty<Type>(), Array.Empty<Type>());
        public T GetValue<T>(T value) => _interceptorHelper.Handler<IService, T>(nameof(GetValue), Service, new object?[] { value }, new Type[] { typeof(T) }, new Type[] { typeof(T) });
        public T GetTypeByName<T>(string value) => _interceptorHelper.Handler<IService, T>(nameof(GetTypeByName), Service, new object?[] { value }, new Type[] { typeof(string) }, new Type[] { typeof(T) });
        public (bool boolValue, int intValue, float floatValue, double doubleValue, decimal decimalValue, string stringValue, DateTime dateTimeValue, Guid guidValue, object objectValue, MyClassValue customValue, object[] objectValues) TestParams(bool boolValue, int intValue, float floatValue, double doubleValue, decimal decimalValue, string stringValue, DateTime dateTimeValue, Guid guidValue, object objectValue, MyClassValue customValue, params object[] objectValues)
            => _interceptorHelper.Handler<IService, (bool boolValue, int intValue, float floatValue, double doubleValue, decimal decimalValue, string stringValue, DateTime dateTimeValue, Guid guidValue, object objectValue, MyClassValue customValue, object[] objectValues)>(
                nameof(TestParams),
                Service,
                new object[] { boolValue, intValue, floatValue, doubleValue, decimalValue, stringValue, dateTimeValue, guidValue, objectValue, customValue, objectValues },
                new Type[] { typeof(bool), typeof(int), typeof(float), typeof(double), typeof(decimal), typeof(string), typeof(DateTime), typeof(Guid), typeof(object), typeof(MyClassValue), typeof(object) },
                Array.Empty<Type>());
        public Task<(bool boolValue, int intValue, float floatValue, double doubleValue, decimal decimalValue, string stringValue, DateTime dateTimeValue, Guid guidValue, object objectValue, MyClassValue customValue, object[] objectValues)> TestParamsAsync(bool boolValue, int intValue, float floatValue, double doubleValue, decimal decimalValue, string stringValue, DateTime dateTimeValue, Guid guidValue, object objectValue, MyClassValue customValue, params object[] objectValues)
            => _interceptorHelper.Handler<IService, Task<(bool boolValue, int intValue, float floatValue, double doubleValue, decimal decimalValue, string stringValue, DateTime dateTimeValue, Guid guidValue, object objectValue, MyClassValue customValue, object[] objectValues)>>(
                nameof(TestParams),
                Service,
                new object[] { boolValue, intValue, floatValue, doubleValue, decimalValue, stringValue, dateTimeValue, guidValue, objectValue, customValue, objectValues },
                new Type[] { typeof(bool), typeof(int), typeof(float), typeof(double), typeof(decimal), typeof(string), typeof(DateTime), typeof(Guid), typeof(object), typeof(MyClassValue), typeof(object) },
                Array.Empty<Type>());

        public Task SayHelloAsync() => _interceptorHelper.Handler<IService, Task>(nameof(SayHello), Service, Array.Empty<object>(), Array.Empty<Type>(), Array.Empty<Type>());
        public Task<string> GetMessageAsync() => _interceptorHelper.Handler<IService, Task<string>>(nameof(SayHello), Service, Array.Empty<object>(), Array.Empty<Type>(), Array.Empty<Type>());
    }
}