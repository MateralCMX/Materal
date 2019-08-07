using Demo.DataTransmitModel.Student;
using Demo.Service.Model.Student;
using Materal.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Service
{
    /// <summary>
    /// 学生服务
    /// </summary>
    public interface IStudentService
    {
        /// <summary>
        /// 添加学生
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task AddStudentAsync(AddStudentModel model);
        /// <summary>
        /// 修改学生
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task EditStudentAsync(EditStudentModel model);
        /// <summary>
        /// 删除学生
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteStudentAsync(Guid id);
        /// <summary>
        /// 获得学生信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StudentDTO> GetStudentInfoAsync(Guid id);
        /// <summary>
        /// 获得学生列表
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        Task<(List<StudentListDTO> studentInfo, PageModel pageModel)> GetStudentListAsync(QueryStudentFilterModel filterModel);
    }
}
