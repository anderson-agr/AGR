using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using AGR.Droid.Audio;
using System.Threading.Tasks;

namespace AGR.Droid
{
    [Activity(Label = "AGR", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
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
     
    }
}

