using AutoMapper;
using Materal.ConvertHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            _weChatServiceUnitOfWork.RegisterAdd(weChatDomain);
            await _weChatServiceUnitOfWork.CommitAsync();
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
        }
        public async Task DeleteWeChatDomainAsync(Guid id)
        {
            WeChatDomain weChatDomainFromDB = await _weChatDomainRepository.FirstOrDefaultAsync(id);
            if (weChatDomainFromDB == null) throw new InvalidOperationException("微信域名不存在");
            _weChatServiceUnitOfWork.RegisterDelete(weChatDomainFromDB);
            await _weChatServiceUnitOfWork.CommitAsync();
        }
        public async Task<WeChatDomainDTO> GetWeChatDomainInfoAsync(Guid id)
        {
            WeChatDomain weChatDomainFromDB = await _weChatDomainRepository.FirstOrDefaultAsync(id);
            if (weChatDomainFromDB == null) throw new InvalidOperationException("微信域名不存在");
            return _mapper.Map<WeChatDomainDTO>(weChatDomainFromDB);
        }
        public async Task<List<WeChatDomainListDTO>> GetWeChatDomainListAsync()
        {
            List<WeChatDomain> actionAuthoritiesFromDB = await _weChatDomainRepository.WhereAsync(m => true).OrderBy(m => m.Index).ToList();
            return _mapper.Map<List<WeChatDomainListDTO>>(actionAuthoritiesFromDB);
        }
        public async Task ExchangeWeChatDomainIndexAsync(Guid id1, Guid id2)
        {
            List<WeChatDomain> weChatDomains = await _weChatDomainRepository.WhereAsync(m => m.ID == id1 || m.ID == id2).ToList();
            if (weChatDomains.Count != 2) throw new InvalidOperationException("该微信域名不存在");
            ExchangeIndex(weChatDomains[0], weChatDomains[1]);
            _weChatServiceUnitOfWork.RegisterEdit(weChatDomains[0]);
            _weChatServiceUnitOfWork.RegisterEdit(weChatDomains[1]);
            await _weChatServiceUnitOfWork.CommitAsync();
        }

        #region 私有方法
        /// <summary>
        /// 调换位序
        /// </summary>
        /// <param name="weChatDomain1"></param>
        /// <param name="weChatDomain2"></param>
        private void ExchangeIndex(WeChatDomain weChatDomain1, WeChatDomain weChatDomain2)
        {
            int temp = weChatDomain1.Index;
            weChatDomain1.Index = weChatDomain2.Index;
            weChatDomain2.Index = temp;
        }
        #endregion
    }
}
