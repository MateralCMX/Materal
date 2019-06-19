using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeChatService.DataTransmitModel.WeChatDomain;
using WeChatService.Service.Model.WeChatDomain;
namespace WeChatService.Service
{
    /// <summary>
    /// 微信域名服务
    /// </summary>
    public interface IWeChatDomainService
    {
        /// <summary>
        /// 添加微信域名
        /// </summary>
        /// <param name="model">添加模型</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task AddWeChatDomainAsync(AddWeChatDomainModel model);
        /// <summary>
        /// 修改微信域名
        /// </summary>
        /// <param name="model">修改模型</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task EditWeChatDomainAsync(EditWeChatDomainModel model);
        /// <summary>
        /// 删除微信域名
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task DeleteWeChatDomainAsync(Guid id);
        /// <summary>
        /// 获得微信域名信息
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task<WeChatDomainDTO> GetWeChatDomainInfoAsync(Guid id);
        /// <summary>
        /// 获得微信域名列表
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task<List<WeChatDomainListDTO>> GetWeChatDomainListAsync();

        /// <summary>
        /// 调换位序
        /// </summary>
        /// <param name="exchangeID"></param>
        /// <param name="targetID"></param>
        /// <param name="forUnder"></param>
        /// <returns></returns>
        Task ExchangeWeChatDomainIndexAsync(Guid exchangeID, Guid targetID, bool forUnder = true);
    }
}
