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
            throw new NotImplementedException();
        }
        public async Task EditWeChatDomainAsync(EditWeChatDomainModel model)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteWeChatDomainAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public async Task<WeChatDomainDTO> GetWeChatDomainInfoAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public async Task<(List<WeChatDomainListDTO> result, PageModel pageModel)> GetWeChatDomainListAsync(QueryWeChatDomainFilterModel filterModel)
        {
            throw new NotImplementedException();
        }
        public async Task ExchangeWeChatDomainIndexAsync(Guid id1, Guid id2)
        {
            List<WeChatDomain> weChatDomains = await _weChatDomainRepository.WhereAsync(m => m.ID == id1 || m.ID == id2).ToList();
            if (weChatDomains.Count != 2) throw new InvalidOperationException("该网页菜单权限不存在");
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
