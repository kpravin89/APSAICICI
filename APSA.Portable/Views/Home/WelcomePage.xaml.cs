using APSA.Portable.Views.Insurance.Scan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace APSA.Portable.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();
            ParticipantID.Text = "Welcome :" + AppStart.App.ParticipantID;
            Token.Text = AppStart.App.AccessToken;

        }

        public async void ICICUser_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            await Navigation.PushAsync(new WelcomePage());
        }
        public async void CheckingInspector_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            await Navigation.PushAsync(new HomePage());
        }
        public async void HealthInspector_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            await Navigation.PushAsync(new WelcomePage());
        }
    }
}
