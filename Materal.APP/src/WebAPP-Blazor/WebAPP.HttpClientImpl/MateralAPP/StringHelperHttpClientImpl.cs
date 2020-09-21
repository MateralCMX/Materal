using Materal.APP.HttpClient;
using Materal.APP.HttpManage;
using Materal.APP.PresentationModel.StringHelper;
using Materal.Model;
using System.Threading.Tasks;

namespace WebAPP.HttpClientImpl.MateralAPP
{
    public class StringHelperHttpClientImpl : MateralAPPHttpClient, IStringHelperManage
    {
        private const string _controllerUrl = "/api/StringHelper/";
        public StringHelperHttpClientImpl(IAuthorityManage authorityManage) : base(authorityManage)
        {
        }

        public async Task<ResultModel<string>> DesDecryptionAsync(StringRequestModel requestModel)
        {
            var result = await SendPostAsync<ResultModel<string>>($"{_controllerUrl}DesDecryption", requestModel);
            return result;
        }

        public async Task<ResultModel<string>> DesEncryptionAsync(StringRequestModel requestModel)
        {
            var result = await SendPostAsync<ResultModel<string>>($"{_controllerUrl}DesEncryption", requestModel);
            return result;
        }
    }
}
