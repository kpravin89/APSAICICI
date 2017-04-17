using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace APSA.Portable.Nimbi.Navigation
{
    [ImplementPropertyChanged]
    public abstract class PageViewModel: INavigatingViewModel
    {
        public string Title { get; set; }
        
        public string AccessToken { get; set; }

        #region INavigatedPage implementation

        public IViewModelNavigation ViewModelNavigation { get; set; }

        #endregion
    }
}
