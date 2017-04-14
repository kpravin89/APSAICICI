using APSA.Portable.Views.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace APSA.Portable.AppStart
{
    public partial class App : Application
    {

        public static string AccessToken { get; set; }

        public static string ParticipantID { get; set; }
        

        public App()
        {
            MainPage = new NavigationPage(new LoginPage());
        }

    }
}
