using AutoMapper;
using Log.PresentationModel.Log.Request;
using Log.Service.Model.Log;

namespace Log.PresentationModel.AutoMapperProfile
{
    /// <summary>
    /// 日志AutoMapper配置
    /// </summary>
    public sealed class LogProfile : Profile
    {
        /// <summary>
        /// 日志AutoMapper配置
        /// </summary>
        public LogProfile()
        {
            CreateMap<QueryLogFilterRequestModel, QueryLogFilterModel>();
        }
    }
}
