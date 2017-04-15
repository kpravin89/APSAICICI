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
    public class MenuListData : List<APSA.Portable.Models.APIModels.Menus.MenuItem>
    {
        public MenuListData()
        {
            this.Add(new APSA.Portable.Models.APIModels.Menus.MenuItem()
            {
                Title = "Login",
                IconSource = "contacts.png",
                TargetType = typeof(BarcodePage)
            });
            this.Add(new APSA.Portable.Models.APIModels.Menus.MenuItem()
            {
                Title = "Welcome",
                IconSource = "accounts.png",
                TargetType = typeof(WelcomePage)
            });

            this.Add(new APSA.Portable.Models.APIModels.Menus.MenuItem()
            {
                Title = "ScanPage",
                IconSource = "leads.png",
                TargetType = typeof(ScanPage)
            });

           
        }
    }
}
