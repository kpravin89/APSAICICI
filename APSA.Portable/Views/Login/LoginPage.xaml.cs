using APSA.Portable.Data.Services.Security;
using APSA.Portable.Models.APIModels.Security;
using APSA.Portable.Nimbi.Navigation;
using APSA.Portable.ViewModel.Login;
using APSA.Portable.Views.Home;
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
    //[RegisterViewModel(typeof(LoginPageViewModel))]
    public partial class LoginPage : ContentPage
    {
        LoginServices oLoginServices;

        private bool _IsLoading;
        public bool IsLoading
        {
            get
            {
                return _IsLoading;
            }
            set
            {
                _IsLoading = value;
                Loader.IsRunning = value;
                Loader.IsVisible = value;
                MainLayout.IsVisible = !value;
            }
        }

        public LoginPage()
        {
            InitializeComponent();
            oLoginServices = new LoginServices();
            ToolbarItems.Clear();
        }

        async void OnLoginClicked(object sender, EventArgs e)
        {
            try
            {
                IsLoading = true;
                var tokens = await oLoginServices.GetTokenAsync(new LoginModel() { participant_access_code = passwordEntry.Text, participant_id = usernameEntry.Text });
                if(tokens.Count() != 0 && !string.IsNullOrWhiteSpace(tokens[0].token) )
                {
                    AppStart.App.AccessToken = tokens[0].token;
                    IsLoading = false;
                    await Navigation.PopAsync();
                    await Navigation.PushAsync(new RootPage());
                }
                IsLoading = false;
                messageLabel.Text = "Login Failed";
            }
            catch (Exception ex)
            {
                IsLoading = false;
                messageLabel.Text = "Login Failed" + ex.Message;
            }


        }

        async void OnSignUpClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            await Navigation.PushAsync(new CalculatorPage());
        }
    }
}
