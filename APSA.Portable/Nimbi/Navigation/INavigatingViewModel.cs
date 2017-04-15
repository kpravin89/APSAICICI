using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSA.Portable.Nimbi.Navigation
{
    public interface INavigatingViewModel
    {
        IViewModelNavigation ViewModelNavigation { get; set; }
    }
}
