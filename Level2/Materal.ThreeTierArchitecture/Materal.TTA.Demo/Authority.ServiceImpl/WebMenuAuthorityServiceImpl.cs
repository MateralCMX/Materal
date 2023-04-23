using Authority.DataTransmitModel.WebMenuAuthority;
using Authority.Domain;
using Authority.Domain.Repositories;
using Authority.Domain.Repositories.Views;
using Authority.EFRepository;
using Authority.Service;
using Authority.Service.Model.WebMenuAuthority;
using AutoMapper;
using Materal.ConvertHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authority.Domain.Views;

namespace Authority.ServiceImpl
{
    /// <summary>
    /// 网页菜单权限服务
    /// </summary>
    public sealed class WebMenuAuthorityServiceImpl : IWebMenuAuthorityService
    {
        private readonly IWebMenuAuthorityRepository _webMenuAuthorityRepository;
        private readonly IUserOwnedWebMenuAuthorityRepository _userOwnedWebMenuAuthorityRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorityUnitOfWork _authorityUnitOfWork;
        public WebMenuAuthorityServiceImpl(IWebMenuAuthorityRepository webMenuAuthorityRepository, IMapper mapper, IAuthorityUnitOfWork authorityUnitOfWork, IUserOwnedWebMenuAuthorityRepository userOwnedWebMenuAuthorityRepository)
        {
            _webMenuAuthorityRepository = webMenuAuthorityRepository;
            _mapper = mapper;
            _authorityUnitOfWork = authorityUnitOfWork;
            _userOwnedWebMenuAuthorityRepository = userOwnedWebMenuAuthorityRepository;
        }
        public async Task AddWebMenuAuthorityAsync(AddWebMenuAuthorityModel model)
        {
            if (string.IsNullOrEmpty(model.Code)) throw new InvalidOperationException("代码为空");
            if (string.IsNullOrEmpty(model.Name)) throw new InvalidOperationException("名称为空");
            if (await _webMenuAuthorityRepository.CountAsync(m => m.Code == model.Code) > 0) throw new InvalidOperationException("代码重复");
            var webMenuAuthority = model.CopyProperties<WebMenuAuthority>();
            _authorityUnitOfWork.RegisterAdd(webMenuAuthority);
            await _authorityUnitOfWork.CommitAsync();
            _webMenuAuthorityRepository.ClearCache();
        }
        public async Task EditWebMenuAuthorityAsync(EditWebMenuAuthorityModel model)
        {
            if (string.IsNullOrEmpty(model.Code)) throw new InvalidOperationException("代码为空");
            if (string.IsNullOrEmpty(model.Name)) throw new InvalidOperationException("名称为空");
            if (await _webMenuAuthorityRepository.CountAsync(m => m.ID != model.ID && m.Code == model.Code) > 0) throw new InvalidOperationException("代码重复");
            WebMenuAuthority webMenuAuthorityFromDB = await _webMenuAuthorityRepository.FirstOrDefaultAsync(model.ID);
            if (webMenuAuthorityFromDB == null) throw new InvalidOperationException("网页菜单权限不存在");
            model.CopyProperties(webMenuAuthorityFromDB);
            _authorityUnitOfWork.RegisterEdit(webMenuAuthorityFromDB);
            await _authorityUnitOfWork.CommitAsync();
            _webMenuAuthorityRepository.ClearCache();
        }
        public async Task DeleteWebMenuAuthorityAsync(Guid id)
        {
            List<WebMenuAuthority> allWebMenuAuthorities = await _webMenuAuthorityRepository.GetAllInfoFromCacheAsync();
            WebMenuAuthority webMenuAuthorityFromDB = allWebMenuAuthorities.FirstOrDefault(m => m.ID == id);
            if (webMenuAuthorityFromDB == null) throw new InvalidOperationException("API权限不存在");
            ICollection<WebMenuAuthority> allChild = GetAllChild(allWebMenuAuthorities, id);
            foreach (WebMenuAuthority menuAuthority in allChild)
            {
                _authorityUnitOfWork.RegisterDelete(menuAuthority);
            }
            _authorityUnitOfWork.RegisterDelete(webMenuAuthorityFromDB);
            await _authorityUnitOfWork.CommitAsync();
            _webMenuAuthorityRepository.ClearCache();
        }
        public async Task<WebMenuAuthorityDTO> GetWebMenuAuthorityInfoAsync(Guid id)
        {
            WebMenuAuthority webMenuAuthorityFromDB = await _webMenuAuthorityRepository.FirstOrDefaultAsync(id);
            if (webMenuAuthorityFromDB == null) throw new InvalidOperationException("网页菜单权限不存在");
            return _mapper.Map<WebMenuAuthorityDTO>(webMenuAuthorityFromDB);
        }

        public async Task<List<WebMenuAuthorityTreeDTO>> GetWebMenuAuthorityTreeAsync()
        {
            List<WebMenuAuthority> allWebMenuAuthorities = await _webMenuAuthorityRepository.GetAllInfoFromCacheAsync();
            return GetTreeList(allWebMenuAuthorities.OrderBy(m => m.Index).ToList());
        }

        public async Task<List<WebMenuAuthorityTreeDTO>> GetWebMenuAuthorityTreeAsync(Guid userID)
        {
            List<UserOwnedWebMenuAuthority> userOwnedWebMenuAuthorities = await _userOwnedWebMenuAuthorityRepository.WhereAsync(m => m.UserID == userID).OrderBy(m => m.Index).ToList();
            return GetTreeList(userOwnedWebMenuAuthorities);
        }

