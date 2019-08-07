using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.DataTransmitModel.Class;
using Demo.Service.Model.Class;
using Materal.Model;

namespace Demo.Service
{
    /// <summary>
    /// 班级服务
    /// </summary>
    public interface IClassService
    {
        /// <summary>
        /// 添加班级
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task AddClassAsync(AddClassModel model);
        /// <summary>
        /// 修改班级
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task EditClassAsync(EditClassModel model);
        /// <summary>
        /// 删除班级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteClassAsync(Guid id);
        /// <summary>
        /// 获得班级信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ClassDTO> GetClassInfoAsync(Guid id);
        /// <summary>
        /// 获得班级列表
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        Task<(List<ClassListDTO> classInfo, PageModel pageModel)> GetClassListAsync(QueryClassFilterModel filterModel);
    }
}
