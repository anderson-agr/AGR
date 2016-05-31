using System;
using System.Globalization;
using System.IO;
using AGR.Data;
using SQLite.Net;
using SQLite.Net.Interop;
using SQLite.Net.Platform.XamarinAndroid;
using Xamarin.Forms;

[assembly: Dependency(typeof(AGR.Droid.Config))]
namespace AGR.Droid
{
    public class Config :IConfig
    {
       public SQLiteConnection GetConnection()
        {
            const string vaNomeBanco = "MeuBanco";
            var vaCaminhoBd = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal),vaNomeBanco);
            return  new SQLiteConnection(new SQLitePlatformAndroid(),vaCaminhoBd);
        }
    }
}