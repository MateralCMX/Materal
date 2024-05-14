namespace Materal.Test.AbstractionsTests
{
    [TestClass]
    public class DateTimeTest : BaseTest
    {
        [TestMethod]
        public void DateTest()
        {
            // 测试构造函数
            Date date1 = new(2022, 12, 31);
            Assert.AreEqual(2022, date1.Year);
            Assert.AreEqual(12, date1.Month);
            Assert.AreEqual(31, date1.Day);
            // 测试闰年
            Date date2 = new(2020, 2, 29);
            Assert.IsTrue(date2.IsLeapYear);
            // 测试最大天数
            Assert.AreEqual(29, date2.MaxDay);
            // 测试添加年、月、日
            Date date3 = new(2022, 12, 31);
            date3 = date3.AddYear(1).AddMonth(1).AddDay(30);
            Assert.AreEqual(2024, date3.Year);
            Assert.AreEqual(3, date3.Month);
            Assert.AreEqual(1, date3.Day);
            // 测试日期比较
            Date date4 = new(2022, 1, 1);
            Date date5 = new(2022, 1, 2);
            Assert.IsTrue(date5 > date4);
            Assert.IsTrue(date5 >= date4);
            Assert.IsTrue(date4 < date5);
            Assert.IsTrue(date4 <= date5);
            Assert.IsFalse(date4 == date5);
            Assert.IsTrue(date4 != date5);
        }
        [TestMethod]
        public void TimeTest()
        {
            // 测试构造函数
            Time time1 = new(23, 59, 59);
            Assert.AreEqual(23, time1.Hour);
            Assert.AreEqual(59, time1.Minute);
            Assert.AreEqual(59, time1.Second);
            // 测试小时、分钟、秒的边界值
            Time time2 = new(24, 60, 60);
            Assert.AreEqual(23, time2.Hour);
            Assert.AreEqual(59, time2.Minute);
            Assert.AreEqual(59, time2.Second);
            Time time3 = new(-1, -1, -1);
            Assert.AreEqual(0, time3.Hour);
            Assert.AreEqual(0, time3.Minute);
            Assert.AreEqual(0, time3.Second);
            // 测试添加年、月、日
            Time time4 = new(0, 0, 0);
            time4 = time4.AddHour(1).AddMinute(5).AddSecond(200);
            Assert.AreEqual(1, time4.Hour);
            Assert.AreEqual(8, time4.Minute);
            Assert.AreEqual(20, time4.Second);
            // 测试时间比较
            Time time5 = new(12, 0, 0);
            Time time6 = new(12, 0, 1);
            Assert.IsTrue(time6 > time5);
            Assert.IsTrue(time6 >= time5);
            Assert.IsTrue(time5 < time6);
            Assert.IsTrue(time5 <= time6);
            Assert.IsFalse(time5 == time6);
            Assert.IsTrue(time5 != time6);
        }
    }
}
