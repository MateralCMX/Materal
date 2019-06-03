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
using Authority.DataTransmitModel.APIAuthority;
using Authority.Domain;
using Authority.Domain.Repositories;
using Authority.EFRepository;
using Authority.Service;
using Authority.Service.Model.APIAuthority;
namespace Authority.ServiceImpl
{
    /// <summary>
    /// API权限服务
    /// </summary>
    public sealed class APIAuthorityServiceImpl : IAPIAuthorityService
    {
        private readonly IAPIAuthorityRepository _aPIAuthorityRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorityUnitOfWork _authorityUnitOfWork;
        public APIAuthorityServiceImpl(IAPIAuthorityRepository aPIAuthorityRepository, IMapper mapper, IAuthorityUnitOfWork authorityUnitOfWork)
        {
            _aPIAuthorityRepository = aPIAuthorityRepository;
            _mapper = mapper;
            _authorityUnitOfWork = authorityUnitOfWork;
        }

        public async Task AddAPIAuthorityAsync(AddAPIAuthorityModel model)
        {
            throw new NotImplementedException();
        }

        public async Task EditAPIAuthorityAsync(EditAPIAuthorityModel model)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAPIAuthorityAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<APIAuthorityDTO> GetAPIAuthorityInfoAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<APIAuthorityTreeDTO>> GetAPIAuthorityTreeAsync()
        {
            throw new NotImplementedException();
        }

        public async Task ExchangeAPIAuthorityParentIDAsync(Guid id, Guid? parentID)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> HasAPIAuthorityAsync(Guid userID, params string[] codes)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> HasLoginAuthorityAsync(Guid userID)
        {
            throw new NotImplementedException();
        }
    }
}
