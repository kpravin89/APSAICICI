using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using APSA.Portable.Nimbi.Core.Attributes;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace APSA.Portable.Nimbi.ModelBase
{
    public class APIModelBase
        : IAPIModelBase
    {
        public string api_url { get; set; }

        public Dictionary<string, string> api_static_parameters { get; private set; }

        public APIModelBase()
        {
            api_static_parameters = new Dictionary<string, string>();
        }

        public string getFullUrl()
        {
            string fullUrl = api_url;
            foreach (KeyValuePair<string, string> keyvalue in api_static_parameters)
            {
                fullUrl = fullUrl.Replace(keyvalue.Key, keyvalue.Value);
            }

            List<PropertyInfo> oProperties = this
                                                .GetType()
                                                .GetRuntimeProperties()
                                                .Where(a => a.IsDefined(typeof(UrlParameterAttribute)))
                                                .ToList();
            foreach (var item in oProperties)
            {
                fullUrl = fullUrl.Replace(item.Name, Convert.ToString(item.GetValue(this)));
            }

            return fullUrl;
        }

        public async Task<T> GetModelFromApiAsync<T>()
            where T : class, IAPIModelBase, new()
        {
            //T Result = null;
            //using (HttpClient client = new HttpClient())
            //{
            //    client.MaxResponseContentBufferSize = 256000;
            //    var response = await client.GetAsync(getFullUrl()).ConfigureAwait(false);
            //    if (response.IsSuccessStatusCode)
            //    {
            //        var content = await response.Content.ReadAsStringAsync();
            //        Result = JsonConvert.DeserializeObject<T>(content);
            //        return Result;
            //    }
            //    return Result;
            //}

            return await GetCustomTypeFromApiAsync<T>();

        }

        public async Task<T> GetCustomTypeFromApiAsync<T>()
        {
            T Result = default(T);
            using (HttpClient client = new HttpClient())
            {
                client.MaxResponseContentBufferSize = 256000;
                var response = await client.GetAsync(getFullUrl()).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Result = JsonConvert.DeserializeObject<T>(content);
                    return Result;
                }
                return Result;
            }

        }
    }
}
