using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace MetinGo.Droid
{
    [Activity(Label = "Metin Go", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            global::Xamarin.Forms.Forms.Init(this, bundle);
			Xamarin.FormsGoogleMaps.Init(this, bundle);
			LoadApplication(new App());
        }

		private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			var exception =  e.ExceptionObject as Exception;
		}
	}
}

