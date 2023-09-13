using Materal.BaseCore.Services;
using Materal.BaseCore.Services.Models;
using MBC.Demo.DataTransmitModel.MyTree;
using MBC.Demo.Services.Models.MyTree;

namespace MBC.Demo.Services
{
    /// <summary>
    /// 我的树服务
    /// </summary>
    public partial interface IMyTreeService : IBaseService<AddMyTreeModel, EditMyTreeModel, QueryMyTreeModel, MyTreeDTO, MyTreeListDTO>
    {
        /// <summary>
        /// 交换位序
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [DataValidation]
        Task ExchangeIndexAsync(ExchangeIndexModel model);
        /// <summary>
        /// 更改父级
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [DataValidation]
        Task ExchangeParentAsync(ExchangeParentModel model);
        /// <summary>
        /// 查询树列表
        /// </summary>
        /// <param name="queryModel"></param>
        [DataValidation]
        Task<List<MyTreeTreeListDTO>> GetTreeListAsync(QueryMyTreeTreeListModel queryModel);
    }
}
