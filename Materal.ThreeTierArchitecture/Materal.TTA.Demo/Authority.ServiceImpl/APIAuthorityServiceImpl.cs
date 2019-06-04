using Authority.DataTransmitModel.APIAuthority;
using Authority.Domain;
using Authority.Domain.Repositories;
using Authority.EFRepository;
using Authority.Service;
using Authority.Service.Model.APIAuthority;
using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly IAuthorityUnitOfWork _authorityUnitOfWork;
        private const string AllAPIAuthorityInfoCacheName = "AllAPIAuthorityInfoCacheName";
        public APIAuthorityServiceImpl(IAPIAuthorityRepository apiAuthorityRepository, IMapper mapper, IAuthorityUnitOfWork authorityUnitOfWork)
        {
            _apiAuthorityRepository = apiAuthorityRepository;
            _mapper = mapper;
            _authorityUnitOfWork = authorityUnitOfWork;
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
            _authorityUnitOfWork.RegisterEdit(apiAuthorityFromDB);
            await _authorityUnitOfWork.CommitAsync();
            _apiAuthorityRepository.ClearCache();
        }
        public async Task DeleteAPIAuthorityAsync(Guid id)
        {
            List<APIAuthority> allAPIAuthorities = await _apiAuthorityRepository.GetAllInfoFromCacheAsync();
            APIAuthority apiAuthorityFromDB = allAPIAuthorities.FirstOrDefault(m=>m.ID == id);
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
            return GetTreeList(allAPIAuthorities);
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
        /// 获取树形列表
        /// </summary>
        /// <param name="allAPIAuthorities">所有API权限</param>
        /// <param name="parentID">父级唯一标识</param>
        /// <returns></returns>
        private List<APIAuthorityTreeDTO> GetTreeList(List<APIAuthority> allAPIAuthorities, Guid? parentID = null)
        {
            var result = new List<APIAuthorityTreeDTO>();
            List<APIAuthority> apiAuthorities = allAPIAuthorities.Where(m => m.ParentID == parentID).ToList();
            foreach (APIAuthority apiAuthority in apiAuthorities)
            {
                result.Add(new APIAuthorityTreeDTO
                {
                    ID = apiAuthority.ID,
                    Name = apiAuthority.Name,
                    Child = GetTreeList(allAPIAuthorities, apiAuthority.ID)
                });
            }
            return result;
        }
        #endregion
    }
}
