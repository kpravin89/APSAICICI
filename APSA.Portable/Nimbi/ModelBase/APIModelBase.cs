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

        public Dictionary<string, string> DefaultRequestHeaders { get; private set; }

        public APIModelBase()
        {
            api_static_parameters = new Dictionary<string, string>();
            DefaultRequestHeaders = new Dictionary<string, string>();
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
                fullUrl = fullUrl.Replace("[" + item.Name + "]", Convert.ToString(item.GetValue(this)));
            }

            return fullUrl;
        }

        public async Task<T> GetModelFromApiAsync<T>(HttpVerb_Enum oHttpVerb, HttpContent oHttpContent)
            where T : class, IAPIModelBase, new()
        {
            return await GetCustomTypeFromApiAsync<T>(oHttpVerb, oHttpContent);
        }

        public async Task<T> GetCustomTypeFromApiAsync<T>(HttpVerb_Enum oHttpVerb, HttpContent oHttpContent)
        {
            T Result = default(T);
            string strURI = getFullUrl();

            using (HttpClient client = new HttpClient())
            {
                client.MaxResponseContentBufferSize = 256000;

                foreach (var item in DefaultRequestHeaders)
                {
                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }

                HttpResponseMessage response;

                if (oHttpVerb == HttpVerb_Enum.post)
                {
                    response = await client.PostAsync(strURI, oHttpContent).ConfigureAwait(false);
                }
                else if(oHttpVerb == HttpVerb_Enum.put)
                {
                    response = await client.PutAsync(strURI, oHttpContent).ConfigureAwait(false);
                }
                else if (oHttpVerb == HttpVerb_Enum.delete)
                {
                    response = await client.DeleteAsync(strURI).ConfigureAwait(false);
                }
                else if (oHttpVerb == HttpVerb_Enum.head)
                {
                    response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, strURI)).ConfigureAwait(false);
                }
                else
                {
                    response = await client.GetAsync(strURI).ConfigureAwait(false);
                }
                                
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
