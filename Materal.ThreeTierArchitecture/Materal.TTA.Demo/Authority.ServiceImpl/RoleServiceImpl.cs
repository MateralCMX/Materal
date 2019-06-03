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
using Authority.DataTransmitModel.Role;
using Authority.Domain;
using Authority.Domain.Repositories;
using Authority.EFRepository;
using Authority.Service;
using Authority.Service.Model.Role;
namespace Authority.ServiceImpl
{
    /// <summary>
    /// 角色服务
    /// </summary>
    public sealed class RoleServiceImpl : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorityUnitOfWork _authorityUnitOfWork;
        public RoleServiceImpl(IRoleRepository roleRepository, IMapper mapper, IAuthorityUnitOfWork authorityUnitOfWork)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _authorityUnitOfWork = authorityUnitOfWork;
        }
        public async Task AddRoleAsync(AddRoleModel model)
        {
            throw new NotImplementedException();
        }
        public async Task EditRoleAsync(EditRoleModel model)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteRoleAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public async Task<RoleDTO> GetRoleInfoAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public async Task<(List<RoleListDTO> result, PageModel pageModel)> GetRoleListAsync(QueryRoleFilterModel filterModel)
        {
            throw new NotImplementedException();
        }
    }
}
