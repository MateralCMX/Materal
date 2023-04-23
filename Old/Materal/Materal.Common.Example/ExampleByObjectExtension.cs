using System;
using System.ComponentModel;

namespace Materal.Common.Example
{
    public class ExampleByObjectExtension
    {
        #region GetDescription
        [Description("描述例子类1")]
        public class GetDescriptionExampleClass1
        {
            [Description("备注")]
            public string Remark { get; set; }
        }
        public enum GetDescriptionExampleEnum1 : byte
        {
            [Description("枚举1")]
            Enum1 = 0,
            [Description("枚举2")]
            Enum2 = 1
        }
        public void GetDescriptionExample1()
        {
            var exampleObject = new GetDescriptionExampleClass1
            {
                Remark = "备注信息"
            };
            string classDescription = exampleObject.GetDescription();
            Console.WriteLine($"类的描述是：{classDescription}");
            string enumDescription = GetDescriptionExampleEnum1.Enum1.GetDescription();
            Console.WriteLine($"枚举的描述是：{enumDescription}");
        }
        public void GetDescriptionExample2()
        {
            var exampleObject = new GetDescriptionExampleClass1
            {
                Remark = "备注信息"
            };
            string propertyDescription = exampleObject.GetDescription(nameof(GetDescriptionExampleClass1.Remark));
            Console.WriteLine($"属性的描述是：{propertyDescription}");
        }
        #endregion

        #region IsNullOrEmptyString
        public void IsNullOrEmptyStringExample1(object inputObject)
        {
            Console.WriteLine(inputObject.IsNullOrEmptyString() ? "对象是空或空字符串" : "对象是不为空并且不是空字符串");
        }
        #endregion

        #region IsNullOrWhiteSpaceString
        public void IsNullOrWhiteSpaceStringExample1(object inputObject)
        {
            Console.WriteLine(inputObject.IsNullOrWhiteSpaceString() ? "对象是空或空字符串或空格" : "对象是不为空并且不是空字符串并且不是空格");
        }
        #endregion
    }
}
