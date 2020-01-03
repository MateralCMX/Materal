using System;
using System.Drawing;

namespace Materal.ConvertHelper.Example
{
    public class ExampleByObjectExtension
    {
        #region ConvertTo
        public void ConvertToExample1()
        {
            const string exampleString1 = "1993";
            var intResult = exampleString1.ConvertTo<int>();
            var floatResult = exampleString1.ConvertTo<float>();
            var doubleResult = exampleString1.ConvertTo<double>();
            var decimalResult = exampleString1.ConvertTo<decimal>();
            const int exampleInt1 = 1993;
            var stringResult = exampleInt1.ConvertTo<string>();
            const string exampleString2 = "1993-04-20";
            var dateTimeResult = exampleString2.ConvertTo<DateTime>();
            string exampleString3 = Guid.NewGuid().ToString();
            var guidResult = exampleString3.ConvertTo<Guid>();
            var customClass1 = new CustomClass1
            {
                Age = 1,
                ID = Guid.NewGuid(),
                Name = "Materal",
                CustomClass3 = new CustomClass3
                {
                    Remark = "Test"
                }
            };
            ObjectExtension.AddConvertDictionary(m =>
            {
                if (m is CustomClass1 tempClass)
                {
                    return new CustomClass2
                    {
                        ID = tempClass.ID,
                        Name = tempClass.Name,
                        Age = tempClass.Age + 10,
                        CustomClass3 = new CustomClass3
                        {
                            Remark = tempClass.CustomClass3.Remark ?? "新的备注"
                        }
                    };
                }
                return null;
            });
            var customClass2 = customClass1.ConvertTo<CustomClass2>();
            ObjectExtension.AddConvertDictionary(m =>
            {
                if (m is CustomClass1 tempClass)
                {
                    return new CustomClass4
                    {
                        ID = tempClass.ID,
                        Name3 = tempClass.Name,
                        Age = tempClass.Age + 10,
                        CustomClass3 = new CustomClass3
                        {
                            Remark = "新的备注"
                        }
                    };
                }
                return null;
            });
            var customClass4 = customClass1.ConvertTo<CustomClass4>();
            Bitmap qrCode = "Materal".ToQRCode();
            qrCode.Save(@"D:\Test\TestQRCode.jpg");
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
