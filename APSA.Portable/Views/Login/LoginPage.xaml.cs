using APSA.Portable.Data.Services.Security;
using APSA.Portable.Models.APIModels.Security;
using APSA.Portable.Views.Insurance.Scan;
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
        LoginServices oLoginServices;

        public LoginPage()
        {
            InitializeComponent();
            oLoginServices = new LoginServices();
        }

        async void OnLoginClicked(object sender, EventArgs e)
        {
            //Loader.IsRunning = true;
            try
            {                
                var tokens = await oLoginServices.GetTokenAsync(new LoginModel() { participant_access_code = passwordEntry.Text, participant_id = usernameEntry.Text });
                if(tokens.Count() != 0 && !string.IsNullOrWhiteSpace(tokens[0].token) )
                {
                    AppStart.App.AccessToken = tokens[0].token;
                    //Loader.IsRunning = false;
                    await Navigation.PopAsync();
                    await Navigation.PushAsync(new HomePage());
                }
                //Loader.IsRunning = false;
                messageLabel.Text = "Login Failed";
            }
            catch (Exception ex)
            {
                //Loader.IsRunning = false;
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
