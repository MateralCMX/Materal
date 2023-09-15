using Materal.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Materal.Test.ExtensionsTests.AOPDITests
{
    [TestClass]
    public class AOPDITest : BaseTest
    {
        private readonly IServiceProvider _serviceProvider;
        public AOPDITest()
        {
            IServiceCollection services = new ServiceCollection();
            //services.AddTransient<IService>(m => new ServiceImpl());
            services.AddTransient<IService, ServiceImpl>();
            //services.AddScoped<IService, ServiceImpl>();
            //services.AddSingleton<IService, ServiceImpl>();
            //_serviceProvider = services.BuildServiceProvider();
            services.AddInterceptor<GlobalInterceptorAttribute>((im, m) => im.DeclaringType == typeof(IService), -2);
            _serviceProvider = services.BuildMateralServiceProvider((serviceType, objType) => serviceType.Name.EndsWith("Service"));
        }
        /// <summary>
        /// SayHello测试
        /// </summary>
        [TestMethod]
        public void SayHelloScopeTest()
        {
            using IServiceScope serviceScope = _serviceProvider.CreateScope();
            IService aopService = serviceScope.ServiceProvider.GetRequiredService<IService>();
            aopService.SayHello();
        }
        /// <summary>
        /// SayHello测试
        /// </summary>
        [TestMethod]
        public void SayHelloTest()
        {
            IService aopService = _serviceProvider.GetRequiredService<IService>();
            aopService.SayHello();
        }
        /// <summary>
        /// 获得装饰器类型测试
        /// </summary>
        [TestMethod]
        public void GetDecoratorTypeTest()
        {
            IService aopService = _serviceProvider.GetRequiredService<IService>();
            IService aopService2 = _serviceProvider.GetRequiredService<IService>();
            int index = aopService.GetIndex();
            int index2 = aopService2.GetIndex();
            Assert.AreEqual(index, index2);//Transient
            //Assert.AreNotEqual(index, index2);//Singleton Scoped
        }
        /// <summary>
        /// 执行测试
        /// </summary>
        [TestMethod]
        public async Task InvokeTestAsync()
        {
            IService aopService = _serviceProvider.GetRequiredService<IService>();
            const string name1 = "Materal";
            const string name2 = "Alice";

            aopService.SayHello();

            aopService.SayHello(name1);

            aopService.SayHello(name1, name2);

            string message = aopService.GetMessage();
            Debug.WriteLine($"message={message}");
            Assert.AreEqual(message, "Hello World!");

            message = aopService.GetMessage(name1);
            Debug.WriteLine($"message={message}");
            Assert.AreEqual(message, $"Hello {name1}!");

            message = aopService.GetMessage(name1, name2);
            Debug.WriteLine($"message={message}");
            Assert.AreEqual(message, $"Hello {name1} and {name2}!");

            message = aopService.GetValue(name1);
            Debug.WriteLine($"message={message}");
            Assert.AreEqual(message, name1);

            message = aopService.GetTypeByName<string>(name1);
            Debug.WriteLine($"message={message}");
            Assert.AreEqual(message, name1);

            bool boolValue = true;
            int intValue = 1;
            float floatValue = 1.1F;
            double doubleValue = 1.1D;
            decimal decimalValue = 1.1M;
            string stringValue = name1;
            DateTime dateTimeValue = DateTime.Now;
            Guid guidValue = Guid.NewGuid();
            object objectValue = new { ID = Guid.NewGuid(), Name = name1 };
            MyClassValue customValue = new();
            object[] objectValues = { new object(), new object() };
            var result = aopService.TestParams(boolValue, intValue, floatValue, doubleValue, decimalValue, stringValue, dateTimeValue, guidValue, objectValue, customValue, objectValues);
            Assert.AreEqual(result.boolValue, boolValue);
            Assert.AreEqual(result.intValue, intValue);
            Assert.AreEqual(result.floatValue, floatValue);
            Assert.AreEqual(result.doubleValue, doubleValue);
            Assert.AreEqual(result.decimalValue, decimalValue);
            Assert.AreEqual(result.stringValue, stringValue);
            Assert.AreEqual(result.dateTimeValue, dateTimeValue);
            Assert.AreEqual(result.guidValue, guidValue);
            Assert.AreEqual(result.objectValue, objectValue);
            Assert.AreEqual(result.customValue, customValue);
            Assert.AreEqual(result.objectValues, objectValues);

            result = await aopService.TestParamsAsync(boolValue, intValue, floatValue, doubleValue, decimalValue, stringValue, dateTimeValue, guidValue, objectValue, customValue, objectValues);
            Assert.AreEqual(result.boolValue, boolValue);
            Assert.AreEqual(result.intValue, intValue);
            Assert.AreEqual(result.floatValue, floatValue);
            Assert.AreEqual(result.doubleValue, doubleValue);
            Assert.AreEqual(result.decimalValue, decimalValue);
            Assert.AreEqual(result.stringValue, stringValue);
            Assert.AreEqual(result.dateTimeValue, dateTimeValue);
            Assert.AreEqual(result.guidValue, guidValue);
            Assert.AreEqual(result.objectValue, objectValue);
            Assert.AreEqual(result.customValue, customValue);
            Assert.AreEqual(result.objectValues, objectValues);
        }
    }
}
