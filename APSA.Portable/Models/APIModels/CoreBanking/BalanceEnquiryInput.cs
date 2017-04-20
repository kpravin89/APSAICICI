using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSA.Portable.Models.APIModels.CoreBanking
{
    public class BalanceEnquiryInput
        : Nimbi.ModelBase.APIModelBase
    {

        public BalanceEnquiryInput()
        {
            base.api_url = @"https://retailbanking.mybluemix.net/banking/icicibank/balanceenquiry?client_id=[Client_id]&token=[Token]>&accountno=[AccountNo]";
            Client_id = @"k.pravin89@gmail.com";
        }

        [Nimbi.Core.Attributes.UrlParameter]
        public string Client_id { get; set; }

        [Nimbi.Core.Attributes.UrlParameter]
        public string Token { get; set; }

        [Nimbi.Core.Attributes.UrlParameter]
        public string AccountNo { get; set; }

    }
}
