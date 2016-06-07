using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using AGR.Interfaces;
using Xamarin.Forms;

namespace AGR.Views
{
    public class FotoPage : ContentPage
    {
        public FotoPage()
        {

            Padding = new Thickness(
                20,
                Device.OnPlatform(40, 20, 0),
                10,
                20);


            var btnFoto = new Button { Text = "Fotografar" };

            btnFoto.Clicked += (sender, e) =>
            {
                var picturetake = DependencyService.Get<IPictureTaker>();
                picturetake.SnapPic();
            };

            var imgFoto = new Image
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            MessagingCenter.Subscribe<IPictureTaker, string>(this, "file", (send, arg) =>
                       {
                           imgFoto.Source = ImageSource.FromFile(arg);
                       });
            Content = new StackLayout
            {
                Spacing = 10,
                Children = { btnFoto, imgFoto }
            };

        }
    }
}
