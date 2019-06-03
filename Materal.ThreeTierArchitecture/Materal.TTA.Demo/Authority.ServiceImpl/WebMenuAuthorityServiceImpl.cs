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
using Authority.DataTransmitModel.WebMenuAuthority;
using Authority.Domain;
using Authority.Domain.Repositories;
using Authority.EFRepository;
using Authority.Service;
using Authority.Service.Model.WebMenuAuthority;
namespace Authority.ServiceImpl
{
    /// <summary>
    /// 网页菜单权限服务
    /// </summary>
    public sealed class WebMenuAuthorityServiceImpl : IWebMenuAuthorityService
    {
        private readonly IWebMenuAuthorityRepository _webMenuAuthorityRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorityUnitOfWork _authorityUnitOfWork;
        public WebMenuAuthorityServiceImpl(IWebMenuAuthorityRepository webMenuAuthorityRepository, IMapper mapper, IAuthorityUnitOfWork authorityUnitOfWork)
        {
            _webMenuAuthorityRepository = webMenuAuthorityRepository;
            _mapper = mapper;
            _authorityUnitOfWork = authorityUnitOfWork;
        }
        public async Task AddWebMenuAuthorityAsync(AddWebMenuAuthorityModel model)
        {
            throw new NotImplementedException();
        }
        public async Task EditWebMenuAuthorityAsync(EditWebMenuAuthorityModel model)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteWebMenuAuthorityAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public async Task<WebMenuAuthorityDTO> GetWebMenuAuthorityInfoAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<WebMenuAuthorityTreeDTO>> GetWebMenuAuthorityTreeAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<WebMenuAuthorityTreeDTO>> GetWebMenuAuthorityTreeAsync(Guid userID)
        {
            throw new NotImplementedException();
        }

        public async Task ExchangeWebMenuAuthorityIndexAsync(Guid id1, Guid id2)
        {
            throw new NotImplementedException();
        }

        public async Task ExchangeWebMenuAuthorityParentIDAsync(Guid id, Guid? parentID, Guid? indexID, bool forUnder = true)
        {
            throw new NotImplementedException();
        }
    }
}
