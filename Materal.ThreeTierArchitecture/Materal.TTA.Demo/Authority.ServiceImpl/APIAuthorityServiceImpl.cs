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
        public APIAuthorityServiceImpl(IAPIAuthorityRepository apiAuthorityRepository, IMapper mapper, IAuthorityUnitOfWork authorityUnitOfWork)
        {
            _apiAuthorityRepository = apiAuthorityRepository;
            _mapper = mapper;
            _authorityUnitOfWork = authorityUnitOfWork;
        }
        public async Task AddAPIAuthorityAsync(AddAPIAuthorityModel model)
        {
            if (string.IsNullOrEmpty(model.Code)) throw new InvalidOperationException("代码不能为空");
            if (string.IsNullOrEmpty(model.Name)) throw new InvalidOperationException("名称不能为空");
            if (await _apiAuthorityRepository.CountAsync(m => m.Code == model.Code) > 0) throw new InvalidOperationException("同一个功能组下只允许存在一个唯一的代码");
            var apiAuthority = model.CopyProperties<APIAuthority>();
            _authorityUnitOfWork.RegisterAdd(apiAuthority);
            await _authorityUnitOfWork.CommitAsync();
        }
        public async Task EditAPIAuthorityAsync(EditAPIAuthorityModel model)
        {
            if (string.IsNullOrEmpty(model.Code)) throw new InvalidOperationException("代码不能为空");
            if (string.IsNullOrEmpty(model.Name)) throw new InvalidOperationException("名称不能为空");
            if (await _apiAuthorityRepository.CountAsync(m => m.ID != model.ID && m.Code == model.Code) > 0) throw new InvalidOperationException("同一个功能组下只允许存在一个唯一的代码");
            APIAuthority apiAuthorityFromDB = await _apiAuthorityRepository.FirstOrDefaultAsync(model.ID);
            if (apiAuthorityFromDB == null) throw new InvalidOperationException("该API权限不存在");
            model.CopyProperties(apiAuthorityFromDB);
            _authorityUnitOfWork.RegisterEdit(apiAuthorityFromDB);
            await _authorityUnitOfWork.CommitAsync();
        }
        public async Task DeleteAPIAuthorityAsync(Guid id)
        {
            APIAuthority apiAuthorityFromDB = await _apiAuthorityRepository.FirstOrDefaultAsync(id);
            if (apiAuthorityFromDB == null) throw new InvalidOperationException("该API权限不存在");
            _authorityUnitOfWork.RegisterDelete(apiAuthorityFromDB);
            await _authorityUnitOfWork.CommitAsync();
        }
        public async Task<APIAuthorityDTO> GetAPIAuthorityInfoAsync(Guid id)
        {
            APIAuthority apiAuthorityFromDB = await _apiAuthorityRepository.FirstOrDefaultAsync(id);
            if (apiAuthorityFromDB == null) throw new InvalidOperationException("该API权限不存在");
            return _mapper.Map<APIAuthorityDTO>(apiAuthorityFromDB);
        }

        public async Task<List<APIAuthorityTreeDTO>> GetAPIAuthorityTreeAsync()
        {
            throw new NotImplementedException();
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
    }
}
