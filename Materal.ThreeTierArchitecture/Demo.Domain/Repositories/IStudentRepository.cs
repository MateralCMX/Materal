using Materal.TTA.Common;
using System;

namespace Demo.Domain.Repositories
{
    /// <summary>
    /// 学生仓储
    /// </summary>
    public interface IStudentRepository : IEFSubordinateRepository<Student, Guid>
    {
    }
}
