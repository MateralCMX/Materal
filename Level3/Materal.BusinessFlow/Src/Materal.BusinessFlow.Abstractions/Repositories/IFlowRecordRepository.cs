using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.DTO;
using Materal.BusinessFlow.Abstractions.Services.Models;
using Materal.TTA.Common;
using System.Linq.Expressions;

namespace Materal.BusinessFlow.Abstractions.Repositories
{
    public interface IFlowRecordRepository : IRepository
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        Guid Add(Guid flowTemplateID, FlowRecord domain);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        void Edit(Guid flowTemplateID, FlowRecord domain);
        /// <summary>
        /// 初始化流程记录
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <returns></returns>
        void Init(Guid flowTemplateID);
        /// <summary>
        /// 第一个
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        FlowRecord First(Guid flowTemplateID, Guid id);
        /// <summary>
        /// 第一个
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        FlowRecord First(Guid flowTemplateID, Expression<Func<FlowRecord, bool>> expression);
        /// <summary>
        /// 第一个或默认
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        FlowRecord? FirstOrDefault(Guid flowTemplateID, Guid id);
        /// <summary>
        /// 第一个或默认
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        FlowRecord? FirstOrDefault(Guid flowTemplateID, Expression<Func<FlowRecord, bool>> expression);
        /// <summary>
        /// 获得最大位序
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowID"></param>
        /// <returns></returns>
        int GetMaxSortIndex(Guid flowTemplateID, Guid flowID);
        /// <summary>
        /// 获得流程记录列表
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        List<FlowRecord> GetList(QueryFlowRecordModel queryModel);
        /// <summary>
        /// 获得流程记录列表
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        List<FlowRecord> GetList(Guid flowTemplateID, Expression<Func<FlowRecord, bool>> filterExpression);
        /// <summary>
        /// 获得流程记录列表
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        List<FlowRecordDTO> GetDTOList(QueryFlowRecordDTOModel queryModel);
        /// <summary>
        /// 获得流程记录列表
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        List<FlowRecordDTO> GetDTOList(Guid flowTemplateID, Expression<Func<FlowRecordDTO, bool>> filterExpression);
        /// <summary>
        /// 初始化流程记录
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <returns></returns>
        Task InitAsync(Guid flowTemplateID);
        /// <summary>
        /// 第一个
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<FlowRecord> FirstAsync(Guid flowTemplateID, Guid id);
        /// <summary>
        /// 第一个
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<FlowRecord> FirstAsync(Guid flowTemplateID, Expression<Func<FlowRecord, bool>> expression);
        /// <summary>
        /// 第一个或默认
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<FlowRecord?> FirstOrDefaultAsync(Guid flowTemplateID, Guid id);
        /// <summary>
        /// 第一个或默认
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<FlowRecord?> FirstOrDefaultAsync(Guid flowTemplateID, Expression<Func<FlowRecord, bool>> expression);
        /// <summary>
        /// 获得最大位序
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowID"></param>
        /// <returns></returns>
        Task<int> GetMaxSortIndexAsync(Guid flowTemplateID, Guid flowID);
        /// <summary>
        /// 获得流程记录列表
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        Task<List<FlowRecord>> GetListAsync(QueryFlowRecordModel queryModel);
        /// <summary>
        /// 获得流程记录列表
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        Task<List<FlowRecord>> GetListAsync(Guid flowTemplateID, Expression<Func<FlowRecord, bool>> filterExpression);
        /// <summary>
        /// 获得流程记录列表
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        Task<List<FlowRecordDTO>> GetDTOListAsync(QueryFlowRecordDTOModel queryModel);
        /// <summary>
        /// 获得流程记录列表
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        Task<List<FlowRecordDTO>> GetDTOListAsync(Guid flowTemplateID, Expression<Func<FlowRecordDTO, bool>> filterExpression);
    }
}
