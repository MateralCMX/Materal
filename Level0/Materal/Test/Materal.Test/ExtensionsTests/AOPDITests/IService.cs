﻿namespace Materal.Test.ExtensionsTests.AOPDITests
{
    public interface IService
    {
        [MyInterceptor]
        void SayHello();
        [MyInterceptor]
        void SayHello(string name);
        [MyInterceptor]
        void SayHello(string name1, string name2);
        [MyInterceptor]
        string GetMessage();
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