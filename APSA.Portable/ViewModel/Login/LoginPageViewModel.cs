using APSA.Portable.Nimbi.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSA.Portable.ViewModel.Login
{
    public class LoginPageViewModel : PageViewModel
    {
        public LoginPageViewModel()
        {
            Title = "Login Page";
        }

        public bool IsLoading { get; set; }

    }
}
