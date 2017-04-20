using APSA.Portable.Models.APIModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSA.Portable.Models.APIModels.CoreBanking
{
    public class BalanceEnquiryOutput
        : Nimbi.ModelBase.APIModelBase
    {

        public CodeObject oCodeObject { get; set; }
        
        public BalanceEnquiryAccount oBalanceEnquiryAccount { get; set; }

    }
}
