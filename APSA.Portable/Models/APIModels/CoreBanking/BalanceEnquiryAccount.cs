using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSA.Portable.Models.APIModels.CoreBanking
{
    public class BalanceEnquiryAccount
        : Nimbi.ModelBase.APIModelBase
    {
        public string accountno { get; set; }
        public DateTime balancetime { get; set; }
        public string accounttype { get; set; }
        public double balance { get; set; }

    }
}
