using System;
using Demo.Domain.Views;
using Materal.TTA.Common;

namespace Demo.Domain.Repositories.Views
{
    /// <summary>
    /// 学生信息仓储
    /// </summary>
    public interface IStudentInfoViewRepository : IEFSubordinateRepository<StudentInfoView, Guid>
    {
    }
}
