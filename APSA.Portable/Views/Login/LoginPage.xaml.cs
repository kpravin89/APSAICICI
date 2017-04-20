using APSA.Portable.Data.Services.Security;
using APSA.Portable.Models.APIModels.Security;
using APSA.Portable.Nimbi.Navigation;
using APSA.Portable.ViewModel.Login;
using APSA.Portable.Views.Controls.Slideout;
using APSA.Portable.Views.Insurance.Scan;
using APSA.Portable.Views.Menus;
using APSA.Portable.Views.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace APSA.Portable.Views.Login
{
    public partial class LoginPage : ContentPage
    {

        #region Fields

        LoginServices oLoginServices;
        LoginPageViewModel oLoginPageViewModel { get; set; }

        #endregion

        #region Constructors

        public LoginPage(LoginMode_Enum LoginMode)
        {
            InitializeComponent();
            oLoginPageViewModel = new LoginPageViewModel();
            oLoginPageViewModel.IsLoading = false;
            oLoginPageViewModel.LoginMode = LoginMode;

            BindingContext = oLoginPageViewModel;
            oLoginServices = new LoginServices();
        }
        #endregion

        #region Events

        async void OnLoginClicked(object sender, EventArgs e)
        {
            try
            {
                oLoginPageViewModel.IsLoading = true;
                var tokens = await oLoginServices.GetTokenAsync(new LoginModel() { participant_access_code = passwordEntry.Text, participant_id = usernameEntry.Text });
                if (tokens.Count() != 0 && !string.IsNullOrWhiteSpace(tokens[0].token))
                {
                    AppStart.App.AccessToken = tokens[0].token;
                    oLoginPageViewModel.IsLoading = false;
                    oLoginPageViewModel.AccessToken = tokens[0].token;

                    //await Navigation.PopAsync();

                    if (oLoginPageViewModel.LoginMode == LoginMode_Enum.BankUser)
                        await Navigation.PushAsync(new Banking.EndUser.EndUserHomePage());
                    else if (oLoginPageViewModel.LoginMode == LoginMode_Enum.BankClerk)
                        await Navigation.PushAsync(new Banking.Clerk.ClerkHomePage());
                    else if (oLoginPageViewModel.LoginMode == LoginMode_Enum.InsuranceUser)
                        await Navigation.PushAsync(new Insurance.EndUser.EndUserHomePage());
                    else if (oLoginPageViewModel.LoginMode == LoginMode_Enum.TrafficPolice)
                        await Navigation.PushAsync(new Insurance.Police.PoliceHomePage());
                    else
                        await Navigation.PushAsync(new Insurance.MedicalClaim.MedicalClaimUserPage());

                }
                oLoginPageViewModel.IsLoading = false;
                messageLabel.Text = "Login Failed";
            }
            catch (Exception ex)
            {
                oLoginPageViewModel.IsLoading = false;
                messageLabel.Text = "Login Failed" + ex.Message;
            }
            
        }

        async void OnSignUpClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            await Navigation.PushAsync(new CalculatorPage());
        }

        #endregion

    }
}
