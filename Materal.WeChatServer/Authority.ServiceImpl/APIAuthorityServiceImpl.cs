using Authority.DataTransmitModel.APIAuthority;
using Authority.Domain;
using Authority.Domain.Repositories;
using Authority.Domain.Repositories.Views;
using Authority.Domain.Views;
using Authority.EFRepository;
using Authority.Service;
using Authority.Service.Model.APIAuthority;
using AutoMapper;
using Common.Model.APIAuthorityConfig;
using Common.Tree;
using Materal.ConvertHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authority.ServiceImpl
{
    /// <summary>
    /// API权限服务
    /// </summary>
    public sealed class APIAuthorityServiceImpl : IAPIAuthorityService
    {
        private readonly IAPIAuthorityRepository _apiAuthorityRepository;
        private readonly IUserOwnedAPIAuthorityRepository _userOwnedAPIAuthorityRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorityUnitOfWork _authorityUnitOfWork;
        public APIAuthorityServiceImpl(IAPIAuthorityRepository apiAuthorityRepository, IMapper mapper, IAuthorityUnitOfWork authorityUnitOfWork, IUserOwnedAPIAuthorityRepository userOwnedAPIAuthorityRepository)
        {
            _apiAuthorityRepository = apiAuthorityRepository;
            _mapper = mapper;
            _authorityUnitOfWork = authorityUnitOfWork;
            _userOwnedAPIAuthorityRepository = userOwnedAPIAuthorityRepository;
        }
        public async Task AddAPIAuthorityAsync(AddAPIAuthorityModel model)
        {
            if (string.IsNullOrEmpty(model.Code)) throw new InvalidOperationException("代码为空");
            if (string.IsNullOrEmpty(model.Name)) throw new InvalidOperationException("名称为空");
            if (await _apiAuthorityRepository.CountAsync(m => m.Code == model.Code) > 0) throw new InvalidOperationException("代码重复");
            var apiAuthority = model.CopyProperties<APIAuthority>();
            _authorityUnitOfWork.RegisterAdd(apiAuthority);
            await _authorityUnitOfWork.CommitAsync();
            _apiAuthorityRepository.ClearCache();
        }
        public async Task EditAPIAuthorityAsync(EditAPIAuthorityModel model)
        {
            if (string.IsNullOrEmpty(model.Code)) throw new InvalidOperationException("代码为空");
            if (string.IsNullOrEmpty(model.Name)) throw new InvalidOperationException("名称为空");
            if (await _apiAuthorityRepository.CountAsync(m => m.ID != model.ID && m.Code == model.Code) > 0) throw new InvalidOperationException("代码重复");
            APIAuthority apiAuthorityFromDB = await _apiAuthorityRepository.FirstOrDefaultAsync(model.ID);
            if (apiAuthorityFromDB == null) throw new InvalidOperationException("API权限不存在");
            model.CopyProperties(apiAuthorityFromDB);
            apiAuthorityFromDB.UpdateTime = DateTime.Now;
            _authorityUnitOfWork.RegisterEdit(apiAuthorityFromDB);
            await _authorityUnitOfWork.CommitAsync();
            _apiAuthorityRepository.ClearCache();
        }
        public async Task DeleteAPIAuthorityAsync(Guid id)
        {
            List<APIAuthority> allAPIAuthorities = await _apiAuthorityRepository.GetAllInfoFromCacheAsync();
            APIAuthority apiAuthorityFromDB = allAPIAuthorities.FirstOrDefault(m => m.ID == id);
            if (apiAuthorityFromDB == null) throw new InvalidOperationException("API权限不存在");
            ICollection<APIAuthority> allChild = GetAllChild(allAPIAuthorities, id);
            foreach (APIAuthority apiAuthority in allChild)
            {
                _authorityUnitOfWork.RegisterDelete(apiAuthority);
            }
            _authorityUnitOfWork.RegisterDelete(apiAuthorityFromDB);
            await _authorityUnitOfWork.CommitAsync();
            _apiAuthorityRepository.ClearCache();
        }
        public async Task<APIAuthorityDTO> GetAPIAuthorityInfoAsync(Guid id)
        {
            APIAuthority apiAuthorityFromDB = await _apiAuthorityRepository.FirstOrDefaultAsync(id);
            if (apiAuthorityFromDB == null) throw new InvalidOperationException("API权限不存在");
            return _mapper.Map<APIAuthorityDTO>(apiAuthorityFromDB);
        }
        public async Task<List<APIAuthorityTreeDTO>> GetAPIAuthorityTreeAsync()
        {
            List<APIAuthority> allAPIAuthorities = await _apiAuthorityRepository.GetAllInfoFromCacheAsync();
            allAPIAuthorities = allAPIAuthorities.OrderBy(m => m.Name).ToList();
            return TreeHelper.GetTreeList<APIAuthorityTreeDTO, APIAuthority, Guid>(allAPIAuthorities);
        }
        public async Task ExchangeAPIAuthorityParentIDAsync(Guid id, Guid? parentID)
        {
            if (parentID.HasValue && !await _apiAuthorityRepository.ExistedAsync(parentID.Value))
            {
                throw new InvalidOperationException("父级唯一标识不存在");
            }
            APIAuthority apiAuthorityFromDB = await _apiAuthorityRepository.FirstOrDefaultAsync(id);
            if (apiAuthorityFromDB == null) throw new InvalidOperationException("该API权限不存在");
            apiAuthorityFromDB.ParentID = parentID;
            apiAuthorityFromDB.UpdateTime = DateTime.Now;
            _authorityUnitOfWork.RegisterEdit(apiAuthorityFromDB);
            await _authorityUnitOfWork.CommitAsync();
            _apiAuthorityRepository.ClearCache();
        }
        public async Task<bool> HasAPIAuthorityAsync(Guid userID, params string[] codes)
        {
            List<UserOwnedAPIAuthority> userOwnedAPIAuthorities = await _userOwnedAPIAuthorityRepository.WhereAsync(m => m.UserID == userID).ToList();
            foreach (string code in codes)
            {
                UserOwnedAPIAuthority userOwnedAPIAuthority = userOwnedAPIAuthorities.FirstOrDefault(m => m.Code == code);
                if (userOwnedAPIAuthority?.ParentID == null) return true;
                if (!HasParentAPIAuthority(userOwnedAPIAuthorities, userOwnedAPIAuthority.ParentID.Value)) return false;
            }
            return true;
        }
        public async Task<bool> HasLoginAuthorityAsync(Guid userID)
        {
            return await HasAPIAuthorityAsync(userID, AuthorityAPIAuthorityConfig.LoginCode);
        }

        #region 私有方法
        /// <summary>
        /// 获得所有子级
        /// </summary>
        /// <param name="apiAuthorities"></param>
        /// <param name="parentID"></param>
        /// <returns></returns>
        private ICollection<APIAuthority> GetAllChild(List<APIAuthority> apiAuthorities, Guid? parentID)
        {
            var result = new List<APIAuthority>();
            List<APIAuthority> child = apiAuthorities.Where(m => m.ParentID == parentID).ToList();
            result.AddRange(child);
            foreach (APIAuthority apiAuthority in child)
            {
                result.AddRange(GetAllChild(apiAuthorities, apiAuthority.ID));
            }
            return result;
        }
        /// <summary>
        /// 是否拥有父级API权限
        /// </summary>
        /// <param name="userOwnedAPIAuthorities"></param>
        /// <param name="parentID"></param>
        /// <returns></returns>
        private bool HasParentAPIAuthority(List<UserOwnedAPIAuthority> userOwnedAPIAuthorities, Guid parentID)
        {
            UserOwnedAPIAuthority userOwnedAPIAuthority = userOwnedAPIAuthorities.FirstOrDefault(m => m.ID == parentID);
            if (userOwnedAPIAuthority == null) return false;
            return !userOwnedAPIAuthority.ParentID.HasValue || HasParentAPIAuthority(userOwnedAPIAuthorities, userOwnedAPIAuthority.ParentID.Value);
        }
        #endregion
    }
}
