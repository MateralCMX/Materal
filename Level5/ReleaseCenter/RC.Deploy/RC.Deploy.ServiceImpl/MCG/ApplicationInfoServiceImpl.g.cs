using Materal.BaseCore.Domain;
using Materal.BaseCore.ServiceImpl;
using Materal.BaseCore.Services.Models;
using Materal.TTA.Common;
using Materal.TTA.EFRepository;
using System.Linq.Expressions;
using RC.Deploy.DataTransmitModel.ApplicationInfo;
using RC.Deploy.Domain;
using RC.Deploy.Domain.Repositories;
using RC.Deploy.Services;
using RC.Deploy.Services.Models.ApplicationInfo;

namespace RC.Deploy.ServiceImpl
{
    /// <summary>
    /// 应用程序信息服务实现
    /// </summary>
    public partial class ApplicationInfoServiceImpl : BaseServiceImpl<AddApplicationInfoModel, EditApplicationInfoModel, QueryApplicationInfoModel, ApplicationInfoDTO, ApplicationInfoListDTO, IApplicationInfoRepository, ApplicationInfo>, IApplicationInfoService
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        public ApplicationInfoServiceImpl(IServiceProvider serviceProvider) : base(serviceProvider) { }
    }
}
