using System.Diagnostics;
using System.Reflection;
using AGR.Interfaces;
using Xamarin.Forms;
using Xamarin.Media;


[assembly:Dependency(typeof(AGR.iOS.PictureTaker_IOS))]
   
namespace AGR.iOS
{
    public class PictureTaker_IOS : IPictureTaker
    {
        public async void SnapPic()
        {
            var piker = new MediaPicker();

            var mediafile = await piker.PickPhotoAsync();
            Debug.WriteLine(mediafile.Path);
            MessagingCenter.Send(this,"file",mediafile.Path);
        }
    }
}