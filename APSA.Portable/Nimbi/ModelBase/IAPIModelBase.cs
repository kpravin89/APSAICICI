using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APSA.Portable.Nimbi.ModelBase
{
    public interface IAPIModelBase
    {

        string api_url { get; set; }

        Dictionary<string, string> api_static_parameters
        {
            get;
        }

        Dictionary<string, string> DefaultRequestHeaders
        {
            get;
        }

        string getFullUrl();

        Task<T> GetModelFromApiAsync<T>(HttpVerb_Enum oHttpVerb, HttpContent oHttpContent)
            where T : class, IAPIModelBase, new();

        Task<T> GetCustomTypeFromApiAsync<T>(HttpVerb_Enum oHttpVerb, HttpContent oHttpContent);  
    }
}
