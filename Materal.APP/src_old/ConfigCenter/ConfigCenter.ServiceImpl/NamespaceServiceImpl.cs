using AutoMapper;
using ConfigCenter.Common;
using ConfigCenter.DataTransmitModel.Namespace;
using ConfigCenter.Domain;
using ConfigCenter.Domain.Repositories;
using ConfigCenter.Services;
using ConfigCenter.Services.Models.Namespace;
using ConfigCenter.SqliteEFRepository;
using Materal.ConvertHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ConfigCenter.ServiceImpl
{
    public class NamespaceServiceImpl : INamespaceService
    {
        private readonly INamespaceRepository _namespaceRepository;
        private readonly IConfigCenterSqliteEFUnitOfWork _configCenterSqliteEFUnitOfWork;
        private readonly IMapper _mapper;

        public NamespaceServiceImpl(INamespaceRepository namespaceRepository, IConfigCenterSqliteEFUnitOfWork configCenterSqliteEFUnitOfWork, IMapper mapper)
        {
            _namespaceRepository = namespaceRepository;
            _configCenterSqliteEFUnitOfWork = configCenterSqliteEFUnitOfWork;
            _mapper = mapper;
        }

        public async Task AddNamespaceAsync(AddNamespaceModel model)
        {
            if (await _namespaceRepository.ExistedAsync(m => m.Name.Equals(model.Name) && m.ProjectID.Equals(model.ProjectID))) throw new ConfigCenterException("名称已存在");
            var @namespace = model.CopyProperties<Namespace>();
            _configCenterSqliteEFUnitOfWork.RegisterAdd(@namespace);
            await _configCenterSqliteEFUnitOfWork.CommitAsync();
        }

        public async Task EditNamespaceAsync(EditNamespaceModel model)
        {
            if (await _namespaceRepository.ExistedAsync(m => m.Name.Equals(model.Name) && m.ProjectID.Equals(model.ProjectID) && m.ID != model.ID)) throw new ConfigCenterException("名称已存在");
            Namespace namespaceFromDb = await _namespaceRepository.FirstOrDefaultAsync(model.ID);
            if (namespaceFromDb == null) throw new ConfigCenterException("命名空间不存在");
            model.CopyProperties(namespaceFromDb);
            namespaceFromDb.UpdateTime = DateTime.Now;
            _configCenterSqliteEFUnitOfWork.RegisterEdit(namespaceFromDb);
            await _configCenterSqliteEFUnitOfWork.CommitAsync();
        }

        public async Task DeleteNamespaceAsync([Required(ErrorMessage = "唯一标识不能为空")]Guid id)
        {
            Namespace namespaceFromDb = await _namespaceRepository.FirstOrDefaultAsync(id);
            if (namespaceFromDb == null) throw new ConfigCenterException("命名空间不存在");
            _configCenterSqliteEFUnitOfWork.RegisterDelete(namespaceFromDb);
            await _configCenterSqliteEFUnitOfWork.CommitAsync();
        }

        public async Task<NamespaceDTO> GetNamespaceInfoAsync([Required(ErrorMessage = "唯一标识不能为空")]Guid id)
        {
            Namespace namespaceFromDb = await _namespaceRepository.FirstOrDefaultAsync(id);
            if (namespaceFromDb == null) throw new ConfigCenterException("命名空间不存在");
            var result = _mapper.Map<NamespaceDTO>(namespaceFromDb);
            return result;
        }

        public async Task<List<NamespaceListDTO>> GetNamespaceListAsync(QueryNamespaceFilterModel filterModel)
        {
            List<Namespace> namespacesFromDb = await _namespaceRepository.FindAsync(filterModel, m=>m.Name, SortOrder.Ascending);
            var result = _mapper.Map<List<NamespaceListDTO>>(namespacesFromDb);
            return result;
        }
    }
}
