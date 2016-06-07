using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using AGR.Droid.Audio;
using System.Threading.Tasks;
using AGR.Interfaces;
using Android.Content;
using Xamarin.Forms;
using Xamarin.Media;

[assembly: Dependency(typeof(AGR.Droid.MainActivity))]

namespace AGR.Droid
{
    [Activity(Label = "AGR", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity, IPictureTaker
    {
        //static Activity activity = null;


        //static public Activity Activity
        //{
        //    get { return (activity); }
        //}
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
        public void setStatus(String message)
        {

            //TextView
            //  status.Text = message;
        }

        public void SnapPic()
        {
            var activity = Forms.Context as Activity;

            var picker = new MediaPicker(activity);
            if (!picker.IsCameraAvailable)
                Console.WriteLine("No camera!");
            else
            {
                var intent = picker.GetTakePhotoUI(new StoreCameraMediaOptions
                {
                    Name = "test.jpg",
                    Directory = "MediaPickerSample"
                });
                activity.StartActivityForResult(intent, 1);
            }

            //
            //var piker = new MediaPicker(activity);
            //var intent = piker.GetTakePhotoUI(new StoreCameraMediaOptions()
            //{
            //    Name = "test.jpg",
            //    DefaultCamera = CameraDevice.Front,
            //    Directory = "fotos"
            //});
            //activity.StartActivityForResult(intent, 1);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            // User canceled
            if (resultCode == Result.Canceled)
                return;

            data.GetMediaFileExtraAsync(Forms.Context).ContinueWith(t =>
            {
                Console.WriteLine(t.Result.Path);
                MessagingCenter.Send(this, "file", t.Result.Path);
            }, TaskScheduler.FromCurrentSynchronizationContext());


            //base.OnActivityResult(requestCode, resultCode, data);
            //if (resultCode == Result.Canceled)
            //{
            //    return;
            //}

            //var mediaFile = await data.GetMediaFileExtraAsync(Forms.Context);
            //System.Diagnostics.Debug.WriteLine(mediaFile.Path);

        }
    }
}

