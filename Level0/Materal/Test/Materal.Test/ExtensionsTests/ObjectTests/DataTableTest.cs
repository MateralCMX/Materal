using Materal.Extensions;
using System.Data;

namespace Materal.Test.ExtensionsTests.ObjectTests
{
    [TestClass]
    public class DataTableTest : BaseTest
    {
        [TestMethod]
        public void DataTableToListTest()
        {
            DataTable dt = new();
            dt.Columns.Add("ID");
            dt.Columns.Add("ID2");
            dt.Rows.Add(Guid.NewGuid(), Guid.NewGuid());
            dt.Rows.Add(Guid.NewGuid(), DBNull.Value);
            List<TestClass> testClasses = dt.ToList<TestClass>();
            Assert.AreEqual(testClasses.Count, 2);
        }
        public class TestClass
        {
            public Guid ID { get; set; }
            public Guid? ID2 { get; set; }
        }
    }
}
