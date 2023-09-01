//using Materal.BaseCore.ServiceImpl;
//using Materal.TTA.EFRepository;
//using MBC.Demo.DataTransmitModel.MyTree;
//using MBC.Demo.Domain;
//using MBC.Demo.Services.Models.MyTree;

//namespace MBC.Demo.ServiceImpl
//{
//    /// <summary>
//    /// 试卷来源服务实现
//    /// </summary>
//    public partial class MyTreeServiceImpl
//    {
//        /// <summary>
//        /// 查询树列表
//        /// </summary>
//        /// <param name="queryModel"></param>
//        /// <returns></returns>
//        public async Task<List<MyTreeTreeListDTO>> GetTreeListAsync(QueryMyTreeTreeListModel queryModel)
//        {
//            List<MyTree> allInfo = DefaultRepository is ICacheEFRepository<MyTree, Guid> cacheRepository
//                ? await cacheRepository.GetAllInfoFromCacheAsync()
//                : await DefaultRepository.FindAsync(m => true);
//            List<MyTreeTreeListDTO> result = allInfo.ToTree<MyTree, MyTreeTreeListDTO>(queryModel.ParentID, (dto, domain) => Mapper.Map(domain, dto));
//            HandlerTreeListDTO(result, queryModel);
//            return result;
//        }
//        /// <summary>
//        /// 处理树列表DTO
//        /// </summary>
//        /// <param name="dtos"></param>
//        partial void HandlerTreeListDTO(List<MyTreeTreeListDTO> dtos, QueryMyTreeTreeListModel queryModel);
//    }
//}
