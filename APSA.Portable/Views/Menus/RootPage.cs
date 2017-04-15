using APSA.Portable.Views.Home;
using APSA.Portable.Views.Insurance.Scan;
using APSA.Portable.Views.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace APSA.Portable.Views.Menus
{
    public class RootPage : Xamarin.Forms.MasterDetailPage
    {
        public RootPage()
        {
            var menuPage = new APSA.Portable.Views.Menus.MenuPage();

            menuPage.Menu.ItemSelected += (sender, e) => NavigateTo(e.SelectedItem as APSA.Portable.Models.APIModels.Menus.MenuItem);

            Master = menuPage;
            Detail = new NavigationPage(new HomePage());
        }
        void NavigateTo(APSA.Portable.Models.APIModels.Menus.MenuItem menu)
        {
            Page displayPage = (Page)Activator.CreateInstance(menu.TargetType);

            Detail = new NavigationPage(displayPage);

            IsPresented = false;
        }
    }
}
