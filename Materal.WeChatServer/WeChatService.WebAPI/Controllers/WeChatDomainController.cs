using Authority.PresentationModel;
using AutoMapper;
using Common.Model.APIAuthorityConfig;
using Materal.Common;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeChatService.DataTransmitModel.WeChatDomain;
using WeChatService.PresentationModel.WeChatDomain.Request;
using WeChatService.Service;
using WeChatService.Service.Model.WeChatDomain;

namespace WeChatService.WebAPI.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// 微信域名控制器
    /// </summary>
    [Route("api/[controller]/[action]"), ApiController]
    public class WeChatDomainController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWeChatDomainService _weChatDomainService;

        /// <summary>
        /// 构造方法
        /// </summary>
        public WeChatDomainController(IWeChatDomainService weChatDomainService, IMapper mapper)
        {
            _weChatDomainService = weChatDomainService;
            _mapper = mapper;
        }
        /// <summary>
        /// 添加微信域名
        /// </summary>
        /// <returns></returns>
        [HttpPost, AuthorityFilter(WeChatServiceAPIAuthorityConfig.AddWeChatDomainCode)]
        public async Task<ResultModel> AddWeChatDomain(AddWeChatDomainRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<AddWeChatDomainModel>(requestModel);
                await _weChatDomainService.AddWeChatDomainAsync(model);
                return ResultModel.Success("添加成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 修改微信域名
        /// </summary>
        /// <returns></returns>
        [HttpPost, AuthorityFilter(WeChatServiceAPIAuthorityConfig.EditWeChatDomainCode)]
        public async Task<ResultModel> EditWeChatDomain(EditWeChatDomainRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<EditWeChatDomainModel>(requestModel);
                await _weChatDomainService.EditWeChatDomainAsync(model);
                return ResultModel.Success("修改成功");

            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 删除微信域名
        /// </summary>
        /// <returns></returns>
        [HttpGet, AuthorityFilter(WeChatServiceAPIAuthorityConfig.DeleteWeChatDomainCode)]
        public async Task<ResultModel> DeleteWeChatDomain(Guid id)
        {
            try
            {
                await _weChatDomainService.DeleteWeChatDomainAsync(id);
                return ResultModel.Success("删除成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 获得微信域名信息
        /// </summary>
        /// <returns></returns>
        [HttpGet, AuthorityFilter(WeChatServiceAPIAuthorityConfig.QueryWeChatDomainCode)]
        public async Task<ResultModel<WeChatDomainDTO>> GetWeChatDomainInfo(Guid id)
        {
            try
            {
                WeChatDomainDTO result = await _weChatDomainService.GetWeChatDomainInfoAsync(id);
                return ResultModel<WeChatDomainDTO>.Success(result, "查询成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel<WeChatDomainDTO>.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 获得微信域名列表
        /// </summary>
        /// <returns></returns>
        [HttpGet, AuthorityFilter(WeChatServiceAPIAuthorityConfig.QueryWeChatDomainCode)]
        public async Task<ResultModel<List<WeChatDomainListDTO>>> GetWeChatDomainList()
        {
            try
            {
                List<WeChatDomainListDTO> result = await _weChatDomainService.GetWeChatDomainListAsync();
                return ResultModel<List<WeChatDomainListDTO>>.Success(result, "查询成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel<List<WeChatDomainListDTO>>.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 调换微信域名位序
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost, AuthorityFilter(AuthorityAPIAuthorityConfig.EditWebMenuAuthorityCode)]
        public async Task<ResultModel> ExchangeWeChatDomainIndex(ExchangeIndexRequestModel<Guid> requestModel)
        {
            try
            {
                await _weChatDomainService.ExchangeWeChatDomainIndexAsync(requestModel.ExchangeID, requestModel.TargetID, requestModel.ForUnder);
                return ResultModel.Success("调换成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
    }
}
