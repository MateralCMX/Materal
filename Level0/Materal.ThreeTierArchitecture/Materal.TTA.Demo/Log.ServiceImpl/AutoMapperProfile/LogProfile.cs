using AutoMapper;
using Log.DataTransmitModel.Log;

namespace Log.ServiceImpl.AutoMapperProfile
{
    /// <summary>
    /// 日志AutoMapper配置
    /// </summary>
    public sealed class LogProfile : Profile
    {
        public LogProfile()
        {
            CreateMap<Domain.Log, LogListDTO>();
            CreateMap<Domain.Log, LogDTO>();
        }
    }
}
