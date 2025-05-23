﻿using Materal.Utils.Consul.Models;
using RC.ServerCenter.Abstractions.DTO.Server;

namespace RC.ServerCenter.Application.AutoMapperProfile
{
    /// <summary>
    /// 映射配置
    /// </summary>
    public class ServerProfile : Profile
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public ServerProfile()
        {
            CreateMap<ConsulServiceModel, DeployListDTO>()
                .ForMember(m => m.Name, n => n.MapFrom(m => GetDeployName(m)))
                .ForMember(m => m.Host, n => n.MapFrom(m => m.Address));
            CreateMap<ConsulServiceModel, EnvironmentServerListDTO>()
                .ForMember(m => m.Name, n => n.MapFrom(m => GetEnvironmentServerName(m)))
                .ForMember(m => m.Host, n => n.MapFrom(m => m.Address));
        }
        /// <summary>
        /// 获得发布服务名字
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string GetDeployName(ConsulServiceModel model)
        {
            string result = string.IsNullOrWhiteSpace(model.Service) ? "未知服务" : model.Service;
            if (model.Tags == null || model.Tags.Length == 0) return result;
            foreach (string item in model.Tags)
            {
                if (item == "Materal" || item == "Materal.MergeBlock" || item == "RC" || item == "RC.Deploy") continue;
                return item;
            }
            return result;
        }
        /// <summary>
        /// 获得环境服务名字
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string GetEnvironmentServerName(ConsulServiceModel model)
        {
            string result = string.IsNullOrWhiteSpace(model.Service) ? "未知服务" : model.Service;
            if (model.Tags == null || model.Tags.Length == 0) return result;
            foreach (string item in model.Tags)
            {
                if (item == "Materal" || item == "Materal.MergeBlock" || item == "RC" || item == "RC.EnvironmentServer") continue;
                return item;
            }
            return result;
        }
    }
}
