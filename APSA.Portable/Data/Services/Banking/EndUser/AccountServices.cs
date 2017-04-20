using APSA.Portable.Models.APIModels.CoreBanking;
using APSA.Portable.Nimbi.ModelBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSA.Portable.Data.Services.Banking.EndUser
{
    public class AccountServices
    {

        public async Task<BalanceEnquiryOutput[]> GetAccountBalance(BalanceEnquiryInput oBalanceEnquiryInput)
        {
            return await oBalanceEnquiryInput.GetCustomTypeFromApiAsync<BalanceEnquiryOutput[]>(HttpVerb_Enum.get, null);
        }


    }
}
