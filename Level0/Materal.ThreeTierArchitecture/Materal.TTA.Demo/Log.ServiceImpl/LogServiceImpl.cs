using AutoMapper;
using Log.DataTransmitModel.Log;
using Log.Domain.Repositories;
using Log.Service;
using Log.Service.Model.Log;
using Materal.Common;
using Materal.LinqHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Log.ServiceImpl
{
    public class LogServiceImpl : ILogService
    {
        private readonly ILogRepository _logRepository;
        private readonly IMapper _mapper;

        public LogServiceImpl(ILogRepository logRepository, IMapper mapper)
        {
            _logRepository = logRepository;
            _mapper = mapper;
        }

        public async Task<LogDTO> GetLogInfoAsync(int id)
        {
            Domain.Log logFromDB = await _logRepository.FirstOrDefaultAsync(id);
            if (logFromDB == null) throw new InvalidOperationException("未找到该日志");
            return _mapper.Map<LogDTO>(logFromDB);
        }

        public async Task<(List<LogListDTO> result, PageModel pageModel)> GetLogListAsync(QueryLogFilterModel filterModel)
        {
            Expression<Func<Domain.Log, bool>> expression = m => true;
            if (!string.IsNullOrEmpty(filterModel.Application))
            {
                expression = expression.And(m => m.Application == filterModel.Application);
            }
            if (!string.IsNullOrEmpty(filterModel.Level))
            {
                expression = expression.And(m => m.Level == filterModel.Level);
            }
            if (filterModel.MinTime.HasValue)
            {
                expression = expression.And(m => EF.Functions.DateDiffMillisecond(m.CreateTime,filterModel.MinTime.Value) >= 0);
            }
            if (filterModel.MaxTime.HasValue)
            {
                expression = expression.And(m => EF.Functions.DateDiffMillisecond(m.CreateTime, filterModel.MaxTime.Value) <= 0);
            }
            (List<Domain.Log> logFromDB, PageModel pageModel) = await _logRepository.PagingAsync(expression, m => m.CreateTime, SortOrder.Descending, filterModel);
            return (_mapper.Map<List<LogListDTO>>(logFromDB), pageModel);
        }
    }
}
