﻿using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Materal.ConvertHelper;
using Materal.Model;
using Materal.NetworkHelper;

namespace Materal.ConfigCenter
{
    public abstract class HttpRepositoryImpl
    {
        public async Task<T> SendGetAsync<T>(string url, Dictionary<string, string> data = null, Dictionary<string, string> heads = null)
        {
            if (heads == null)
            {
                heads = GetDefaultHeads();
            }
            ResultModel<T> result = await HttpManager.SendGetAsync<ResultModel<T>>(url, data, heads, Encoding.UTF8);
            if (result.ResultType == Common.ResultTypeEnum.Fail) throw new MateralConfigCenterException(result.Message);
            return result.Data;
        }
        public async Task SendGetAsync(string url, Dictionary<string, string> data = null, Dictionary<string, string> heads = null)
        {
            if (heads == null)
            {
                heads = GetDefaultHeads();
            }
            ResultModel result = await HttpManager.SendGetAsync<ResultModel>(url, data, heads, Encoding.UTF8);
            if (result.ResultType == Common.ResultTypeEnum.Fail) throw new MateralConfigCenterException(result.Message);
        }

        protected virtual Dictionary<string, string> GetDefaultHeads()
        {
            var heads = new Dictionary<string, string>
            {
                ["Content-Type"] = "application/json"
            };
            return heads;
        }
    }
}
