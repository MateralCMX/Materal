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
using Authority.DataTransmitModel.ActionAuthority;
using Authority.Domain;
using Authority.Domain.Repositories;
using Authority.EFRepository;
using Authority.Service;
using Authority.Service.Model.ActionAuthority;
namespace Authority.ServiceImpl
{
    /// <summary>
    /// 功能权限服务
    /// </summary>
    public sealed class ActionAuthorityServiceImpl : IActionAuthorityService
    {
        private readonly IActionAuthorityRepository _actionAuthorityRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorityUnitOfWork _authorityUnitOfWork;
        public ActionAuthorityServiceImpl(IActionAuthorityRepository actionAuthorityRepository, IMapper mapper, IAuthorityUnitOfWork authorityUnitOfWork)
        {
            _actionAuthorityRepository = actionAuthorityRepository;
            _mapper = mapper;
            _authorityUnitOfWork = authorityUnitOfWork;
        }
        public async Task AddActionAuthorityAsync(AddActionAuthorityModel model)
        {
            throw new NotImplementedException();
        }
        public async Task EditActionAuthorityAsync(EditActionAuthorityModel model)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteActionAuthorityAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionAuthorityDTO> GetActionAuthorityInfoAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public async Task<(List<ActionAuthorityListDTO> result, PageModel pageModel)> GetActionAuthorityListAsync(QueryActionAuthorityFilterModel filterModel)
        {
            throw new NotImplementedException();
        }
    }
}
