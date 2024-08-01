namespace Materal.Test.ExtensionsTests
{
    [TestClass]
    public class TypeTest : BaseTest
    {
        public override void AddServices(IServiceCollection services)
        {
            base.AddServices(services);
            services.AddScoped<Type2>();
        }
        [TestMethod]
        public void InstantiationTest()
        {
            Type type1Type = typeof(Type1);
            Type1 type1obj1 = type1Type.Instantiation<Type1>([1]);
            Assert.IsTrue(type1obj1.A == 1 && type1obj1.Type2.B == 1, "实例化错误");
            Type1 type1obj2 = type1Type.Instantiation<Type1>(ServiceProvider, [1]);
            Assert.IsTrue(type1obj2.A == 1 && type1obj2.Type2.B == 0, "实例化错误");
            Type1 type1obj3 = type1Type.Instantiation<Type1>(ServiceProvider);
            Assert.IsTrue(type1obj3.A == 1 && type1obj3.Type2.B == 0, "实例化错误");
        }
        private class Type1
        {
            public Type1(Type2 type2)
            {
                A = 1;
                Type2 = type2;
            }
            public Type1(int a)
            {
                A = a;
                Type2 = new Type2() { B = 1 };
            }
            public Type1(int a, Type2 type2)
            {
                A = a;
                Type2 = type2;
            }
            public int A { get; set; }
            public Type2 Type2 { get; set; }
        }
        private class Type2
        {
            public int B { get; set; }
        }
    }
}
