using System.ComponentModel.DataAnnotations;

namespace Materal.Test.ExtensionsTests.ObjectTests
{
    [TestClass]
    public class ValidationTest : BaseTest
    {
        [TestMethod]
        public void ValidationValueTest()
        {
            CustomModel model = new();
            model.DictionaryProperty.Add("Test", new());
            if (!model.Validation(out string errorMessage))
            {
                Console.WriteLine(errorMessage);
            }
        }
        private class CustomModel
        {
            //[Required, Min(5), Max(10)]
            //public int IntProperty { get; set; } = 6;
            //[Required, MinLength(1), MaxLength(2)]
            //public List<CustomModel2> ListProperty { get; set; } = new();
            [Required, MinLength(1), MaxLength(2)]
            public Dictionary<string, CustomModel2> DictionaryProperty { get; set; } = new();
            //[Required]
            //public CustomModel2 ClassProperty { get; set; } = new();
        }
        private class CustomModel2
        {
            [Required]
            public CustomModel3 Class2Property { get; set; } = new();
        }
        private class CustomModel3
        {
            [Required, Min(5), Max(10)]
            public int IntProperty { get; set; } = 4;
        }
    }
}
