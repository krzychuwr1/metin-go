using System;
using System.IO;
using MetinGo.Droid.Database;
using MetinGo.Infrastructure.Database;
using Xamarin.Forms;

[assembly: Dependency(typeof(DatabasePathProvider))]
namespace MetinGo.Droid.Database
{
    public class DatabasePathProvider : IDatabasePathProvider
    {
        public string GetPath() => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "MetinGo.db");
    }
}
