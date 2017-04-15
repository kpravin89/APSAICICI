using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace APSA.Portable.Views.Menus
{
    public class MenuListView : ListView
    {
        public MenuListView()
        {
             List<APSA.Portable.Models.APIModels.Menus.MenuItem> data = new MenuListData();
            
            ItemsSource = data;
            VerticalOptions = LayoutOptions.FillAndExpand;
            BackgroundColor = Color.Transparent;

            var cell = new DataTemplate(typeof(ImageCell));
            cell.SetBinding(TextCell.TextProperty, "Title");
            cell.SetBinding(ImageCell.ImageSourceProperty, "IconSource");

            ItemTemplate = cell;
            SelectedItem = data[0];
        }
    }
}
