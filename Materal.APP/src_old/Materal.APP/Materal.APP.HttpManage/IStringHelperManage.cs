using System.Threading.Tasks;
using Materal.APP.PresentationModel.StringHelper;
using Materal.Model;

namespace Materal.APP.HttpManage
{
    public interface IStringHelperManage
    {
        /// <summary>
        /// Des字符串解密
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<ResultModel<string>> DesDecryptionAsync(StringRequestModel requestModel);
        /// <summary>
        /// Des字符串加密
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<ResultModel<string>> DesEncryptionAsync(StringRequestModel requestModel);
    }
}
