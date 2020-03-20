using AutoMapper;
using Materal.ConfigCenter.ProtalServer.DataTransmitModel.Project;
using Materal.ConfigCenter.ProtalServer.Domain;
using Materal.ConfigCenter.ProtalServer.Domain.Repositories;
using Materal.ConfigCenter.ProtalServer.PresentationModel.Project;
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
    public class ProjectServiceImpl : IProjectService
    {
        private readonly INamespaceRepository _namespaceRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IProtalServerUnitOfWork _protalServerUnitOfWork;
        private readonly IMapper _mapper;

        public ProjectServiceImpl(IProjectRepository projectRepository, IProtalServerUnitOfWork protalServerUnitOfWork, IMapper mapper, INamespaceRepository namespaceRepository)
        {
            _projectRepository = projectRepository;
            _protalServerUnitOfWork = protalServerUnitOfWork;
            _mapper = mapper;
            _namespaceRepository = namespaceRepository;
        }

        public async Task AddProjectAsync(AddProjectModel model)
        {
            if (await _projectRepository.ExistedAsync(m => m.Name.Equals(model.Name))) throw new MateralConfigCenterException("名称已存在");
            var project = model.CopyProperties<Project>();
            _protalServerUnitOfWork.RegisterAdd(project);
            _protalServerUnitOfWork.RegisterAdd(new Namespace
            {
                Name = "Application",
                Description = "默认命名空间",
                ProjectID = project.ID
            });
            await _protalServerUnitOfWork.CommitAsync();
        }

        public async Task EditProjectAsync(EditProjectModel model)
        {
            if (await _projectRepository.ExistedAsync(m => m.Name.Equals(model.Name) && m.ID != model.ID)) throw new MateralConfigCenterException("名称已存在");
            Project projectFromDb = await _projectRepository.FirstOrDefaultAsync(model.ID);
            if (projectFromDb == null) throw new MateralConfigCenterException("项目不存在");
            model.CopyProperties(projectFromDb);
            projectFromDb.UpdateTime = DateTime.Now;
            _protalServerUnitOfWork.RegisterEdit(projectFromDb);
            await _protalServerUnitOfWork.CommitAsync();
        }

        public async Task DeleteProjectAsync([Required(ErrorMessage = "唯一标识不能为空")]Guid id)
        {
            Project projectFromDb = await _projectRepository.FirstOrDefaultAsync(id);
            if (projectFromDb == null) throw new MateralConfigCenterException("项目不存在");
            _protalServerUnitOfWork.RegisterDelete(projectFromDb);
            List<Namespace> namespaces = await _namespaceRepository.FindAsync(m => m.ProjectID == id);
            foreach (Namespace @namespace in namespaces)
            {
                _protalServerUnitOfWork.RegisterDelete(@namespace);
            }
            await _protalServerUnitOfWork.CommitAsync();
        }

        public async Task<ProjectDTO> GetProjectInfoAsync([Required(ErrorMessage = "唯一标识不能为空")]Guid id)
        {
            Project projectFromDb = await _projectRepository.FirstOrDefaultAsync(id);
            if (projectFromDb == null) throw new MateralConfigCenterException("项目不存在");
            var result = _mapper.Map<ProjectDTO>(projectFromDb);
            return result;
        }

        public async Task<List<ProjectListDTO>> GetProjectListAsync(QueryProjectFilterModel filterModel)
        {
            List<Project> projectsFromDb = await _projectRepository.FindAsync(filterModel, m => m.Name, SortOrder.Ascending);
            var result = _mapper.Map<List<ProjectListDTO>>(projectsFromDb);
            return result;
        }
    }
}
