using Materal.TFMS.EventBus;

namespace Materal.TFMS.TestClient
{
    public class NewRegistrationFormEvent : IntegrationEvent
    {
        /// <summary>
        /// 报名表唯一标识
        /// </summary>
        public Guid RegistrationFormID { get; set; }
        /// <summary>
        /// 班级唯一标识
        /// </summary>
        public Guid ClassID { get; set; }
        /// <summary>
        /// 学生唯一标识
        /// </summary>
        public Guid? StudentID { get; set; }
        /// <summary>
        /// 课时
        /// </summary>
        public decimal? SchoolHour { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public decimal SumPrice { get; set; }
        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}