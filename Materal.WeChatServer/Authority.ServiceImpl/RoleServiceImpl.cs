using Authority.DataTransmitModel.Role;
using Authority.Domain;
using Authority.Domain.Repositories;
using Authority.EFRepository;
using Authority.Service;
using Authority.Service.Model.Role;
using AutoMapper;
using Materal.ConvertHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Authority.ServiceImpl
{
    /// <summary>
    /// 角色服务
    /// </summary>
    public sealed class RoleServiceImpl : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IRoleActionAuthorityRepository _roleActionAuthorityRepository;
        private readonly IRoleAPIAuthorityRepository _roleAPIAuthorityRepository;
        private readonly IRoleWebMenuAuthorityRepository _roleWebMenuAuthorityRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorityUnitOfWork _authorityUnitOfWork;
        public RoleServiceImpl(IRoleRepository roleRepository, IMapper mapper, IAuthorityUnitOfWork authorityUnitOfWork, IRoleActionAuthorityRepository roleActionAuthorityRepository, IRoleAPIAuthorityRepository roleAPIAuthorityRepository, IRoleWebMenuAuthorityRepository roleWebMenuAuthorityRepository)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _authorityUnitOfWork = authorityUnitOfWork;
            _roleActionAuthorityRepository = roleActionAuthorityRepository;
            _roleAPIAuthorityRepository = roleAPIAuthorityRepository;
            _roleWebMenuAuthorityRepository = roleWebMenuAuthorityRepository;
        }
        public async Task AddRoleAsync(AddRoleModel model)
        {
            if (string.IsNullOrEmpty(model.Code)) throw new InvalidOperationException("代码为空");
            if (string.IsNullOrEmpty(model.Name)) throw new InvalidOperationException("名称为空");
            if (await _roleRepository.CountAsync(m => m.Code == model.Code) > 0) throw new InvalidOperationException("代码重复");
            var role = model.CopyProperties<Role>();
            AddRoleActionAuthorities(role, model.ActionAuthorityIDs);
            AddRoleAPIAuthorities(role, model.APIAuthorityIDs);
            AddRoleWebMenuAuthorities(role, model.WebMenuAuthorityIDs);
            _authorityUnitOfWork.RegisterAdd(role);
            await _authorityUnitOfWork.CommitAsync();
            _roleRepository.ClearCache();
        }
        public async Task EditRoleAsync(EditRoleModel model)
        {
            if (string.IsNullOrEmpty(model.Code)) throw new InvalidOperationException("代码为空");
            if (string.IsNullOrEmpty(model.Name)) throw new InvalidOperationException("名称为空");
            if (await _roleRepository.CountAsync(m => m.ID != model.ID && m.Code == model.Code) > 0) throw new InvalidOperationException("代码重复");
            Role roleFromDB = await _roleRepository.FirstOrDefaultAsync(model.ID);
            if (roleFromDB == null) throw new InvalidOperationException("角色不存在");
            model.CopyProperties(roleFromDB);
            await EditRoleActionAuthorities(roleFromDB, model.ActionAuthorityIDs);
            await EditRoleAPIAuthorities(roleFromDB, model.APIAuthorityIDs);
            await EditRoleWebMenuAuthorities(roleFromDB, model.WebMenuAuthorityIDs);
            _authorityUnitOfWork.RegisterEdit(roleFromDB);
            await _authorityUnitOfWork.CommitAsync();
            _roleRepository.ClearCache();
        }
        public async Task DeleteRoleAsync(Guid id)
        {
            Role roleFromDB = await _roleRepository.FirstOrDefaultAsync(id);
            if (roleFromDB == null) throw new InvalidOperationException("角色不存在");
            roleFromDB.IsDelete = true;
            _authorityUnitOfWork.RegisterEdit(roleFromDB);
            await _authorityUnitOfWork.CommitAsync();
            _roleRepository.ClearCache();
        }
        public async Task<RoleDTO> GetRoleInfoAsync(Guid id)
        {
            Role roleFromDB = await _roleRepository.FirstOrDefaultAsync(id);
            if (roleFromDB == null) throw new InvalidOperationException("角色不存在");
            return _mapper.Map<RoleDTO>(roleFromDB);
        }
        public async Task<List<RoleTreeDTO>> GetRoleTreeAsync()
        {
            List<Role> allRoles = await _roleRepository.GetAllInfoFromCacheAsync();
            return GetTreeList(allRoles);
        }
        public async Task ExchangeRoleParentIDAsync(Guid id, Guid? parentID)
        {
            if (parentID.HasValue && !await _roleRepository.ExistedAsync(parentID.Value))
            {
                throw new InvalidOperationException("父级唯一标识不存在");
            }
            Role roleFromDB = await _roleRepository.FirstOrDefaultAsync(id);
            if (roleFromDB == null) throw new InvalidOperationException("该角色不存在");
            roleFromDB.ParentID = parentID;
            _authorityUnitOfWork.RegisterEdit(roleFromDB);
            await _authorityUnitOfWork.CommitAsync();
        }
        #region 私有方法
        /// <summary>
        /// 获取树形列表
        /// </summary>
        /// <param name="allRoles">所有角色信息</param>
        /// <param name="parentID">父级唯一标识</param>
        /// <returns></returns>
        private List<RoleTreeDTO> GetTreeList(List<Role> allRoles, Guid? parentID = null)
        {
            var result = new List<RoleTreeDTO>();
            List<Role> roles = allRoles.Where(m => m.ParentID == parentID).ToList();
            foreach (Role role in roles)
            {
                result.Add(new RoleTreeDTO
                {
                    ID = role.ID,
                    Name = role.Name,
                    Child = GetTreeList(allRoles, role.ID)
                });
            }
            return result;
        }
        /// <summary>
        /// 添加角色功能权限
        /// </summary>
        /// <param name="role"></param>
        /// <param name="actionAuthorityIDs"></param>
        private void AddRoleActionAuthorities(Role role, IEnumerable<Guid> actionAuthorityIDs)
        {
            if (role.RoleActionAuthorities == null)
            {
                role.RoleActionAuthorities = new List<RoleActionAuthority>();
            }
            foreach (Guid id in actionAuthorityIDs)
            {
                role.RoleActionAuthorities.Add(new RoleActionAuthority
                {
                    ActionAuthorityID = id
                });
            }
        }
        /// <summary>
        /// 编辑角色功能权限
        /// </summary>
        /// <param name="role"></param>
        /// <param name="actionAuthorityIDs"></param>
        /// <returns></returns>
        private async Task EditRoleActionAuthorities(Role role, Guid[] actionAuthorityIDs)
        {
            role.RoleActionAuthorities = await _roleActionAuthorityRepository.WhereAsync(m => m.RoleID == role.ID).ToList();
            List<Guid> roleActionAuthorityIDs = role.RoleActionAuthorities.Select(m => m.ActionAuthorityID).ToList();
            List<Guid> deleteIDs = roleActionAuthorityIDs.Except(actionAuthorityIDs).ToList();
            List<RoleActionAuthority> deleteModel = role.RoleActionAuthorities.Where(m => deleteIDs.Contains(m.ActionAuthorityID)).ToList();
            foreach (RoleActionAuthority roleActionAuthority in deleteModel)
            {
                _authorityUnitOfWork.RegisterDelete(roleActionAuthority);
            }
            List<Guid> addIDs = actionAuthorityIDs.Except(roleActionAuthorityIDs).ToList();
            AddRoleActionAuthorities(role, addIDs);
        }
        /// <summary>
        /// 添加角色网页菜单权限
        /// </summary>
        /// <param name="role"></param>
        /// <param name="webMenuAuthorityIDs"></param>
        private void AddRoleWebMenuAuthorities(Role role, IEnumerable<Guid> webMenuAuthorityIDs)
        {
            if (role.RoleWebMenuAuthorities == null)
            {
                role.RoleWebMenuAuthorities = new List<RoleWebMenuAuthority>();
            }
            foreach (Guid id in webMenuAuthorityIDs)
            {
                role.RoleWebMenuAuthorities.Add(new RoleWebMenuAuthority
                {
                    WebMenuAuthorityID = id
                });
            }
        }
        /// <summary>
        /// 编辑角色网页菜单权限
        /// </summary>
        /// <param name="role"></param>
        /// <param name="webMenuAuthorityIDs"></param>
        /// <returns></returns>
        private async Task EditRoleWebMenuAuthorities(Role role, Guid[] webMenuAuthorityIDs)
        {
            role.RoleWebMenuAuthorities = await _roleWebMenuAuthorityRepository.WhereAsync(m => m.RoleID == role.ID).ToList();
            List<Guid> roleWebMenuAuthorityIDs = role.RoleWebMenuAuthorities.Select(m => m.WebMenuAuthorityID).ToList();
            List<Guid> deleteIDs = roleWebMenuAuthorityIDs.Except(webMenuAuthorityIDs).ToList();
            List<RoleWebMenuAuthority> deleteModel = role.RoleWebMenuAuthorities.Where(m => deleteIDs.Contains(m.WebMenuAuthorityID)).ToList();
            foreach (RoleWebMenuAuthority roleWebMenuAuthority in deleteModel)
            {
                _authorityUnitOfWork.RegisterDelete(roleWebMenuAuthority);
            }
            List<Guid> addIDs = webMenuAuthorityIDs.Except(roleWebMenuAuthorityIDs).ToList();
            AddRoleWebMenuAuthorities(role, addIDs);
        }
        /// <summary>
        /// 添加角色API权限
        /// </summary>
        /// <param name="role"></param>
        /// <param name="apiAuthorityIDs"></param>
        private void AddRoleAPIAuthorities(Role role, IEnumerable<Guid> apiAuthorityIDs)
        {
            if (role.RoleAPIAuthorities == null)
            {
                role.RoleAPIAuthorities = new List<RoleAPIAuthority>();
            }
            foreach (Guid id in apiAuthorityIDs)
            {
                role.RoleAPIAuthorities.Add(new RoleAPIAuthority
                {
                    APIAuthorityID = id
                });
            }
        }
        /// <summary>
        /// 编辑角色API权限
        /// </summary>
        /// <param name="role"></param>
        /// <param name="apiAuthorityIDs"></param>
        /// <returns></returns>
        private async Task EditRoleAPIAuthorities(Role role, Guid[] apiAuthorityIDs)
        {
            role.RoleAPIAuthorities = await _roleAPIAuthorityRepository.WhereAsync(m => m.RoleID == role.ID).ToList();
            List<Guid> roleAPIAuthorityIDs = role.RoleAPIAuthorities.Select(m => m.APIAuthorityID).ToList();
            List<Guid> deleteIDs = roleAPIAuthorityIDs.Except(apiAuthorityIDs).ToList();
            List<RoleAPIAuthority> deleteModel = role.RoleAPIAuthorities.Where(m => deleteIDs.Contains(m.APIAuthorityID)).ToList();
            foreach (RoleAPIAuthority roleAPIAuthority in deleteModel)
            {
                _authorityUnitOfWork.RegisterDelete(roleAPIAuthority);
            }
            List<Guid> addIDs = apiAuthorityIDs.Except(roleAPIAuthorityIDs).ToList();
            foreach (Guid id in addIDs)
            {
                role.RoleAPIAuthorities.Add(new RoleAPIAuthority
                {
                    APIAuthorityID = id
                });
            }
            AddRoleAPIAuthorities(role, addIDs);
        }
        #endregion
    }
}
