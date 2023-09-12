using Materal.BaseCore.Domain;
using Materal.BaseCore.ServiceImpl;
using Materal.BaseCore.Services.Models;
using Materal.TTA.Common;
using Materal.TTA.EFRepository;
using System.Linq.Expressions;
using RC.ServerCenter.DataTransmitModel.Namespace;
using RC.ServerCenter.Domain;
using RC.ServerCenter.Domain.Repositories;
using RC.ServerCenter.Services;
using RC.ServerCenter.Services.Models.Namespace;

namespace RC.ServerCenter.ServiceImpl
{
    /// <summary>
    /// 命名空间服务实现
    /// </summary>
    public partial class NamespaceServiceImpl : BaseServiceImpl<AddNamespaceModel, EditNamespaceModel, QueryNamespaceModel, NamespaceDTO, NamespaceListDTO, INamespaceRepository, Namespace>, INamespaceService
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        public NamespaceServiceImpl(IServiceProvider serviceProvider) : base(serviceProvider) { }
    }
}
