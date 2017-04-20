using APSA.Portable.Nimbi.Navigation;
using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace APSA.Portable.ViewModel.Login
{
    [ImplementPropertyChanged]
    public class LoginPageViewModel : PageViewModel
    {
        public LoginPageViewModel()
        {
            Title = "Login Page";
        }
        
        public bool IsLoading { get; set; }

        public bool IsSignUpRequired
        {
            get
            {
                if (LoginMode == LoginMode_Enum.BankUser || LoginMode == LoginMode_Enum.InsuranceUser)
                    return true;
                else
                    return false;
            }
        }
        
        public LoginMode_Enum LoginMode { get; set; }

    }
}
