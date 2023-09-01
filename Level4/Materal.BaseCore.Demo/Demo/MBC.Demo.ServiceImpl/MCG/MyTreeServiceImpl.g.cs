using Materal.BaseCore.ServiceImpl;
using Materal.BaseCore.Services.Models;
using Materal.TTA.EFRepository;
using MBC.Demo.DataTransmitModel.MyTree;
using MBC.Demo.Domain;
using MBC.Demo.Domain.Repositories;
using MBC.Demo.Services;
using MBC.Demo.Services.Models.MyTree;

namespace MBC.Demo.ServiceImpl
{
    /// <summary>
    /// 我的树服务实现
    /// </summary>
    public partial class MyTreeServiceImpl : BaseServiceImpl<AddMyTreeModel, EditMyTreeModel, QueryMyTreeModel, MyTreeDTO, MyTreeListDTO, IMyTreeRepository, MyTree>, IMyTreeService
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        public MyTreeServiceImpl(IServiceProvider serviceProvider) : base(serviceProvider) { }
        /// <summary>
        /// 交换位序
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task ExchangeIndexAsync(ExchangeIndexModel model)
        {
            OnExchangeIndexBefore(model);
            await ServiceImplHelper.ExchangeIndexAndExchangeParentByGroupPropertiesAsync<IMyTreeRepository, MyTree>(model, DefaultRepository, UnitOfWork, new string[] {  }, new string[] {  });
            OnExchangeIndexAfter(model);
        }
        /// <summary>
        /// 交换位序之前
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        partial void OnExchangeIndexBefore(ExchangeIndexModel model);
        /// <summary>
        /// 交换位序之后
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        partial void OnExchangeIndexAfter(ExchangeIndexModel model);
        /// <summary>
        /// 更改父级
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task ExchangeParentAsync(ExchangeParentModel model)
        {
            OnExchangeParentBefore(model);
            await ServiceImplHelper.ExchangeParentByGroupPropertiesAsync<IMyTreeRepository, MyTree>(model, DefaultRepository, UnitOfWork);
            OnExchangeParentAfter(model);
        }
        /// <summary>
        /// 更改父级之前
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        partial void OnExchangeParentBefore(ExchangeParentModel model);
        /// <summary>
        /// 更改父级之后
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        partial void OnExchangeParentAfter(ExchangeParentModel model);
        /// <summary>
        /// 查询树列表
        /// </summary>
        /// <param name="queryModel"></param>
        public async Task<List<MyTreeTreeListDTO>> GetTreeListAsync(QueryMyTreeTreeListModel queryModel)
        {
            List<MyTree> allInfo = DefaultRepository is ICacheEFRepository<MyTree, Guid> cacheRepository ? await cacheRepository.GetAllInfoFromCacheAsync() : await DefaultRepository.FindAsync(m => true);
            List<MyTreeTreeListDTO> result = allInfo.ToTree<MyTree, MyTreeTreeListDTO>(queryModel.ParentID, (dto, domain) => Mapper.Map(domain, dto));
            HandlerTreeListDTO(result, queryModel);
            return result;
        }
        /// <summary>
        /// 处理树列表DTO
        /// </summary>
        /// <param name="dtos"></param>
        partial void HandlerTreeListDTO(List<MyTreeTreeListDTO> dtos, QueryMyTreeTreeListModel queryModel);
    }
}
