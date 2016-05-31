using System;
using System.IO;
using AGR.Data;
using ObjCRuntime;
using SQLite.Net;
using SQLite.Net.Interop;
using SQLite.Net.Platform.XamarinIOS;
using Xamarin.Forms;

[assembly:Dependency(typeof(AGR.iOS.Config))]

namespace AGR.iOS
{
    public class Config : IConfig
    {
       public SQLiteConnection GetConnection()
        {
            const string vaNomeBanco = "MeuBanco";
            var vaCaminhoBd = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal), "..", "Library", vaNomeBanco);
            return new SQLiteConnection(new SQLitePlatformIOS(), vaCaminhoBd);

        }
    }
}

