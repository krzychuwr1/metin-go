using System;
using System.IO;
using MetinGo.Infrastructure.Database;
using MetinGo.UWP.Database;
using Xamarin.Forms;

[assembly: Dependency(typeof(DatabasePathProvider))]
namespace MetinGo.UWP.Database
{
    public class DatabasePathProvider : IDatabasePathProvider
    {
        public string GetPath() => Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "MetinGo.db");
    }
}
