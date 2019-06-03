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
using Authority.DataTransmitModel.Incident;
using Authority.Domain;
using Authority.Domain.Repositories;
using Authority.EFRepository;
using Authority.Service;
using Authority.Service.Model.Incident;
namespace Authority.ServiceImpl
{
    /// <summary>
    /// 事件服务
    /// </summary>
    public sealed class IncidentServiceImpl : IIncidentService
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorityUnitOfWork _authorityUnitOfWork;
        public IncidentServiceImpl(IIncidentRepository incidentRepository, IMapper mapper, IAuthorityUnitOfWork authorityUnitOfWork)
        {
            _incidentRepository = incidentRepository;
            _mapper = mapper;
            _authorityUnitOfWork = authorityUnitOfWork;
        }
        public async Task AddIncidentAsync(AddIncidentModel model)
        {
            throw new NotImplementedException();
        }
        public async Task EditIncidentAsync(EditIncidentModel model)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteIncidentAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public async Task<IncidentDTO> GetIncidentInfoAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public async Task<(List<IncidentListDTO> result, PageModel pageModel)> GetIncidentListAsync(QueryIncidentFilterModel filterModel)
        {
            throw new NotImplementedException();
        }
    }
}
