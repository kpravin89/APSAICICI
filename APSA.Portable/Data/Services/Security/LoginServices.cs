using APSA.Portable.Models.APIModels.Security;
using APSA.Portable.Nimbi.ModelBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSA.Portable.Data.Services.Security
{
    public class LoginServices
    {
        public async Task<TokenModel[]> GetTokenAsync(LoginModel oLoginModel)
        {
            return await oLoginModel.GetCustomTypeFromApiAsync<TokenModel[]>(HttpVerb_Enum.get, null);
        }


    }
}
