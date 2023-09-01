using AutoMapper;
using MBC.Demo.PresentationModel.MyTree;
using MBC.Demo.Services.Models.MyTree;
using MBC.Demo.DataTransmitModel.MyTree;
using MBC.Demo.Domain;

namespace MBC.Demo.WebAPI.AutoMapperProfile
{
    /// <summary>
    /// 我的树AutoMapper映射配置基类
    /// </summary>
    public partial class MyTreeProfileBase : Profile
    {
        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Init()
        {
            CreateMap<AddMyTreeModel, MyTree>();
            CreateMap<EditMyTreeModel, MyTree>();
            CreateMap<AddMyTreeRequestModel, AddMyTreeModel>();
            CreateMap<EditMyTreeRequestModel, EditMyTreeModel>();
            CreateMap<MyTree, MyTreeTreeListDTO>();
            CreateMap<MyTree, MyTreeListDTO>();
            CreateMap<MyTree, MyTreeDTO>();
            CreateMap<QueryMyTreeRequestModel, QueryMyTreeModel>();
            CreateMap<QueryMyTreeTreeListRequestModel, QueryMyTreeTreeListModel>();
        }
    }
    /// <summary>
    /// 我的树AutoMapper映射配置
    /// </summary>
    public partial class MyTreeProfile : MyTreeProfileBase
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public MyTreeProfile()
        {
            Init();
        }
    }
}
