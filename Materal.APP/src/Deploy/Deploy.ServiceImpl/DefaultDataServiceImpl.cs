using AutoMapper;
using Deploy.Common;
using Deploy.DataTransmitModel.DefaultData;
using Deploy.Domain;
using Deploy.Domain.Repositories;
using Deploy.Services;
using Deploy.Services.Models.DefaultData;
using Deploy.SqliteEFRepository;
using Materal.ConvertHelper;
using Materal.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deploy.ServiceImpl
{
    public class DefaultDataServiceImpl : IDefaultDataService
    {
        private readonly IMapper _mapper;
        private readonly IDeploySqliteEFUnitOfWork _deploySqliteEFUnitOfWork;
        private readonly IDefaultDataRepository _defaultDataRepository;

        public DefaultDataServiceImpl(IMapper mapper, IDeploySqliteEFUnitOfWork deploySqliteEFUnitOfWork, IDefaultDataRepository defaultDataRepository)
        {
            _mapper = mapper;
            _deploySqliteEFUnitOfWork = deploySqliteEFUnitOfWork;
            _defaultDataRepository = defaultDataRepository;
        }

        public async Task AddAsync(AddDefaultDataModel model)
        {
            if (await _defaultDataRepository.ExistedAsync(m => m.Key == model.Key && m.ApplicationType == model.ApplicationType))
            {
                throw new DeployException("键重复");
            }
            var defaultData = model.CopyProperties<DefaultData>();
            _deploySqliteEFUnitOfWork.RegisterAdd(defaultData);
            await _deploySqliteEFUnitOfWork.CommitAsync();
        }

        public async Task EditAsync(EditDefaultDataModel model)
        {
            if (await _defaultDataRepository.ExistedAsync(m => m.ID != model.ID && m.Key == model.Key && m.ApplicationType == model.ApplicationType))
            {
                throw new DeployException("键重复");
            }
            DefaultData defaultDataFromDB = await _defaultDataRepository.FirstOrDefaultAsync(m => m.ID == model.ID);
            if (defaultDataFromDB == null) throw new DeployException("默认数据不存在");
            model.CopyProperties(defaultDataFromDB);
            defaultDataFromDB.UpdateTime = DateTime.Now;
            _deploySqliteEFUnitOfWork.RegisterEdit(defaultDataFromDB);
            await _deploySqliteEFUnitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            DefaultData defaultDataFromDB = await _defaultDataRepository.FirstOrDefaultAsync(m => m.ID == id);
            if (defaultDataFromDB == null) throw new DeployException("默认数据不存在");
            _deploySqliteEFUnitOfWork.RegisterDelete(defaultDataFromDB);
            await _deploySqliteEFUnitOfWork.CommitAsync();
        }

        public async Task<DefaultDataDTO> GetInfoAsync(Guid id)
        {
            DefaultData defaultDataFromDB = await _defaultDataRepository.FirstOrDefaultAsync(m => m.ID == id);
            if (defaultDataFromDB == null) throw new DeployException("默认数据不存在");
            var result = _mapper.Map<DefaultDataDTO>(defaultDataFromDB);
            return result;
        }

        public async Task<(List<DefaultDataListDTO> defaultDataList, PageModel pageModel)> GetListAsync(QueryDefaultDataFilterModel model)
        {
            (List<DefaultData> defaultDataList, PageModel pageModel) = await _defaultDataRepository.PagingAsync(model);
            var result = _mapper.Map<List<DefaultDataListDTO>>(defaultDataList);
            return (result, pageModel);
        }
    }
}
