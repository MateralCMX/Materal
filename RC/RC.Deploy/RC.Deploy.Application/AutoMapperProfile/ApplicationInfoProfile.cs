using RC.Deploy.Abstractions.DTO.ApplicationInfo;
using RC.Deploy.Application.Services.Models;

namespace RC.Deploy.Application.AutoMapperProfile
{
    public partial class ApplicationInfoProfile
    {
        /// <summary>
        /// 初始化
        /// </summary>
        protected override void Init()
        {
            base.Init();
            CreateMap<ApplicationRuntimeModel, ApplicationInfoDTO>()
                .AfterMap(ApplicationRuntimeModelToDTO);
            CreateMap<ApplicationRuntimeModel, ApplicationInfoListDTO>()
                .AfterMap(ApplicationRuntimeModelToDTO);
            CreateMap<FileInfo, FileInfoDTO>();
        }
        /// <summary>
        /// 模型转换为数据传输模型
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dto"></param>
        private void ApplicationRuntimeModelToDTO(ApplicationRuntimeModel model, ApplicationInfoListDTO dto)
        {
            dto.ID = model.ApplicationInfo.ID;
            dto.CreateTime = model.ApplicationInfo.CreateTime;
            dto.Name = model.ApplicationInfo.Name;
            dto.RootPath = model.ApplicationInfo.RootPath;
            dto.MainModule = model.ApplicationInfo.MainModule;
            dto.ApplicationType = model.ApplicationInfo.ApplicationType;
            dto.IsIncrementalUpdating = model.ApplicationInfo.IsIncrementalUpdating;
            dto.RunParams = model.ApplicationInfo.RunParams;
        }
    }
}
