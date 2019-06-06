using AutoMapper;
using Materal.Common;
using Materal.ConvertHelper;
using Materal.LinqHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WeChatService.DataTransmitModel.Application;
using WeChatService.Domain;
using WeChatService.Domain.Repositories;
using WeChatService.EFRepository;
using WeChatService.Service;
using WeChatService.Service.Model.Application;
namespace WeChatService.ServiceImpl
{
    /// <summary>
    /// 应用服务
    /// </summary>
    public sealed class ApplicationServiceImpl : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IMapper _mapper;
        private readonly IWeChatServiceUnitOfWork _weChatServiceUnitOfWork;
        public ApplicationServiceImpl(IApplicationRepository applicationRepository, IMapper mapper, IWeChatServiceUnitOfWork weChatServiceUnitOfWork)
        {
            _applicationRepository = applicationRepository;
            _mapper = mapper;
            _weChatServiceUnitOfWork = weChatServiceUnitOfWork;
        }
        public async Task AddApplicationAsync(AddApplicationModel model)
        {
            throw new NotImplementedException();
        }
        public async Task EditApplicationAsync(EditApplicationModel model)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteApplicationAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public async Task<ApplicationDTO> GetApplicationInfoAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public async Task<(List<ApplicationListDTO> result, PageModel pageModel)> GetApplicationListAsync(QueryApplicationFilterModel filterModel)
        {
            throw new NotImplementedException();
        }
    }
}
