namespace Materal.Test.ExtensionsTests
{
    [TestClass]
    public class DateTimeHelperTest
    {
        [TestMethod]
        public void DateTimeOffsetToTimeStampTest()
        {
            DateTimeOffset beforeTime = DateTimeOffset.UtcNow;
            long timeStamp = beforeTime.GetTimeStamp();
            DateTimeOffset afterTime = DateTimeHelper.TimeStampToDateTimeOffset(timeStamp, DateTimeKind.Utc);
            Assert.AreEqual(beforeTime, afterTime);
            beforeTime = DateTimeOffset.Now;
            timeStamp = beforeTime.GetTimeStamp();
            afterTime = DateTimeHelper.TimeStampToDateTimeOffset(timeStamp, DateTimeKind.Local);
            Assert.AreEqual(beforeTime, afterTime);
        }
        [TestMethod]
        public void DateTimeToTimeStampTest()
        {
            DateTime beforeTime = DateTime.UtcNow;
            long timeStamp = beforeTime.GetTimeStamp();
            DateTime afterTime = DateTimeHelper.TimeStampToDateTime(timeStamp, beforeTime.Kind);
            Assert.AreEqual(beforeTime, afterTime);
            beforeTime = DateTime.Now;
            timeStamp = beforeTime.GetTimeStamp();
            afterTime = DateTimeHelper.TimeStampToDateTime(timeStamp, beforeTime.Kind);
            Assert.AreEqual(beforeTime, afterTime);
        }
    }
}
