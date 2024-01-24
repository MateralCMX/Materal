﻿using RC.Deploy.Abstractions.DTO.ApplicationInfo;
using RC.Deploy.Abstractions.RequestModel.ApplicationInfo;

namespace RC.Deploy.Abstractions.Controllers
{
    /// <summary>
    /// 应用程序信息控制器
    /// </summary>
    public partial interface IApplicationInfoController : IMergeBlockControllerBase<AddApplicationInfoRequestModel, EditApplicationInfoRequestModel, QueryApplicationInfoRequestModel, ApplicationInfoDTO, ApplicationInfoListDTO>
    {
    }
}