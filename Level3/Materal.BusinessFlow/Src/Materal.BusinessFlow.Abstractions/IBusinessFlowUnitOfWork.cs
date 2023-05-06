using Materal.TTA.ADONETRepository;

namespace Materal.BusinessFlow.Abstractions
{
    public interface IBusinessFlowUnitOfWork : IADONETUnitOfWork<Guid>
    {
        /// <summary>
        /// 获得参数
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns></returns>
        string GetParams(string paramName);
    }
}