        public async Task ExchangeWebMenuAuthorityIndexAsync(Guid id1, Guid id2)
        {
            List<WebMenuAuthority> webMenuAuthorities = await _webMenuAuthorityRepository.WhereAsync(m => m.ID == id1 || m.ID == id2).ToList();
            if (webMenuAuthorities.Count != 2) throw new InvalidOperationException("该网页菜单权限不存在");
            ExchangeIndex(webMenuAuthorities[0], webMenuAuthorities[1]);
            _authorityUnitOfWork.RegisterEdit(webMenuAuthorities[0]);
            _authorityUnitOfWork.RegisterEdit(webMenuAuthorities[1]);
            await _authorityUnitOfWork.CommitAsync();
        }

        public async Task ExchangeWebMenuAuthorityParentIDAsync(Guid id, Guid? parentID, Guid? indexID, bool forUnder = true)
        {
            if (parentID.HasValue && !await _webMenuAuthorityRepository.ExistedAsync(parentID.Value))
            {
                throw new InvalidOperationException("父级唯一标识不存在");
            }
            WebMenuAuthority webMenuAuthorityFromDB = await _webMenuAuthorityRepository.FirstOrDefaultAsync(id);
            if (webMenuAuthorityFromDB == null) throw new InvalidOperationException("该网页菜单权限不存在");
            webMenuAuthorityFromDB.ParentID = parentID;
            if (indexID.HasValue)
            {
                WebMenuAuthority indexWebMenuAuthority = await _webMenuAuthorityRepository.FirstOrDefaultAsync(indexID.Value);
                if (indexWebMenuAuthority == null) throw new InvalidOperationException("位序唯一标项不存在");
                if (forUnder && webMenuAuthorityFromDB.Index < indexWebMenuAuthority.Index ||
                    !forUnder && webMenuAuthorityFromDB.Index > indexWebMenuAuthority.Index)
                {
                    ExchangeIndex(webMenuAuthorityFromDB, indexWebMenuAuthority);
                    _authorityUnitOfWork.RegisterEdit(indexWebMenuAuthority);
                }
            }
            _authorityUnitOfWork.RegisterEdit(webMenuAuthorityFromDB);
            await _authorityUnitOfWork.CommitAsync();
        }
        #region 私有方法
        /// <summary>
        /// 获得所有子级
        /// </summary>
        /// <param name="webMenuAuthorities"></param>
        /// <param name="parentID"></param>
        /// <returns></returns>
        private ICollection<WebMenuAuthority> GetAllChild(List<WebMenuAuthority> webMenuAuthorities, Guid? parentID)
        {
            var result = new List<WebMenuAuthority>();
            List<WebMenuAuthority> child = webMenuAuthorities.Where(m => m.ParentID == parentID).ToList();
            result.AddRange(child);
            foreach (WebMenuAuthority webMenuAuthority in child)
            {
                result.AddRange(GetAllChild(webMenuAuthorities, webMenuAuthority.ID));
            }
            return result;
        }
        /// <summary>
        /// 获取树形列表
        /// </summary>
        /// <param name="allWebMenuAuthorities">所有网页菜单权限信息</param>
        /// <param name="parentID">父级唯一标识</param>
        /// <returns></returns>
        private List<WebMenuAuthorityTreeDTO> GetTreeList(List<WebMenuAuthority> allWebMenuAuthorities, Guid? parentID = null)
        {
            var result = new List<WebMenuAuthorityTreeDTO>();
            List<WebMenuAuthority> webMenuAuthorities = allWebMenuAuthorities.Where(m => m.ParentID == parentID).ToList();
            foreach (WebMenuAuthority webMenuAuthority in webMenuAuthorities)
            {
                result.Add(new WebMenuAuthorityTreeDTO
                {
                    ID = webMenuAuthority.ID,
                    Name = webMenuAuthority.Name,
                    Child = GetTreeList(allWebMenuAuthorities, webMenuAuthority.ID)
                });
            }
            return result;
        }
        /// <summary>
        /// 获取树形列表
        /// </summary>
        /// <param name="allWebMenuAuthorities">所有网页菜单权限信息</param>
        /// <param name="parentID">父级唯一标识</param>
        /// <returns></returns>
        private List<WebMenuAuthorityTreeDTO> GetTreeList(List<UserOwnedWebMenuAuthority> allWebMenuAuthorities, Guid? parentID = null)
        {
            var result = new List<WebMenuAuthorityTreeDTO>();
            List<UserOwnedWebMenuAuthority> webMenuAuthorities = allWebMenuAuthorities.Where(m => m.ParentID == parentID).ToList();
            foreach (UserOwnedWebMenuAuthority webMenuAuthority in webMenuAuthorities)
            {
                result.Add(new WebMenuAuthorityTreeDTO
                {
                    ID = webMenuAuthority.ID,
                    Name = webMenuAuthority.Name,
                    Child = GetTreeList(allWebMenuAuthorities, webMenuAuthority.ID)
                });
            }
            return result;
        }
        /// <summary>
        /// 调换位序
        /// </summary>
        /// <param name="webMenuAuthority1"></param>
        /// <param name="webMenuAuthority2"></param>
        private void ExchangeIndex(WebMenuAuthority webMenuAuthority1, WebMenuAuthority webMenuAuthority2)
        {
            int temp = webMenuAuthority1.Index;
            webMenuAuthority1.Index = webMenuAuthority2.Index;
            webMenuAuthority2.Index = temp;
        }
        #endregion
    }
}
