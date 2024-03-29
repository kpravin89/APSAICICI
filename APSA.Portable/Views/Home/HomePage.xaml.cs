﻿using APSA.Portable.Views.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace APSA.Portable.Views.Home
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }


        public async void BankUser_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PopAsync();
            await Navigation.PushAsync(new LoginPage(ViewModel.Login.LoginMode_Enum.BankUser));
        }

        public async void BankClerk_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PopAsync();
            await Navigation.PushAsync(new LoginPage(ViewModel.Login.LoginMode_Enum.BankClerk));
        }

        public async void InsurnaceUser_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PopAsync();
            await Navigation.PushAsync(new LoginPage(ViewModel.Login.LoginMode_Enum.InsuranceUser));
        }

        public async void TrafficPolice_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PopAsync();
            await Navigation.PushAsync(new LoginPage(ViewModel.Login.LoginMode_Enum.TrafficPolice));
        }

        public async void MedicalClaimUser_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PopAsync();
            await Navigation.PushAsync(new LoginPage(ViewModel.Login.LoginMode_Enum.MedicalClaim));
        }
    }
}
