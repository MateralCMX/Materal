namespace Materal.Test.ExtensionsTests.DependencyInjectionTests
{
    public class ServiceImpl : IService
    {
#pragma warning disable CS0649
        [PropertyInjection]
        private IRepository? _repository;
#pragma warning restore CS0649
        public void SayHello() => _repository?.SayHello();
    }
}


//using System.Diagnostics;

//namespace Materal.Test.ExtensionsTests.AOPDITests
//{
//    public class ServiceImpl : IService
//    {
//        private int _index = 0;
//        public string Name { get; set; } = "Materal";
//        public string GetName => Name;
//        public string SetName { set => Name = value; }
//        public int GetIndex() => _index++;
//        public string GetMessage() => "Hello World!";
//        public string GetMessage(string name) => $"Hello {name}!";
//        public string GetMessage(string name1, string name2) => $"Hello {name1} and {name2}!";

//        public async Task<string> GetMessageAsync() => await Task.FromResult("Hello World!");

//        public T GetTypeByName<T>(string value)
//        {
//            if (value is T result) return result;
//            throw new MateralException("暂时不支持String以外类型");
//        }
//        public T GetValue<T>(T value) => value;
//        //[MyInterceptor]
//        public void SayHello() => Debug.WriteLine(GetMessage());
//        public void SayHello(string name) => Debug.WriteLine(GetMessage(name));
//        public void SayHello(string name1, string name2) => Debug.WriteLine(GetMessage(name1, name2));

//        public async Task SayHelloAsync()
//        {
//            Debug.WriteLine(GetMessage());
//            await Task.CompletedTask;
//        }

//        public (bool boolValue, int intValue, float floatValue, double doubleValue, decimal decimalValue, string stringValue, DateTime dateTimeValue, Guid guidValue, object objectValue, MyClassValue customValue, object[] objectValues) TestParams(bool boolValue, int intValue, float floatValue, double doubleValue, decimal decimalValue, string stringValue, DateTime dateTimeValue, Guid guidValue, object objectValue, MyClassValue customValue, params object[] objectValues)
//            => (boolValue, intValue, floatValue, doubleValue, decimalValue, stringValue, dateTimeValue, guidValue, objectValue, customValue, objectValues);
//        public Task<(bool boolValue, int intValue, float floatValue, double doubleValue, decimal decimalValue, string stringValue, DateTime dateTimeValue, Guid guidValue, object objectValue, MyClassValue customValue, object[] objectValues)> TestParamsAsync(bool boolValue, int intValue, float floatValue, double doubleValue, decimal decimalValue, string stringValue, DateTime dateTimeValue, Guid guidValue, object objectValue, MyClassValue customValue, params object[] objectValues)
//            => Task.FromResult((boolValue, intValue, floatValue, doubleValue, decimalValue, stringValue, dateTimeValue, guidValue, objectValue, customValue, objectValues));
//    }
//}