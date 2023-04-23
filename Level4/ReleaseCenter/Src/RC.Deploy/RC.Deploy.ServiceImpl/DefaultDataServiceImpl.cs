using Materal.Utils.Model;
using RC.Core.Common;
using RC.Deploy.DataTransmitModel.DefaultData;
using RC.Deploy.Domain;
using RC.Deploy.Services.Models.DefaultData;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace RC.Deploy.ServiceImpl
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
        protected override async Task<(List<DefaultDataListDTO> data, PageModel pageInfo)> GetListAsync(Expression<Func<DefaultData, bool>> expression, QueryDefaultDataModel model, Expression<Func<DefaultData, object>>? orderExpression = null, SortOrder sortOrder = SortOrder.Descending)
        {
            if (orderExpression == null)
            {
                sortOrder = SortOrder.Ascending;
                orderExpression = m => m.Key;
            }
            return await base.GetListAsync(expression, model, orderExpression, sortOrder);
        }
    }
}
