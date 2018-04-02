using System;
using System.Linq;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MetinGo.Views;
using Xamarin.Forms;

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

        //public override void OnBackPressed()
        //{
        //    if (Xamarin.Forms.Application.Current.MainPage is MasterDetailPage masterDetail)
        //    {
        //        if (!(masterDetail.Detail is NavigationPage navigationPage && navigationPage.Pages.First() is MapPage))
        //        {
        //            masterDetail.Detail = new NavigationPage(new MapPage());
        //        }
        //        else
        //            base.OnBackPressed();
        //    }
        //    else
        //        base.OnBackPressed();
        //}
        
	}
}

