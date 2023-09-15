namespace Materal.Test.ExtensionsTests.AOPDITests
{
    [MyInterceptor3(Order = 2)]
    public interface IService
    {
        string Name { get; set; }
        string GetName { get; }
        string SetName { set; }
        [MyInterceptor(Order = -1)]
        [MyInterceptor2(Order = 1)]
        void SayHello();
        [MyInterceptor]
        Task SayHelloAsync();
        [MyInterceptor]
        void SayHello(string name);
        [MyInterceptor]
        void SayHello(string name1, string name2);
        [MyInterceptor]
        string GetMessage();
        [MyInterceptor]
        Task<string> GetMessageAsync();
        [MyInterceptor]
        string GetMessage(string name);
        [MyInterceptor]
        string GetMessage(string name1, string name2);
        [MyInterceptor]
        int GetIndex();
        [MyInterceptor]
        T GetValue<T>(T value);
        [MyInterceptor]
        T GetTypeByName<T>(string value);
        [MyInterceptor]
        (bool boolValue, int intValue, float floatValue, double doubleValue, decimal decimalValue, string stringValue, DateTime dateTimeValue, Guid guidValue, object objectValue, MyClassValue customValue, object[] objectValues) TestParams(bool boolValue, int intValue, float floatValue, double doubleValue, decimal decimalValue, string stringValue, DateTime dateTimeValue, Guid guidValue, object objectValue, MyClassValue customValue, params object[] objectValues);
        [MyInterceptor]
        Task<(bool boolValue, int intValue, float floatValue, double doubleValue, decimal decimalValue, string stringValue, DateTime dateTimeValue, Guid guidValue, object objectValue, MyClassValue customValue, object[] objectValues)> TestParamsAsync(bool boolValue, int intValue, float floatValue, double doubleValue, decimal decimalValue, string stringValue, DateTime dateTimeValue, Guid guidValue, object objectValue, MyClassValue customValue, params object[] objectValues);
    }
    public class MyClassValue { }
}