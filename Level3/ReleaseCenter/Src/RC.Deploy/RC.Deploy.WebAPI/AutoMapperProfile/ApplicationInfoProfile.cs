using RC.Deploy.DataTransmitModel.ApplicationInfo;
using RC.Deploy.ServiceImpl.Models;

namespace RC.Deploy.WebAPI.AutoMapperProfile
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
                .AfterMap(ModelToDTO);
            CreateMap<ApplicationRuntimeModel, ApplicationInfoListDTO>()
                .AfterMap(ModelToDTO);
            CreateMap<FileInfo, FileInfoDTO>();
        }
        /// <summary>
        /// 模型转换为数据传输模型
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dto"></param>
        private void ModelToDTO(ApplicationRuntimeModel model, ApplicationInfoListDTO dto)
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
