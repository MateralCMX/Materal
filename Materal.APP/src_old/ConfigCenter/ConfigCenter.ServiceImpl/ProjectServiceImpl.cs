using AutoMapper;
using ConfigCenter.Common;
using ConfigCenter.DataTransmitModel.Project;
using ConfigCenter.Domain;
using ConfigCenter.Domain.Repositories;
using ConfigCenter.Services;
using ConfigCenter.Services.Models.Project;
using ConfigCenter.SqliteEFRepository;
using Materal.ConvertHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ConfigCenter.ServiceImpl
{
    public class ProjectServiceImpl : IProjectService
    {
        private readonly INamespaceRepository _namespaceRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IConfigCenterSqliteEFUnitOfWork _configCenterSqliteEFUnitOfWork;
        private readonly IMapper _mapper;

        public ProjectServiceImpl(IProjectRepository projectRepository, IConfigCenterSqliteEFUnitOfWork configCenterSqliteEFUnitOfWork, IMapper mapper, INamespaceRepository namespaceRepository)
        {
            _projectRepository = projectRepository;
            _configCenterSqliteEFUnitOfWork = configCenterSqliteEFUnitOfWork;
            _mapper = mapper;
            _namespaceRepository = namespaceRepository;
        }

        public async Task AddProjectAsync(AddProjectModel model)
        {
            if (await _projectRepository.ExistedAsync(m => m.Name.Equals(model.Name))) throw new ConfigCenterException("名称已存在");
            var project = model.CopyProperties<Project>();
            _configCenterSqliteEFUnitOfWork.RegisterAdd(project);
            _configCenterSqliteEFUnitOfWork.RegisterAdd(new Namespace
            {
                Name = "Application",
                Description = "默认命名空间",
                ProjectID = project.ID
            });
            await _configCenterSqliteEFUnitOfWork.CommitAsync();
        }

        public async Task EditProjectAsync(EditProjectModel model)
        {
            if (await _projectRepository.ExistedAsync(m => m.Name.Equals(model.Name) && m.ID != model.ID)) throw new ConfigCenterException("名称已存在");
            Project projectFromDb = await _projectRepository.FirstOrDefaultAsync(model.ID);
            if (projectFromDb == null) throw new ConfigCenterException("项目不存在");
            model.CopyProperties(projectFromDb);
            projectFromDb.UpdateTime = DateTime.Now;
            _configCenterSqliteEFUnitOfWork.RegisterEdit(projectFromDb);
            await _configCenterSqliteEFUnitOfWork.CommitAsync();
        }

        public async Task DeleteProjectAsync([Required(ErrorMessage = "唯一标识不能为空")]Guid id)
        {
            Project projectFromDb = await _projectRepository.FirstOrDefaultAsync(id);
            if (projectFromDb == null) throw new ConfigCenterException("项目不存在");
            _configCenterSqliteEFUnitOfWork.RegisterDelete(projectFromDb);
            List<Namespace> namespaces = await _namespaceRepository.FindAsync(m => m.ProjectID == id);
            foreach (Namespace @namespace in namespaces)
            {
                _configCenterSqliteEFUnitOfWork.RegisterDelete(@namespace);
            }
            await _configCenterSqliteEFUnitOfWork.CommitAsync();
        }

        public async Task<ProjectDTO> GetProjectInfoAsync([Required(ErrorMessage = "唯一标识不能为空")]Guid id)
        {
            Project projectFromDb = await _projectRepository.FirstOrDefaultAsync(id);
            if (projectFromDb == null) throw new ConfigCenterException("项目不存在");
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
