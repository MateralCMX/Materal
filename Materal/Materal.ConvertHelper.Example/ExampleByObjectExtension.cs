using System;

namespace Materal.ConvertHelper.Example
{
    public class ExampleByObjectExtension
    {
        #region ConvertTo
        public void ConvertToExample1()
        {
            //const string exampleString1 = "1993";
            //int intResult = exampleString1.ConvertTo<int>();
            //float floatResult = exampleString1.ConvertTo<float>();
            //double doubleResult = exampleString1.ConvertTo<double>();
            //decimal decimalResult = exampleString1.ConvertTo<decimal>();
            //const int exampleInt1 = 1993;
            //string stringResult = exampleInt1.ConvertTo<string>();
            //const string exampleString2 = "1993-04-20";
            //DateTime dateTimeResult = exampleString2.ConvertTo<DateTime>();
            //string exampleString3 = Guid.NewGuid().ToString();
            //Guid guidResult = exampleString3.ConvertTo<Guid>();
            //CustomClass1 customClass1 = new CustomClass1
            //{
            //    Age = 1,
            //    ID = Guid.NewGuid(),
            //    Name = "Materal",
            //    CustomClass3 = new CustomClass3
            //    {
            //        Remark = "Test"
            //    }
            //};
            //CustomClass2 customClass2 = customClass1.ConvertTo<CustomClass2>();
            //ObjectExtension.AddConvertDictionary<CustomClass1, CustomClass4>(m => new CustomClass4
            //{
            //    ID = m.ID,
            //    Name3 = m.Name,
            //    Age = m.Age + 10,
            //    CustomClass3 = new CustomClass3
            //    {
            //        Remark = "新的备注"
            //    }
            //});
            //CustomClass4 customClass4 = customClass1.ConvertTo<CustomClass4>();
        }
        #endregion

        public class CustomClass1
        {
            public Guid ID { get; set; }

            public string Name { get; set; }

            public int Age { get; set; }
            public CustomClass3 CustomClass3 { get; set; }
        }

        public class CustomClass2
        {
            public Guid ID { get; set; }

            public string Name { get; set; }

            public int Age { get; set; }
            public CustomClass3 CustomClass3 { get; set; }
        }
        public class CustomClass3
        {
            public string Remark { get; set; }
        }
        public class CustomClass4
        {
            public Guid ID { get; set; }

            public string Name3 { get; set; }

            public int Age { get; set; }
            public CustomClass3 CustomClass3 { get; set; }
        }
    }
}
