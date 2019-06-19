using Authority.DataTransmitModel.WebMenuAuthority;
using Authority.Domain;
using Authority.Domain.Repositories;
using Authority.Domain.Repositories.Views;
using Authority.Domain.Views;
using Authority.EFRepository;
using Authority.Service;
using Authority.Service.Model.WebMenuAuthority;
using AutoMapper;
using Common.Tree;
using Materal.ConvertHelper;
using Materal.LinqHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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
            if (string.IsNullOrEmpty(model.Name)) throw new InvalidOperationException("名称为空");
            if (!string.IsNullOrEmpty(model.Code) && await _webMenuAuthorityRepository.CountAsync(m => m.Code == model.Code) > 0) throw new InvalidOperationException("代码重复");
            var webMenuAuthority = model.CopyProperties<WebMenuAuthority>();
            webMenuAuthority.Index = _webMenuAuthorityRepository.GetMaxIndex() + 1;
            _authorityUnitOfWork.RegisterAdd(webMenuAuthority);
            await _authorityUnitOfWork.CommitAsync();
            _webMenuAuthorityRepository.ClearCache();
        }
        public async Task EditWebMenuAuthorityAsync(EditWebMenuAuthorityModel model)
        {
            if (string.IsNullOrEmpty(model.Name)) throw new InvalidOperationException("名称为空");
            if (!string.IsNullOrEmpty(model.Code) && await _webMenuAuthorityRepository.CountAsync(m => m.ID != model.ID && m.Code == model.Code) > 0) throw new InvalidOperationException("代码重复");
            WebMenuAuthority webMenuAuthorityFromDB = await _webMenuAuthorityRepository.FirstOrDefaultAsync(model.ID);
            if (webMenuAuthorityFromDB == null) throw new InvalidOperationException("网页菜单权限不存在");
            model.CopyProperties(webMenuAuthorityFromDB);
            webMenuAuthorityFromDB.UpdateTime = DateTime.Now;
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
            List<WebMenuAuthority> allWebMenuAuthorities = (await _webMenuAuthorityRepository.GetAllInfoFromCacheAsync()).OrderBy(m => m.Index).ToList();
            return TreeHelper.GetTreeList<WebMenuAuthorityTreeDTO, WebMenuAuthority, Guid>(allWebMenuAuthorities, null, 
                webMenuAuthority => _mapper.Map<WebMenuAuthorityTreeDTO>(webMenuAuthority));
        }
        public async Task<List<WebMenuAuthorityTreeDTO>> GetWebMenuAuthorityTreeAsync(Guid userID)
        {
            List<UserOwnedWebMenuAuthority> userOwnedWebMenuAuthorities = await _userOwnedWebMenuAuthorityRepository.WhereAsync(m => m.UserID == userID).OrderBy(m => m.Index).ToList();
            return TreeHelper.GetTreeList<WebMenuAuthorityTreeDTO, UserOwnedWebMenuAuthority, Guid>(userOwnedWebMenuAuthorities, null, 
                webMenuAuthority => _mapper.Map<WebMenuAuthorityTreeDTO>(webMenuAuthority));
        }
        public async Task ExchangeWebMenuAuthorityIndexAsync(Guid exchangeID, Guid targetID, bool forUnder = true)
        {
            List<WebMenuAuthority> webMenuAuthorities = await GetWebMenuAuthoritiesByIDs(exchangeID, targetID);
            webMenuAuthorities = await GetWebMenuAuthoritiesByIndex(webMenuAuthorities[0], webMenuAuthorities[1]);
            ExchangeIndex(exchangeID, forUnder, webMenuAuthorities);
            await _authorityUnitOfWork.CommitAsync();
            _webMenuAuthorityRepository.ClearCache();
        }
        public async Task ExchangeWebMenuAuthorityParentIDAsync(Guid id, Guid? parentID, Guid? targetID, bool forUnder = true)
        {
            if (parentID.HasValue && !await _webMenuAuthorityRepository.ExistedAsync(parentID.Value))
            {
                throw new InvalidOperationException("父级唯一标识不存在");
            }
            WebMenuAuthority webMenuAuthorityFromDB = await _webMenuAuthorityRepository.FirstOrDefaultAsync(id);
            if (webMenuAuthorityFromDB == null) throw new InvalidOperationException("该网页菜单权限不存在");
            webMenuAuthorityFromDB.ParentID = parentID;
            if (targetID.HasValue)
            {
                WebMenuAuthority indexWebMenuAuthority = await _webMenuAuthorityRepository.FirstOrDefaultAsync(targetID.Value);
                List<WebMenuAuthority> webMenuAuthorities = await GetWebMenuAuthoritiesByIndex(webMenuAuthorityFromDB, indexWebMenuAuthority);
                ExchangeIndex(id, forUnder, webMenuAuthorities);
            }
            _authorityUnitOfWork.RegisterEdit(webMenuAuthorityFromDB);
            await _authorityUnitOfWork.CommitAsync();
            _webMenuAuthorityRepository.ClearCache();
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
        /// 根据ID组获取信息
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        private async Task<List<WebMenuAuthority>> GetWebMenuAuthoritiesByIDs(Guid id1, Guid id2, params Guid[] ids)
        {
            Expression<Func<WebMenuAuthority, bool>> expression = m => m.ID == id1 || m.ID == id2;
            expression = ids.Aggregate(expression, (current, id) => current.Or(m => m.ID == id));
            List<WebMenuAuthority> webMenuAuthorities = await _webMenuAuthorityRepository.WhereAsync(expression).ToList();
            if (webMenuAuthorities.Count != ids.Length + 2) throw new InvalidOperationException("该网页菜单权限不存在");
            return webMenuAuthorities;
        }
        /// <summary>
        /// 根据位序获取同级的位序内信息
        /// </summary>
        /// <param name="webMenuAuthority1"></param>
        /// <param name="webMenuAuthority2"></param>
        /// <returns></returns>
        private async Task<List<WebMenuAuthority>> GetWebMenuAuthoritiesByIndex(WebMenuAuthority webMenuAuthority1, WebMenuAuthority webMenuAuthority2)
        {
            if (webMenuAuthority1.ParentID != webMenuAuthority2.ParentID) throw new InvalidOperationException("两个网页菜单权限不属于同级");
            var webMenuAuthorities = new List<WebMenuAuthority>
            {
                webMenuAuthority1,
                webMenuAuthority2
            };
            webMenuAuthorities = webMenuAuthorities.OrderBy(m => m.Index).ToList();
            WebMenuAuthority firstWebMenuAuthority = webMenuAuthorities[0];
            WebMenuAuthority lastWebMenuAuthority = webMenuAuthorities[1];
            webMenuAuthorities.AddRange(await _webMenuAuthorityRepository.WhereAsync(m => m.ParentID == firstWebMenuAuthority.ParentID && m.Index > firstWebMenuAuthority.Index && m.Index < lastWebMenuAuthority.Index).ToList());
            webMenuAuthorities = webMenuAuthorities.OrderBy(m => m.Index).ToList();
            return webMenuAuthorities;
        }
        /// <summary>
        /// 调换位序
        /// </summary>
        /// <param name="exchangeID"></param>
        /// <param name="forUnder"></param>
        /// <param name="webMenuAuthorities"></param>
        private void ExchangeIndex(Guid exchangeID, bool forUnder, IReadOnlyList<WebMenuAuthority> webMenuAuthorities)
        {
            var count = 0;
            int startIndex;
            int indexTemp;
            if (exchangeID == webMenuAuthorities[0].ID)
            {
                startIndex = forUnder ? webMenuAuthorities.Count - 1 : webMenuAuthorities.Count - 2;
                indexTemp = webMenuAuthorities[startIndex].Index;
                for (int i = startIndex; i > count; i--)
                {
                    webMenuAuthorities[i].Index = webMenuAuthorities[i - 1].Index;
                    webMenuAuthorities[i].UpdateTime = DateTime.Now;
                    _authorityUnitOfWork.RegisterEdit(webMenuAuthorities[i]);
                }
            }
            else
            {
                count = webMenuAuthorities.Count - 1;
                startIndex = forUnder ? 1 : 0;
                indexTemp = webMenuAuthorities[startIndex].Index;
                for (int i = startIndex; i < count; i++)
                {
                    webMenuAuthorities[i].Index = webMenuAuthorities[i + 1].Index;
                    webMenuAuthorities[i].UpdateTime = DateTime.Now;
                    _authorityUnitOfWork.RegisterEdit(webMenuAuthorities[i]);
                }
            }
            webMenuAuthorities[count].Index = indexTemp;
            webMenuAuthorities[count].UpdateTime = DateTime.Now;
            _authorityUnitOfWork.RegisterEdit(webMenuAuthorities[count]);
        }
        #endregion
    }
}
