using Android.App;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.V7.App;
using System;

namespace KargozariHamrah
{
    [Activity(Label = "AradActivation", Theme = "@style/MyTheme")]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        string id = string.Empty;
        public string Imei
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(id)) return id;

                id = Build.Serial;
                if (string.IsNullOrWhiteSpace(id) || id == Build.Unknown || id == "0")
                {
                    try
                    {
                        var context = Application.Context;
                        id = Settings.Secure.GetString(context.ContentResolver, Settings.Secure.AndroidId);
                    }
                    catch (Exception ex)
                    {
                        Android.Util.Log.Warn("DeviceInfo", "Unable to get id: " + ex);
                    }
                }

                return id;
            }
        }
    }
}