using Materal.NetworkHelper;
using Materal.V8ScriptEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Materal.ConvertHelper;
using Newtonsoft.Json;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        public static async Task Main()
        {
            //var url = "https://test.wsmsd.cn/sit/api/v2/mms/openApi/addMer";
            //var heads = "{\"Content-Type\":\"application/json\",\"Authorization\":\"LKLAPI-SHA256withRSA appid=\\\"OP00000003\\\",serial_no=\\\"00dfba8194c41b84cf\\\",timestamp=\\\"1650293288\\\",nonce_str=\\\"Hv[nDU`[vxIB\\\",signature=\\\"vyDi9IOi5oOG5L6iMQHdY9L33sNZN2yim8P8U4RsC9iabywm84jUHisnWbEhJj6/oiLKNQXXQ3wyuyzhRh/KQvuIaNvidk43eLjDfPDSHY1LNKevIpIoxD+kQym90Srm24mIQ4rhnKajpAynhIaStg13VdQs7Fu341+NOfsQQ2/FYbxI4MzJlgTO2p/Eb4bm32Qeudy6m2IJlMnWfuE5nMb/aHPokWjwV33voa2k1VtL10zBiToALFb2Gq5yX5HvnfiJIBrXexzx4iBro/NUYMRJc5Q8wEODwd2xmuvUn1BBGic09eMw3Gvau/c2GYkkRW9jKevObzpBypYi3tGNwA==\\\"\"}".JsonToObject<Dictionary<string, string>>();
            //var data = "{\"timestamp\":null,\"rnd\":null,\"ver\":null,\"reqId\":null,\"reqData\":{\"version\":\"1.0\",\"orderNo\":\"2021020112000012345678\",\"posType\":\"SUPER_POS\",\"orgCode\":\"1\",\"merRegName\":\"佳谦进件测试商户1\",\"merBizName\":\"佳谦进件测试商户1\",\"merRegDistCode\":\"1125\",\"merRegAddr\":\"西城时代商业楼\",\"mccCode\":\"5811\",\"merBlisName\":\"佳谦进件测试商户1\",\"merBlis\":null,\"merBlisStDt\":null,\"merBlisExpDt\":null,\"merBusiContent\":\"645\",\"larName\":\"佳谦进件测试商户\",\"larIdType\":\"01\",\"larIdcard\":\"532331199209030014\",\"larIdcardStDt\":\"2010-04-14\",\"larIdcardExpDt\":\"2010-04-14\",\"merContactMobile\":\"18214558240\",\"merContactName\":\"佳谦进件测试商户1\",\"shopName\":null,\"shopDistCode\":null,\"shopAddr\":null,\"shopContactName\":null,\"shopContactMobile\":null,\"openningBankCode\":\"5689742154689745\",\"openningBankName\":\"建设银行\",\"clearingBankCode\":\"5689742154689745\",\"acctNo\":\"5689742154689745\",\"acctName\":\"测试结算账号\",\"acctTypeCode\":\"57\",\"settlePeriod\":\"T+1\",\"clearDt\":\"ZERO\",\"acctIdType\":null,\"acctIdcard\":null,\"acctIdDt\":null,\"devSerialNo\":null,\"devTypeName\":null,\"termVer\":null,\"termNum\":null,\"retUrl\":\"http://116.55.251.31:8900/SettlementCenterAPI/MerchantEnter/AddMerchantCallBack\",\"feeData\":{\"feeRateTypeCode\":\"300\",\"feeRateTypeName\":\"银行卡借记卡\",\"feeRatePct\":\"0.6\",\"feeUpperAmtPcnt\":null,\"feeLowerAmtPcnt\":null,\"feeRateStDt\":null},\"fileData\":{\"attFileId\":\"3fa85f64-5717-4562-b3fc-2c963f66afa6\",\"attType\":\"01\"},\"contractNo\":null}}".JsonToObject();
            //var result = await HttpManager.SendPostAsync(url, data, null, heads, Encoding.UTF8, null);
            //Console.WriteLine(result);
            var user = new User
            {
                Name = "Materal",
                Text = "1234"
            };
            var userJson = user.ToJson();
            var user2 = new User
            {
                Name = "Materal",
                Text = null
            };
            var user2Json = user2.ToJson();
        }
        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class User
        {
            public string Name { get; set; }
            public string Text { get; set; }
        }
    }
}
