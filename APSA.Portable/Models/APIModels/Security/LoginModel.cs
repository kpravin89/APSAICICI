using APSA.Portable.Nimbi.ModelBase;
using APSA.Portable.Nimbi.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSA.Portable.Models.APIModels.Security
{
    public class LoginModel
        : APIModelBase
    {
        public LoginModel()
        {
            base.api_url = @"https://corporateapiprojectwar.mybluemix.net/corporate_banking/mybank/authenticate_client?client_id=participant_id&password=participant_access_code";
            participant_id = @"k.pravin89@gmail.com";
            participant_access_code = "TFGQVM9M";            
        }

        [UrlParameter]
        public string participant_id { get; set; }

        [UrlParameter]
        public string participant_access_code { get; set; }
        
    }
}
