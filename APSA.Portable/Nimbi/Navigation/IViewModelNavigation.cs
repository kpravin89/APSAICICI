using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSA.Portable.Nimbi.Navigation
{
    public interface IViewModelNavigation
    {
        Task<object> PopAsync();

        Task<object> PopModalAsync();

        Task PopToRootAsync();

        Task PushAsync(object viewModel);

        Task PushModalAsync(object viewModel);
    }
}
