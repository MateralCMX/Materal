using AutoMapper;
using Materal.ConfigCenter.ProtalServer.DataTransmitModel.Namespace;
using Materal.ConfigCenter.ProtalServer.Domain;
using Materal.ConfigCenter.ProtalServer.Domain.Repositories;
using Materal.ConfigCenter.ProtalServer.PresentationModel.Namespace;
using Materal.ConfigCenter.ProtalServer.Services;
using Materal.ConfigCenter.ProtalServer.SqliteEFRepository;
using Materal.ConvertHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Materal.ConfigCenter.ProtalServer.ServiceImpl
{
    public class NamespaceServiceImpl : INamespaceService
    {
        private readonly INamespaceRepository _namespaceRepository;
        private readonly IProtalServerUnitOfWork _protalServerUnitOfWork;
        private readonly IMapper _mapper;

        public NamespaceServiceImpl(INamespaceRepository namespaceRepository, IProtalServerUnitOfWork protalServerUnitOfWork, IMapper mapper)
        {
            _namespaceRepository = namespaceRepository;
            _protalServerUnitOfWork = protalServerUnitOfWork;
            _mapper = mapper;
        }

        public async Task AddNamespaceAsync(AddNamespaceModel model)
        {
            if (await _namespaceRepository.ExistedAsync(m => m.Name.Equals(model.Name) && m.ProjectID.Equals(model.ProjectID))) throw new MateralConfigCenterException("名称已存在");
            var @namespace = model.CopyProperties<Namespace>();
            _protalServerUnitOfWork.RegisterAdd(@namespace);
            await _protalServerUnitOfWork.CommitAsync();
        }

        public async Task EditNamespaceAsync(EditNamespaceModel model)
        {
            if (await _namespaceRepository.ExistedAsync(m => m.Name.Equals(model.Name) && m.ProjectID.Equals(model.ProjectID) && m.ID != model.ID)) throw new MateralConfigCenterException("名称已存在");
            Namespace namespaceFromDb = await _namespaceRepository.FirstOrDefaultAsync(model.ID);
            if (namespaceFromDb == null) throw new MateralConfigCenterException("命名空间不存在");
            model.CopyProperties(namespaceFromDb);
            namespaceFromDb.UpdateTime = DateTime.Now;
            _protalServerUnitOfWork.RegisterEdit(namespaceFromDb);
            await _protalServerUnitOfWork.CommitAsync();
        }

        public async Task DeleteNamespaceAsync([Required(ErrorMessage = "唯一标识不能为空")]Guid id)
        {
            Namespace namespaceFromDb = await _namespaceRepository.FirstOrDefaultAsync(id);
            if (namespaceFromDb == null) throw new MateralConfigCenterException("命名空间不存在");
            _protalServerUnitOfWork.RegisterDelete(namespaceFromDb);
            await _protalServerUnitOfWork.CommitAsync();
        }

        public async Task<NamespaceDTO> GetNamespaceInfoAsync([Required(ErrorMessage = "唯一标识不能为空")]Guid id)
        {
            Namespace namespaceFromDb = await _namespaceRepository.FirstOrDefaultAsync(id);
            if (namespaceFromDb == null) throw new MateralConfigCenterException("命名空间不存在");
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
