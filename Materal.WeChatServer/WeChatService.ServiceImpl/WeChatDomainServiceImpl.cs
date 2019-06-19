using AutoMapper;
using Materal.ConvertHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Materal.LinqHelper;
using WeChatService.DataTransmitModel.WeChatDomain;
using WeChatService.Domain;
using WeChatService.Domain.Repositories;
using WeChatService.EFRepository;
using WeChatService.Service;
using WeChatService.Service.Model.WeChatDomain;

namespace WeChatService.ServiceImpl
{
    /// <summary>
    /// 微信域名服务
    /// </summary>
    public sealed class WeChatDomainServiceImpl : IWeChatDomainService
    {
        private readonly IWeChatDomainRepository _weChatDomainRepository;
        private readonly IMapper _mapper;
        private readonly IWeChatServiceUnitOfWork _weChatServiceUnitOfWork;
        public WeChatDomainServiceImpl(IWeChatDomainRepository weChatDomainRepository, IMapper mapper, IWeChatServiceUnitOfWork weChatServiceUnitOfWork)
        {
            _weChatDomainRepository = weChatDomainRepository;
            _mapper = mapper;
            _weChatServiceUnitOfWork = weChatServiceUnitOfWork;
        }
        public async Task AddWeChatDomainAsync(AddWeChatDomainModel model)
        {
            if (string.IsNullOrEmpty(model.Url)) throw new InvalidOperationException("Url为空");
            if (string.IsNullOrEmpty(model.Name)) throw new InvalidOperationException("名称为空");
            if (await _weChatDomainRepository.CountAsync(m => m.Url == model.Url) > 0) throw new InvalidOperationException("Url已存在");
            var weChatDomain = model.CopyProperties<WeChatDomain>();
            weChatDomain.Index = _weChatDomainRepository.GetMaxIndex() + 1;
            _weChatServiceUnitOfWork.RegisterAdd(weChatDomain);
            await _weChatServiceUnitOfWork.CommitAsync();
            _weChatDomainRepository.ClearCache();
        }
        public async Task EditWeChatDomainAsync(EditWeChatDomainModel model)
        {
            if (string.IsNullOrEmpty(model.Url)) throw new InvalidOperationException("Url为空");
            if (string.IsNullOrEmpty(model.Name)) throw new InvalidOperationException("名称为空");
            if (await _weChatDomainRepository.CountAsync(m => m.ID != model.ID && m.Url == model.Url) > 0) throw new InvalidOperationException("Url已存在");
            WeChatDomain weChatDomainFromDB = await _weChatDomainRepository.FirstOrDefaultAsync(model.ID);
            if (weChatDomainFromDB == null) throw new InvalidOperationException("微信域名不存在");
            model.CopyProperties(weChatDomainFromDB);
            weChatDomainFromDB.UpdateTime = DateTime.Now;
            _weChatServiceUnitOfWork.RegisterEdit(weChatDomainFromDB);
            await _weChatServiceUnitOfWork.CommitAsync();
            _weChatDomainRepository.ClearCache();
        }
        public async Task DeleteWeChatDomainAsync(Guid id)
        {
            WeChatDomain weChatDomainFromDB = await _weChatDomainRepository.FirstOrDefaultAsync(id);
            if (weChatDomainFromDB == null) throw new InvalidOperationException("微信域名不存在");
            _weChatServiceUnitOfWork.RegisterDelete(weChatDomainFromDB);
            await _weChatServiceUnitOfWork.CommitAsync();
            _weChatDomainRepository.ClearCache();
        }
        public async Task<WeChatDomainDTO> GetWeChatDomainInfoAsync(Guid id)
        {
            WeChatDomain weChatDomainFromDB = await _weChatDomainRepository.FirstOrDefaultAsync(id);
            if (weChatDomainFromDB == null) throw new InvalidOperationException("微信域名不存在");
            return _mapper.Map<WeChatDomainDTO>(weChatDomainFromDB);
        }
        public async Task<List<WeChatDomainListDTO>> GetWeChatDomainListAsync()
        {
            List<WeChatDomain> weChatDomainsFromCache = await _weChatDomainRepository.GetAllInfoFromCacheAsync();
            List<WeChatDomain> weChatDomains = weChatDomainsFromCache.OrderBy(m => m.Index).ToList();
            return _mapper.Map<List<WeChatDomainListDTO>>(weChatDomains);
        }
        public async Task ExchangeWeChatDomainIndexAsync(Guid exchangeID, Guid targetID, bool forUnder = true)
        {
            List<WeChatDomain> weChatDomains = await GetWeChatDomainsByIDs(exchangeID, targetID);
            weChatDomains = await GetWeChatDomainsByIndex(weChatDomains[0], weChatDomains[1]);
            ExchangeIndex(exchangeID, forUnder, weChatDomains);
            await _weChatServiceUnitOfWork.CommitAsync();
            _weChatDomainRepository.ClearCache();
        }
        #region 私有方法
        /// <summary>
        /// 根据ID组获取信息
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        private async Task<List<WeChatDomain>> GetWeChatDomainsByIDs(Guid id1, Guid id2, params Guid[] ids)
        {
            Expression<Func<WeChatDomain, bool>> expression = m => m.ID == id1 || m.ID == id2;
            expression = ids.Aggregate(expression, (current, id) => current.Or(m => m.ID == id));
            List<WeChatDomain> weChatDomains = await _weChatDomainRepository.WhereAsync(expression).ToList();
            if (weChatDomains.Count != ids.Length + 2) throw new InvalidOperationException("该微信域名不存在");
            return weChatDomains;
        }
        /// <summary>
        /// 根据位序获取同级的位序内信息
        /// </summary>
        /// <param name="weChatDomain1"></param>
        /// <param name="weChatDomain2"></param>
        /// <returns></returns>
        private async Task<List<WeChatDomain>> GetWeChatDomainsByIndex(WeChatDomain weChatDomain1, WeChatDomain weChatDomain2)
        {
            var weChatDomains = new List<WeChatDomain>
            {
                weChatDomain1,
                weChatDomain2
            };
            weChatDomains = weChatDomains.OrderBy(m => m.Index).ToList();
            WeChatDomain firstWeChatDomain = weChatDomains[0];
            WeChatDomain lastWeChatDomain = weChatDomains[1];
            weChatDomains.AddRange(await _weChatDomainRepository.WhereAsync(m => m.Index > firstWeChatDomain.Index && m.Index < lastWeChatDomain.Index).ToList());
            weChatDomains = weChatDomains.OrderBy(m => m.Index).ToList();
            return weChatDomains;
        }
        /// <summary>
        /// 调换位序
        /// </summary>
        /// <param name="exchangeID"></param>
        /// <param name="forUnder"></param>
        /// <param name="weChatDomains"></param>
        private void ExchangeIndex(Guid exchangeID, bool forUnder, IReadOnlyList<WeChatDomain> weChatDomains)
        {
            var count = 0;
            int startIndex;
            int indexTemp;
            if (exchangeID == weChatDomains[0].ID)
            {
                startIndex = forUnder ? weChatDomains.Count - 1 : weChatDomains.Count - 2;
                indexTemp = weChatDomains[startIndex].Index;
                for (int i = startIndex; i > count; i--)
                {
                    weChatDomains[i].Index = weChatDomains[i - 1].Index;
                    weChatDomains[i].UpdateTime = DateTime.Now;
                    _weChatServiceUnitOfWork.RegisterEdit(weChatDomains[i]);
                }
            }
            else
            {
                count = weChatDomains.Count - 1;
                startIndex = forUnder ? 1 : 0;
                indexTemp = weChatDomains[startIndex].Index;
                for (int i = startIndex; i < count; i++)
                {
                    weChatDomains[i].Index = weChatDomains[i + 1].Index;
                    weChatDomains[i].UpdateTime = DateTime.Now;
                    _weChatServiceUnitOfWork.RegisterEdit(weChatDomains[i]);
                }
            }
            weChatDomains[count].Index = indexTemp;
            weChatDomains[count].UpdateTime = DateTime.Now;
            _weChatServiceUnitOfWork.RegisterEdit(weChatDomains[count]);
        }
        #endregion
    }
}
