﻿using RC.Deploy.Abstractions.DTO.DefaultData;
using RC.Deploy.Abstractions.Services.Models.DefaultData;

namespace RC.Deploy.Application.Services
{
    public partial class DefaultDataServiceImpl
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="RCException"></exception>
        public override async Task<Guid> AddAsync(AddDefaultDataModel model)
        {
            if (await DefaultRepository.ExistedAsync(m => m.ApplicationType == model.ApplicationType && m.Key == model.Key)) throw new RCException("键重复");
            return await base.AddAsync(model);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="RCException"></exception>
        public override async Task EditAsync(EditDefaultDataModel model)
        {
            if (await DefaultRepository.ExistedAsync(m => m.ID != model.ID && m.ApplicationType == model.ApplicationType && m.Key == model.Key)) throw new RCException("键重复");
            await base.EditAsync(model);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="model"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        protected override async Task<(List<DefaultDataListDTO> data, RangeModel rangeInfo)> GetListAsync(Expression<Func<DefaultData, bool>> expression, QueryDefaultDataModel model, Expression<Func<DefaultData, object>>? orderExpression = null, SortOrderEnum sortOrder = SortOrderEnum.Descending)
        {
            if (orderExpression == null)
            {
                sortOrder = SortOrderEnum.Ascending;
                orderExpression = m => m.Key;
            }
            return await base.GetListAsync(expression, model, orderExpression, sortOrder);
        }
    }
}
