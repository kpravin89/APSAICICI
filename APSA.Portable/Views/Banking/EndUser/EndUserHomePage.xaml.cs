using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace APSA.Portable.Views.Banking.EndUser
{
    public partial class EndUserHomePage : ContentPage
    {
        public EndUserHomePage()
        {
            InitializeComponent();
        }


        public async void GetAccountDetails_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GetAccountDetailsPage());
        }

        public async void AddKYCForm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddKYCFormPage());
        }
        public async void LinkAadharCard_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LinkAadharCardPage());
        }
        public async void CallCustomerCare_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CallCustomerCarePage());
        }

    }
}
